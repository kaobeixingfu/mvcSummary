using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Standard.Common
{
    public class ReadXml
    {
        /// <summary>
        /// 读Xml文件
        /// <param name="path">文件路经</param>
        /// </summary>
        /// <returns></returns>
        public static XmlDocument Read(string path)
        {
            XmlDocument xml = new XmlDocument();
            var result = string.Empty;
            //path = System.IO.Path.GetFullPath(path);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
            }
            if (!string.IsNullOrEmpty(result))
            {
                xml.LoadXml(result);
            }
            return xml;
        }


        /// <summary>
        /// 获取上传文件Xml文件的路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetUploadFileXmlPath(string fileName)
        {
            return "~/App_Data/Upload/" + fileName + ".xml";
        }

        /// <summary>
        /// 获取申报材料编码信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetGenerateCodeFilePath(string fileName)
        {
            return "~/App_Data/GenCode/ProjectReportRule.xml";
        }
        /// <summary>
        /// 获取swftool工具路经
        /// </summary>
        /// <returns></returns>
        public static string GetSwfFilePath()
        {
            return "~/App_Data/FileConvert/SwfConfig.xml";
        }
        /// <summary>
        /// 获取节点的属性
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetXmlNodeAttributes(XmlNode node, string attributeName)
        {
            if (node != null && node.Attributes != null && node.Attributes[attributeName] != null)
            {
                return node.Attributes[attributeName].Value ?? "";
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
