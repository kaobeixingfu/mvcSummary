using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Model
{
    public class SaveEntityModel
    {
        public EntityState State { get; set; }

        public object Entity { get; set; }

        public string UpdateFields { get; set; }
    }
}
