using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Standard.DAL;
using Standard.IBLL.FunctoinIBLL;
using Standard.IDAL;

namespace Standard.BLL.NinjectConfig
{
    public class BLLModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<DbContext>().To<Standard.Model.RoadFlowWebFormEntities>(); //如果使用IKernel kernel = new StandardKernel(new BLLModule())的这种方式需要bind 上下文。   [Inject]  protected IRepositoryBase dbRepository { get; set; }方式则可以不需要
            Bind<IRepositoryBase>().To<DbRepository>();
            Bind<IUsers>().To<BLL_Users>();
        }
    }
}
