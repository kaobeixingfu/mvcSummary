using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    /// <summary>
    /// View调用Controller时返回的参数类型
    /// </summary>
    public class JsonResultView
    {
        /// <summary>
        /// 是否成功标识
        /// </summary>
        public bool flag { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }
}
