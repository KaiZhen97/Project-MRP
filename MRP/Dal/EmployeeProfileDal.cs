using MRP.Dal;
using MRP.Models;
using MRP.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;

namespace MRP.Dal
{

    public class EmployeeProfileDal
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        //private EmailDal emailDal = new EmailDal();
        private LogError logError = new LogError();
        private Common common = new Common();
        private AuditBL auditBL = new AuditBL();

        public List<V_EmployeeProfileList> postEmployeeProfileList(RequestParameter.inputFilterEmployeeList input, HttpRequestMessage request)
        {
            try
            {
                List<V_EmployeeProfileList> EmployeeProfileList = new List<V_EmployeeProfileList>();

                EmployeeProfileList = dbContext.V_EmployeeProfileList.Where(w => w.Deleted == 0).OrderBy(c => c.EmployeeName).ToList();

                if (input.CompanyID != 0)
                {
                    EmployeeProfileList = EmployeeProfileList.Where(w => w.CompanyID == input.CompanyID).OrderBy(c => c.EmployeeName).ToList();
                }

                if (input.DepartmentID != 0)
                {
                    EmployeeProfileList = EmployeeProfileList.Where(w => w.DepartmentID == input.DepartmentID).OrderBy(c => c.EmployeeName).ToList();
                }

                //if (input.EmployeeStatus == 0)
                //{
                //    EmployeeProfileList = EmployeeProfileList.Where(w => w.Deleted == 0).OrderBy(c => c.EmployeeName).ToList();
                //}

                if (input.EmployeeStatus == 1)
                {
                    EmployeeProfileList = EmployeeProfileList.Where(w => w.ResignDate == null).OrderBy(c => c.EmployeeName).ToList();
                }

                if (input.EmployeeStatus == 2)
                {
                    EmployeeProfileList = EmployeeProfileList.Where(w => w.ResignDate != null).OrderBy(c => c.EmployeeName).ToList();
                }
                return EmployeeProfileList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        public List<Dependant> postDependantList(RequestParameter.inputID input, HttpRequestMessage request)
        {
            try
            {
                List<Dependant> DependantList = new List<Dependant>();

                DependantList = dbContext.Dependants.Where(w => w.EmployeeProfileID == input.ID).OrderByDescending(c => c.DependantName).ToList();

                return DependantList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        public List<CertAttachment> postEduTrainList(RequestParameter.inputID input, HttpRequestMessage request)
        {
            try
            {
                List<CertAttachment> EduTrainList = new List<CertAttachment>();

                EduTrainList = dbContext.CertAttachments.Where(w => w.EmployeeID == input.ID).OrderByDescending(c => c.CertDate).ToList();

                return EduTrainList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        public List<PromotionHistory> postPromotionList(RequestParameter.inputID input, HttpRequestMessage request)
        {
            try
            {
                List<PromotionHistory> PromotionList = new List<PromotionHistory>();

                PromotionList = dbContext.PromotionHistories.Where(w => w.EmployeeID == input.ID).OrderByDescending(c => c.PromotionDate).ToList();

                return PromotionList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        public bool addEmployeeProfile(RequestParameter.inputAddEmployeeProfile input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                DateTime datetimeNow = DateTime.Now;
                EmployeeProfile EmployeeProfileModel = new EmployeeProfile();
                EmployeeProfileModel.EmployeeName = input.EmployeeName;
                EmployeeProfileModel.EmployeeAddress = input.EmployeeAddress;
                EmployeeProfileModel.StaffNumber = input.StaffNumber;
                EmployeeProfileModel.EmailAddress = input.EmailAddress;
                EmployeeProfileModel.PassportNumber = input.PassportNumber;
                //if (input.PassportNumber != EmployeeProfileModel.PassportNumber)
                //    EmployeeProfileModel.PassportNumber = input.PassportNumber;
                EmployeeProfileModel.ICNumber = input.ICNumber;
                //if (input.ICNumber != EmployeeProfileModel.ICNumber)
                //    EmployeeProfileModel.ICNumber = input.ICNumber;

                EmployeeProfileModel.CompanyID = input.CompanyID;
                EmployeeProfileModel.DepartmentID = input.DepartmentID;
                //if (input.UserGroupID != "")
                //    EmployeeProfileModel.UserGroupID = new Guid(input.UserGroupID);
                if (input.RoleID != "")
                    EmployeeProfileModel.RoleID = new Guid(input.RoleID);
                EmployeeProfileModel.ReportingManager = new Guid(input.ReportingManager);
                EmployeeProfileModel.OchartReportingManager = new Guid(input.OchartReportingManager);
                EmployeeProfileModel.Designation = input.Designation;
                EmployeeProfileModel.TelNumber = input.TelNumber;
                EmployeeProfileModel.Religion = input.Religion;
                EmployeeProfileModel.MaritalStatus = input.MaritalStatus;
                EmployeeProfileModel.IncomeTaxNo = input.IncomeTaxNo;
                EmployeeProfileModel.BankName = input.BankName;
                EmployeeProfileModel.PersonalEmailAddress = input.PersonalEmailAddress;
                EmployeeProfileModel.HighestAcademic = input.HighestAcademic;
                EmployeeProfileModel.Gender = input.Gender;
                EmployeeProfileModel.Race = input.Race;
                EmployeeProfileModel.EPFNo = input.EPFNo;
                EmployeeProfileModel.AccountNo = input.AccountNo;
                EmployeeProfileModel.HouseNo = input.HouseNo;
                EmployeeProfileModel.Nationality = input.Nationality;
                EmployeeProfileModel.ECP1Name = input.ECP1Name;
                EmployeeProfileModel.ECP1TelNumber = input.ECP1TelNumber;
                EmployeeProfileModel.ECP1HpNumber = input.ECP1HpNumber;
                EmployeeProfileModel.ECP1Relationship = input.ECP1Relationship;
                EmployeeProfileModel.ECP2Name = input.ECP2Name;
                EmployeeProfileModel.ECP2TelNumber = input.ECP2TelNumber;
                EmployeeProfileModel.ECP2HpNumber = input.ECP2HpNumber;
                EmployeeProfileModel.ECP2Relationship = input.ECP2Relationship;
                EmployeeProfileModel.SpouseName = input.SpouseName;
                EmployeeProfileModel.SpouseIC = input.SpouseIC;
                EmployeeProfileModel.SpouseHpNumber = input.SpouseHpNumber;
                EmployeeProfileModel.SpouseOccupation = input.SpouseOccupation;
                EmployeeProfileModel.SpousePassport = input.SpousePassport;
                EmployeeProfileModel.SpouseTelNumber = input.SpouseTelNumber;
                EmployeeProfileModel.ConfirmationDate = input.ConfirmationDate;
                EmployeeProfileModel.JoinDate = input.JoinDate;
                EmployeeProfileModel.CardNumber = input.CardNumber;
                EmployeeProfileModel.ChineseName = input.ChineseName;
                EmployeeProfileModel.JobLevel = input.JobLevel;
                EmployeeProfileModel.ResignDate = input.ResignDate;

                dbContext.EmployeeProfiles.Add(EmployeeProfileModel);
                dbContext.SaveChanges();

                EmployeeProfile EmployeeProfileIDModel = dbContext.EmployeeProfiles.Where(c => c.StaffNumber == input.StaffNumber && c.Deleted == 0).First();

                if (input.Dependant.Count != 0)
                {
                    //add back selected 
                    foreach (var inputDependant in input.Dependant)
                    {
                        Dependant DependantModel = new Dependant();
                        DependantModel.DependantName = inputDependant.DependantName;
                        DependantModel.DependantGender = inputDependant.DependantGender;
                        DependantModel.DependantDOB = inputDependant.DependantDOB;
                        DependantModel.DependantOccupation = inputDependant.DependantOccupation;
                        DependantModel.EmployeeProfileID = EmployeeProfileIDModel.ID;

                        dbContext.Dependants.Add(DependantModel);
                        dbContext.SaveChanges();
                    }
                }

                #region audit
                auditBL.auditAddDelete(userGuid, "add new profile", null, null, "EmployeeProfile");
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool editEmployeeProfile(RequestParameter.inputEditEmployeeProfile input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                common.extractUserRole(request);
                EmployeeProfile EmployeeProfileModel = dbContext.EmployeeProfiles.Where(c => c.ID == input.ID).FirstOrDefault();

                if (EmployeeProfileModel != null)
                {

                    var oldValue = common.convertObjToJsonString(EmployeeProfileModel);
                    EmployeeProfileModel.EmployeeName = input.EmployeeName;
                    EmployeeProfileModel.EmployeeAddress = input.EmployeeAddress;
                    EmployeeProfileModel.StaffNumber = input.StaffNumber;
                    EmployeeProfileModel.EmailAddress = input.EmailAddress;
                    EmployeeProfileModel.PassportNumber = input.PassportNumber;
                    EmployeeProfileModel.ICNumber = input.ICNumber;
                    EmployeeProfileModel.CompanyID = input.CompanyID;
                    EmployeeProfileModel.DepartmentID = input.DepartmentID;
                    //if (input.UserGroupID != "")
                    //    EmployeeProfileModel.UserGroupID = new Guid(input.UserGroupID);
                    if (input.RoleID != "")
                        EmployeeProfileModel.RoleID = new Guid(input.RoleID);
                    EmployeeProfileModel.ReportingManager = new Guid(input.ReportingManager);
                    EmployeeProfileModel.OchartReportingManager = new Guid(input.OchartReportingManager);
                    EmployeeProfileModel.Designation = input.Designation;
                    EmployeeProfileModel.TelNumber = input.TelNumber;
                    EmployeeProfileModel.Religion = input.Religion;
                    EmployeeProfileModel.MaritalStatus = input.MaritalStatus;
                    EmployeeProfileModel.IncomeTaxNo = input.IncomeTaxNo;
                    EmployeeProfileModel.BankName = input.BankName;
                    EmployeeProfileModel.PersonalEmailAddress = input.PersonalEmailAddress;
                    EmployeeProfileModel.HighestAcademic = input.HighestAcademic;
                    EmployeeProfileModel.Gender = input.Gender;
                    EmployeeProfileModel.Race = input.Race;
                    EmployeeProfileModel.EPFNo = input.EPFNo;
                    EmployeeProfileModel.AccountNo = input.AccountNo;
                    EmployeeProfileModel.HouseNo = input.HouseNo;
                    EmployeeProfileModel.Nationality = input.Nationality;
                    EmployeeProfileModel.ECP1Name = input.ECP1Name;
                    EmployeeProfileModel.ECP1TelNumber = input.ECP1TelNumber;
                    EmployeeProfileModel.ECP1HpNumber = input.ECP1HpNumber;
                    EmployeeProfileModel.ECP1Relationship = input.ECP1Relationship;
                    EmployeeProfileModel.ECP2Name = input.ECP2Name;
                    EmployeeProfileModel.ECP2TelNumber = input.ECP2TelNumber;
                    EmployeeProfileModel.ECP2HpNumber = input.ECP2HpNumber;
                    EmployeeProfileModel.ECP2Relationship = input.ECP2Relationship;
                    EmployeeProfileModel.SpouseName = input.SpouseName;
                    EmployeeProfileModel.SpouseIC = input.SpouseIC;
                    EmployeeProfileModel.SpouseHpNumber = input.SpouseHpNumber;
                    EmployeeProfileModel.SpouseOccupation = input.SpouseOccupation;
                    EmployeeProfileModel.SpousePassport = input.SpousePassport;
                    EmployeeProfileModel.SpouseTelNumber = input.SpouseTelNumber;
                    EmployeeProfileModel.ConfirmationDate = input.ConfirmationDate;
                    EmployeeProfileModel.JoinDate = input.JoinDate;
                    EmployeeProfileModel.CardNumber = input.CardNumber;
                    EmployeeProfileModel.ChineseName = input.ChineseName;
                    EmployeeProfileModel.JobLevel = input.JobLevel;
                    EmployeeProfileModel.ResignDate = input.ResignDate;

                    dbContext.SaveChanges();

                    //delete all ProjectResources
                    List<Dependant> dependantModel = dbContext.Dependants.Where(w => w.EmployeeProfileID == EmployeeProfileModel.ID).ToList();

                    foreach (var data in dependantModel)
                    {
                        dbContext.Dependants.Remove(data);
                        dbContext.SaveChanges();
                    }

                    if (input.Dependant.Count != 0)
                    {
                        //add back selected 
                        foreach (var inputDependant in input.Dependant)
                        {
                            Dependant DependantModel = new Dependant();
                            DependantModel.DependantName = inputDependant.DependantName;
                            DependantModel.DependantGender = inputDependant.DependantGender;
                            DependantModel.DependantDOB = inputDependant.DependantDOB;
                            DependantModel.DependantOccupation = inputDependant.DependantOccupation;
                            DependantModel.EmployeeProfileID = EmployeeProfileModel.ID;

                            dbContext.Dependants.Add(DependantModel);
                            dbContext.SaveChanges();
                        }
                    }

                    UAMUser userModel = dbContext.UAMUsers.Where(w => w.LoginID == EmployeeProfileModel.StaffNumber).FirstOrDefault();

                    if (userModel != null)
                    {
                        var oldValue2 = common.convertObjToJsonString(userModel);

                        userModel.LoginID = EmployeeProfileModel.StaffNumber;
                        if (input.RoleID != "")
                            userModel.RoleID = EmployeeProfileModel.RoleID;
                        if (EmployeeProfileModel.ResignDate != null)
                            userModel.AccessStatus = 1;
                        dbContext.SaveChanges();

                        UAMUserProfile userProfileModel = dbContext.UAMUserProfiles.Where(w => w.StaffNumber == EmployeeProfileModel.StaffNumber).FirstOrDefault();
                        if (userProfileModel != null)
                        {
                            var oldValue3 = common.convertObjToJsonString(userProfileModel);
                            userProfileModel.StaffNumber = EmployeeProfileModel.StaffNumber;
                            userProfileModel.StaffName = EmployeeProfileModel.EmployeeName;
                            userProfileModel.DepartmentID = EmployeeProfileModel.DepartmentID;
                            userProfileModel.Designation = EmployeeProfileModel.Designation;
                            userProfileModel.PhoneNum = EmployeeProfileModel.TelNumber;

                            dbContext.SaveChanges();
                        }

                        UAMSMMEmailList emailModel = dbContext.UAMSMMEmailLists.Where(w => w.AccessID == userModel.AccessID).FirstOrDefault();
                        if (emailModel != null)
                        {
                            var oldValue4 = common.convertObjToJsonString(emailModel);
                            emailModel.EmailAddress = EmployeeProfileModel.EmailAddress;

                            dbContext.SaveChanges();
                        }
                    }

                    #region audit
                    auditBL.auditUpdate(userGuid, "edit Employee Profile", EmployeeProfileModel.ID.ToString(), oldValue, common.convertObjToJsonString(EmployeeProfileModel), "EmployeeProfile");
                    #endregion

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

        public bool deleteEmployeeProfile(RequestParameter.inputID input, HttpRequestMessage request)
        {
            try
            {
                EmployeeProfile EmployeeProfileModel = dbContext.EmployeeProfiles.Where(c => c.ID == input.ID).FirstOrDefault();

                Guid userGuid = common.extractUserID(request);
                if (EmployeeProfileModel != null)
                {
                    EmployeeProfileModel.Deleted = 1;
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete EmployeeProfile", EmployeeProfileModel.ID.ToString(), common.convertObjToJsonString(EmployeeProfileModel), "EmployeeProfile");
                    #endregion
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

        public EmployeeProfile postEmployeeProfileByID(RequestParameter.inputID input)
        {
            try
            {
                EmployeeProfile EmployeeProfileModel = new EmployeeProfile();

                EmployeeProfileModel = dbContext.EmployeeProfiles.Where(c => c.ID == input.ID).FirstOrDefault();

                List<Dependant> dependantModel = dbContext.Dependants.Where(w => w.EmployeeProfileID == EmployeeProfileModel.ID).ToList();

                foreach (var item in dependantModel)
                {
                    EmployeeProfileModel.Dependants.Add(item);
                }

                return EmployeeProfileModel;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool verifyEmployeeProfile(RequestParameter.inputAddEmployeeProfile inputAdd, RequestParameter.inputEditEmployeeProfile inputEdit)
        {
            try
            {
                var countRecord = 0;

                if (inputAdd != null)
                {
                    countRecord = dbContext.V_EmployeeProfileList.Where(c => c.EmployeeName == inputAdd.EmployeeName && c.Deleted == 0 && c.AccessStatus == 0).Count();
                }
                else
                {
                    countRecord = dbContext.V_EmployeeProfileList.Where(c => c.EmployeeName == inputEdit.EmployeeName && c.ID != inputEdit.ID && c.Deleted == 0 && c.AccessStatus == 0).Count();
                }

                if (countRecord == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public string verifyICPassport(RequestParameter.inputAddEmployeeProfile inputAdd, RequestParameter.inputEditEmployeeProfile inputEdit)
        {
            try
            {
                if (inputAdd != null)
                {
                    if (inputAdd.ICNumber == "" && inputAdd.PassportNumber == "")
                        return Resources.NO_IDENTITY_DATA;
                    else if (inputAdd.ICNumber != "" && inputAdd.PassportNumber != "")
                        return Resources.IDENTITY_FAILED;
                    else
                        return "true";
                }
                else
                {
                    if (inputEdit.ICNumber == "" && inputEdit.PassportNumber == "")
                        return Resources.NO_IDENTITY_DATA;
                    else if (inputEdit.ICNumber != "" && inputEdit.PassportNumber != "")
                        return Resources.NO_DATA;
                    else
                        return "true";
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return "false";
            }
        }

        public List<EmployeeProfile> getActiveEmployeeProfile()
        {
            try
            {
                List<EmployeeProfile> activeEmployeeProfileList = new List<EmployeeProfile>();
                activeEmployeeProfileList = dbContext.EmployeeProfiles.OrderBy(o => o.EmployeeName).ToList();

                return activeEmployeeProfileList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public V_EmployeeProfileList postEmployeeProfileList(RequestParameter.inputID input)
        {
            try
            {
                V_EmployeeProfileList EmployeeProfileList = new V_EmployeeProfileList();
                EmployeeProfileList = dbContext.V_EmployeeProfileList.Where(w => w.ID == input.ID).FirstOrDefault();

                return EmployeeProfileList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        // for employee update their profiles

        public List<V_EmployeeProfileList> getUpdateEmployeeProfileList(HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                UAMUserProfile SingleEmployeeIDModel = dbContext.UAMUserProfiles.Where(c => c.AccessID == userGuid && c.Deleted == 0).First();

                List<V_EmployeeProfileList> EmployeeProfileList = dbContext.V_EmployeeProfileList.Where(c => c.StaffNumber == SingleEmployeeIDModel.StaffNumber).ToList();

                return EmployeeProfileList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        //public bool editSingleEmployeeProfile(RequestParameter.inputEditUpdateEmployeeProfile input, HttpRequestMessage request)
        //{
        //    try
        //    {
        //        Guid userGuid = common.extractUserID(request);
        //        common.extractUserRole(request);
        //        EmployeeProfile EmployeeProfileModel = dbContext.EmployeeProfiles.Where(c => c.ID == input.ID).FirstOrDefault();

        //        if (EmployeeProfileModel != null)
        //        {

        //            var oldValue = common.convertObjToJsonString(EmployeeProfileModel);
        //            //DateTime dueDate = input.StartDate.AddDays(input.LeadTime * 7);
        //            EmployeeProfileModel.EmployeeAddress = input.EmployeeAddress;
        //            //EmployeeProfileModel.EmailAddress = input.EmailAddress;
        //            EmployeeProfileModel.TelNumber = input.TelNumber;
        //            EmployeeProfileModel.HouseNo = input.HouseNo;
        //            EmployeeProfileModel.PersonalEmailAddress = input.PersonalEmailAddress;
        //            //EmployeeProfileModel.ConfirmationDate = input.ConfirmationDate;
        //            EmployeeProfileModel.ECP1Name = input.ECP1Name;
        //            EmployeeProfileModel.ECP1TelNumber = input.ECP1TelNumber;
        //            EmployeeProfileModel.ECP1HpNumber = input.ECP1HpNumber;
        //            EmployeeProfileModel.ECP1Relationship = input.ECP1Relationship;
        //            EmployeeProfileModel.ECP2Name = input.ECP2Name;
        //            EmployeeProfileModel.ECP2TelNumber = input.ECP2TelNumber;
        //            EmployeeProfileModel.ECP2HpNumber = input.ECP2HpNumber;
        //            EmployeeProfileModel.ECP2Relationship = input.ECP2Relationship;
        //            EmployeeProfileModel.SpouseName = input.SpouseName;
        //            EmployeeProfileModel.SpouseOccupation = input.SpouseOccupation;
        //            EmployeeProfileModel.SpouseIC = input.SpouseIC;
        //            EmployeeProfileModel.SpousePassport = input.SpousePassport;
        //            EmployeeProfileModel.SpouseHpNumber = input.SpouseHpNumber;
        //            EmployeeProfileModel.SpouseTelNumber = input.SpouseTelNumber;

        //            dbContext.SaveChanges();

        //            //delete all ProjectResources
        //            List<Dependant> dependantModel = dbContext.Dependants.Where(w => w.EmployeeProfileID == EmployeeProfileModel.ID).ToList();

        //            foreach (var data in dependantModel)
        //            {
        //                dbContext.Dependants.Remove(data);
        //                dbContext.SaveChanges();
        //            }

        //            if (input.Dependant.Count != 0)
        //            {
        //                //add back selected 
        //                foreach (var inputDependant in input.Dependant)
        //                {
        //                    Dependant DependantModel = new Dependant();
        //                    DependantModel.DependantName = inputDependant.DependantName;
        //                    DependantModel.DependantGender = inputDependant.DependantGender;
        //                    DependantModel.DependantDOB = inputDependant.DependantDOB;
        //                    DependantModel.DependantOccupation = inputDependant.DependantOccupation;
        //                    DependantModel.EmployeeProfileID = EmployeeProfileModel.ID;

        //                    dbContext.Dependants.Add(DependantModel);
        //                    dbContext.SaveChanges();
        //                }
        //            }

        //            bool emailEmployeeEditProfile = emailDal.emailEmployeeEditProfile(input, request);

        //            #region audit
        //            auditBL.auditUpdate(userGuid, "edit Employee Profile", EmployeeProfileModel.ID.ToString(), oldValue, common.convertObjToJsonString(EmployeeProfileModel), "EmployeeProfile");
        //            #endregion
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return false;
        //    }
        //}

        public bool verifyUpdateEmployeeProfile(RequestParameter.inputEditUpdateEmployeeProfile inputAdd, RequestParameter.inputEditUpdateEmployeeProfile inputEdit)
        {
            try
            {
                var countRecord = 0;

                if (inputAdd != null)
                {
                    countRecord = dbContext.EmployeeProfiles.Where(c => c.EmployeeName == inputAdd.EmployeeName && c.Deleted == 0).Count();
                }
                else
                {
                    countRecord = dbContext.EmployeeProfiles.Where(c => c.EmployeeName == inputEdit.EmployeeName && c.ID != inputEdit.ID && c.Deleted == 0).Count();
                }

                if (countRecord == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        //filter by company
        public List<Company> postCompanyList()
        {
            try
            {
                List<Company> companyList = new List<Company>();
                companyList = dbContext.Companies.OrderBy(o => o.CompanyName).ToList();

                return companyList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        //filter by department
        public List<Department> postDepartmentList()
        {
            try
            {
                List<Department> departmentList = new List<Department>();
                departmentList = dbContext.Departments.OrderBy(o => o.DepartmentName).ToList();

                return departmentList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        //profile photo upload
        public bool photoUploadFile(string EmployeeID, string filename)
        {
            try
            {
                EmployeeProfile checkAdd = dbContext.EmployeeProfiles.Where(c => c.StaffNumber == EmployeeID).FirstOrDefault();

                if (checkAdd.PhotoUploadFile != null)
                {
                    var filePath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFile/ProfilePhoto/" + checkAdd.PhotoUploadFile);

                    if (File.Exists(filePath) == true)
                    {
                        File.Delete(filePath);
                    }
                }

                EmployeeProfile photoUploadProfileModel = dbContext.EmployeeProfiles.Where(c => c.StaffNumber == EmployeeID).OrderByDescending(c => c.ID).FirstOrDefault();

                if (photoUploadProfileModel != null)
                {
                    photoUploadProfileModel.PhotoUploadFile = filename;

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

        public string getProfileIcon(HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                V_UserList user = dbContext.V_UserList.Where(w => w.AccessID == userGuid).FirstOrDefault();

                EmployeeProfile profileIcon = dbContext.EmployeeProfiles.Where(w => w.PhotoUploadFile != null && w.StaffNumber == user.StaffNumber && w.Deleted == 0).FirstOrDefault();

                if (profileIcon == null)
                    return null;

                return profileIcon.PhotoUploadFile;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        //get reporting manager
        public List<V_UserList> getIndividualList(HttpRequestMessage request)
        {
            try
            {
                List<V_UserList> dataList = new List<V_UserList>();

                dataList = dbContext.V_UserList.Where(w => w.AccessStatus == 0).OrderBy(c => c.StaffName).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

        //get Bank
        public List<Bank> getBankList(HttpRequestMessage request)
        {
            try
            {
                List<Bank> dataList = new List<Bank>();

                dataList = dbContext.Banks.OrderBy(c => c.BankName).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return null;
            }
        }

    }
}