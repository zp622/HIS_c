using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Dao;
using HIS_c.Models;

namespace HIS_c.Service
{
    public class MemberService
    {
        private MemberDao memberDao = new MemberDao();

        private ApiResult<List<Member>> apiResult = new ApiResult<List<Member>>();

        public ApiResult<List<Member>> addMenber(Member member)
        {
            int i = memberDao.addMember(member);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = memberDao.getAll(null,1,10);
                apiResult.total = memberDao.getAll(null, 1, 1000000).Count;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
                apiResult.data = null;
            }
            return apiResult;
        }

        public ApiResult<List<Member>> getAll(Member member, int currentPage, int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = memberDao.getAll(member,currentPage,pageSize);
            apiResult.total = memberDao.getAll(member, 1, 1000000).Count;
            return apiResult;
        }


        public ApiResult<List<Member>> updMember(Member member)
        {
            int i = memberDao.updMember(member);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "修改成功";
                apiResult.data = memberDao.getAll(null,1,10);
                apiResult.total = memberDao.getAll(null, 1, 1000000).Count;
            }
            else if (i == -1)
            {
                apiResult.code = 198;
                apiResult.message = "职工号必传";
                apiResult.data = null;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "修改失败";
                apiResult.data = null;
            }
            return apiResult;
        }
    }

}