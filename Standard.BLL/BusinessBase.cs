using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.BLL.NinjectConfig;
using Standard.DAL;
using Standard.IDAL;
using Standard.IBLL;

namespace Standard.BLL
{
    public class BusinessBase
    {

        //private static IKernel kernel = new StandardKernel(new BLLModule());
        //public IRepositoryBase dbRepository = kernel.Get<IRepositoryBase>();
        [Inject]
        protected IRepositoryBase dbRepository { get; set; }

        public T GetEntity<T>(object key) where T : class
        {
            return dbRepository.Find<T>(key);
        }


    }
}
