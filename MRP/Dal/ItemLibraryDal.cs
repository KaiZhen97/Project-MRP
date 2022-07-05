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

        //public List<V_ItemLibraryList> getSupplierList()
        //{
        //    try
        //    {
        //        var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedDate == null).ToList();
        //        //var data = dbContext.V_ItemLibraryList.Where(c => c.Purchaser1 != null || c.Purchaser2 != null).ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}


        public List<V_ItemLibraryList> getSupplierNameList()
        {
            try
            {
                //var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedByStaffName == null).ToList();
                var data = dbContext.V_ItemLibraryList.Where(c => c.SupplierName != null).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<V_ItemLibraryList> getPurchaserNameList()
        {
            try
            {
                //var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedByStaffName == null).ToList();
                var data = dbContext.V_ItemLibraryList.Where(c => c.Purchaser1 != null || c.Purchaser2 != null).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<ItemLibrarySupplier> getSupplierList()
        {
            try
            {
                //var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedByStaffName == null).ToList();
                //var data = dbContext.V_ItemLibraryList.Where(c => c.Purchaser1 != null || c.Purchaser2 != null).ToList();

                var data = dbContext.ItemLibrarySuppliers.ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        //public List<V_ItemLibraryList> getNameList()
        //{
        //    try
        //    {
        //        var data = dbContext.V_ItemLibraryList.Where(c => c.DeletedByStaffName == null).ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}


        //public List<V_CategoryList> getSelectList()
        //{
        //    try
        //    {
        //        var data = dbContext.V_CategoryList.ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}

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

                Guid appKey = Guid.NewGuid();

                AppItemLibrary newAppItemLibrary = new AppItemLibrary();

                newAppItemLibrary.AppKey = appKey;
                newAppItemLibrary.RequestDate = DateTime.Now;

                ItemLibrary itemLibrary = new ItemLibrary();

                itemLibrary.CategoryID = input.CategoryID;
                itemLibrary.IPN = input.IPN;
                itemLibrary.Manufacturer = input.Manufacturer;
                itemLibrary.MPN = input.MPN;
                itemLibrary.ItemDescription = input.ItemDescription;
                //itemLibrary.SupplierName = input.SupplierName;
                //itemLibrary.SupplierCode = input.SupplierCode;
                //itemLibrary.Currency = input.Currency;
                //itemLibrary.UOM = input.UOM;
                //itemLibrary.UnitPrice = input.UnitPrice;
                //itemLibrary.UnitPriceDiscount = input.UnitPriceDiscount;
                //itemLibrary.MinAmountPerOrder = input.MinAmountPerOrder;
                itemLibrary.RequiredSN = input.RequiredSN;
                itemLibrary.Tariff = input.Tariff;
                itemLibrary.RequiredCalibration = input.RequiredCalibration;
                itemLibrary.MoreDetails = input.MoreDetails;
                //itemLibrary.DeliveryTerm = input.DeliveryTerm;
                //itemLibrary.QuotationDate = input.QuotationDate;
                //itemLibrary.QuotationValidity = input.QuotationValidity;
                //itemLibrary.Std_LeadTime_Days = input.Std_LeadTime_Days;
                itemLibrary.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                itemLibrary.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                itemLibrary.KeyTechSpec = input.KeyTechSpec;
                //itemLibrary.IsDefault = input.IsDefault;
                itemLibrary.IsDraft = 0;
                itemLibrary.Remark = input.Remark;

                itemLibrary.CreatedBy = userGuid;
                itemLibrary.CreatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.LastUpdatedDate = DateTime.Now;
                //itemLibrary.DeletedByStaffName = null;

                dbContext.AppItemLibraries.Add(newAppItemLibrary);
                dbContext.ItemLibraries.Add(itemLibrary);
                dbContext.SaveChanges();

                if (input.PWPItemList != null && input.PWPItemList.Count() > 0)
                {
                    foreach (RequestParameter.inputAddItemLibraryPWP item in input.PWPItemList)
                    {
                        ItemLibraryPWPMapping itemLibraryPWP = new ItemLibraryPWPMapping();

                        itemLibraryPWP.Parent_ItemLibraryID = itemLibrary.ID;
                        itemLibraryPWP.PWP_ItemLibraryID = item.PWPItemLibraryID;
                        itemLibraryPWP.PWP_UnitPriceDiscount = item.PWPUnitPriceDiscount;

                        dbContext.ItemLibraryPWPMappings.Add(itemLibraryPWP);
                    }

                    dbContext.SaveChanges();
                }

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

                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);
                
                itemLibrary.CategoryID = input.CategoryID;
                itemLibrary.Manufacturer = input.Manufacturer;
                itemLibrary.MPN = input.MPN;
                itemLibrary.ItemDescription = input.ItemDescription;
                //itemLibrary.SupplierName = input.SupplierName;
                //itemLibrary.SupplierCode = input.SupplierCode;
                //itemLibrary.Currency = input.Currency;
                //itemLibrary.UOM = input.UOM;
                //itemLibrary.UnitPrice = input.UnitPrice;
                //itemLibrary.UnitPriceDiscount = input.UnitPriceDiscount;
                //itemLibrary.MinAmountPerOrder = input.MinAmountPerOrder;
                itemLibrary.RequiredSN = input.RequiredSN;
                itemLibrary.Tariff = input.Tariff;
                itemLibrary.RequiredCalibration = input.RequiredCalibration;
                itemLibrary.MoreDetails = input.MoreDetails;
                //itemLibrary.DeliveryTerm = input.DeliveryTerm;
                //itemLibrary.QuotationDate = input.QuotationDate;
                //itemLibrary.QuotationValidity = input.QuotationValidity;
                //itemLibrary.Std_LeadTime_Days = input.Std_LeadTime_Days;
                itemLibrary.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                itemLibrary.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                itemLibrary.KeyTechSpec = input.KeyTechSpec;
                //itemLibrary.IsDefault = input.IsDefault;
                itemLibrary.IsDraft = 0;
                itemLibrary.IsDraft = input.IsDraft;
                itemLibrary.Remark = input.Remark;

                //itemLibrary.CreatedBy = userGuid;
                //itemLibrary.CreatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.LastUpdatedDate = DateTime.Now;
                //itemLibrary.DeletedByStaffName = null;

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

        public bool postDeleteItemLibrary(RequestParameter.inputDeleteItemLibrary input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                if (itemLibrary == null)
                    return false;

                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

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

        public bool postAddDraftItem(RequestParameter.inputAddDraftItem input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                Guid appKey = Guid.NewGuid();

                AppItemLibrary newAppItemLibrary = new AppItemLibrary();

                newAppItemLibrary.AppKey = appKey;
                newAppItemLibrary.RequestDate = DateTime.Now;

                ItemLibrary itemLibrary = new ItemLibrary();

                itemLibrary.CategoryID = input.CategoryID;
                itemLibrary.IPN = input.IPN;
                itemLibrary.Manufacturer = input.Manufacturer;
                itemLibrary.MPN = input.MPN;
                itemLibrary.ItemDescription = input.ItemDescription;
                //itemLibrary.SupplierName = input.SupplierName;
                //itemLibrary.SupplierCode = input.SupplierCode;
                //itemLibrary.Currency = input.Currency;
                //itemLibrary.UOM = input.UOM;
                //itemLibrary.UnitPrice = input.UnitPrice;
                //itemLibrary.UnitPriceDiscount = input.UnitPriceDiscount;
                //itemLibrary.MinAmountPerOrder = input.MinAmountPerOrder;
                itemLibrary.RequiredSN = input.RequiredSN;
                itemLibrary.Tariff = input.Tariff;
                itemLibrary.RequiredCalibration = input.RequiredCalibration;
                itemLibrary.MoreDetails = input.MoreDetails;
                //itemLibrary.DeliveryTerm = input.DeliveryTerm;
                //itemLibrary.QuotationDate = input.QuotationDate;
                //itemLibrary.QuotationValidity = input.QuotationValidity;
                //itemLibrary.Std_LeadTime_Days = input.Std_LeadTime_Days;
                itemLibrary.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                itemLibrary.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                itemLibrary.KeyTechSpec = input.KeyTechSpec;
                //itemLibrary.IsDefault = input.IsDefault;
                itemLibrary.IsDraft = 1;
                itemLibrary.Remark = input.Remark;

                itemLibrary.CreatedBy = userGuid;
                itemLibrary.CreatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.LastUpdatedDate = DateTime.Now;
                //itemLibrary.DeletedByStaffName = null;

                dbContext.AppItemLibraries.Add(newAppItemLibrary);
                dbContext.ItemLibraries.Add(itemLibrary);
                dbContext.SaveChanges();

                if (input.PWPItemList != null && input.PWPItemList.Count() > 0)
                {
                    foreach (RequestParameter.inputAddItemLibraryPWP item in input.PWPItemList)
                    {
                        ItemLibraryPWPMapping itemLibraryPWP = new ItemLibraryPWPMapping();

                        itemLibraryPWP.Parent_ItemLibraryID = itemLibrary.ID;
                        itemLibraryPWP.PWP_ItemLibraryID = item.PWPItemLibraryID;
                        itemLibraryPWP.PWP_UnitPriceDiscount = item.PWPUnitPriceDiscount;

                        dbContext.ItemLibraryPWPMappings.Add(itemLibraryPWP);
                    }

                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postSaveDraftItem(RequestParameter.inputSaveDraftItem input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

                itemLibrary.CategoryID = input.CategoryID;
                itemLibrary.Manufacturer = input.Manufacturer;
                itemLibrary.MPN = input.MPN;
                itemLibrary.ItemDescription = input.ItemDescription;
                //itemLibrary.SupplierName = input.SupplierName;
                //itemLibrary.SupplierCode = input.SupplierCode;
                //itemLibrary.Currency = input.Currency;
                //itemLibrary.UOM = input.UOM;
                //itemLibrary.UnitPrice = input.UnitPrice;
                //itemLibrary.UnitPriceDiscount = input.UnitPriceDiscount;
                //itemLibrary.MinAmountPerOrder = input.MinAmountPerOrder;
                itemLibrary.RequiredSN = input.RequiredSN;
                itemLibrary.Tariff = input.Tariff;
                itemLibrary.RequiredCalibration = input.RequiredCalibration;
                itemLibrary.MoreDetails = input.MoreDetails;
                //itemLibrary.DeliveryTerm = input.DeliveryTerm;
                //itemLibrary.QuotationDate = input.QuotationDate;
                //itemLibrary.QuotationValidity = input.QuotationValidity;
                //itemLibrary.Std_LeadTime_Days = input.Std_LeadTime_Days;
                itemLibrary.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                itemLibrary.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                itemLibrary.KeyTechSpec = input.KeyTechSpec;
                //itemLibrary.IsDefault = input.IsDefault;
                itemLibrary.IsDraft = 1;
                itemLibrary.Remark = input.Remark;

                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.LastUpdatedDate = DateTime.Now;

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

        public bool postDeleteDraftItem(RequestParameter.inputDeleteDraftItem input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                ItemLibrary itemLibrary = dbContext.ItemLibraries.Where(c => c.ID == input.ID).FirstOrDefault();

                var targetPWP = dbContext.ItemLibraryPWPMappings.Where(c => c.Parent_ItemLibraryID == input.ID);
                dbContext.ItemLibraryPWPMappings.RemoveRange(targetPWP);

                itemLibrary.LastUpdatedDate = DateTime.Now;
                itemLibrary.LastUpdatedBy = userGuid;
                itemLibrary.DeletedDate = DateTime.Now;
                itemLibrary.DeletedBy = userGuid;

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

                Category category = dbContext.Categories.Where(c => c.ID == input.ID).FirstOrDefault();

                if (category == null)
                    return false;

                category.CategoryName = input.CategoryName;
                category.Description = input.Description;
                category.LastUpdatedDate = DateTime.Now;
                category.LastUpdatedBy = userGuid;

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

                Category category = dbContext.Categories.Where(c => c.ID == input.ID).FirstOrDefault();

                if (category == null)
                    return false;

                category.LastUpdatedDate = DateTime.Now;
                category.LastUpdatedBy = userGuid;
                category.DeletedDate = DateTime.Now;
                category.DeletedBy = userGuid;
                category.DeletedRemark = input.DeletedRemark;

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