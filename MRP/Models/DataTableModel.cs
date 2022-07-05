using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.Database;
using MRP.Database;


namespace MRP.Models
{
    public class DataTableModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    #region E-Profile System
    public class DataTableCompany : DataTableModel
    {
        public List<Company> data { get; set; }
    }

    public class DataTableTeam : DataTableModel
    {
        public List<V_TeamList> data { get; set; }
    }

    public class DataTableEmployeeProfile : DataTableModel
    {
        public List<V_EmployeeProfileList> data { get; set; }
        public bool AccessRight { get; set; }
    }
    public class DataTableProbation : DataTableModel
    {
        public List<V_ProbationList> data { get; set; }
        public bool AccessRight { get; set; }
    }

    public class DataTableDependant : DataTableModel
    {
        public List<Dependant> data { get; set; }
    }

    public class DataTableEduTrain : DataTableModel
    {
        public List<CertAttachment> data { get; set; }
    }

    public class DataTablePromotion : DataTableModel
    {
        public List<PromotionHistory> data { get; set; }
        public bool AccessRight { get; set; }
    }

    public class DataTableFileTypeAttachment : DataTableModel
    {
        public List<FileTypeAttachment> data { get; set; }
    }

    public class DataTableEmployeeAttachment : DataTableModel
    {
        public List<V_EmployeeAttachmentList> data { get; set; }
    }

    public class DataTableTrainingList : DataTableModel
    {
        public List<V_TrainingList> data { get; set; }
    }

    public class DataTableRegisteredEmployee : DataTableModel
    {
        public List<V_RegisteredEmployee> data { get; set; }
        public int TotalEmployee { get; set; }
        public int RegisteredEmployee { get; set; }
        public int NonRegisteredEmployee { get; set; }
    }

    public class DataTableCategory : DataTableModel
    {
        public List<WebFrameWorkLib.Database.V_CategoryList> data { get; set; }
    }

    public class DataTablePolicy : DataTableModel
    {
        public List<V_PolicyAndMemoList> data { get; set; }
    }

    public class DataTableEmployeeReportingManager : DataTableModel
    {
        public List<V_EmployeeReportingManagerList> data { get; set; }
        public bool AccessRight { get; set; }
    }
    #endregion

    #region MRP

    #region Item Library
    public class DataTableItemLibrary : DataTableModel
    {
        public List<V_ItemLibraryList> data { get; set; }
    }

    public class DataTableItemLibrarySupplier : DataTableModel
    {
        public List<ItemLibrarySupplier> data { get; set; }
    }

    public class DataTableILMCategory : DataTableModel
    {
        public List<MRP.Database.V_CategoryList> data { get; set; }
    }
    #endregion

    #region RFQ
    public class DataTableRFQ : DataTableModel
    {
        public List<V_RFQList> data { get; set; }
    }

    public class DataTableRFQList : DataTableModel
    {
        public List<RFQList> data { get; set; }
    }

    public class DataTableUpdateTrace : DataTableModel
    {
        public List<V_UpdateHistoryTrace> data { get; set; }
    }
    #endregion
    #endregion
}
