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
    
    public partial class AppLibrary
    {
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public System.Guid Type { get; set; }
        public int OpenMode { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string Params { get; set; }
        public string Manager { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
        public string UseMember { get; set; }
    }
}