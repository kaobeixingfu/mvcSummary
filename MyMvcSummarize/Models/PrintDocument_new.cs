using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace MyMvcSummarize.Models
{
    public class PrintDocument_new : PrintDocument
    {
        public object data { get; set; }

    }
}