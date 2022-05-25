var authToken = "";
var reqTableURL = "";
var reqLoadStatusUrl = "";
var reqLoadModuleNameUrl = "";
var reqAddNewRFQUrl = "";
var reqEditRFQUrl = "";

var RFQSetupTable = null;

var currModalMode = 'new';

function loadTable(reqTableUrl, authToken) {
    t = $('#tblRFQData').dataTable(
        {
            "processing": true,
            "serverSide": false,
            "stateSave": true,
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
                    "data": "RFQNo",
                    "width": "10%"
                },
                {
                    "data": "Title",
                    "width": "10%"
                },
                {
                    "data": "Description",
                    "width": "30%"
                },
                {
                    "data": "Active",
                    "mRender": function (data, type, full) {
                        if (data == 1)
                            return "Submitted";
                        else
                            return "Not Submitted";
                    },
                    "width": "5%"
                },
                {
                    "data": "CreatedByStaffName",
                    "width": "10%"
                },
                {
                    "data": "Purchaser1",
                    "width": "10%"
                },
                {
                    "data": "CreatedDate",
                    "width": "10%"
                },
                {
                    "data": "LastUpdatedDate",
                    "width": "10%"
                }
            ],
            "initComplete": function (settings, json) {
                $('[data-toggle="tooltip"]').tooltip();
            },
            "searchable": true,
            "orderable": true,
            "responsive": {
                "details": false
            },
            "order": [[1, 'asc']],
            "autoWidth": true,
        });

    $('.form-control input-sm').attr('style', 'width:auto;');
}

function AddSubmitRFQ(reqAddSubmitRFQUrl, authToken) {
    var data = {

    }

    // Set Button Busy


    $.ajax({
        url: reqAddSubmitRFQUrl,
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









function ShowDrawer_RFQModal(mode) {

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

function CloseDrawer_RFQModal() {
    currModalMode = '';

    $("#ModuleSetupModal").removeClass("show");
}