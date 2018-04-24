using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    public class FtpHelper
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">需要上传的文件的绝对路径</param>
        /// <param name="targetDir">Ftp目标目录</param>
        /// <param name="hostname">ftp地址</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public static string UploadFile(string path, string targetDir, string hostname, string username, string password)
        {
            if (targetDir.Trim() == "")
            {
                return "上传目录不能为空";
            }
            string target = Path.GetFileName(path); //使用临时文件名
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                return "上传文件不存在";
            }

            string uri = "FTP://" + hostname + "/" + targetDir + "/" + target;
            System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
            //设置FTP命令 设置所要执行的FTP命令，
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            //告诉ftp文件大小
            ftp.ContentLength = fi.Length;
            //缓冲大小设置为2KB
            const int bufferSize = 2048;
            byte[] content = new byte[bufferSize - 1 + 1];
            int dataRead;
            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fi.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, bufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < bufferSize));
                        rs.Close();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            // ftp = null;
            //设置FTP命令
            //ftp = GetRequest(uri, username, password);
            //ftp.Method = System.Net.WebRequestMethods.Ftp.Rename; //改名
            //DateTime dt = DateTime.Now;
            //string date = dt.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "_";
            //ftp.RenameTo = date + fi.Name;
            try
            {
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("530"))
                {
                    return "ftp登录错误";
                }
                if (ex.Message.Contains("550"))
                {
                    ftp = GetRequest(uri, username, password);
                    ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                    ftp.GetResponse();
                    return "文件错误";
                }
            }
            return "上传成功";
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">需要上传的文件的绝对路径</param>
        /// <param name="targetDir">Ftp目标目录(/Upload/2015/)</param>
        /// <param name="hostname">ftp地址</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        /// <param name="filename">文件名称</param>
        public static string UploadFileJSXM(string path, string targetDir, string hostname, string username, string password, string filename)
        {
            if (targetDir.Trim() == "")
            {
                return "上传目录不能为空";
            }
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                return "上传文件不存在,文件路径" + path;
            }
            filename = filename + Path.GetExtension(path);
            var uploadDir = "ftp://" + hostname + targetDir;
            string uri = uploadDir + filename;

            //如果目录存在，则先创建
            if (!ftpIsExistsFile(targetDir, hostname, username, password))
            {
                FtpHelper.MakeMoreDir(targetDir, hostname, username, password);
            }

            //正常情况应该判断FTP上文件是否存在，存在则不上传

            System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
            //设置FTP命令 设置所要执行的FTP命令，
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            //告诉ftp文件大小
            ftp.ContentLength = fi.Length;
            //缓冲大小设置为2KB
            const int bufferSize = 2048;
            byte[] content = new byte[bufferSize - 1 + 1];
            int dataRead;
            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fi.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, bufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < bufferSize));
                        rs.Close();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            // ftp = null;
            //设置FTP命令
            //ftp = GetRequest(uri, username, password);
            //ftp.Method = System.Net.WebRequestMethods.Ftp.Rename; //改名
            //DateTime dt = DateTime.Now;
            //string date = dt.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "_";
            //ftp.RenameTo = date + fi.Name;
            try
            {
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("530"))
                {
                    return "ftp登录错误";
                }
                if (ex.Message.Contains("550"))
                {
                    ftp = GetRequest(uri, username, password);
                    ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                    ftp.GetResponse();
                    return "文件错误";
                }
            }
            return "";
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localDir">下载至本地路径</param>
        /// <param name="ftpDir">ftp目标文件路径</param>
        /// <param name="ftpFile">从ftp要下载的文件名</param>
        /// <param name="hostname">ftp地址即IP</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public static void DownloadFile(string localfile, string ftpFile, string hostname, string username,
            string password)
        {
            // string uri = "FTP://" + hostname + ftpDir + "/" + ftpFile;
            string uri = "FTP://" + hostname + ftpFile;
            //string tmpname = Guid.NewGuid().ToString();
            //string localfile = localDir + @"\" + tmpname;
            System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = false;
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    //loop to read & write to file
                    using (FileStream fs = new FileStream(localfile, FileMode.Create))
                    {
                        try
                        {
                            byte[] buffer = new byte[2048];
                            int read = 0;
                            do
                            {
                                read = responseStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);
                            } while (!(read == 0));
                            responseStream.Close();
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception exception)
                        {
                            //catch error and delete file only partially downloaded
                            fs.Close();
                            //delete target file as it's incomplete
                            //File.Delete(localfile);
                            throw exception;
                        }
                    }
                    responseStream.Close();
                }
                response.Close();
            }

            //try
            //{
            //    File.Delete(localDir + @"\" + ftpFile);
            //    File.Move(localfile, localDir + @"\" + ftpFile);

            //    ftp = null;
            //    //ftp = GetRequest(uri, username, password);
            //    //ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
            //    //ftp.GetResponse();
            //}
            //catch (Exception ex)
            //{
            //    File.Delete(localfile);
            //    throw ex;
            //}
            // 记录日志 "从" + uri.ToString() + "下载到" + localDir + @"\" + FtpFile + "成功." );
            ftp = null;
        }

        /// <summary>
        /// 搜索远程文件
        /// </summary>
        /// <param name="targetDir"></param>
        /// <param name="hostname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="SearchPattern"></param>
        /// <returns></returns>
        public static List<string> ListDirectory(string targetDir, string hostname, string username, string password,
            string SearchPattern)
        {
            List<string> result = new List<string>();
            try
            {
                string uri = "FTP://" + hostname + targetDir + "/" + SearchPattern;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;
                ftp.UsePassive = true;
                ftp.UseBinary = true;

                string str = GetStringResponse(ftp);
                str = str.Replace("\r\n", "\r").TrimEnd('\r');
                str = str.Replace("\n", "\r");
                if (str != string.Empty)
                    result.AddRange(str.Split('\r'));
                return result;
            }
            catch
            {
            }
            return null;
        }

        /// 在ftp服务器上创建目录(只支持一级目录的创建)
        /// <summary>
        /// <param name="dirName">创建的目录名称(例如"/Upload/tmpFile")</param>
        /// <param name="ftpHostIP">ftp主机</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// </summary>
        public static void MakeDir(string dirName, string ftpHostIP, string username, string password)
        {
            try
            {
                string uri = "ftp://" + ftpHostIP + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }

            catch (Exception ex)
            {
                var str = ex.Message;
            }
        }
        /// <summary>
        /// 创建FTP目录（支持多级目录不存在创建）
        /// </summary>
        /// <param name="dirName">/11/22/33/ 其中11，22，33目录均不存在</param>
        /// <param name="fptHostIP"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void MakeMoreDir(string dirName, string hostName, string username, string password)
        {
            try
            {
                var dirPaths = dirName.Split('/');
                var tmpPaths = new List<string>();
                for (int i = 0; i < dirPaths.Length; i++)
                {
                    if (!string.IsNullOrEmpty(dirPaths[i]))
                    {
                        var parentPath = tmpPaths.Count > 0 ? string.Join("/", tmpPaths) + "/" : "";
                        var targetDir = "/" + parentPath + dirPaths[i] + "/";
                        var uploadDir = "ftp://" + hostName + targetDir;
                        //如果目录不存在，则先创建
                        if (!ftpIsExistsFile(targetDir, hostName, username, password))
                        {
                            FtpHelper.MakeDir(targetDir, hostName, username, password);
                        }

                        tmpPaths.Add(dirPaths[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }


        }


        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        /// <param name="ftpHostIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void delDir(string dirName, string ftpHostIP, string username, string password)
        {
            try
            {
                string uri = "ftp://" + ftpHostIP + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="currentFilename">当前目录名称</param>
        /// <param name="newFilename">重命名目录名称</param>
        /// <param name="ftpServerIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void Rename(string currentFilename, string newFilename, string ftpServerIP, string username,
            string password)
        {
            try
            {
                FileInfo fileInf = new FileInfo(currentFilename);
                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {

            }
        }


        private static string GetStringResponse(FtpWebRequest ftp)
        {
            //Get the result, streaming to a string
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(datastream, System.Text.Encoding.Default))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }
                    datastream.Close();
                }
                response.Close();
            }
            return result;
        }

        private static FtpWebRequest GetRequest(string uri, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(uri);
            //提供身份验证信息
            result.Credentials = new System.Net.NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            return result;
        }

        /// <summary>
        /// 向Ftp服务器上传文件并创建和本地相同的目录结构
        /// 遍历目录和子目录的文件
        /// </summary>
        /// <param name="file"></param>
        //private void GetFileSystemInfos(FileSystemInfo file)
        //{
        //    string getDirecName = file.Name;
        //    if (!ftpIsExistsFile(getDirecName, "192.168.0.172", "Anonymous", "") && file.Name.Equals(FileName))
        //    {
        //        MakeDir(getDirecName, "192.168.0.172", "Anonymous", "");
        //    }
        //    if (!file.Exists) return;
        //    DirectoryInfo dire = file as DirectoryInfo;
        //    if (dire == null) return;
        //    FileSystemInfo[] files = dire.GetFileSystemInfos();
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        FileInfo fi = files[i] as FileInfo;
        //        if (fi != null)
        //        {
        //            DirectoryInfo DirecObj = fi.Directory;
        //            string DireObjName = DirecObj.Name;
        //            if (FileName.Equals(DireObjName))
        //            {
        //                UploadFile(fi, DireObjName, "192.168.0.172", "Anonymous", "");
        //            }
        //            else
        //            {
        //                Match m = Regex.Match(files[i].FullName, FileName + "+.*" + DireObjName);
        //                //UploadFile(fi, FileName+"/"+DireObjName, "192.168.0.172", "Anonymous", "");
        //                UploadFile(fi, m.ToString(), "192.168.0.172", "Anonymous", "");
        //            }
        //        }
        //        else
        //        {
        //            string[] ArrayStr = files[i].FullName.Split('\\');
        //            string finame = files[i].Name;
        //            Match m = Regex.Match(files[i].FullName, FileName + "+.*" + finame);
        //            //MakeDir(ArrayStr[ArrayStr.Length - 2].ToString() + "/" + finame, "192.168.0.172", "Anonymous", "");
        //            MakeDir(m.ToString(), "192.168.0.172", "Anonymous", "");
        //            GetFileSystemInfos(files[i]);
        //        }
        //    }
        //}
        /// <summary>
        /// 判断ftp服务器上该目录是否存在
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="ftpHostIP"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool ftpIsExistsFile(string dirName, string ftpHostIP, string username, string password)
        {
            bool flag = true;
            try
            {
                string uri = "ftp://" + ftpHostIP + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}
