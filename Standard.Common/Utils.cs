using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Standard.Common
{
    public class Utils
    {
        /// <summary>
        ///     将post传值转换成字符串
        /// </summary>
        /// <param name="s">post流</param>
        /// <returns></returns>
        public static string GetPostData(Stream s)
        {
            try
            {
                var count = 0;
                var buffer = new byte[1024];
                var builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (s != null)
                {
                    s.Flush();
                    s.Close();
                    s.Dispose();
                }
            }
        }

        /// <summary>
        ///     系统定义的密码盐
        /// </summary>
        /// <returns></returns>
        //public static string GetSystemPassWordSalt()
        //{
        //    var systemSalt = ConfigurationManager.AppSettings["systemSalt"];
        //    return systemSalt;
        //}

        /// <summary>
        ///     管理员可自定义的密码盐
        /// </summary>
        /// <returns></returns>
        //public static string GetAdminPassWordSalt()
        //{
        //    var adminSalt = ConfigurationManager.AppSettings["adminSalt"];
        //    return adminSalt;
        //}

        /// <summary>
        ///     获取加密后的密码字符串
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //public static string GetHashPassWord(string pwd)
        //{
        //    var pwdstr = GetMd5Hash(GetSystemPassWordSalt() + pwd);
        //    pwdstr = GetMd5Hash(GetAdminPassWordSalt() + pwdstr);
        //    return pwdstr;
        //}

        /// <summary>
        ///     生成手机随机验证码
        /// </summary>
        /// <returns></returns>
        public static string GetTelNoValidateCode(int length)
        {
            var telStr = "";
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                var code = rd.Next(0, 9);
                telStr += code;
            }
            return telStr;
        }
        #region 解决乱码
        public static string ToUtf8String(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c >= 0 && c <= 255)
                {
                    sb.Append(c);
                }
                else
                {
                    byte[] b;
                    try
                    {
                        b = Encoding.UTF8.GetBytes(c.ToString());
                    }
                    catch (Exception ex)
                    {
                        b = new byte[0];
                    }
                    for (int j = 0; j < b.Length; j++)
                    {
                        int k = b[j];
                        if (k < 0) k += 256;

                        sb.Append("%" + Convert.ToString(k, 16).ToUpper());
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region Guid

        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        #endregion

        #region Reflection

        /// <summary>
        ///     根据类型获取BindingFlags.Public 和 BindingFlags.Instance 的属性
        ///     增加了缓存机制,提高反射性能
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertyInfos(Type type)
        {
            var proInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return proInfo;
        }

        /// <summary>
        ///     转换类型
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public static object[] GetPropertyTypeValues<T>(string propertyName, string propertyValue) where T : class
        {
            var list = new List<object>();
            if (propertyName.IndexOf('!') > -1)
            {
                var names = propertyName.Split('!');
                var values = propertyValue.Split('!');
                for (var i = 0; i < names.Count(); i++)
                {
                    list.Add(GetPropertyTypeValue<T>(names[i], values[i]));
                }
            }
            else list.Add(GetPropertyTypeValue<T>(propertyName, propertyValue));
            return list.ToArray();
        }

        /// <summary>
        ///     根据目标对象获取指定属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
        public static object GetPropertyTypeValue<T>(string propertyName, string propertyValue) where T : class
        {
            var t = typeof(T).GetProperty(propertyName).PropertyType;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (propertyValue == null || propertyValue.Length == 0)
                {
                    return null;
                }
                var nullableConverter = new NullableConverter(t);
                t = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(propertyValue, t);
        }


        /// <summary>
        ///     获取实体属性值
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyTypeValue(object obj, string propertyName)
        {
            try
            {
                return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        ///     给属性赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            var propertyObj = obj.GetType().GetProperty(propertyName);
            if (propertyObj != null)
            {
                var newValue = value;
                if (value != null)
                {
                    if (propertyObj.PropertyType == typeof(decimal?) || propertyObj.PropertyType == typeof(decimal))
                    {
                        newValue = ConvertToDecimal(value.ToString());
                    }
                    else if (propertyObj.PropertyType == typeof(int?) || propertyObj.PropertyType == typeof(int))
                    {
                        int tmpValue = 0;
                        int.TryParse(value.ToString(), out tmpValue);
                        newValue = tmpValue;
                    }
                    else if (propertyObj.PropertyType == typeof(DateTime?) ||
                             propertyObj.PropertyType == typeof(DateTime))
                    {
                        newValue = ConvertToDateTime(value.ToString());
                    }
                }
                propertyObj.SetValue(obj, newValue, null);
            }
        }


        /// <summary>
        ///     将对象entity转换成T类型，（暂时未考虑集合、类等属性）
        /// </summary>
        /// <typeparam name="T">目的类型</typeparam>
        /// <param name="entity">源实体</param>
        /// <param name="tSource">源类型</param>
        /// <param name="dicMapPropertyInfo">不相同属性映射关系 key:目的属性 value:源属性</param>
        /// <returns></returns>
        [Obsolete("这个方法已经过时，请使用ConvertObjectValue<T>(object entity, Dictionary<string, string> dicMapPropertyInfo)")]
        public static T ConvertObjectValue<T>(object entity, Type tSource, Dictionary<string, string> dicMapPropertyInfo)
            where T : class
        {
            var model = Activator.CreateInstance<T>();
            var destPropertyNames = GetPropertyInfos(model.GetType()).Select(a => a.Name);
            var srcPropertyNames = GetPropertyInfos(tSource).Select(a => a.Name);

            //先获取相同属性
            var propertyNames = destPropertyNames as string[] ?? destPropertyNames.ToArray();
            propertyNames.Where(a => srcPropertyNames.Contains(a))
                .ToList()
                .ForEach(a => { SetPropertyValue(model, a, GetPropertyTypeValue(entity, a)); });

            //不同属性映射关系
            if (dicMapPropertyInfo != null)
            {
                foreach (var key in dicMapPropertyInfo.Keys)
                {
                    if (propertyNames.Contains(key) && srcPropertyNames.Contains(dicMapPropertyInfo[key]))
                    {
                        SetPropertyValue(model, key, GetPropertyTypeValue(entity, dicMapPropertyInfo[key]));
                    }
                }
            }
            return model;
        }

        /// <summary>
        ///     将对象entity转换成T类型，（暂时未考虑集合、类等属性）
        /// </summary>
        /// <typeparam name="T">目的类型</typeparam>
        /// <param name="entity">源实体</param>
        /// <param name="dicMapPropertyInfo">不相同属性映射关系 key:目的属性 value:源属性</param>
        /// <returns></returns>
        public static T ConvertObjectValue<T>(object entity, Dictionary<string, string> dicMapPropertyInfo)
            where T : class
        {
            var tSource = entity.GetType();
            var model = Activator.CreateInstance<T>();
            var destPropertyNames = GetPropertyInfos(model.GetType()).Select(a => a.Name);
            var srcPropertyNames = GetPropertyInfos(tSource).Select(a => a.Name);

            //先获取相同属性
            var propertyNames = destPropertyNames as string[] ?? destPropertyNames.ToArray();
            propertyNames.Where(a => srcPropertyNames.Contains(a))
                .ToList()
                .ForEach(a => { SetPropertyValue(model, a, GetPropertyTypeValue(entity, a)); });

            //不同属性映射关系
            if (dicMapPropertyInfo != null)
            {
                foreach (var key in dicMapPropertyInfo.Keys)
                {
                    if (propertyNames.Contains(key) && srcPropertyNames.Contains(dicMapPropertyInfo[key]))
                    {
                        SetPropertyValue(model, key, GetPropertyTypeValue(entity, dicMapPropertyInfo[key]));
                    }
                }
            }
            return model;
        }

        /// <summary>
        ///     将对象entity转换成T类型，（暂时未考虑集合、类等属性）
        /// </summary>
        /// <typeparam name="T">目的类型</typeparam>
        /// <param name="entity">源实体</param>
        /// <returns></returns>
        public static T ConvertObjectValue<T>(object entity) where T : class
        {
            return ConvertObjectValue<T>(entity, null);
        }

        /// <summary>
        /// 将DataTable转换为List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ConvertDataTableToEntity<T>(DataTable dt) where T : class
        {
            var list = ConvertDataTableToEntities<T>(dt);
            if (list != null && list.Any())
                return list.FirstOrDefault();
            else
            {
                return Activator.CreateInstance<T>();
            }

        }
        /// <summary>
        /// 将DataTable转换为List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToEntities<T>(DataTable dt) where T : class
        {
            var list = new List<T>();
            var cols = dt.Columns;
            foreach (DataRow row in dt.Rows)
            {
                var t = System.Activator.CreateInstance<T>();
                foreach (DataColumn col in cols)
                {
                    var colName = col.ColumnName;
                    if (t.GetType().GetProperties().Any(a => a.Name == colName))
                    {
                        var colValue = row[colName];
                        if (colValue is System.DBNull)
                        {
                            colValue = null;
                        }

                        SetPropertyValue(t, colName, colValue);
                    }
                }
                list.Add(t);
            }
            return list;
        }


        #endregion

        #region Json

        /// <summary>
        ///     将对象转换为json
        /// </summary>
        /// <param name="obj">要转换对象</param>
        /// <returns></returns>
        public static string GetJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetApiJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            json = json.Replace("null", "\"\"");
            return json;

        }
        /// <summary>
        ///     将对象转换为json
        /// </summary>
        /// <param name="obj">要转换对象</param>
        /// <returns></returns>
        public static string GetJsonWithJObject(object obj)
        {
            return JObject.FromObject(obj).ToString();
        }

        public static string ObjectToJson(object obj)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     将Json字符串转填充入指定对象的属性
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json"></param>
        public static T GetEntityFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 读取Json字符串的某个Key的value值
        /// </summary>
        /// <param name="jsonText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReaderJsonValue(string jsonText, string key)
        {
            JObject jo = JObject.Parse(jsonText);
            var values = jo.Properties().Where(a => a.Name == key).Select(a => a.Value.ToString()).ToList();
            if (values != null && values.Any())
            {
                return values.FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region 加密解密

        /// <summary>
        ///     获取Md5码
        /// </summary>
        /// <param name="input">需计算字符串</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            var buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(input));
            var builder = new StringBuilder();
            for (var i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        ///     获取Sha1码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSha1Hash(string input)
        {
            var cleanBytes = Encoding.Default.GetBytes(input);
            var hashedBytes = SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        /// <summary>
        ///     字符Web编码
        /// </summary>
        /// <param name="data">需要加码字符串</param>
        /// <returns></returns>
        public static string GetEncodeString(string data)
        {
            return HttpUtility.UrlEncode(data);
        }

        /// <summary>
        ///     字符Web解码
        /// </summary>
        /// <param name="encodedData">已加码字符串</param>
        /// <returns></returns>
        public static string GetDecodeString(string encodedData)
        {
            return HttpUtility.UrlDecode(encodedData);
        }

        /// <summary>
        ///     获取配置信息
        /// </summary>
        /// <param name="tokenName">节点名次</param>
        /// <returns></returns>
        //public static string GetConfigurationSetting(string tokenName)
        //{
        //    return ConfigurationManager.AppSettings[tokenName];
        //}
        /// <summary>
        /// 生成随机密码
        /// </summary>
        /// <param name="length">密码长度</param>
        /// <returns></returns>
        public static string GetRadomPassWord(int length)
        {
            string pwdStr = String.Empty;
            //去掉部分不易辨认的字符（o,l等）
            string pwdSource = "23456789ABCDEFGHKMNPQRSTWSY";
            int radomNum;
            Random radom = new Random();
            for (int i = 0; i < length; i++)
            {
                radomNum = radom.Next(pwdSource.Length);
                pwdStr += pwdSource[radomNum];
            }
            return pwdStr.ToUpper();
        }
        #endregion

        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal DecimalParse(object obj)
        {
            if (obj == null) return 0;
            if (string.IsNullOrEmpty(obj.ToString())) return 0;
            decimal outDecimal = 0;
            decimal.TryParse(obj.ToString(), out outDecimal);
            return outDecimal;
        }

        // 将为""的String转换成0
        public static decimal ConvertToDecimal(string s, int length = 0)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            decimal num;
            if (decimal.TryParse(s, out num))
            {
                return length > -1 ? Math.Round(num, length) : num;
            }
            return 0;
        }


        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string decimalToString(decimal? obj)
        {
            if (obj == null) return "0";
            if (string.IsNullOrEmpty(obj.ToString())) return "0";
            return obj.ToString();
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new DateTime();
            }
            else
            {
                DateTime date = new DateTime();
                DateTime.TryParse(value.ToString(), out date);
                return date;
            }
        }/// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeNullToNow(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return DateTime.Now;
            }
            else
            {
                DateTime date = new DateTime();
                DateTime.TryParse(value.ToString(), out date);
                return (date == new DateTime() ? DateTime.Now : date);
            }
        }
        /// <summary>
        /// 获取本地日期，格式年月日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetShortDateTimeString(DateTime? date)
        {
            if (date == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToDateTime(date).ToString("yyyy年MM月dd日");
            }
        }

        public static string GetEmptyDateTimeString()
        {
            return "xxxx年xx月xx日";
        }

        /// <summary>
        /// URL转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">实体</param>
        /// <param name="query">URL字符串</param>
        /// <returns>T</returns>
        public static T ConvetURLQueryToObject<T>(T entity, string query) where T : class
        {
            //(例如query=  ?humanid=1&humanname=33333&address=冉家坝)
            query = query.Replace("?", "");
            var tmpQueryArry = query.Split('&');
            Type type = entity.GetType();
            for (int i = 0; i < tmpQueryArry.Length; i++)
            {
                var item = tmpQueryArry[i].Split('=');
                string name = item[0].ToString();
                string value = item[1].ToString();
                var propertyArry = type.GetProperties();
                for (int j = 0; j < propertyArry.Length; j++)
                {
                    string propName = propertyArry[j].Name;
                    //string propValue = propertyArry[j].GetValue(oAuthUser, null).ToString();
                    if (name == propName)
                    {
                        propertyArry[j].SetValue(entity, HttpUtility.UrlDecode(value));
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取指定字符出现的索引位置
        /// </summary>
        /// <param name="findString">字符串信息</param>
        /// <param name="findChar">需要查询的单个字符</param>
        /// <param name="n">第几次出现</param>
        /// <returns></returns>
        public static int GetCharIndex(string findString, char findChar, int n)
        {
            char c;
            int count = 0;
            for (int i = 0; i < findString.Length; i++)
            {
                c = findString[i];
                if (c == findChar)//求得a中包含该字符的个数，以便遍历   
                {
                    count++;
                }
            }
            Console.WriteLine("There are total {0} of char '{1}' in your input string.", count, findChar);
            int index = 0;
            Console.WriteLine("Please input the SEQUENCE of the char '{0}' in your input string:", findChar);
            if (n > count)
            {
                Console.WriteLine("Error:The Num must be less than or equal to {0}.", count);
                return -1;
            }
            for (int j = 1; j <= count; j++)
            {
                index = findString.IndexOf(findChar, index);
                if (j == n)
                {
                    break;
                }
                else
                {
                    index = findString.IndexOf(findChar, index + 1);
                }

            }
            Console.WriteLine("The Index of the No.{0} char '{1}' in your input string is {2}.", n, findChar, index);
            return index;
        }

    }
}
