//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Standard.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersApp
    {
        public System.Guid ID { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid ParentID { get; set; }
        public System.Guid RoleID { get; set; }
        public Nullable<System.Guid> AppID { get; set; }
        public string Title { get; set; }
        public string Params { get; set; }
        public string Ico { get; set; }
        public int Sort { get; set; }
    }
}
