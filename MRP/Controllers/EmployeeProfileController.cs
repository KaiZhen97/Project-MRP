using MRP.BusinessLogic;
using MRP.Dal;
using MRP.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;

namespace MRP.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EmployeeProfileController : ApiController
    {
        private EmployeeProfileDal EmployeeProfileDal = new EmployeeProfileDal();
        private EmployeeProfileBL EmployeeProfileBL = new EmployeeProfileBL();
        private Common common = new Common();

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEmployeeProfileList([FromBody] RequestParameter.inputFilterEmployeeList input)
        {
            DataTableEmployeeProfile data = new DataTableEmployeeProfile();

            List<V_EmployeeProfileList> EmployeeProfileList = EmployeeProfileDal.postEmployeeProfileList(input, Request);

            data.data = EmployeeProfileList;
            data.draw = 1;
            if (EmployeeProfileList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = EmployeeProfileList.Count;
                data.recordsTotal = EmployeeProfileList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDependantList([FromBody] RequestParameter.inputID input)
        {
            DataTableDependant data = new DataTableDependant();

            List<Dependant> DependantList = EmployeeProfileDal.postDependantList(input, Request);

            data.data = DependantList;
            data.draw = 1;
            if (DependantList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = DependantList.Count;
                data.recordsTotal = DependantList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEduTrainList([FromBody] RequestParameter.inputID input)
        {
            DataTableEduTrain data = new DataTableEduTrain();

            List<CertAttachment> CertList = EmployeeProfileDal.postEduTrainList(input, Request);

            data.data = CertList;
            data.draw = 1;
            if (CertList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = CertList.Count;
                data.recordsTotal = CertList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postPromotionList([FromBody] RequestParameter.inputID input)
        {
            DataTablePromotion data = new DataTablePromotion();

            List<PromotionHistory> PromotionList = EmployeeProfileDal.postPromotionList(input, Request);

            data.data = PromotionList;
            data.draw = 1;
            if (PromotionList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = PromotionList.Count;
                data.recordsTotal = PromotionList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddEmployeeProfile([FromBody] RequestParameter.inputAddEmployeeProfile input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = EmployeeProfileBL.postAddEmployeeProfile(input, ModelState, Request);

            return response;
        }


        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEmployeeProfileByID([FromBody] RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = EmployeeProfileBL.postEmployeeProfileByID(input, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditEmployeeProfile([FromBody] RequestParameter.inputEditEmployeeProfile input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = EmployeeProfileBL.postEditEmployeeProfile(input, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteEmployeeProfile([FromBody] RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = EmployeeProfileBL.postDeleteEmployeeProfile(input, ModelState, Request);

            return response;
        }

        //retrieve dropdown data
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getActiveEmployeeProfileList()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var activeEmployeeProfile = EmployeeProfileDal.getActiveEmployeeProfile();
            response = Request.CreateResponse(HttpStatusCode.OK, activeEmployeeProfile);
            return response;
        }

        //get project details
        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEmployeeProfileDetails([FromBody] RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var EmployeeProfileDetails = EmployeeProfileDal.postEmployeeProfileList(input);
            response = Request.CreateResponse(HttpStatusCode.OK, EmployeeProfileDetails);
            return response;
        }

        // for employee edit their details
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getUpdateSingleEmployeeProfileList()
        {
            DataTableEmployeeProfile data = new DataTableEmployeeProfile();

            List<V_EmployeeProfileList> SingleEmployeeProfileList = EmployeeProfileDal.getUpdateEmployeeProfileList(Request);

            data.data = SingleEmployeeProfileList;
            data.draw = 1;
            if (SingleEmployeeProfileList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = SingleEmployeeProfileList.Count;
                data.recordsTotal = SingleEmployeeProfileList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        //[HttpPost]
        //[Authorize]
        //public HttpResponseMessage postEditSingleEmployeeProfile([FromBody] RequestParameter.inputEditUpdateEmployeeProfile input)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();

        //    response = EmployeeProfileBL.postEditSingleEmployeeProfile(input, ModelState, Request);

        //    return response;
        //}

        //filter by company
        [HttpPost]
        [Authorize]
        public HttpResponseMessage postCompanyList()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var companyList = EmployeeProfileDal.postCompanyList();
            response = Request.CreateResponse(HttpStatusCode.OK, companyList);
            return response;
        }

        //filter by department
        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDepartmentList()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var departmentList = EmployeeProfileDal.postDepartmentList();
            response = Request.CreateResponse(HttpStatusCode.OK, departmentList);
            return response;
        }

        //profile photo upload
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postPhotoUploadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = EmployeeProfileBL.postPhotoUploadFile(provider, Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getProfileIcon()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var profileIcon = EmployeeProfileDal.getProfileIcon(Request);
            response = Request.CreateResponse(HttpStatusCode.OK, profileIcon);
            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getIndividualList()
        {
            List<V_UserList> dataList = EmployeeProfileDal.getIndividualList(Request);

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, dataList);
            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getBankList()
        {
            List<Bank> dataList = EmployeeProfileDal.getBankList(Request);

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, dataList);
            return response;
        }
    }
}
