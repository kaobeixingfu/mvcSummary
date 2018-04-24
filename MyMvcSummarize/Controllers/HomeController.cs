using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMvcSummarize.Models;
using MyMvcSummarize.NinjectConfig;
using Ninject;
using Standard.BLL.NinjectConfig;
using Standard.Common;
using Standard.IBLL.FunctoinIBLL;

namespace MyMvcSummarize.Controllers
{
    public class HomeController : Controller
    {
        //private static IKernel kernel = new StandardKernel(new BLLModule());
        //IUsers Services = kernel.Get<IUsers>();
        [Inject]
        private Standard.IBLL.FunctoinIBLL.IUsers Services { get; set; }

        protected System.Timers.Timer tmr;
        /// <summary>
        /// 测试打印
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //PrintDocument fPrintDocument = new PrintDocument();
            //PrinterName = fPrintDocument.PrinterSettings.PrinterName;
            //tmr = new System.Timers.Timer();
            //tmr.Interval = 5000;
            //tmr.Elapsed += tmr_Elapsed;
            //tmr.Start();
            return View();
        }

        public string PrinterName = "", orderno = "";
        void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (true)
                {
                    List<Print> plist = new List<Print>();
                    for (int i = 0; i < 1; i++)
                    {
                        Print temp = new Print();
                        temp.Title = "Title" + 1;
                        temp.OrderNo = "OrderNo" + 1;
                        temp.Name = "Name" + 1;
                        temp.CardNo = "CardNo" + 1;
                        temp.sTime = "sTime" + 1;
                        temp.sAddress = "sAddress" + 1;
                        temp.eAddress = "eAddress" + 1;
                        temp.price = 1;
                        temp.freeNum = 1;
                        temp.Pay = "Pay" + 1;
                        plist.Add(temp);
                    }

                    orderno = plist[0].OrderNo;
                    PrintDocument_new fPrintDocument = new PrintDocument_new();
                    fPrintDocument.PrinterSettings.PrinterName = PrinterName;
                    fPrintDocument.data = plist;
                    fPrintDocument.DefaultPageSettings.Landscape = false;//zongxiang
                    fPrintDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                    fPrintDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    fPrintDocument.EndPrint += fPrintDocument_EndPrint;
                    fPrintDocument.Print();
                }
                else
                {

                    tmr.Start();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected int count = 0;
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                object obj = ((PrintDocument_new)sender).data;
                List<Print> plist = obj as List<Print>;
                if (count < plist.Count)
                {
                    Print print = plist[count];
                    string free = print.freeNum > 0 ? "免票数" + print.freeNum : "";
                    string text = string.Format(@"线路:{0}
订单号:{1}
姓名:{2}
身份证号:{3}
发车时间:{4}
发车地址:{5}
目的地:{6}
票价:{7}元  {8}
支付方式:{9}", print.Title, print.OrderNo, print.Name, print.CardNo,
             print.sTime, print.sAddress, print.eAddress, print.price, free, print.Pay);

                    byte[] b = System.Text.Encoding.GetEncoding("gb2312").GetBytes(text);
                    System.Text.Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
                    text = gb.GetString(b);
                    PrivateFontCollection privateFonts = new PrivateFontCollection();
                    System.Drawing.Font font;
                    System.Drawing.Font font2;
                    try
                    {
                        privateFonts.AddFontFile(@"msyh.ttf");//加载字体
                        font = new Font(privateFonts.Families[0], 11, FontStyle.Regular);
                        font2 = new Font(privateFonts.Families[0], 10, FontStyle.Regular);
                    }
                    catch
                    {
                        font = new Font(new FontFamily("黑体"), 11);
                        font2 = new Font(new FontFamily("黑体"), 10);
                    }
                    e.Graphics.DrawString(text, font, System.Drawing.Brushes.Black, 280.50f, 60.51f);
                    e.Graphics.DrawString(text, font2, System.Drawing.Brushes.Black, 570.21f, 30.51f);
                    e.Graphics.DrawString(text, font2, System.Drawing.Brushes.Black, 800.69f, 30.51f);
                    count++;
                }

                if (count < plist.Count)
                {
                    e.HasMorePages = true;  //分页 
                }
                else
                {
                    e.HasMorePages = false;
                    count = 0;
                }
                //e.Graphics.DrawString("打印机测试", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, 10, 35);
            }
            catch (Exception ex)
            {

            }
        }
        void fPrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            try
            {
                //打印完成
            }
            catch (Exception ex)
            {

            }
        }


        public ActionResult Index1()
        {


            DataTable tblDatas = new DataTable("Datas");
            DataColumn dc = null;
            dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//

            dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));

            DataRow newRow;
            newRow = tblDatas.NewRow();
            newRow["Product"] = "大话西游";
            newRow["Version"] = "2.0";
            newRow["Description"] = "我很喜欢";
            tblDatas.Rows.Add(newRow);

            newRow = tblDatas.NewRow();
            newRow["Product"] = "梦幻西游";
            newRow["Version"] = "3.0";
            newRow["Description"] = "反倒是测试测试";
            tblDatas.Rows.Add(newRow);
            // 2.调用方法：
            //打印：
            //  new PrintHelper().Print(tblDatas, "Title报表打印");
            //预览：
            new PrintHelper().PrintPriview(tblDatas, "Title报表打印");

            //Services.InsertProc();
            //Services.SelectList();
            //Services.GetDataSource();

            return View();
        }

        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Default()
        {
            Services.GetDataSource();
            return View();
        }

        public ActionResult TestSql()
        {

            return View();
        }
    }
}
