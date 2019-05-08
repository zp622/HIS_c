using HIS_c.Models;
using HIS_c.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIS_c.Controllers
{
    public class UserController : ApiController
    {
        private UserService userService = new UserService();
        // Post api/User/
        [EnableCors(origins: "http://localhost:8080", headers: "*",
    methods: "*", SupportsCredentials = true)]
        [HttpPost]
        public ApiResult<List<UserModel>> userLogin([FromBody]UserModel userInfo)
        {
            return userService.userInfoGet(userInfo);
        }

        [HttpPost]
        public ApiResult<List<String>> login([FromBody]string userInfo)
        {
            return null;
        }

    }
}