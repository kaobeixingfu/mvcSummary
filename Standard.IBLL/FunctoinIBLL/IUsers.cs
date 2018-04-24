using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.Model;

namespace Standard.IBLL.FunctoinIBLL
{
    public interface IUsers
    {

        UsersViewModel GetDataSource();

        void Insert(Users obj);

        void Update(Users obj);

        void Delete(string paperID);

        Users SelectByID(string paperID);

        List<Users> SelectList();

        List<Users>  SelectListByDeptID(int deptID);

        void InsertProc();
    }
}
