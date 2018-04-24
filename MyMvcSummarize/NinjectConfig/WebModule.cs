using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Standard.IBLL.FunctoinIBLL;
using Standard.BLL;
using Standard.IBLL;
using Standard.IDAL;
using Standard.DAL;

namespace MyMvcSummarize.NinjectConfig
{
    public class WebModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            //Bind<DbContext>().To<Standard.DAL.RoadFlowWebFormEntities>();
            //Bind<IRepositoryBase>().To<DbRepository>();
            //Bind<IUsers>().To<BLL_Users>();
        }
    }
}