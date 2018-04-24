using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcSummarize.Models
{
    public class Print
    {
        public string Title { get; set; }
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string CardNo { get; set; }
        public string sTime { get; set; }
        public string sAddress { get; set; }
        public string eAddress { get; set; }
        public double price { get; set; }
        public int freeNum { get; set; }
        public string Pay { get; set; }
    }
}