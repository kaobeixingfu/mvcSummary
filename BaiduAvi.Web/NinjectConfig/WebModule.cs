using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiduAvi.Web.NinjectConfig
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