using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRP.Models
{
    public class RequestParameter
    {
        public class inputID
        {
            [Required]
            public int ID { get; set; }
        }

        public class inputGuid
        {
            [Required]
            public string Guid { get; set; }
        }

        #region EmployeeProfile
        public class inputAddEmployeeProfile
        {
            [Required]
            public int CompanyID { get; set; }
            [Required]
            public int DepartmentID { get; set; }
            public string UserGroupID { get; set; }
            [Required]
            public string ReportingManager { get; set; }
            [Required]
            public string OchartReportingManager { get; set; }
            [Required, MaxLength(250)]
            public string EmployeeName { get; set; }
            [Required, MaxLength(250)]
            public string EmployeeAddress { get; set; }
            [Required, MaxLength(250)]
            public string StaffNumber { get; set; }
            [Required, MaxLength(250)]
            public string EmailAddress { get; set; }
            [MaxLength(50)]
            public string PassportNumber { get; set; }
            [MaxLength(50)]
            public string ICNumber { get; set; }
            //public int AccessID { get; set; }
            //public int LoginID { get; set; }
            //[MaxLength(50)]
            //public string Password { get; set; }
            public string RoleID { get; set; }
            //public int AccessStatus { get; set; }
            [MaxLength(50)]
            public string Designation { get; set; }
            [MaxLength(100)]
            public string TelNumber { get; set; }
            [MaxLength(50)]
            public string Religion { get; set; }
            public int MaritalStatus { get; set; }
            [MaxLength(100)]
            public string IncomeTaxNo { get; set; }
            public int BankName { get; set; }
            [MaxLength(100)]
            public string PersonalEmailAddress { get; set; }
            [MaxLength(100)]
            public string HighestAcademic { get; set; }
            public int Gender { get; set; }
            [MaxLength(50)]
            public string Race { get; set; }
            [MaxLength(100)]
            public string EPFNo { get; set; }
            [MaxLength(100)]
            public string AccountNo { get; set; }
            [MaxLength(50)]
            public string HouseNo { get; set; }
            [MaxLength(100)]
            public string Nationality { get; set; }
            [MaxLength(100)]
            public string ECP1Name { get; set; }
            [MaxLength(50)]
            public string ECP1TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP1HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP1Relationship { get; set; }
            [MaxLength(50)]
            public string ECP2Name { get; set; }
            [MaxLength(50)]
            public string ECP2TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP2HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP2Relationship { get; set; }
            [MaxLength(50)]
            public string SpouseName { get; set; }
            [MaxLength(50)]
            public string SpouseIC { get; set; }
            [MaxLength(50)]
            public string SpousePassport { get; set; }
            [MaxLength(50)]
            public string SpouseOccupation { get; set; }
            [MaxLength(50)]
            public string SpouseTelNumber { get; set; }
            [MaxLength(50)]
            public string SpouseHpNumber { get; set; }
            public List<inputDependant> Dependant { get; set; }
            public Nullable<DateTime> ConfirmationDate { get; set; }
            public Nullable<DateTime> JoinDate { get; set; }
            public Nullable<DateTime> ResignDate { get; set; }
            public string CardNumber { get; set; }
            [MaxLength(250)]
            public string ChineseName { get; set; }
            [MaxLength(100)]
            public string JobLevel { get; set; }
        }

        public class inputEditEmployeeProfile : inputID
        {
            [Required]
            public int CompanyID { get; set; }
            [Required]
            public int DepartmentID { get; set; }
            public string UserGroupID { get; set; }
            [Required]
            public string ReportingManager { get; set; }
            [Required]
            public string OchartReportingManager { get; set; }
            [Required, MaxLength(250)]
            public string EmployeeName { get; set; }
            [Required, MaxLength(250)]
            public string EmployeeAddress { get; set; }
            [Required, MaxLength(250)]
            public string StaffNumber { get; set; }
            [Required, MaxLength(250)]
            public string EmailAddress { get; set; }
            [MaxLength(50)]
            public string PassportNumber { get; set; }
            [MaxLength(50)]
            public string ICNumber { get; set; }
            //public int AccessID { get; set; }
            //public int LoginID { get; set; }
            //[MaxLength(50)]
            //public string Password { get; set; }
            public string RoleID { get; set; }
            //public int AccessStatus { get; set; }
            [MaxLength(50)]
            public string Designation { get; set; }
            [MaxLength(100)]
            public string TelNumber { get; set; }
            [MaxLength(50)]
            public string Religion { get; set; }
            public int MaritalStatus { get; set; }
            [MaxLength(100)]
            public string IncomeTaxNo { get; set; }
            public int BankName { get; set; }
            [MaxLength(100)]
            public string PersonalEmailAddress { get; set; }
            [MaxLength(100)]
            public string HighestAcademic { get; set; }
            public int Gender { get; set; }
            [MaxLength(50)]
            public string Race { get; set; }
            [MaxLength(100)]
            public string EPFNo { get; set; }
            [MaxLength(100)]
            public string AccountNo { get; set; }
            [MaxLength(50)]
            public string HouseNo { get; set; }
            [MaxLength(100)]
            public string Nationality { get; set; }
            [MaxLength(100)]
            public string ECP1Name { get; set; }
            [MaxLength(50)]
            public string ECP1TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP1HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP1Relationship { get; set; }
            [MaxLength(50)]
            public string ECP2Name { get; set; }
            [MaxLength(50)]
            public string ECP2TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP2HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP2Relationship { get; set; }
            [MaxLength(50)]
            public string SpouseName { get; set; }
            [MaxLength(50)]
            public string SpouseIC { get; set; }
            [MaxLength(50)]
            public string SpousePassport { get; set; }
            [MaxLength(50)]
            public string SpouseOccupation { get; set; }
            [MaxLength(50)]
            public string SpouseTelNumber { get; set; }
            [MaxLength(50)]
            public string SpouseHpNumber { get; set; }
            public List<inputDependant> Dependant { get; set; }
            public Nullable<DateTime> ConfirmationDate { get; set; }
            public Nullable<DateTime> JoinDate { get; set; }
            public Nullable<DateTime> ResignDate { get; set; }
            public string CardNumber { get; set; }
            [MaxLength(250)]
            public string ChineseName { get; set; }
            [MaxLength(100)]
            public string JobLevel { get; set; }

        }

        public class inputDependant
        {
            public string DependantName { get; set; }
            public int DependantGender { get; set; }
            public string DependantDOB { get; set; }
            public string DependantOccupation { get; set; }
        }

        // Employee to update

        public class inputEditUpdateEmployeeProfile : inputID
        {
            //public int CompanyID { get; set; }
            //public int DepartmentID { get; set; }
            //public string UserGroupID { get; set; }
            [MaxLength(250)]
            public string EmployeeName { get; set; }
            [MaxLength(250)]
            public string EmployeeAddress { get; set; }
            //[MaxLength(250)]
            //public string StaffNumber { get; set; }
            //[MaxLength(250)]
            //public string EmailAddress { get; set; }
            //[MaxLength(50)]
            //public string PassportNumber { get; set; }
            //[MaxLength(50)]
            //public string ICNumber { get; set; }
            //public int AccessID { get; set; }
            //public int LoginID { get; set; }
            //[MaxLength(50)]
            //public string Password { get; set; }
            //public string RoleID { get; set; }
            //public int AccessStatus { get; set; }
            //[MaxLength(50)]
            //public string Designation { get; set; }
            [MaxLength(100)]
            public string TelNumber { get; set; }
            //[MaxLength(50)]
            //public string Religion { get; set; }
            //public int MaritalStatus { get; set; }
            //[MaxLength(100)]
            //public string IncomeTaxNo { get; set; }
            //public int BankName { get; set; }
            [MaxLength(100)]
            public string PersonalEmailAddress { get; set; }
            //[MaxLength(100)]
            //public string HighestAcademic { get; set; }
            //public int Gender { get; set; }
            //[MaxLength(50)]
            //public string Race { get; set; }
            //[MaxLength(100)]
            //public string EPFNo { get; set; }
            //[MaxLength(100)]
            //public string AccountNo { get; set; }
            [MaxLength(50)]
            public string HouseNo { get; set; }
            //[MaxLength(100)]
            //public string Nationality { get; set; }
            [MaxLength(100)]
            public string ECP1Name { get; set; }
            [MaxLength(50)]
            public string ECP1TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP1HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP1Relationship { get; set; }
            [MaxLength(50)]
            public string ECP2Name { get; set; }
            [MaxLength(50)]
            public string ECP2TelNumber { get; set; }
            [MaxLength(50)]
            public string ECP2HpNumber { get; set; }
            [MaxLength(50)]
            public string ECP2Relationship { get; set; }
            [MaxLength(50)]
            public string SpouseName { get; set; }
            [MaxLength(50)]
            public string SpouseIC { get; set; }
            [MaxLength(50)]
            public string SpousePassport { get; set; }
            [MaxLength(50)]
            public string SpouseOccupation { get; set; }
            [MaxLength(50)]
            public string SpouseTelNumber { get; set; }
            [MaxLength(50)]
            public string SpouseHpNumber { get; set; }
            public List<inputDependant> Dependant { get; set; }
            public Nullable<DateTime> ConfirmationDate { get; set; }
            public Nullable<DateTime> JoinDate { get; set; }
            public Nullable<DateTime> ResignDate { get; set; }
            public string CardNumber { get; set; }
            [MaxLength(250)]
            public string ChineseName { get; set; }
            [MaxLength(100)]
            public string JobLevel { get; set; }
        }

        public class inputFilterEmployeeList
        {
            public int CompanyID { get; set; }
            public int DepartmentID { get; set; }
            public int EmployeeStatus { get; set; }
        }
        #endregion

        #region MRP
        #region Item Library
        public class inputAddItemLibrary
        {
            //[Required]
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string IPN { get; set; }
            public string Manufacturer { get; set; }
            public string MPN { get; set; }
            [Required]
            public string ItemDescription { get; set; }
            public int RequiredSN { get; set; }
            public string Tariff { get; set; }
            public int RequiredCalibration { get; set; }
            public string MoreDetails { get; set; }
            public string DeliveryTerm { get; set; }
            public string SupplierName { get; set; }
            public string Purchaser1 { get; set; }
            public string Purchaser2 { get; set; }
            public string KeyTechSpec { get; set; }
            public int IsDraft { get; set; }
            public string Remark { get; set; }

            public List<inputAddItemLibraryPWP> PWPItemList { get; set; }
        }

        public class inputEditItemLibrary : inputAddItemLibrary
        {
            public int ID { get; set; }
        }

        public class inputDeleteItemLibrary
        {
            public int ID { get; set; }
            public string DeletedRemark { get; set; }
        }

        public class inputAddItemLibraryPWP
        {
            public int PWPItemLibraryID { get; set; }
            public decimal PWPUnitPriceDiscount { get; set; }
            public decimal FinalUnitPrice { get; set; }
        }

        public class inputAddDraftItem : inputAddItemLibrary
        {

        }

        public class inputSaveDraftItem : inputEditItemLibrary
        {

        }

        public class inputDeleteDraftItem
        {
            public int ID { get; set; }
        }

        public class inputAddCategory
        {
            [Required]
            public string CategoryName { get; set; }
            public string Description { get; set; }
        }

        public class inputEditCategory
        {
            [Required]
            public int ID { get; set; }
            [Required]
            public string CategoryName { get; set; }
            public string Description { get; set; }
        }

        public class inputDeleteCategory
        {
            [Required]
            public int ID { get; set; }
            public string DeletedRemark { get; set; }
        }
        #endregion


        public class inputAddRFQ
        {

            public int RFQ_StatusID { get; set; }
            [Required]
            public string Title { get; set; }
            public string Description { get; set; }
            public string Purchaser1 { get; set; }
            public string Purchaser2 { get; set; }
            public int Req_CancelAppr { get; set; }
            public int CancelApprv { get; set; }
            public List<string> Watchers { get; set; }
            public string tempWatcher { get; set; }
        }

        public class inputEditRFQ : inputAddRFQ
        {
            [Required]
            public int ID { get; set; }
            public int IsDraft { get; set; }
            public ArrayList CanceledFilesList { get; set; }
            public int TraceAction { get; set; }
        }

        public class inputDeleteRFQ : inputID
        {
            public string CancelRemark { get; set; }
        }

        public class inputSubmitPendingRFQ
        {
            [Required]
            public string value { get; set; }
            public string name { get; set; }
            public List<purchaser> purchaser { get; set; }
        }

        public class inputAddWatcher : inputID
        {
            [Required]
            public string email { get; set; }
        }

        public class purchaser
        {
            public Guid value { get; set; }
        }

        public class inputSubmitPendingRFQList
        {
            public List<inputSubmitPendingRFQ> submitList { get; set; }
        }

        #endregion
    }
}