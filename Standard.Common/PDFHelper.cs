using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using Aspose.Pdf.Generator;
using Aspose.Words;
using NPOI.SS.Formula.Functions;

namespace Standard.Common
{
    //public class PDFHelper
    //{


    //    /// <summary>
    //    /// 搜索文件夹中的文件
    //    /// </summary>
    //    /// <param name="dir"></param>
    //    /// <returns></returns>
    //    private static List<string> GetAll(DirectoryInfo dir)//搜索文件夹中的文件
    //    {
    //        List<string> FileList = new List<string>();

    //        FileInfo[] allFile = dir.GetFiles();
    //        foreach (FileInfo fi in allFile)
    //        {
    //            FileList.Add(fi.Name);
    //        }

    //        DirectoryInfo[] allDir = dir.GetDirectories();
    //        foreach (DirectoryInfo d in allDir)
    //        {
    //            GetAll(d);
    //        }
    //        return FileList;
    //    }

    //    public static string DocxToDoc(string sourceDirPath, string targetDirPath)
    //    {
    //        try
    //        {
    //            DirectoryInfo dir = new DirectoryInfo(sourceDirPath);
    //            List<string> fileList = GetAll(dir);
    //            foreach (var item in fileList)
    //            {
    //                if (item.Contains(".docx"))
    //                {
    //                    string sourcePath = sourceDirPath + "\\" + item;
    //                    string targetPath = targetDirPath + "\\" + item.Replace(".docx", ".doc");
    //                    Aspose.Words.Document doc = new Aspose.Words.Document(sourcePath);
    //                    doc.Save(targetPath, Aspose.Words.SaveFormat.Doc);
    //                }
    //            }
    //            return "成功";

    //        }
    //        catch (Exception exception)
    //        {

    //            return "失败";
    //        }
    //    }

    //    /// <summary>
    //    /// 统一保存为PDF文件
    //    /// </summary>
    //    /// <param name="fs_filePath">源文件</param>
    //    /// <param name="fs_convertedFilePath">新文件</param>
    //    /// <param name="fileExtension">文件类型</param>
    //    public static void SavePDF(string fs_filePath, string fs_convertedFilePath, string fileExtension)
    //    {
    //        if (System.IO.File.Exists(fs_filePath))
    //        {
    //            switch (fileExtension)
    //            {
    //                case ".docx":
    //                case ".doc":
    //                case ".dotx":
    //                case ".dot":
    //                    Common.PDFHelper.DOCConvertToPDF(fs_filePath, fs_convertedFilePath);
    //                    break;
    //                //case "xls":
    //                //case "xlsx":
    //                //case "xlsm":
    //                //case "xlsb":
    //                //    Common.PDFHelper.XLSConvertToPDF(fs_filePath, fs_convertedFilePath);
    //                //    break;
    //                case ".pdf":
    //                    System.IO.File.Copy(fs_filePath, fs_convertedFilePath);
    //                    break;
    //                case ".txt":
    //                    break;
    //            }

    //        }

    //    }

    //    ///Word转换成pdf
    //    /// <summary>
    //    /// 把Word文件转换成为PDF格式文件
    //    /// </summary>
    //    /// <param name="sourcePath">源文件路径</param>
    //    /// <param name="targetPath">目标文件路径</param>
    //    /// <returns>true=转换成功</returns>
    //    /// 
    //    public static bool DOCConvertToPDF(string sourcePath, string targetPath)
    //    {

    //        try
    //        {


    //            Aspose.Words.Document doc = new Aspose.Words.Document(sourcePath);
    //            doc.Save(targetPath, Aspose.Words.SaveFormat.Doc);
    //            return true;

    //        }
    //        catch (Exception exception)
    //        {

    //            return false;
    //        }


