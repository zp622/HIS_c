using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIS_c.Models;
using HIS_c.Service;
using Newtonsoft.Json.Linq;

namespace HIS_c.Controllers
{
    public class MemberController : ApiController
    {
        private MemberService memberService = new MemberService();

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<Member>> addMember([FromBody]Member member)
        {
            return memberService.addMenber(member);
        }

        /// <summary>
        /// 获取所有员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<Member>> getAll([FromBody]JObject obj)
        {
            Member member = obj["member"].ToObject<Member>();
            int currentPage = obj["currentPage"].ToObject<Int32>();
            int pageSize = obj["pageSize"].ToObject<Int32>();
            return memberService.getAll(member,currentPage,pageSize);
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<Member>> updMember([FromBody]Member member)
        {
            return memberService.updMember(member);
        }
    }
}