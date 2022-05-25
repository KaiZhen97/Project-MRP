var authToken = "";
var reqTableURL = "";
var reqLoadStatusUrl = "";
var reqLoadModuleNameUrl = "";
var reqAddNewModuleUrl = "";
var reqEditModuleUrl = "";

var moduleSetupTable = null;

var currModalMode = 'new';

function loadTable(reqTableUrl, authToken) {
    moduleSetupTable = $('#ModuleSetupTable').DataTable(
        {
            "processing": true,
            "serverSide": false,
            "columnDefs": [
                { "targets": 0, "searchable": false, "className": "never" },
                { "className": "center", "targets": 1, "width": "2%", "orderable": false },
                { "targets": 2, "orderable": true, "width": "20%", "className": "left" }
            ],
            "ajax":
            {
                "url": reqTableUrl,
                "contentType": "application/json",
                "type": "GET",
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
                    "data": null,
                    "visible": false
                },
                {
                    "data": "ModuleID",
                    "mRender": function (data, type, full) {
                        var createDate = data;

                        var contructStr = '<a class=\"clickableLink\" href="' + "#" + data + '"><i class="icon mdi mdi-pencil-box-outline" style="color:#3498DB;"data-toggle="tooltip" data-placement="top" title="Edit"></i></a>'
                        return contructStr;
                    }
                },
                {
                    "data": "ParentModuleName",
                },
                {
                    "data": "ModuleName",
                },
                {
                    "data": "ModuleLink",
                },
                {
                    "data": "DisplaySequence",
                },
                {
                    "data": "DisplayName",
                },
                {
                    "data": "GroupSequence",
                },
                {
                    "data": "ModuleStatus",
                    "mRender": function (data, type, full) {
                        if (data == 0)
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
        });

    //$(".dataTables_filter input").attr("placeholder", "Filter");

    //t.on('order.dt search.dt', function (d) {
    //}).draw();
    //$(".dataTables_filter input").attr("placeholder", "Filter");
}

function loadStatus(reqLoadStatusUrl, authToken) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqLoadStatusUrl,
            type: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken
            },
            success: function (data, status, xhr) {
                var target = $("#inputStatus");

                // Reset option
                target.html("<option value=''>Select...</option>");

                data.map(item => {
                    target.append(`<option value='${item.ID}'>${item.Val}</option>`);
                })

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        });
    });

    //return load ? Promise.resolve(returnData) : Promise.reject(returnData);
}

function loadModuleName(reqLoadModuleNameUrl, authToken) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqLoadModuleNameUrl,
            type: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken
            },
            success: function (data, status, xhr) {
                console.log(data);

                var target = $("#inputParentTab");

                // Reset option
                target.html("");
                target.html("<option value=''>Select...</option>");

                data.data.map(item => {
                    target.append(`<option value='${item.ModuleID}'>${item.ModuleName}</option>`);
                })

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        });
    });
}

function AddNewModule(reqAddNewModuleUrl, authToken) {
    var data = {
        
    }

    // Set Button Busy


    $.ajax({
        url: reqAddNewModuleUrl,
        type: 'Post',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        data: JSON.stringify(data),
        dataType: "json",
        success: function (data, status, xhr) {

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    })
}

function EditModule(reqAddNewModuleUrl, authToken) {
    var data = {

    }
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddNewModuleUrl,
            type: 'Post',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken
            },
            data: JSON.stringify(data),
            dataType: "json",
            success: function (data, status, xhr) {

            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        })
    })
}

function ShowDrawer_ModuleSetupModal(mode) {

    if (mode === 'new') {
        currModalMode = mode;
        $("#ModuleSetupModal").attr('title', 'Add New Module');
    }
    else {

    }
    
    
    if (reqLoadStatusUrl !== null && reqLoadModuleNameUrl !== null && authToken !== null) {
        $("#ModuleSetupModal").addClass("show");
        $("#ModuleSetupModal").addClass("loading");

        loadStatus(reqLoadStatusUrl, authToken)
            .then((data) => {
                return loadModuleName(reqLoadModuleNameUrl, authToken);
            })
            .catch(error => {
                ShowAlert("Add New Module", "Error occor while pre loading modal.\nSee error details in console.");
                console.log(error);
            })
            .finally(() => {
                $("#ModuleSetupModal").removeClass("loading");
            })

    } else {
        //$("#ModuleSetupModal").addClass("show");
        ShowError("Add New Module", "Add New Module open without pre loading.\nSee console for more details.");
        console.log(reqLoadStatusUrl, reqLoadModuleNameUrl, authToken);
    }
}

function CloseDrawer_ModuleSetupModal() {
    currModalMode = '';

    $("#ModuleSetupModal").removeClass("show");
}
