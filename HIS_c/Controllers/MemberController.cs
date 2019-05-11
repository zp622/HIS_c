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

        [HttpPost]
        public ApiResult<List<Member>> addMember(Member member)
        {
            return memberService.addMenber(member);
        }

        [HttpGet]
        public ApiResult<List<Member>> getAll()
        {
            return memberService.getAll();
        }
    }
}