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

        public ApiResult<List<BookingForm>> getBookingForm(BookingForm form, int currentPage, int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = bookingFromDao.getBookingForms(form, currentPage, pageSize);
            apiResult.total = bookingFromDao.getBookingForms(form, 1, 1000000).Count;
            return apiResult;
        }

        public ApiResult<List<BookingForm>> addBookingForm(BookingForm bookingForm)
        {
            //Patient patient = new Patient();
            //patient.patientNo = bookingForm.patientNo;
            //patient = patientDao.getAll(patient, 1, 1)[0];
            //bookingForm.address = patient.address;
            //string time = bookingForm.registerTime;
            //bookingForm.registerTime = time.Substring(0, 10);
            //bookingForm.waitingNo = (bookingFromDao.getCount(bookingForm) + 1).ToString();
            //bookingForm.registerTime = time;

            //根据科室名称 去查询科室位置
            Department dept = new Department();
            dept.deptName = bookingForm.registerDept;
            DepartmentDao deptDao = new DepartmentDao();
            List<Department> deptList = deptDao.getDept(dept, 1, 100000);
            bookingForm.registerDept = dept.deptName;
            bookingForm.address = deptList[0].address;
            //计算当前是第几个挂号的
            bookingForm.waitingNo = (bookingFromDao.getHasCount(bookingForm) + 1).ToString();
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

        /// <summary>
        /// 返回查询挂号余额的结果  拼接提示字符串
        /// </summary>
        /// <param name="bookingForm"></param>
        /// <returns></returns>
        public ApiResult<int> queryBookFormCount(BookingForm bookingForm)
        {
            ApiResult<int> apiResult = new ApiResult<int>();
            string dept = bookingForm.registerDept;
            string type = bookingForm.registerType;
            string date = bookingForm.registerTime;
            string doctor = bookingForm.doctor;
            int result = bookingFromDao.getHasCount(bookingForm);
            if (result < 20)
            {
                int count = 20 - result;
                apiResult.code = 200;
                apiResult.message = dept + ":‘" + date + "’的" + doctor + type + " 挂号剩余 " + count + " 个";
                apiResult.data = result;
            }
            else
            {
                apiResult.code = 0;
                apiResult.message = dept + ":‘" + date + "’的" + doctor + type + " 挂号已满";
                apiResult.data = result;
            }
            return apiResult;
        }

        public ApiResult<Int32> updBookingForm(BookingForm bookingForm)
        {
            ApiResult<Int32> apiResult = new ApiResult<Int32>();
            int i = bookingFromDao.updBookingForm(bookingForm);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "修改成功";
                apiResult.data = 1;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "修改失败";
                apiResult.data = 0;
            }
            return apiResult;
        }

        public ApiResult<int> ttsFun(string msg)
        {
            ApiResult<int> apiResult = new ApiResult<Int32>();
            int i = bookingFromDao.ttsFun(msg);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "呼叫成功";
                apiResult.data = 1;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "呼叫失败";
                apiResult.data = 0;
            }
            return apiResult;
        }
    }
}