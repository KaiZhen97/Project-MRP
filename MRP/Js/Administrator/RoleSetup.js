var tableRoleList = null;

function loadTable_RoleList(reqTableRoleListUrl, authToken) {
    tableRoleList = $("#Table_RoleList").DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqTableRoleListUrl,
                "contentType": "application/json",
                "type": "Get",
                "dataType": "JSON",
                "beforeSend": function (xhr) {
                    xhr.setRequestHeader("Authorization",
                        "Bearer " + authToken);
                },
                "data": function (d) {
                    return d;
                }
            },
            "columns": [
                {
                    "data": "RoleID",
                    "visible": false,
                },
                { "data": "RoleName" },
                { "data": "Description" },
                {
                    "data": "RoleStatus",
                    "mRender": function (data, type, full) {
                        if (data === 0)
                            return "Active";
                        else
                            return "Inactive";
                    }
                }
            ],
            "initComplete": function (settings, json) {
                //draw tooltip after the table complete
                $('[data-toggle="tooltip"]').tooltip();
            },
            "searchable": true,
            "orderable": true,
            "responsive": true,
        }
    );
}

function loadUserStatus(reqUserStatusUrl, authToken) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqUserStatusUrl,
            type: 'Get',
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                var target_AddNew = $("#selectAddNewRole_Status");
                var target_Edit = $("#selectEditRole_Status");

                target_AddNew.html("<option value=''>Select...</option>");
                target_Edit.html("<option value=''>Select...</option>");

                data.map(item => {
                    target_AddNew.append(`<option value='${item.ID}'>${item.Val}</option>`);
                    target_Edit.append(`<option value='${item.ID}'>${item.Val}</option>`);
                })

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function loadRoleDetailsByID(reqRoleDetailsByIdUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqRoleDetailsByIdUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#inputEditRole_RoleName").val(data.RoleName);
                $("#inputEditRole_Description").val(data.Description || "");
                $("#selectEditRole_Status").val(data.RoleStatus);

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function addNewRole(reqAddNewRoleUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddNewRoleUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function editRole(reqEditRoleUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditRoleUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function loadTable_RoleModule(reqTableRoleModuleUrl, authToken) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqTableRoleModuleUrl,
            type: 'Get',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'Authorization': 'Bearer ' + authToken,
            },
            success: function (data, status, xhr) {
                if (data.data !== null) {
                    console.log(data.data);

                    var firstLevel = data.data;
                    var construcStr = "";
                    var countfirstlevel = 0;

                    firstLevel.map((parentModule) => {
                        var firstlevelChkboxName = "firstlvl" + countfirstlevel.toString();
                        var countsndlevel = 0;

                        construcStr = construcStr +
                            `<tr>` +
                                `<td style='width: auto'>${CheckBoxWrapper(`<input class="table-ckb" type="checkbox" value="${parentModule.ModuleID}" name="${firstlevelChkboxName}" id="${firstlevelChkboxName}" />`)}</td>` +
                                `<td style='width:100%' colspan='3'>${parentModule.ModuleName}</td>` +
                            `</tr>`;

                        $.each(parentModule.subModuleList, function (key, subModule) {
                            var sndlevelChkboxName = firstlevelChkboxName + "sndlvl" + countsndlevel.toString();
                            var countrdlevel = 0;

                            construcStr = construcStr +
                                `<tr>` +
                                    `<td style='width: 50px'></td>` +
                                    `<td style='width: auto'>${CheckBoxWrapper(`<input class="table-ckb" type="checkbox" value="${parentModule.ModuleID + '|' + subModule.ModuleID}" name="${sndlevelChkboxName}" id="${sndlevelChkboxName}" />`)}</td>` +
                                    `<td style='width: 100%' colspan='2'>${subModule.ModuleName}</td>` +
                                `</tr>`;

                            $.each(subModule.ModuleActionList, function (key, actionList) {
                                var trdlevelChkboxName = sndlevelChkboxName + "trdlvl" + countrdlevel.toString();

                                construcStr = construcStr +
                                    `<tr>` +
                                        `<td style='width: 50px'></td>` +
                                        `<td style='width: 50px'></td>` +
                                        `<td style='width: auto'>${CheckBoxWrapper(`<input class="table-ckb" type="checkbox" value="${parentModule.ModuleID + '|' + subModule.ModuleID + '|' + actionList.MAction}" name="${trdlevelChkboxName}" id="${trdlevelChkboxName}" />`)}</td>` +
                                        `<td style='width: 100%' colspan='1'>${actionList.Description}</td>` +
                                    `</tr>`;

                                countrdlevel++
                            })

                            countsndlevel++
                        })

                        countfirstlevel++;
                    })

                    $("#Modal_RoleModuleSetup tbody").html(construcStr);

                    resolve()
                }
                else {
                    error("No data found");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function loadCheckedItem_RoleModule(reqLoadCheckedItemUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqLoadCheckedItemUrl,
            type: "POST",
            data: JSON.stringify(dataStr),
            dataType: "json",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken
            },
            success: function (data, status, xhr) {
                var contrutStr = "";
                $.each(data, function (key, value) {
                    var moduleStrVal = "";

                    if (value.ParentModuleID != null) {
                        if (moduleStrVal == "")
                            moduleStrVal = value.ParentModuleID;
                        else
                            moduleStrVal = moduleStrVal + '|' + value.ParentModuleID;
                    }

                    if (value.ModuleID != null) {
                        if (moduleStrVal == "")
                            moduleStrVal = value.ModuleID;
                        else
                            moduleStrVal = moduleStrVal + '|' + value.ModuleID;
                    }

                    if (value.MAction != null) {
                        if (moduleStrVal == "")
                            moduleStrVal = value.MAction;
                        else
                            moduleStrVal = moduleStrVal + '|' + value.MAction;
                    }
                    var target = $("input[type=checkbox][value='" + moduleStrVal + "']");

                    target.prop('checked', true);
                    target.addClass('curr-db-val')

                    contrutStr = contrutStr + value.ModuleID + ",";
                });

                $("#" + hiddenChkBoxValue_ID).val(contrutStr.substring(0, contrutStr.length - 1));

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        });
    })
    
}

function saveRoleModule(reqSaveRoleModule, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqSaveRoleModule,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve();
            },
            error: function (xhr, ajaxOptions, thrownEror) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function initTable_RoleModule() {
    $("input[name^='first']").change(function () {
        CheckBox_CheckModify(this, $(this).prop("checked"));

        generateValueData();
    });
}

function generateValueData() {
    var contrutStr = "";
    $("input[type='checkbox']").each(function (i, obj) {
        if (this.checked) {
            if (contrutStr == "")
                contrutStr = $(this).val();
            else
                contrutStr = contrutStr + "," + $(this).val();
        }
    });

    $("#" + hiddenChkBoxValue_ID).val(contrutStr);
}