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
    
    public partial class Log
    {
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public System.DateTime WriteTime { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string URL { get; set; }
        public string Contents { get; set; }
        public string Others { get; set; }
        public string OldXml { get; set; }
        public string NewXml { get; set; }
    }
}
