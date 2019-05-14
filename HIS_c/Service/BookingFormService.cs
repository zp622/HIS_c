using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Dao;
using HIS_c.Models;

namespace HIS_c.Service
{
    public class BookingFormService
    {
        private BookingFromDao bookingFromDao = new BookingFromDao();

        private PatientDao patientDao = new PatientDao();

        private ApiResult<List<BookingForm>> apiResult = new ApiResult<List<BookingForm>>();

        public ApiResult<List<BookingForm>> getBookingForm(BookingForm form,int currentPage,int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = bookingFromDao.getBookingForms(form, currentPage, pageSize);
            apiResult.total = bookingFromDao.getBookingForms(form, 1, 1000000).Count;
            return apiResult;
        }

        public ApiResult<List<BookingForm>> addBookingForm(BookingForm bookingForm)
        {
            Patient patient = new Patient();
            patient.patientNo = bookingForm.patientNo;
            patient = patientDao.getAll(patient, 1, 1)[0];
            bookingForm.address = patient.address;
            string time = bookingForm.registerTime;
            bookingForm.registerTime = time.Substring(0, 10);
            bookingForm.waitingNo = (bookingFromDao.getCount(bookingForm)+1).ToString();
            bookingForm.registerTime = time;
            int i = bookingFromDao.addBookingForm(bookingForm);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "挂号成功";
                apiResult.data = bookingFromDao.getBookingForms(bookingForm, 1, 10);
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "挂号失败";
                apiResult.data = null;
            }
            return apiResult;
        }
    }
}