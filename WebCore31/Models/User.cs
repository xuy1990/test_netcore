using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore31.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

        /// <summary>
        /// 用户实体类
        /// </summary>
        public class User
        {
            /// <summary>
            /// 用户主键Id
            /// </summary>
            /// <example>1</example>
            public int id { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            /// <example>fanqi</example>
            public string username { get; set; }
            /// <summary>
            /// 用户密码
            /// </summary>
            /// <example>admin</example>
            public string password { get; set; }
            /// <summary>
            /// 用户年龄
            /// </summary>
            /// <example>25</example>
            public int? age { get; set; }
            /// <summary>
            /// 用户邮箱
            /// </summary>
            /// <example>fanqisoft@163.com</example>
            public string email { get; set; }
        }
}