    //        #region Office转换（暂不使用）
    //        //bool result = false;
    //        //Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;
    //        //object paramMissing = Type.Missing;
    //        //Word.Application wordApplication = new Word.Application();
    //        //Word.Document wordDocument = null;
    //        //try
    //        //{
    //        //    object paramSourceDocPath = sourcePath;
    //        //    string paramExportFilePath = targetPath;
    //        //    Word.WdExportFormat paramExportFormat = exportFormat;
    //        //    bool paramOpenAfterExport = false;
    //        //    Word.WdExportOptimizeFor paramExportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
    //        //    Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
    //        //    int paramStartPage = 0;
    //        //    int paramEndPage = 0;
    //        //    Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
    //        //    bool paramIncludeDocProps = true;
    //        //    bool paramKeepIRM = true;
    //        //    Word.WdExportCreateBookmarks paramCreateBookmarks = Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
    //        //    bool paramDocStructureTags = true;
    //        //    bool paramBitmapMissingFonts = true;
    //        //    bool paramUseISO19005_1 = false;
    //        //    wordDocument = wordApplication.Documents.Open(
    //        //        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
    //        //        ref paramMissing, ref paramMissing, ref paramMissing,
    //        //        ref paramMissing, ref paramMissing, ref paramMissing,
    //        //        ref paramMissing, ref paramMissing, ref paramMissing,
    //        //        ref paramMissing, ref paramMissing, ref paramMissing,
    //        //        ref paramMissing);
    //        //    if (wordDocument != null)
    //        //        wordDocument.ExportAsFixedFormat(paramExportFilePath,
    //        //            paramExportFormat, paramOpenAfterExport,
    //        //            paramExportOptimizeFor, paramExportRange, paramStartPage,
    //        //            paramEndPage, paramExportItem, paramIncludeDocProps,
    //        //            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
    //        //            paramBitmapMissingFonts, paramUseISO19005_1,
    //        //            ref paramMissing);
    //        //    result = true;
    //        //}
    //        //catch
    //        //{
    //        //    result = false;
    //        //}
    //        //finally
    //        //{
    //        //    if (wordDocument != null)
    //        //    {
    //        //        wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
    //        //        wordDocument = null;
    //        //    }
    //        //    if (wordApplication != null)
    //        //    {
    //        //        wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
    //        //        wordApplication = null;
    //        //    }
    //        //    GC.Collect();
    //        //    GC.WaitForPendingFinalizers();
    //        //    GC.Collect();
    //        //    GC.WaitForPendingFinalizers();
    //        //}
    //        //return result; 
    //        #endregion

    //    }

    //    public static void PDf2Word()
    //    {
    //        //XML
    //        Document pdfDocument = new Document(@"d:\test.pdf");
    //        pdfDocument.Save(@"d:\test.doc", SaveFormat.Doc);
    //    }

    //    #region EXCEL和PPT转PDF（暂不使用）
    //    /// <summary>
    //    /// 把Excel文件转换成PDF格式文件  
    //    /// </summary>
    //    /// <param name="sourcePath">源文件路径</param>
    //    /// <param name="targetPath">目标文件路径</param>
    //    /// <returns>true=转换成功</returns>
    //    //public bool XLSConvertToPDF(string sourcePath, string targetPath)
    //    //{
    //    //    bool result = false;
    //    //    Excel.XlFixedFormatType targetType = Excel.XlFixedFormatType.xlTypePDF;
    //    //    object missing = Type.Missing;
    //    //    Excel.ApplicationClass application = null;
    //    //    Excel.Workbook workBook = null;
    //    //    try
    //    //    {
    //    //        application = new Excel.ApplicationClass();
    //    //        object target = targetPath;
    //    //        object type = targetType;
    //    //        workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
    //    //            missing, missing, missing, missing, missing, missing, missing, missing, missing);
    //    //        workBook.ExportAsFixedFormat(targetType, target, Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
    //    //        result = true;
    //    //    }
    //    //    catch
    //    //    {
    //    //        result = false;
    //    //    }
    //    //    finally
    //    //    {
    //    //        if (workBook != null)
    //    //        {
    //    //            workBook.Close(true, missing, missing);
    //    //            workBook = null;
    //    //        }
    //    //        if (application != null)
    //    //        {
    //    //            application.Quit();
    //    //            application = null;
    //    //        }
    //    //        GC.Collect();
    //    //        GC.WaitForPendingFinalizers();
    //    //        GC.Collect();
    //    //        GC.WaitForPendingFinalizers();
    //    //    }
    //    //    return result;
    //    //}
    //    ///<summary>        
    //    /// 把PowerPoint文件转换成PDF格式文件       
    //    ///</summary>        
    //    ///<param name="sourcePath">源文件路径</param>     
    //    ///<param name="targetPath">目标文件路径</param> 
    //    ///<returns>true=转换成功</returns> 
    //    //public bool PPTConvertToPDF(string sourcePath, string targetPath)
    //    //{
    //    //    bool result;
    //    //    PowerPoint.PpSaveAsFileType targetFileType = PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
    //    //    object missing = Type.Missing;
    //    //    PowerPoint.ApplicationClass application = null;
    //    //    PowerPoint.Presentation persentation = null;
    //    //    try
    //    //    {
    //    //        application = new PowerPoint.ApplicationClass();
    //    //        persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse); persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);
    //    //        result = true;
    //    //    }
    //    //    catch
    //    //    {
    //    //        result = false;
    //    //    }
    //    //    finally
    //    //    {
    //    //        if (persentation != null)
    //    //        {
    //    //            persentation.Close();
    //    //            persentation = null;
    //    //        }
    //    //        if (application != null)
    //    //        {
    //    //            application.Quit();
    //    //            application = null;
    //    //        }
    //    //        GC.Collect();
    //    //        GC.WaitForPendingFinalizers();
    //    //        GC.Collect();
    //    //        GC.WaitForPendingFinalizers();
    //    //    }
    //    //    return result;
    //    //} 
    //    #endregion

