using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using MRP.BusinessLogic;
using MRP.Dal;
using MRP.Models;
using MRP.Database;
using WebFrameWorkLib.Database;
namespace MRP.Controllers
{
    public class RFQController : ApiController
    {
        private RFQBL RFQBL = new RFQBL();
        private RFQDal RFQDal = new RFQDal();

        #region RFQList
        //[HttpGet]
        //public HttpResponseMessage getActiveRFQList()
        //{
        //    DataTableRFQ data = new DataTableRFQ();

        //    List<V_RFQList> dataList = RFQDal.getActiveRFQList(Request);

        //    data.data = dataList;
        //    data.draw = 1;
        //    if (dataList == null)
        //    {
        //        data.recordsFiltered = 0;
        //        data.recordsTotal = 0;
        //    }
        //    else
        //    {
        //        data.recordsFiltered = dataList.Count;
        //        data.recordsTotal = dataList.Count;
        //    }

        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response = Request.CreateResponse(HttpStatusCode.OK, data);
        //    return response;
        //}

        [HttpGet]
        public HttpResponseMessage getDraftRFQList()
        {
            DataTableRFQ data = new DataTableRFQ();

            List<V_RFQList> dataList = RFQDal.getDraftRFQList(Request);

            data.data = dataList;
            data.draw = 1;
            if (dataList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = dataList.Count;
                data.recordsTotal = dataList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postDeleteRFQ([FromBody]RequestParameter.inputDeleteRFQ input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postDeleteRFQ(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postDeleteRFQDraft([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postDeleteRFQDraft(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postClosedRFQByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postClosedRFQByID(input, ModelState, Request);
            return response;
        }

        #endregion


        #region AssignPurchaser
        [HttpGet]
        public HttpResponseMessage getPendingAssigmentList(HttpRequestMessage Request)
        {
            DataTableRFQ data = new DataTableRFQ();

            List<V_RFQList> dataList = RFQDal.getPendingAssignmentList(Request);

            data.data = dataList;
            data.draw = 1;
            if (dataList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = dataList.Count;
                data.recordsTotal = dataList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postSubmitAssignmentRFQ([FromBody]RequestParameter.inputSubmitPendingRFQList input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postSubmitAssignmentRFQ(input, Request);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage getAssignedPurchaser()
        {
            DataTableRFQ data = new DataTableRFQ();

            List<V_RFQList> dataList = RFQDal.getAssignedPurchaser(Request);

            data.data = dataList;
            data.draw = 1;
            if (dataList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = dataList.Count;
                data.recordsTotal = dataList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }
        #endregion


        #region RaiseRFQ
        [HttpPost]
        public HttpResponseMessage postSubmitDraftRFQ([FromBody]RequestParameter.inputEditRFQ input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postSubmitDraftRFQ(input, ModelState, Request);
            return response;
        }


        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postAddRFQDraftWithUpload()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = RFQBL.postAddRFQDraftWithUpload(provider, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postAddRFQWithUpload()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = RFQBL.postAddRFQWithUpload(provider, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postEditRFQDraftWithUpload()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = RFQBL.postEditRFQDraftWithUpload(provider, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postSubmitDraftRFQWithUpload()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = RFQBL.postSubmitDraftRFQWithUpload(provider, Request);

            return response;
        }
        #endregion


        #region RFQDetails
        [HttpPost]
        public HttpResponseMessage postUpdateHistoryTrace([FromBody]RequestParameter.inputID input)
        {
            DataTableUpdateTrace data = new DataTableUpdateTrace();

            List<V_UpdateHistoryTrace> dataList = RFQDal.postUpdateHistoryTrace(input);
            data.data = dataList;
            data.draw = 1;

            if (dataList == null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = dataList.Count;
                data.recordsTotal = dataList.Count;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> postEditRFQWithUpload()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            response = RFQBL.postEditRFQWithUpload(provider, Request);

            return response;
        }

        [HttpPost]
        public HttpResponseMessage postAddWatcher([FromBody]RequestParameter.inputAddWatcher input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postAddWatcher(input, ModelState, Request);
            return response;
        }
        #endregion

        #region RFQOnHand
        [HttpGet]
        public HttpResponseMessage getRFQOnHandList()
        {
            DataTableRFQ data = new DataTableRFQ();

            List<V_RFQList> dataList = RFQDal.getRFQOnHandList(Request);
            data.data = dataList;
            data.draw = 1;
            if (dataList != null)
            {
                data.recordsFiltered = 0;
                data.recordsTotal = 0;
            }
            else
            {
                data.recordsFiltered = dataList.Count;
                data.recordsTotal = dataList.Count;
            }
            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }


        #endregion


        [HttpPost]
        public HttpResponseMessage postRFQByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postRFQByID(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postWatcherEmailByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = RFQBL.postWatcherEmailByID(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postAttachmentByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            //response = RFQBL.postAttachmentByID(input, ModelState, Request);
            return response;

        }
    }
}
