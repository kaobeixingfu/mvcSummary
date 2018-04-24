using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace BaiduAvi.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [Inject]
        private Standard.IBLL.FunctoinIBLL.IUsers Services { get; set; }

        public ActionResult Index()
        {
            Services.GetDataSource();
            return View();
        }

    }
}
