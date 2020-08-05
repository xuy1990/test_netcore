using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore31.Models;

namespace WebCore31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <remarks>
        /// Demo
        /// Get /api/user/get
        /// </remarks>
        /// <returns>系统用户列表</returns>
        /// <response code="201">返回用户列表</response>
        /// <response code="401">没有权限</response>
        [HttpGet("get")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public IList<User> GetUsers()
        {
            return new List<User>
            {
                new User{ id = 1,username = "fanqi",password = "admin",age = 25,email = "fanqisoft@163.com"},
                new User{ id = 2,username = "gaoxing",password = "admin",age = 22,email = "gaoxing@163.com"}
            };
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="user">新建用户信息</param>
        /// <returns>是否创建成功信息</returns>
        [HttpPost("add")]
        public IActionResult CreateUser([FromForm] User user)
        {
            return Ok();
        }
    }
}
