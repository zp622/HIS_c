﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIS_c.Service;
using HIS_c.Models;
using Newtonsoft.Json.Linq;

namespace HIS_c.Controllers
{
    public class BookingFormController : ApiController
    {
        private BookingFormService bookingFormService = new BookingFormService();

        /// <summary>
        /// 获取挂号单信息，带分页
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<BookingForm>> getBookingForm([FromBody]JObject obj)
        {
            BookingForm form = obj["bookingForm"].ToObject<BookingForm>();
            int currentPage = obj["currentPage"].ToObject<Int32>();
            int pageSize = obj["pageSize"].ToObject<Int32>();
            return bookingFormService.getBookingForm(form,currentPage,pageSize);
        }

        /// <summary>
        /// 添加挂号单信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<BookingForm>> addBookForm([FromBody]BookingForm form)
        {
            return bookingFormService.addBookingForm(form);
        }
    }
}