using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MRP.BusinessLogic;
using MRP.Dal;
using MRP.Models;
using MRP.Database;

namespace MRP.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class ItemLibraryController : ApiController
    {
        private ItemLibraryBL itemLibraryBL = new ItemLibraryBL();
        private ItemLibraryDal itemLibraryDal = new ItemLibraryDal();

        #region ItemLibrary
        [HttpGet]
        public HttpResponseMessage getActiveItemLibraryList()
        {
            DataTableItemLibrary data = new DataTableItemLibrary();

            List<V_ItemLibraryList> dataList = itemLibraryDal.getActiveItemLibraryList();

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
        public HttpResponseMessage postItemLibraryByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postItemLibraryByID(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postAddItemLibrary([FromBody]RequestParameter.inputAddItemLibrary input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postAddItemLibrary(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postEditItemLibrary([FromBody]RequestParameter.inputEditItemLibrary input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postEditItemLibrary(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postDeleteItemLibrary([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postDeleteItemLibrary(input, ModelState, Request);
            return response;
        }
        #endregion

        #region Category
        [HttpGet]
        public HttpResponseMessage getActiveCategoryList()
        {
            DataTableILMCategory data = new DataTableILMCategory();

            List<V_CategoryList> dataList = itemLibraryDal.getActiveCategoryList();

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
        public HttpResponseMessage getActiveCategoryByID([FromBody]RequestParameter.inputID input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.getActiveCategoryByID(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postAddNewCategory([FromBody]RequestParameter.inputAddCategory input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postAddNewCategory(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postEditCategory([FromBody]RequestParameter.inputEditCategory input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postEditCategory(input, ModelState, Request);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postDeleteCategory([FromBody]RequestParameter.inputDeleteCategory input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = itemLibraryBL.postDeleteCategory(input, ModelState, Request);
            return response;
        }
        #endregion
    }
}
