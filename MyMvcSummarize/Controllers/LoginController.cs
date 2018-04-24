using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvcSummarize.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        /// <summary>
        /// 登录首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
