using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cyc.Order.Data
{
    public enum AccountState : byte
    {
        /// <summary>
        /// 未认证
        /// </summary>
        [Description("未认证")]
        Uncertified = 0,

        /// <summary>
        /// 已认证
        /// </summary>
        [Description("已认证")]
        Certified = 1
    }
}
