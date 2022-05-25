var tableTabList = null;

function loadTableRoleList(reqTableTabListUrl, authToken) {
    tableTabList = $("#Table_TabList").DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqTableTabListUrl,
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
                    "data": "ModuleID",
                    "visible": false,
                },
                { "data": "ModuleName" },
                { "data": "ModuleLink" },
                { "data": "DisplaySequence" },
                {
                    "data": "ModuleStatus",
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
                var target_AddNew = $("#selectAddNewTab_Status");
                var target_Edit = $("#selectEditTab_Status");

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

function saveAddNewTab(reqAddNewTabUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddNewTabUrl,
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

function loadTabDetails(reqLoadTabDetailsUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqLoadTabDetailsUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#inputEditTab_TabName").val(data.ModuleName);
                $("#inputEditTab_HTMLTagId").val(data.HTMLTagID);
                $("#inputEditTab_DisplaySequence").val(data.DisplaySequence);
                $("#inputEditTab_TabLink").val(data.ModuleLink);
                $("#inputEditTab_HTMLIcon").val(data.HTMLTagIcon);
                $("#selectEditTab_Status").val(data.ModuleStatus);

                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function saveEditTab(reqEditTabUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditTabUrl,
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