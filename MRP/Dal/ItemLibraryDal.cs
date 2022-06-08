using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.BusinessLogic;
using MRP.Database;
using MRP.Models;
using System.Net.Http;

namespace MRP.Dal
{
    public class ItemLibraryDal
    {
        private MRPEntities dbContext = new MRPEntities();
        private LogError logError = new LogError();
        private Common common = new Common();
        private AuditBL auditBL = new AuditBL();

        #region ItemLibrary
        public List<V_ItemLibraryList> getActiveItemLibraryList()
        {
            try
            {
                var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedDate == null && c.IsDraft == 0).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<V_ItemLibraryList> getDraftItemLibraryList()
        {
            try
            {
                var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedDate == null && c.IsDraft == 1).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
            //{
            //    logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            //        System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            //    return null;
            //}
        }

        public V_ItemLibraryList postItemLibraryByID(RequestParameter.inputID input)
        {
            try
            {
                V_ItemLibraryList data = dbContext.V_ItemLibraryList.Where(c => c.ID == input.ID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddItemLibrary(RequestParameter.inputAddItemLibrary input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                // Create app key for IPN Counter purpose
                Guid appKey = Guid.NewGuid();

                AppItemLibrary newAppItemLibrary = new AppItemLibrary();

                newAppItemLibrary.AppKey = appKey;
                newAppItemLibrary.RequestDate = DateTime.Now;

                ItemLibrary newItemLibrary = new ItemLibrary();

                newItemLibrary.CategoryID = input.CategoryID;
                newItemLibrary.IPN = input.IPN;
                newItemLibrary.Manufacturer = input.Manufacturer;
                newItemLibrary.MPN = input.MPN;
                newItemLibrary.ItemDescription = input.ItemDescription;
                newItemLibrary.SupplierName = input.SupplierName;
                newItemLibrary.SupplierCode = input.SupplierCode;
                newItemLibrary.Currency = input.Currency;
                newItemLibrary.UOM = input.UOM;
                newItemLibrary.UnitPrice = input.UnitPrice;
                newItemLibrary.UnitPriceDiscount = input.UnitPriceDiscount;
                newItemLibrary.MinAmountPerOrder = input.MinAmountPerOrder;
                newItemLibrary.RequiredSN = input.RequiredSN;
                newItemLibrary.Tariff = input.Tariff;
                newItemLibrary.RequiredCalibration = input.RequiredCalibration;
                newItemLibrary.MoreDetails = input.MoreDetails;
                newItemLibrary.DeliveryTerm = input.DeliveryTerm;
                newItemLibrary.QuotationDate = input.QuotationDate;
                newItemLibrary.QuotationValidity = input.QuotationValidity;
                newItemLibrary.Std_LeadTime_Days = input.Std_LeadTime_Days;
                newItemLibrary.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newItemLibrary.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                //newItemLibrary.Purchaser1 = input.Purchaser1;
                //newItemLibrary.Purchaser2 = input.Purchaser2;
                newItemLibrary.KeyTechSpec = input.KeyTechSpec;
                newItemLibrary.IsDefault = input.IsDefault;
                newItemLibrary.IsDraft = input.IsDraft;
                newItemLibrary.Remark = input.Remark;

                newItemLibrary.CreatedBy = userGuid;
                newItemLibrary.CreatedDate = DateTime.Now;
                newItemLibrary.LastUpdatedBy = userGuid;
                newItemLibrary.LastUpdatedDate = DateTime.Now;

                dbContext.AppItemLibraries.Add(newAppItemLibrary);
                dbContext.ItemLibraries.Add(newItemLibrary);
                dbContext.SaveChanges();

                ////Add PWP Mapping
                //if (input.PWPItemList != null && input.PWPItemList.Count() > 0)
                //{
                //    foreach (RequestParameter.inputAddItemLibraryPWP item in input.PWPItemList)
                //    {
                //        ItemLibraryPWPMapping itemLibraryPWP = new ItemLibraryPWPMapping();

                //        itemLibraryPWP.Parent_ItemLibraryID = newItemLibrary.ID;
                //        itemLibraryPWP.PWP_ItemLibraryID = item.PWPItemLibraryID;
                //        itemLibraryPWP.PWP_UnitPriceDiscount = item.PWPUnitPriceDiscount;

                //        dbContext.ItemLibraryPWPMappings.Add(itemLibraryPWP);
                //    }

                //    dbContext.SaveChanges();
                //}

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postEditItemLibrary(RequestParameter.inputEditItemLibrary input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                if (itemLibrary == null)
                    return false;

                // Clear PWP Mapping
                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

                // Edit Item Library
                var targetItemLib = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                targetItemLib.CategoryID = input.CategoryID;
                targetItemLib.Manufacturer = input.Manufacturer;
                targetItemLib.MPN = input.MPN;
                targetItemLib.ItemDescription = input.ItemDescription;
                targetItemLib.SupplierName = input.SupplierName;
                targetItemLib.SupplierCode = input.SupplierCode;
                targetItemLib.Currency = input.Currency;
                targetItemLib.UOM = input.UOM;
                targetItemLib.UnitPrice = input.UnitPrice;
                targetItemLib.UnitPriceDiscount = input.UnitPriceDiscount;
                targetItemLib.MinAmountPerOrder = input.MinAmountPerOrder;
                targetItemLib.RequiredSN = input.RequiredSN;
                targetItemLib.Tariff = input.Tariff;
                targetItemLib.RequiredCalibration = input.RequiredCalibration;
                targetItemLib.MoreDetails = input.MoreDetails;
                targetItemLib.DeliveryTerm = input.DeliveryTerm;
                targetItemLib.QuotationDate = input.QuotationDate;
                targetItemLib.QuotationValidity = input.QuotationValidity;
                targetItemLib.Std_LeadTime_Days = input.Std_LeadTime_Days;
                targetItemLib.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                targetItemLib.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                targetItemLib.KeyTechSpec = input.KeyTechSpec;
                targetItemLib.IsDefault = input.IsDefault;
                targetItemLib.IsDraft = input.IsDraft;
                targetItemLib.Remark = input.Remark;
                targetItemLib.LastUpdatedBy = userGuid;
                targetItemLib.LastUpdatedDate = DateTime.Now;

                //Add PWP Mapping
                if (input.PWPItemList != null && input.PWPItemList.Count() > 0)
                {
                    foreach (RequestParameter.inputAddItemLibraryPWP item in input.PWPItemList)
                    {
                        ItemLibraryPWPMapping itemLibraryPWP = new ItemLibraryPWPMapping();

                        itemLibraryPWP.Parent_ItemLibraryID = input.ID;
                        itemLibraryPWP.PWP_ItemLibraryID = item.PWPItemLibraryID;
                        itemLibraryPWP.PWP_UnitPriceDiscount = item.PWPUnitPriceDiscount;

                        dbContext.ItemLibraryPWPMappings.Add(itemLibraryPWP);
                    }
                }

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        //public bool postDraftItemLibrary(RequestParameter.inputDraftItemLibrary input, HttpRequestMessage request)
        //{
        //    try
        //    {
        //        Guid userGuid = common.extractUserID(request);

        //        ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

        //        if (itemLibrary == null)
        //            return false;

        //        // Clear PWP Mapping
        //        //var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
        //        //dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

        //        // Edit Item Library
        //        var targetItemLib = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

        //        targetItemLib.CategoryID = input.CategoryID;
        //        targetItemLib.Manufacturer = input.Manufacturer;
        //        targetItemLib.MPN = input.MPN;
        //        targetItemLib.ItemDescription = input.ItemDescription;
        //        targetItemLib.SupplierName = input.SupplierName;
        //        targetItemLib.SupplierCode = input.SupplierCode;
        //        targetItemLib.Currency = input.Currency;
        //        targetItemLib.UOM = input.UOM;
        //        targetItemLib.UnitPrice = input.UnitPrice;
        //        targetItemLib.UnitPriceDiscount = input.UnitPriceDiscount;
        //        targetItemLib.MinAmountPerOrder = input.MinAmountPerOrder;
        //        targetItemLib.RequiredSN = input.RequiredSN;
        //        targetItemLib.Tariff = input.Tariff;
        //        targetItemLib.RequiredCalibration = input.RequiredCalibration;
        //        targetItemLib.MoreDetails = input.MoreDetails;
        //        targetItemLib.DeliveryTerm = input.DeliveryTerm;
        //        targetItemLib.QuotationDate = input.QuotationDate;
        //        targetItemLib.QuotationValidity = input.QuotationValidity;
        //        targetItemLib.Std_LeadTime_Days = input.Std_LeadTime_Days;
        //        targetItemLib.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
        //        targetItemLib.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
        //        targetItemLib.KeyTechSpec = input.KeyTechSpec;
        //        targetItemLib.IsDefault = input.IsDefault;
        //        targetItemLib.IsDraft = input.IsDraft;
        //        targetItemLib.Remark = input.Remark;
        //        targetItemLib.LastUpdatedBy = userGuid;
        //        targetItemLib.LastUpdatedDate = DateTime.Now;


        //        // Add PWP Mapping
        //        //if (input.PWPItemList != null && input.PWPItemList.Count() > 0)
        //        //{
        //        //    foreach (RequestParameter.inputAddItemLibraryPWP item in input.PWPItemList)
        //        //    {
        //        //        ItemLibraryPWPMapping itemLibraryPWP = new ItemLibraryPWPMapping();

        //        //        itemLibraryPWP.Parent_ItemLibraryID = input.ID;
        //        //        itemLibraryPWP.PWP_ItemLibraryID = item.PWPItemLibraryID;
        //        //        itemLibraryPWP.PWP_UnitPriceDiscount = item.PWPUnitPriceDiscount;

        //        //        dbContext.ItemLibraryPWPMappings.Add(itemLibraryPWP);
        //        //    }
        //        //}

        //        dbContext.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return false;
        //    }
        //}

        public bool postDeleteItemLibrary(RequestParameter.inputDeleteItemLibrary input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                if (itemLibrary == null)
                    return false;

                // Clear PWP Mapping
                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

                // Change target Item Library status to deleted
                itemLibrary.LastUpdatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.DeletedDate = DateTime.Now;
                itemLibrary.DeletedBy = userGuid;
                itemLibrary.DeletedRemark = input.DeletedRemark;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteDraftItem(RequestParameter.inputDeleteDraftItem input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                if (itemLibrary == null)
                    return false;

                // Clear PWP Mapping
                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

                // Change target Item Library status to deleted
                itemLibrary.LastUpdatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.DeletedDate = DateTime.Now;
                itemLibrary.DeletedBy = userGuid;
                itemLibrary.DeletedRemark = "Deleted";

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        #endregion


        #region Category
        public List<V_CategoryList> getActiveCategoryList()
        {
            try
            {
                var data = dbContext.V_CategoryList.Where(w => w.DeletedDate == null).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public V_CategoryList getActiveCategoryByID(RequestParameter.inputID input)
        {
            try
            {
                var data = dbContext.V_CategoryList.Where(c => c.ID == input.ID).FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddNewCategory(RequestParameter.inputAddCategory input, HttpRequestMessage request)
        {
            try
            {
                Guid userID = common.extractUserID(request);

                var newData = new Category()
                {
                    CategoryName = input.CategoryName,
                    Description = input.Description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = userID,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = userID,
                };

                dbContext.Categories.Add(newData);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postEditCategory(RequestParameter.inputEditCategory input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                Category targetCategory = dbContext.Categories.Where(c => c.ID == input.ID).FirstOrDefault();

                if (targetCategory == null)
                    return false;

                targetCategory.CategoryName = input.CategoryName;
                targetCategory.Description = input.Description;
                targetCategory.LastUpdatedDate = DateTime.Now;
                targetCategory.LastUpdatedBy = userGuid;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteCategory(RequestParameter.inputDeleteCategory input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                Category itemLibrary = dbContext.Categories.Where(c => c.ID == input.ID).FirstOrDefault();

                if (itemLibrary == null)
                    return false;

                itemLibrary.LastUpdatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.DeletedDate = DateTime.Now;
                itemLibrary.DeletedBy = userGuid;
                itemLibrary.DeletedRemark = input.DeletedRemark;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        #endregion
    }
}