    //    /// <summary>
    //    /// 将PDF转换为SWF文件
    //    /// </summary>
    //    /// <param name="pdfPath">PDF文件路径</param>
    //    /// <param name="swfPath">SWF文件路径</param>
    //    /// <param name="page"></param>
    //    public static void ConvertToSwf(string pdfPath, string swfPath, string swfConfig)
    //    {
    //        //try
    //        //{
    //        //    string pdf2swfToolPath = "";
    //        //    var xml = ReadXml.Read(swfConfig);
    //        //    if (xml != null)
    //        //    {
    //        //        var nodes = xml.SelectNodes("/root/config/swfConfig");

    //        //        if (nodes != null)
    //        //        {
    //        //            foreach (XmlNode node in nodes)
    //        //            {
    //        //                if (node.Attributes != null && node.Attributes["name"].Value == "swfTool")
    //        //                {
    //        //                    pdf2swfToolPath = node.Attributes["filePath"].Value.ToString();
    //        //                }
    //        //            }
    //        //        }
    //        //    }
    //        //    string disk = pdf2swfToolPath.Substring(0, 2).ToString();
    //        //    Process p = new Process();
    //        //    p.StartInfo.FileName = "cmd.exe ";
    //        //    p.StartInfo.UseShellExecute = false;
    //        //    p.StartInfo.RedirectStandardInput = true;
    //        //    p.StartInfo.RedirectStandardOutput = true;
    //        //    p.StartInfo.RedirectStandardError = true;
    //        //    p.StartInfo.CreateNoWindow = true;
    //        //    p.Start();
    //        //    string cmd = "pdf2swf.exe -t" + " " + pdfPath + " -s flashversion=9 -o " + swfPath;
    //        //    //转换为swf失败的情况下 转换一页是成功的
    //        //    //string cmd = "pdf2swf.exe  -S -j75 -T9  -p1 " + " " + pdfPath + " -o " + swfPath;
    //        //    p.StandardInput.WriteLine(disk);
    //        //    p.StandardInput.WriteLine("cd " + pdf2swfToolPath);  
    //        //    p.StandardInput.WriteLine(cmd);
    //        //    p.Close();

    //        //}
    //        //catch (Exception ex)
    //        //{
    //        //    throw ex;
    //        //}
    //        try
    //        {
    //            string pdf2swfToolPath = "";
    //            var xml = ReadXml.Read(swfConfig);
    //            if (xml != null)
    //            {
    //                var nodes = xml.SelectNodes("/root/config/swfConfig");

    //                if (nodes != null)
    //                {
    //                    foreach (XmlNode node in nodes)
    //                    {
    //                        if (node.Attributes != null && node.Attributes["name"].Value == "swfTool")
    //                        {
    //                            pdf2swfToolPath = node.Attributes["filePath"].Value.ToString();
    //                        }
    //                    }
    //                }
    //            }
    //            pdf2swfToolPath = @"D:\Program Files (x86)\SWFTools\pdf2swf.exe";

    //            if (!File.Exists(pdf2swfToolPath))
    //            {
    //                throw new ApplicationException("Can not find: " + pdf2swfToolPath);
    //            }

    //            StringBuilder sb = new StringBuilder();
    //            sb.Append(" -o \"" + swfPath + "\"");//output
    //            sb.Append(" -z");
    //            sb.Append(" -s flashversion=9");//flash version
    //            sb.Append(" -s disablelinks");//禁止PDF里面的链接
    //            // sb.Append(" -p " + "1" + "-" + page);//page range
    //            sb.Append(" -j 100");//Set quality of embedded jpeg pictures to quality. 0 is worst (small), 100 is best (big). (default:85)
    //            sb.Append(" \"" + pdfPath + "\"");//input

    //            System.Diagnostics.Process proc = new System.Diagnostics.Process();
    //            proc.StartInfo.FileName = pdf2swfToolPath;
    //            proc.StartInfo.Arguments = sb.ToString();
    //            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    //            proc.Start();
    //            proc.WaitForExit();
    //            proc.Close();

    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //    }


    //}
}
