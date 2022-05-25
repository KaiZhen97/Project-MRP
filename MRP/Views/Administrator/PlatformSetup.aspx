<%@ Page Title="Platform Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlatformSetup.aspx.cs" Inherits="MRP.Views.Administrator.PlatformSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/PlatformSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <asp:TextBox runat="server" ID="hiddenPlatformID" ClientInstanceName="hiddenPlatformID"></asp:TextBox>
        <asp:TextBox runat="server" ID="hiddenChkBoxValue" ClientInstanceName="hiddenChkBoxValue"></asp:TextBox>
    </form>

    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <table id="PlatformSetupTable">
        <thead>
            <tr>
                <th></th>
                <th>Platform</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="Edit Platform" id="Modal_EditPlatform">
        <h4 class="_table-title"></h4>
        <table>
            <thead>
                <tr>
                    <th>
                        <div class="_checkbox-cont">
                            <input type="checkbox" id="SelectAll"/>
                            <i class="fa-solid fa-check _checkbox-checkmark"></i>
                        </div>

                    </th>
                    <th>Select All</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <div class="d-flex flex-row-reverse">
            <button class="solid-button submit mt-3" id="btnSaveEditPlatform">Save</button>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/PlatformSetup") %>

    <script>
        var hiddenChkBoxValue_ID = "<%= hiddenChkBoxValue.ClientID%>"
        var hiddenPlatformID_ID = "<%= hiddenPlatformID.ClientID%>"
        $(`#${hiddenChkBoxValue_ID}, #${hiddenPlatformID_ID}`).css('display', 'none');

        var reqTableUrl = "<%= ResolveUrl("~/api/UAM/getPlatformList") %>";
        var reqEditModalTable = "<%= ResolveUrl("~/api/UAM/getRoleList")%>";
        var reqCheckedItem = "<%= ResolveUrl("~/api/UAM/postPlatformRoleByID") %>";
        var reqSaveEditPlatform = "<%= ResolveUrl("~/api/UAM/postEditPlatformRole") %>"
        var authToken = "<%= Session["accessToken"].ToString() %>";

        $(document).ready(function () {
            $("#SelectAll").on("click", function (e) {
                var currVal = $(this).prop('checked');

                $("input[name^='first']").each((key, value) => {
                    var target = $(value);

                    target.prop("checked", currVal);

                    CheckBox_CheckModify(target, currVal);
                    generateValueData();
                });
            });

            $("#btnSaveEditPlatform").on('click', function (e) {
                var dataStr = {
                    platformID: $(`#${hiddenPlatformID_ID}`).val(),
                    roleID: $(`#${hiddenChkBoxValue_ID}`).val()
                }

                console.log(dataStr);

                $(this).addClass('busy');

                SaveEditPlatform(reqSaveEditPlatform, authToken, dataStr)
                    .then((data) => {
                        ShowPass("Save Edit Platform", data.Message);

                        $("#Modal_EditPlatform").removeClass("loading");
                        $("#Modal_EditPlatform").removeClass("show");
                    })
                    .catch((error) => {
                        ShowError("Save Edit Platform", `Fail to save edit: ${error.responseText}`);
                    })
                    .finally(() => {
                        $(this).removeClass('busy');
                    })
            });

            loadTable(reqTableUrl, authToken);
        });

        $.contextMenu({
            selector: '#PlatformSetupTable tbody tr',
            items: {
                "edit": {
                    name: "Edit", icon: "fa-edit", callback: function (key, options) {
                        var row = table.row(options.$trigger);
                        var platformID = row.data().PlatformID;
                        
                        $(`#${hiddenPlatformID_ID}`).val(platformID);

                        $("#Modal_EditPlatform ._table-title").html(row.data().PlatformName);
                        $("#Modal_EditPlatform").addClass("show");
                        $("#Modal_EditPlatform").addClass("loading");

                        loadEditModalTable(reqEditModalTable, authToken)
                            .then(() => {
                                var dataStr = {
                                    platformID: platformID
                                };

                                initEditModalTable();

                                return loadCheckedItem(reqCheckedItem, authToken, dataStr);
                            })
                            .then(() => {
                                generateValueData();
                            })
                            .catch((error) => {
                                ShowError("Load Edit Modal Error", `${error.responseText}: Please see console log for error details.`);
                                $("#Modal_EditPlatform").removeClass("show");
                            })
                            .finally(() => {
                                $("#Modal_EditPlatform").removeClass("loading");
                            })
                    }
                },
            }
        });
    </script>
</asp:Content>
