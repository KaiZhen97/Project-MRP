using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace WebFrameWorkLib.Dal
{
    public class UAMDal
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private LogError logError = new LogError();
        private AuditBL auditBL = new AuditBL();
        private Common common = new Common();
        private string systemID = System.Configuration.ConfigurationManager.AppSettings["SystemID"];

        #region role
        public List<UAMUserRole> getRoleList(HttpRequestMessage request)
        {

            try
            {
                List<V_RoleMenuList> menuList = getRoleMenuList(request);

                if (common.extractUserRole(request) == "Admin")
                {
                    return dbContext.UAMUserRoles.Where(c => c.Deleted == 0).OrderBy(c => c.RoleName).ToList();
                }
                List<UAMUserRole> UserRoleList = new List<UAMUserRole>();

                //Guid superAdminRoleID = new Guid("DFA724BD-F4D9-467B-BBC6-220249BA512D");
                Guid superAdminRoleID = new Guid("02630B36-6286-40C0-BE42-38A83BF88F8C");
                var UserRole = dbContext.UAMUserRoles.Where(c => c.Deleted == 0 && c.RoleID != superAdminRoleID).OrderBy(c => c.RoleName);

                if (UserRole != null)
                {
                    UserRoleList = UserRole.ToList();
                    return UserRoleList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddRole(WebApiParameter.InputAddRole inputAddRole, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid newRoleID = Guid.NewGuid();

                UAMUserRole role = new UAMUserRole();
                role.RoleID = newRoleID;
                role.RoleName = inputAddRole.roleName;
                role.Description = inputAddRole.roleDesc;
                role.RoleStatus = Convert.ToInt16(inputAddRole.roleStatus);
                role.Deleted = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.roleDeleteStatus>("NotDeleted"));

                dbContext.UAMUserRoles.Add(role);
                dbContext.SaveChanges();

                #region audit
                auditBL.auditAddDelete(userGuid, "add new role", role.RoleID.ToString(), null, "UAMUserRole");
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

        public bool verifyRoleName(string roleName, string roleID)
        {

            try
            {
                UAMUserRole verifyRoleName = new UAMUserRole();

                if (roleID == null)
                {
                    verifyRoleName = dbContext.UAMUserRoles.Where(c => c.RoleName == roleName && c.Deleted == 0).FirstOrDefault();
                }
                else
                {
                    Guid roleGUID = new Guid(roleID);
                    verifyRoleName = dbContext.UAMUserRoles.Where(c => c.RoleName == roleName && c.RoleID != roleGUID && c.Deleted == 0).FirstOrDefault();
                }

                if (verifyRoleName == null)
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

        public UAMUserRole postRoleDetailsByID(WebApiParameter.InputRoleDetails inputRoleDetails)
        {
            try
            {
                Guid roleID = new Guid(inputRoleDetails.roleID);
                var role = dbContext.UAMUserRoles.Where(c => c.RoleID == roleID).FirstOrDefault();

                if (role != null)
                    return role;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditRole(WebApiParameter.InputEditRole inputEditRole, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid roleID = new Guid(inputEditRole.roleID);
                var role = dbContext.UAMUserRoles.Where(c => c.RoleID == roleID).FirstOrDefault();

                if (role != null)
                {

                    var oldRoleValue = common.convertObjToJsonString(role);

                    role.RoleName = inputEditRole.roleName;
                    role.Description = inputEditRole.roleDesc;
                    role.RoleStatus = Convert.ToInt16(inputEditRole.status);
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditUpdate(userGuid, "edit role", roleID.ToString(), oldRoleValue, common.convertObjToJsonString(role), "UAMUserRole");
                    #endregion
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteRole(WebApiParameter.InputDeleteRole inputDeleteRole, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid roleID = new Guid(inputDeleteRole.roleID);
                var role = dbContext.UAMUserRoles.Where(c => c.RoleID == roleID).FirstOrDefault();

                if (role != null)
                {
                    role.Deleted = 1;
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete role", roleID.ToString(), common.convertObjToJsonString(role), "UAMUserRole");
                    #endregion
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyRoleInUSe(WebApiParameter.InputDeleteRole inputDeleteRole)
        {
            try
            {
                Guid roleID = new Guid(inputDeleteRole.roleID);
                var roleModule = dbContext.UAMRoleModules.Where(c => c.RoleID == roleID).ToList();

                if (roleModule.Count() > 0)
                {
                    return false;
                }

                var user = dbContext.UAMUsers.Where(c => c.RoleID == roleID).ToList();

                if (user.Count() > 0)
                {
                    return false;
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



        #endregion

        #region tab
        public List<UAMModule> getTabList()
        {

            try
            {
                List<UAMModule> ModuleList = new List<UAMModule>();
                Guid sysID = new Guid(systemID);

                var Module = dbContext.UAMModules.Where(c => c.ParentModuleID == null && c.SystemID == sysID).OrderBy(c => c.DisplaySequence);

                if (Module != null)
                {
                    ModuleList = Module.ToList();
                    return ModuleList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddTab(WebApiParameter.InputAddTab inputAddTab, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);
                Guid sysID = new Guid(systemID);
                Guid newTabID = Guid.NewGuid();

                UAMModule tab = new UAMModule();
                tab.ModuleID = newTabID;
                tab.ModuleName = inputAddTab.tabName;
                tab.Description = inputAddTab.tabName;
                tab.ModuleStatus = Convert.ToInt16(inputAddTab.status);
                tab.DisplaySequence = Convert.ToInt16(inputAddTab.sequence);
                tab.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                tab.ModuleLink = inputAddTab.tabLink;
                tab.HTMLTagID = inputAddTab.htmlID;
                tab.HTMLTagIcon = inputAddTab.htmlIcon;
                tab.SystemID = sysID;

                dbContext.UAMModules.Add(tab);
                dbContext.SaveChanges();

                #region audit
                auditBL.auditAddDelete(userGuid, "add new tab", tab.ModuleID.ToString(), null, "UAMModule");
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

        public bool verifyTabName(string tabName, string moduleID)
        {

            try
            {
                UAMModule tab = new UAMModule();
                Guid sysID = new Guid(systemID);

                if (moduleID == null)
                {
                    tab = dbContext.UAMModules.Where(c => c.ModuleName == tabName && c.SystemID == sysID).FirstOrDefault();
                }
                else
                {
                    Guid moduleGUID = new Guid(moduleID);
                    tab = dbContext.UAMModules.Where(c => c.ModuleName == tabName && c.ModuleID != moduleGUID && c.SystemID == sysID).FirstOrDefault();
                }

                if (tab == null)
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

        public UAMModule postTabDetailsByID(WebApiParameter.InputTabDetails inputTabDetails)
        {
            try
            {
                Guid moduleID = new Guid(inputTabDetails.moduleID);
                var module = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (module != null)
                    return module;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditTab(WebApiParameter.InputEditTab inputEditTab, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid moduleID = new Guid(inputEditTab.moduleID);
                var tab = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (tab != null)
                {
                    var oldTabValue = common.convertObjToJsonString(tab);

                    tab.ModuleName = inputEditTab.tabName;
                    tab.Description = inputEditTab.tabName;
                    tab.ModuleStatus = Convert.ToInt16(inputEditTab.status);
                    tab.DisplaySequence = Convert.ToInt16(inputEditTab.sequence);
                    tab.UpdateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    tab.ModuleLink = inputEditTab.tabLink;
                    tab.HTMLTagID = inputEditTab.htmlID;
                    tab.HTMLTagIcon = inputEditTab.htmlIcon;

                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditUpdate(userGuid, "edit tab", moduleID.ToString(), oldTabValue, common.convertObjToJsonString(tab), "UAMModule");
                    #endregion

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteTab(WebApiParameter.InputDeleteTab inputDeleteTab, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid moduleID = new Guid(inputDeleteTab.moduleID);
                var module = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (module != null)
                {
                    dbContext.UAMModules.Remove(module);
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete tab", moduleID.ToString(), "", "UAMModule");
                    #endregion
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyTabInUSe(WebApiParameter.InputDeleteTab inputDeleteTab)
        {
            try
            {
                Guid tabID = new Guid(inputDeleteTab.moduleID);
                var subModule = dbContext.UAMModules.Where(c => c.ParentModuleID == tabID).ToList();

                if (subModule.Count() > 0)
                {
                    return false;
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
        #endregion

        #region module
        public List<V_ChildModuleList> getModuleList()
        {
            try
            {
                List<V_ChildModuleList> ModuleList = new List<V_ChildModuleList>();
                Guid sysID = new Guid(systemID);
                var Module = dbContext.V_ChildModuleList.Where(c => c.ParentModuleID != null && c.SystemID == sysID).OrderBy(c => c.ParentModuleName).ThenBy(c => c.DisplaySequence).ThenBy(c => c.DisplayName).ThenBy(c => c.GroupSequence).ToList();

                if (Module != null)
                {
                    ModuleList = Module;

                    return ModuleList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddModule(WebApiParameter.InputAddModule inputAddModule, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid newModuleID = Guid.NewGuid();
                Guid sysID = new Guid(systemID);
                UAMModule newModule = new UAMModule();
                newModule.ModuleID = newModuleID;
                newModule.ModuleName = inputAddModule.moduleName;
                newModule.Description = inputAddModule.moduleName;
                newModule.ModuleStatus = Convert.ToInt16(inputAddModule.status);
                newModule.DisplaySequence = Convert.ToInt16(inputAddModule.sequence);
                newModule.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                newModule.ModuleLink = inputAddModule.moduleLink;
                newModule.DisplayName = inputAddModule.groupName;
                newModule.HTMLTagID = inputAddModule.htmlID;
                newModule.HTMLTagIcon = inputAddModule.htmlIcon;
                newModule.SystemID = sysID;
                if (inputAddModule.groupSequence != "")
                    newModule.GroupSequence = Convert.ToInt16(inputAddModule.groupSequence);
                newModule.ParentModuleID = new Guid(inputAddModule.parentTabID);

                dbContext.UAMModules.Add(newModule);
                dbContext.SaveChanges();

                #region audit
                auditBL.auditAddDelete(userGuid, "add new module", newModule.ModuleID.ToString(), null, "UAMModule");
                #endregion

                inserteModuleActions(newModuleID, inputAddModule.actions, Request);

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyModuleName(string moduleName, string moduleID)
        {
            try
            {
                UAMModule module = new UAMModule();
                Guid sysID = new Guid(systemID);

                if (moduleID == null)
                {
                    module = dbContext.UAMModules.Where(c => c.ModuleName == moduleName && c.SystemID == sysID).FirstOrDefault();
                }
                else
                {
                    Guid moduleGUID = new Guid(moduleID);
                    module = dbContext.UAMModules.Where(c => c.ModuleName == moduleName && c.ModuleID != moduleGUID && c.SystemID == sysID).FirstOrDefault();
                }

                if (module == null)
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

        public void deleteModuleActions(Guid moduleID, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                var moduleList = dbContext.UAMModuleActions.Where(c => c.ModuleID == moduleID).ToList();

                if (moduleList != null)
                {
                    foreach (var module in moduleList)
                    {
                        dbContext.UAMModuleActions.Remove(module);
                        dbContext.SaveChanges();

                        #region audit
                        auditBL.auditAddDelete(userGuid, "delete module action", moduleID.ToString(), null, "UAMModuleAction");
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        public void inserteModuleActions(Guid moduleID, string actionStr, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Array actions = actionStr.Split(';');

                foreach (var action in actions)
                {
                    Guid newModuleActionID = Guid.NewGuid();
                    string actionDesc = action.ToString();
                    var actionID = dbContext.UAMActions.Where(c => c.Description == actionDesc).FirstOrDefault().ActionID;

                    if (actionID != null)
                    {
                        UAMModuleAction ModuleAction = new UAMModuleAction();
                        ModuleAction.ModuleActionID = newModuleActionID;
                        ModuleAction.ModuleID = moduleID;
                        ModuleAction.MAction = actionID;
                        ModuleAction.Status = 1; //always 1 at the moment.
                        dbContext.UAMModuleActions.Add(ModuleAction);
                        dbContext.SaveChanges();

                        #region audit
                        auditBL.auditAddDelete(userGuid, "add module action", moduleID.ToString(), null, "UAMModuleAction");
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        public UAMModule postModuleDetailsByID(WebApiParameter.InputModuleDetails inputModuleDetails)
        {
            try
            {
                Guid moduleID = new Guid(inputModuleDetails.moduleID);
                var module = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (module != null)
                    return module;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<UAMModuleAction> postModuleActionDetailsByID(WebApiParameter.InputModuleActionDetails inputModuleActionDetails)
        {
            try
            {
                Guid moduleID = new Guid(inputModuleActionDetails.moduleID);
                var module = dbContext.UAMModuleActions.Where(c => c.ModuleID == moduleID).ToList();

                if (module != null)
                    return module;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditModule(WebApiParameter.InputEditModule inputEditModule, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid moduleID = new Guid(inputEditModule.moduleID);
                var module = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (module != null)
                {
                    var oldModule = common.convertObjToJsonString(module);

                    module.ModuleName = inputEditModule.moduleName;
                    module.Description = inputEditModule.moduleName;
                    module.ModuleStatus = Convert.ToInt16(inputEditModule.status);
                    module.DisplaySequence = Convert.ToInt16(inputEditModule.sequence);
                    module.DisplayName = inputEditModule.groupName;
                    if (inputEditModule.groupSequence != "")
                        module.GroupSequence = Convert.ToInt16(inputEditModule.groupSequence);
                    module.ParentModuleID = new Guid(inputEditModule.parentTabID);
                    module.HTMLTagID = inputEditModule.htmlID;
                    module.HTMLTagIcon = inputEditModule.htmlIcon;
                    module.ModuleLink = inputEditModule.moduleLink;
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditUpdate(userGuid, "edit module", moduleID.ToString(), oldModule, common.convertObjToJsonString(module), "UAMModule");
                    #endregion

                    deleteModuleActions(moduleID, Request);
                    inserteModuleActions(moduleID, inputEditModule.actions, Request);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteModule(WebApiParameter.InputDeleteModule inputDeleteModule, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid moduleID = new Guid(inputDeleteModule.moduleID);
                var module = dbContext.UAMModules.Where(c => c.ModuleID == moduleID).FirstOrDefault();

                if (module != null)
                {
                    var moduleAction = dbContext.UAMModuleActions.Where(c => c.ModuleID == moduleID);
                    dbContext.UAMModuleActions.RemoveRange(moduleAction);
                    dbContext.SaveChanges();


                    dbContext.UAMModules.Remove(module);
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete module", moduleID.ToString(), "", "UAMModule");
                    #endregion
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        #endregion

        #region role module
        public List<WebApiParameter.OutputRoleModule> getRoleModuleList()
        {
            try
            {
                List<WebApiParameter.OutputRoleModule> outputRoleModuleList = new List<WebApiParameter.OutputRoleModule>();
                Guid sysID = new Guid(systemID);

                var parentModule = dbContext.UAMModules.Where(c => c.ParentModuleID == null && c.SystemID == sysID).OrderBy(c => c.DisplaySequence).ThenBy(c => c.DisplayName).ToList();

                if (parentModule != null)
                {
                    foreach (var item in parentModule)
                    {
                        WebApiParameter.OutputRoleModule OutputRoleModule = new WebApiParameter.OutputRoleModule();

                        OutputRoleModule.ModuleID = item.ModuleID.ToString();
                        OutputRoleModule.ModuleName = item.ModuleName;
                        OutputRoleModule.DisplayName = item.DisplayName;
                        OutputRoleModule.Description = item.Description;
                        OutputRoleModule.ModuleLink = item.ModuleLink;
                        OutputRoleModule.DisplaySequence = item.DisplaySequence.ToString();
                        OutputRoleModule.GroupSequence = item.GroupSequence.ToString();

                        List<WebApiParameter.SubModule> subModuleList = new List<WebApiParameter.SubModule>();
                        var subModules = dbContext.UAMModules.Where(c => c.ParentModuleID == item.ModuleID && c.SystemID == sysID).OrderBy(c => c.DisplayName).ThenBy(c => c.GroupSequence).ToList();

                        if (subModules != null)
                        {
                            foreach (var subitem in subModules)
                            {
                                WebApiParameter.SubModule subModule = new WebApiParameter.SubModule();
                                subModule.ModuleID = subitem.ModuleID.ToString();
                                subModule.ModuleName = subitem.ModuleName;
                                subModule.DisplayName = subitem.DisplayName;
                                subModule.Description = subitem.Description;
                                subModule.ModuleLink = subitem.ModuleLink;
                                subModule.DisplaySequence = subitem.DisplaySequence.ToString();
                                subModule.GroupSequence = subitem.GroupSequence.ToString();

                                var actionList = dbContext.V_ModuleAction.Where(c => c.ModuleID == subitem.ModuleID).ToList();

                                if (actionList != null)
                                {
                                    subModule.ModuleActionList = actionList;
                                }

                                subModuleList.Add(subModule);
                            }
                        }

                        OutputRoleModule.subModuleList = subModuleList;
                        outputRoleModuleList.Add(OutputRoleModule);
                    }
                    return outputRoleModuleList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool deleteRoleModule(Guid roleID, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);
                Guid sysID = new Guid(systemID);

                var roleModule = dbContext.UAMRoleModules.Where(c => c.RoleID == roleID && c.SystemID==sysID);

                if (roleModule != null)
                {
                    dbContext.UAMRoleModules.RemoveRange(roleModule);
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete role module", roleID.ToString(), null, "UAMRoleModule");
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

        public bool postEditRoleModule(WebApiParameter.InputEditRoleModule inputEditRoleModule, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);
                Guid sysID = new Guid(systemID);
                Guid roleID = new Guid(inputEditRoleModule.roleID);
                if (deleteRoleModule(roleID, Request))
                {
                    if (inputEditRoleModule.moduleStr != "")
                    {
                        string[] modules = inputEditRoleModule.moduleStr.Split(',');

                        foreach (var module in modules)
                        {
                            string[] insertValues = module.Split('|');

                            UAMRoleModule RoleModule = new UAMRoleModule();
                            RoleModule.RoleModuleID = Guid.NewGuid();
                            RoleModule.RoleID = new Guid(inputEditRoleModule.roleID);
                            RoleModule.SystemID = sysID;

                            if (insertValues.Count() == 3)
                            {
                                RoleModule.ModuleID = new Guid(insertValues[1]);
                                RoleModule.MAction = new Guid(insertValues[2]);
                            }

                            if (insertValues.Count() == 2)
                            {
                                RoleModule.ModuleID = new Guid(insertValues[1]);
                            }

                            if (insertValues.Count() == 1)
                            {
                                RoleModule.ModuleID = new Guid(insertValues[0]);
                            }

                            dbContext.UAMRoleModules.Add(RoleModule);
                            dbContext.SaveChanges();

                            #region audit
                            auditBL.auditAddDelete(userGuid, "add role module", RoleModule.RoleModuleID.ToString(), null, "UAMRoleModule");
                            #endregion
                        }
                    }
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

        public List<V_RoleModuleList> postRoleModuleDetailsByID(WebApiParameter.InputRoleModuleDetails inputRoleModuleDetails)
        {
            try
            {
                Guid roleID = new Guid(inputRoleModuleDetails.roleID);
                Guid sysID = new Guid(systemID);
                var roleModule = dbContext.V_RoleModuleList.Where(c => c.RoleID == roleID && c.SystemID == sysID).ToList();

                if (roleModule != null)
                    return roleModule;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }
        #endregion

        #region access right
        public List<V_RoleMenuList> getRoleMenuList(HttpRequestMessage Request)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var customClaimValue = principal.Claims.Where(c => c.Type == "UserID").Single().Value;
                Guid userID = new Guid(customClaimValue);
                Guid sysID = new Guid(systemID);

                var roleID = dbContext.UAMUsers.Where(c => c.AccessID == userID).FirstOrDefault().RoleID;

                List<V_RoleMenuList> roleMenuList = new List<V_RoleMenuList>();

                var roleMenu = dbContext.V_RoleMenuList.Where(c => c.RoleID == roleID && c.SystemID == sysID).OrderBy(c => c.ParentModuleID).ThenBy(c => c.DisplaySequence).ThenBy(c => c.GroupSequence).ToList();

                if (roleMenu != null)
                {
                    roleMenuList = roleMenu.ToList();
                    return roleMenuList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postCheckLinkAccessRight(WebApiParameter.InputUrl input, HttpRequestMessage request)
        {
            try
            {
                List<V_RoleMenuList> menuList = getRoleMenuList(request);

                if (common.extractUserRole(request) == "Admin")
                {
                    return true;
                }

                if (menuList == null)
                {
                    return false;
                }
                
                foreach (var item in menuList)
                {
                    string _link = item.ModuleLink;

                    if (_link != "")
                    {
                        //remove extention
                        if (_link.Contains("."))
                        {
                            var removeExtentionIndex = _link.LastIndexOf(".");
                            _link = _link.Substring(0, removeExtentionIndex);
                        }
                        
                        var startIndex = _link.LastIndexOf("/") + 1;
                        _link = _link.Substring(startIndex);

                        if (input.CurrentURL.Contains(_link))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        #endregion

        #region platform
        public List<UAMPlatform> getPlatformList()
        {

            try
            {
                List<UAMPlatform> PlatformList = new List<UAMPlatform>();

                var Platform = dbContext.UAMPlatforms;

                if (Platform != null)
                {
                    PlatformList = Platform.ToList();
                    return PlatformList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditPlatformRole(WebApiParameter.InputEditPlatformRole inputEditPlatformRole, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                string[] roleIDList = inputEditPlatformRole.roleID.Split(',');

                //delete all the table name in audit table
                Guid platformID = new Guid(inputEditPlatformRole.platformID);
                var deleteItems = dbContext.UAMPlatformLoginRoles.Where(c => c.PlatformID == platformID);
                dbContext.UAMPlatformLoginRoles.RemoveRange(deleteItems);
                dbContext.SaveChanges();

                //insert new table setting to table
                foreach (var roleID in roleIDList)
                {
                    UAMPlatformLoginRole PlatformLoginRole = new UAMPlatformLoginRole();
                    PlatformLoginRole.PlatformLoginRoleID = Guid.NewGuid();
                    PlatformLoginRole.PlatformID = platformID;
                    PlatformLoginRole.RoleID = new Guid(roleID);
                    dbContext.UAMPlatformLoginRoles.Add(PlatformLoginRole);
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "add platform login role", PlatformLoginRole.PlatformLoginRoleID.ToString(), common.convertObjToJsonString(PlatformLoginRole), "UAMPlatformLoginRole");
                    #endregion
                }

                ////always insert for super admin
                //UAMPlatformLoginRole PlatformLoginRoleAdmin = new UAMPlatformLoginRole();
                //PlatformLoginRoleAdmin.UAMPlatformLoginRoleID = Guid.NewGuid();
                //PlatformLoginRoleAdmin.UAMPlatformID = platformID;
                //PlatformLoginRoleAdmin.RoleID = new Guid("02630B36-6286-40C0-BE42-38A83BF88F8C"); 
                ////PlatformLoginRoleAdmin.RoleID = new Guid("DFA724BD-F4D9-467B-BBC6-220249BA512D");
                //dbContext.UAMPlatformLoginRoles.Add(PlatformLoginRoleAdmin);
                //dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public List<UAMPlatformLoginRole> postPlatformRoleByID(WebApiParameter.InputPlatformRole inputPlatformRole)
        {
            try
            {
                Guid platformID = new Guid(inputPlatformRole.platformID);
                var platformRole = dbContext.UAMPlatformLoginRoles.Where(c => c.PlatformID == platformID).ToList();

                if (platformRole != null)
                    return platformRole;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }
        #endregion

        #region user group
        public List<V_UAMUserGroup> getUserGroupList()
        {

            try
            {
                List<V_UAMUserGroup> UserGroupList = new List<V_UAMUserGroup>();

                var UserGroup = dbContext.V_UAMUserGroup.Where(c => c.Deleted == 0).OrderBy(c => c.UserGroupName);

                if (UserGroup != null)
                {
                    UserGroupList = UserGroup.ToList();
                    return UserGroupList;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postAddUserGroup(WebApiParameter.InputAddUserGroup inputAddUserGroup, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);
                DateTime datetimeNow = DateTime.Now;
                Guid newUserGroupID = Guid.NewGuid();

                UAMUserGroup userGroup = new UAMUserGroup();
                userGroup.UserGroupID = newUserGroupID;
                userGroup.UserGroupName = inputAddUserGroup.userGroupName;
                userGroup.DepartmentID = inputAddUserGroup.DepartmentID;
                userGroup.CreatedBy = userGuid;
                userGroup.CreatedDate = datetimeNow;
                userGroup.Deleted = 0;
                dbContext.UAMUserGroups.Add(userGroup);
                dbContext.SaveChanges();

                #region audit
                auditBL.auditAddDelete(userGuid, "add new user group", userGroup.UserGroupID.ToString(), null, "UAMUserGroup");
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

        public bool verifyUserGroupName(string userGroupName, string userGroupID)
        {

            try
            {
                UAMUserGroup UserGroup = new UAMUserGroup();

                if (userGroupID == null)
                {
                    UserGroup = dbContext.UAMUserGroups.Where(c => c.UserGroupName == userGroupName && c.Deleted == 0).FirstOrDefault();
                }
                else
                {
                    Guid userGroupGUID = new Guid(userGroupID);
                    UserGroup = dbContext.UAMUserGroups.Where(c => c.UserGroupName == userGroupName && c.UserGroupID != userGroupGUID && c.Deleted == 0).FirstOrDefault();
                }

                if (UserGroup == null)
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

        public UAMUserGroup postUserGroupDetailsByID(WebApiParameter.InputUserGroupDetails inputUserGroupDetails)
        {
            try
            {
                Guid userGroupID = new Guid(inputUserGroupDetails.userGroupID);
                var userGroup = dbContext.UAMUserGroups.Where(c => c.UserGroupID == userGroupID).FirstOrDefault();

                if (userGroup != null)
                    return userGroup;

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditUserGroup(WebApiParameter.InputEditUserGroup inputEditUserGroup, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);
                DateTime datetimeNow = DateTime.Now;
                Guid userGroupID = new Guid(inputEditUserGroup.userGroupID);
                var userGroup = dbContext.UAMUserGroups.Where(c => c.UserGroupID == userGroupID).FirstOrDefault();

                if (userGroup != null)
                {
                    var olduserGroupValue = common.convertObjToJsonString(userGroup);

                    userGroup.UserGroupName = inputEditUserGroup.userGroupName;
                    userGroup.DepartmentID = inputEditUserGroup.DepartmentID;
                    userGroup.UpdatedBy = userGuid;
                    userGroup.UpdatedDate = datetimeNow;

                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditUpdate(userGuid, "edit user group", userGroupID.ToString(), olduserGroupValue, common.convertObjToJsonString(userGroup), "UAMUserGroup");
                    #endregion

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteUserGroup(WebApiParameter.InputDeleteUserGroup inputDeleteUserGroup, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid userGroupID = new Guid(inputDeleteUserGroup.userGroupID);
                var userGroup = dbContext.UAMUserGroups.Where(c => c.UserGroupID == userGroupID).FirstOrDefault();

                if (userGroup != null)
                {
                    userGroup.Deleted = 1;
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete user group", userGroupID.ToString(), "", "UAMUserGroup");
                    #endregion
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyUserGroupInUse(WebApiParameter.InputDeleteUserGroup inputDeleteUserGroup)
        {
            try
            {
                Guid userGroupID = new Guid(inputDeleteUserGroup.userGroupID);
                var userGroup = dbContext.UAMUsers.Where(c => c.UserGroupID == userGroupID).ToList();

                if (userGroup.Count() > 0)
                {
                    return false;
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
        #endregion
    }
}