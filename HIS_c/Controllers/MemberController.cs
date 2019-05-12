using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIS_c.Models;
using HIS_c.Service;

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
        public ApiResult<List<Member>> getAll()
        {
            return memberService.getAll();
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