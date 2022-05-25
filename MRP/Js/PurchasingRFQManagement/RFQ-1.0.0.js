/*init methods*/
var tblEq;
var detailRows = [];

$(window).on('load', function () {
    "use strict";
    init();

    function init() {
        var projectID = sessionStorage.projectID;
        getProject(projectID);
    }
});

$(document).ready(function () {
    $.when(jobloadmenu).done(function () {
        if (!allowAdd)
            $("#btnAddNew").remove();

        $("#btnAddNew").click(function () {
            $("#lblModal").text("Add New Equipment");
            //$("#divCalibrationForm").removeAttr("hidden");
            setMode(0);
            assemblyLineDropdown(0);
            calibrationShowForm();
        });

        $("#btnCloseModalSM, #btnCloseModalBg").click(function () {
            closeModal();
        });
    });

    $('#btnSubmit').click(function (e) {
        validateData.form();
    });

    $("#selFProject").change(function () {
        tblEq.ajax.reload();
        sessionStorage.projectID = $("#selFProject").val();
    });

    $('.clickableLink').click(function ($e) {
        $e.preventDefault();
    });

    $('#txtCalibrationStatus').change(function () {
        calibrationShowForm();
    });

    $("#selProject").change(function () {
        assemblyLineDropdown(0);
    });

    $("#equipmentUpload").on("change", function () {
        if ($('#equipmentUpload')[0].files)
            uploadEquipmentFile();
    });
});
/*init methods end */

/*data tables*/
function loadTable(reqTableUrl, authToken) {
    tblEq = $('#tblData').DataTable(
    {
        "processing": true,
        "serverSide": false,
        "stateSave": true,
        "columnDefs": [
        //{ "className": "center", "targets": [0, 6, 7, 9] },
        ],
        "ajax":
        {
            "url": reqTableUrl,
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "beforeSend": function (xhr) {
                xhr.setRequestHeader("Authorization",
                    "Bearer " + authToken);
            },
            "data": function (d) {
                d.fProjectID = $("#selFProject").val();
                d.fAssemblyLineID = 0;
                d.fEquipmentStatusID = 0;
                //d.fCalibrationStatusID = 0;
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                if (!json)
                    return [];
                return json.data;
            }
        },
        "columns": [
        {
            "data": "ID",
            "orderable": false,
            "mRender": function (data, type, full) {
                var contructStr = "";
                if (allowEdit) {
                    //1 - Calibrated, 2 - Calibration In Progress, 3 - Due For Calibration, 4 - Calibration Not Required, 5 - Faulty, 6 - Missing
                    if (full.EquipmentStatusID == 1 || full.EquipmentStatusID == 2 || full.EquipmentStatusID == 3) {
                        contructStr += '&nbsp;&nbsp;<a class="clickableLink" href="/Calibration/' + data + '" style="color:#2A4DFC;"'
                            + 'data-toggle="tooltip" data-placement="top" title="Calibration"><i class="fa fa-wrench"></i></a>';
                    }
                    if (full.EquipmentStatusID == 1 || full.EquipmentStatusID == 3) {
                        contructStr += '&nbsp;&nbsp;<a class="clickableLink" onclick="setSendForCalibration(' + data + ');" style="color:#673ab7;"'
                            + 'data-toggle="tooltip" data-placement="top" title="Send for calibration"><i class="fa-solid fa-truck"></i></a>';
                    }
                    contructStr += '&nbsp;&nbsp;<a class="clickableLink" onclick="setEditSequence(' + data + ');" style="color:#1E90FF;"'
                        + 'data-toggle="tooltip" data-placement="top" title="Edit"><i class="fa fa-edit"></i></a>';
                }
                if (allowDelete) {
                    contructStr += '&nbsp;&nbsp;<a class="clickableLink" onclick="setHiddenFieldDelete(' + data + ');" style="color:#FF0000;"'
                                    + 'data-toggle="tooltip" data-placement="top" title="Delete"><i class="fa fa-trash-o"></i></a>'
                }
                //contructStr += '&nbsp;&nbsp;<a class="clickableLink details-control" '
                //                + 'style="color:#2A4DFC;" data-toggle="tooltip" data-placement="top" title="Details"><i class="fa fa-info"></i></a>';
                contructStr += '&nbsp;&nbsp;<a class="clickableLink" href="/EquipmentDetails/' + data + '" style="color:#2A4DFC;"'
                                + ' data-toggle="tooltip" data-placement="top" title="Details"><i class="fa fa-info"></i></a>';

                return contructStr;
            },
            "width": "10%",
            "className": "center"
        },
        {
            "data": "ProjectName",
            "visible": false
        },
        {
            "data": "AssemblyLine",
            "width": "10%",
        },
        {
            "data": "EQID",
            "width": "10%",
        },
        {
            "data": "EquipmentName",
            "width": "10%",
        },
        {
            "data": "Description",
            "width": "15%",
        },
        {
            "data": "EquipmentRange",
            "visible": false
        },
        {
            "data": "Brand",
            "visible": false
        },
        {
            "data": "Model",
            "visible": false
        },
        {
            "data": "Supplier",
            "width": "10%",
        },
        {
            "data": "SerialNumber",
            "visible": false
        },
        {
            "data": "CalibrationFrequency",
            "visible": false
        },
        {
            "data": "Resolution",
            "visible": false
        },
        {
            "data": "PermissibleError",
            "visible": false
        },
        {
            "data": "Uncertainty",
            "visible": false
        },
        {
            "data": "Location",
            "visible": false
        },
        {
            "data": "RegisterDate",
            "mRender": function (data, type, full) {
                return data.replace("T", " ").substring(0, 10);
            },
            "className": "center",
            "visible": false
        },
        {
            "data": "LastCalibrationDate",
            "mRender": function (data, type, full) {
                if (data != null)
                    return data.replace("T", " ").substring(0, 10);
                return "";
            },
            "width": "10%",
            "className": "center"
        },
        {
            "data": "CalibrationExpiryDate",
            "mRender": function (data, type, full) {
                if (data != null)
                    return data.replace("T", " ").substring(0, 10);
                return "";
            },
            "width": "10%",
            "className": "center"
        },
        {
            "data": "PIC",
            "width": "10%",
        },
        {
            "data": "EquipmentStatusID",
            "mRender": function (data, type, full) {
                //1 - Calibrated, 2 - Calibration In Progress, 3 - Due For Calibration, 4 - Calibration Not Required, 5 - Faulty, 6 - Missing
                if (data == 1)
                    return '<span class="badge bgc-green-50 c-green-700 p-10 lh-0 tt-c badge-pill">Calibrated</span>';
                else if (data == 2)
                    return '<span class="badge bgc-orange-50 c-orange-800 p-10 lh-0 tt-c badge-pill">Calibration In Progress</span>';
                else if (data == 3)
                    return '<span class="badge bgc-red-50 c-red-700 p-10 lh-0 tt-c badge-pill">Due For Calibration</span>';
                else if (data == 4)
                    return '<span class="badge bgc-green-50 c-green-700 p-10 lh-0 tt-c badge-pill">Calibration Not Required</span>';
                else if (data == 5)
                    return '<span class="badge bgc-red-50 c-red-700 p-10 lh-0 tt-c badge-pill">Faulty</span>';
                else if (data == 6)
                    return '<span class="badge bgc-red-50 c-red-700 p-10 lh-0 tt-c badge-pill">Missing</span>';
            },
            "width": "5%",
            "className": "center"
        }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            //if (aData.EquipmentStatus == 3 || aData.EquipmentStatus == 4 || aData.EquipmentStatus == 6) {
            //    $('td', nRow).css('background-color', '#e2e3e5');
            //    $('td', nRow).css('color', '#383d41');
            //    $('td', nRow).css('border-top', '1px solid #ffffff');
            //    $('td', nRow).css('cursor', 'not-allowed');
            //}
        },
        "dom": '<"row"<"col-md-2"l><"col-md-8"B><"col-md-2"f>><"fullWidthFilter"rt><"fullWidthFilter"ip>',
        "buttons": [
            {
                extend: 'excel',
                text: 'Download Excel',
                //footer: true,
                //title: 'Information Management System',
                //messageTop: function () {
                //    return topMessage;
                //},
            },
            {
                text: 'Upload Equipment',
                attr: {
                    id: 'btnUploadEquipment'
                },
                //action: function (e, dt, node, config) {
                //    console.log(e);
                //}
            }
        ],
        "initComplete": function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
            $('.buttons-pdf').addClass("btn btn-primary");
            $('.buttons-excel').addClass("btn btn-primary");
            $('#btnUploadEquipment').addClass("btn btn-primary");
            $('#btnUploadEquipment').click(function () {
                $('#equipmentUpload').trigger('click');
            });

            $('#tblData tbody').on('click', 'td a.details-control', function () {
                var tr = $(this).closest('tr');
                var row = tblEq.row(tr);
                var idx = row.data().ID;

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                    detailRows.splice(idx, 1);
                }
                else {
                    // Open this row
                    row.child(childRowFileFormat(row.data())).show();
                    tr.addClass('shown');
                    detailRows.push(idx);
                }
            });
        },
        "searchable": true,
        "orderable": true,
        "responsive": {
            "details": false
        },
        "order": [[3, 'asc']],
        "autoWidth": true,
    });

    $('.form-control input-sm').attr('style', 'width:auto;');
}
/*data tables end*/

/*ajax*/
function retrieveData(id) {
    var dataStr = { ID: $("#hdnID").val() };
    setMode(1);
    $.ajax({
        url: reqDataUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#txtEQID").val(data.EQID);
            $("#txtEquipmentName").val(data.EquipmentName);
            $("#selProject").val(data.ProjectID);
            $("#txtDescription").val(data.Description);
            $("#txtSupplier").val(data.Supplier);
            $("#txtRegisterDate").val(new DateFormatter().formatDate(new Date(data.RegisterDate), "Y-m-d"));
            $("#txtPIC").val(data.PIC);

            $("#txtEquipmentRange").val(data.EquipmentRange);
            $("#txtBrand").val(data.Brand);
            $("#txtModel").val(data.Model);
            $("#txtSerialNumber").val(data.SerialNumber);
            $("#txtResolution").val(data.Resolution);
            $("#txtPermissibleError").val(data.PermissibleError);
            $("#txtUncertainty").val(data.Uncertainty);
            $("#txtLocation").val(data.Location);
            $("#txtRemarks").val(data.Remarks);
            $("#txtProperty").val(data.Property);
            $("#selEquipmentType").val(data.EquipmentTypeID);

            $("#txtCalibrationStatus").val(0);
            //1 - Calibrated, 2 - Calibration In Progress, 3 - Due For Calibration, 4 - Calibration Not Required, 5 - Faulty, 6 - Missing
            if (data.EquipmentStatusID == 1 || data.EquipmentStatusID == 2 || data.EquipmentStatusID == 3) {
                $("#txtCalibrationStatus").val(1);
                $("#txtCalibrationDate").val(new DateFormatter().formatDate(new Date(data.CalibrationExpiryDate), "Y-m-d"));
                $("#txtCalibrationSupplier").val(data.CalibrationSupplier);
                $("#txtCalibrationFrequency").val(data.CalibrationFrequency);
            }

            assemblyLineDropdown(data.AssemblyLineID);
            calibrationShowForm();
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function deleteData(id) {
    var dataStr = { ID: $("#hdnID").val() };
    $.ajax({
        url: reqDeleteUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            tblEq.ajax.reload();
            alert('Data Deleted Successfully');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var _msg = '';
            if (xhr.responseJSON.Message)
                _msg = "\nReason:" + xhr.responseJSON.Message;
            alert('Data Deleted Failed' + _msg);
        }
    });
}

function addNewData() {
    var dataStr = {
        EQID: $("#txtEQID").val(),
        EquipmentName: $("#txtEquipmentName").val(),
        EquipmentTypeID: $("#selEquipmentType").val(),
        ProjectID: $("#selProject").val(),
        AssemblyLineID: $("#selAssemblyLine").val(),
        Description: $("#txtDescription").val(),
        Supplier: $("#txtSupplier").val(),
        RegisterDate: $("#txtRegisterDate").val(),
        PIC: $("#txtPIC").val(),
        CalibrationRequired: $("#txtCalibrationStatus").val(),
        CalibrationExpiryDate: $("#txtCalibrationDate").val(),
        CalibrationSupplier: $("#txtCalibrationSupplier").val(),
        EquipmentStatusID: 1,
        EquipmentRange: $("#txtEquipmentRange").val(),
        Brand: $("#txtBrand").val(),
        Model: $("#txtModel").val(),
        SerialNumber: $("#txtSerialNumber").val(),
        CalibrationFrequency: $("#txtCalibrationFrequency").val(),
        Resolution: $("#txtResolution").val(),
        PermissibleError: $("#txtPermissibleError").val(),
        Uncertainty: $("#txtUncertainty").val(),
        Location: $("#txtLocation").val(),
        Remarks: $("#txtRemarks").val(),
        Property: $("#txtProperty").val(),
    };

    if ($("#txtCalibrationStatus").val() == 0) {
        dataStr.CalibrationExpiryDate = "";
        dataStr.CalibrationSupplier = "";
        dataStr.txtCalibrationFrequency = "";
    }

    $.ajax({
        url: reqAddUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            tblEq.ajax.reload();
            alert('Data Added Successfully');
            closeModal();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#lblMsgError').text(xhr.responseJSON.Message);
            alert('Data Added Failed');
        }
    });
}

function editData() {
    var dataStr = {
        ID: $("#hdnID").val(),
        EQID: $("#txtEQID").val(),
        EquipmentName: $("#txtEquipmentName").val(),
        EquipmentTypeID: $("#selEquipmentType").val(),
        ProjectID: $("#selProject").val(),
        AssemblyLineID: $("#selAssemblyLine").val(),
        Description: $("#txtDescription").val(),
        Supplier: $("#txtSupplier").val(),
        RegisterDate: $("#txtRegisterDate").val(),
        PIC: $("#txtPIC").val(),
        CalibrationRequired: $("#txtCalibrationStatus").val(),
        CalibrationExpiryDate: $("#txtCalibrationDate").val(),
        CalibrationSupplier: $("#txtCalibrationSupplier").val(),
        EquipmentStatusID: 1,
        EquipmentRange: $("#txtEquipmentRange").val(),
        Brand: $("#txtBrand").val(),
        Model: $("#txtModel").val(),
        SerialNumber: $("#txtSerialNumber").val(),
        CalibrationFrequency: $("#txtCalibrationFrequency").val(),
        Resolution: $("#txtResolution").val(),
        PermissibleError: $("#txtPermissibleError").val(),
        Uncertainty: $("#txtUncertainty").val(),
        Location: $("#txtLocation").val(),
        Remarks: $("#txtRemarks").val(),
        Property: $("#txtProperty").val(),
    };

    if ($("#txtCalibrationStatus").val() == 0) {
        dataStr.CalibrationExpiryDate = "";
        dataStr.CalibrationSupplier = "";
        dataStr.txtCalibrationFrequency = "";
    }

    $.ajax({
        url: reqEditUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            tblEq.ajax.reload();
            alert('Data Edited Successfully');
            closeModal();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Data Edited Failed');
            $('#lblMsgError').text(xhr.responseJSON.Message);
        }
    });
}

function sendForCalibrationEquipment(id, days) {
    var dataStr = {
        ID: id,
        EstimateCalibrationDays: days
    };

    $.ajax({
        url: reqSendCaliEquipmentUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            tblEq.ajax.reload();
            alert('"Calibration In Progress" status successfully updated.');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to update "Calibration In Progress" status.');
        }
    });
}

function getProject(projectID) {
    $.ajax({
        url: reqGetProjectUrl,
        type: "GET",
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#selProject option").remove();
            $("#selProject").append('<option value="">Select</option>');
            $("#selFProject option").remove();
            $("#selFProject").append('<option value="">Select</option>');

            $.each(data, function (key, value) {
                $("#selProject").append('<option value="' + value.ID + '">' + value.ProjectName + '</option>');

                if (value.ID == projectID)
                    $("#selFProject").append('<option value="' + value.ID + '" selected="selected">' + value.ProjectName + '</option>');
                else
                    $("#selFProject").append('<option value="' + value.ID + '">' + value.ProjectName + '</option>');
            });

            getEquipmentType();
            loadTable(reqTableUrl, authToken);
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function getAssemblyLine(AssemblyLineID) {
    var dataStr = {
        ID: $("#selProject").val(),
    }
    $.ajax({
        url: reqAssemblyLineUrl,
        type: "POST",
        data: JSON.stringify(dataStr),
        asnyc: false,
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#selAssemblyLine option").remove();
            $("#selAssemblyLine").append('<option value="">Select</option>');

            $.each(data, function (key, value) {
                if (value.ID == AssemblyLineID)
                    $("#selAssemblyLine").append('<option value="' + value.ID + '" selected="selected">' + value.AssemblyLine1 + '</option>');
                else
                    $("#selAssemblyLine").append('<option value="' + value.ID + '">' + value.AssemblyLine1 + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function getEquipmentType() {
    $.ajax({
        url: reqGetEquipmentTypeUrl,
        type: "GET",
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#selEquipmentType option").remove();
            $("#selEquipmentType").append('<option value="">Select</option>');
            $("#selFEquipmentType option").remove();
            $("#selFEquipmentType").append('<option value="">Select</option>');

            $.each(data, function (key, value) {
                $("#selEquipmentType").append('<option value="' + value.ID + '">' + value.EquipmentType1 + '</option>');

                //if (value.ID == EquipmentTypeID)
                //    $("#selFEquipmentType").append('<option value="' + value.ID + '" selected="selected">' + value.EquipmentType + '</option>');
                //else
                //    $("#selFEquipmentType").append('<option value="' + value.ID + '">' + value.EquipmentType + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function uploadEquipmentFile() {
    $('#btnUploadEquipment span').text("Loading");
    $('#btnUploadEquipment').attr("disabled", "disabled");
    var fileData = new FormData();
    if ($('#equipmentUpload')[0]) {
        var _countFile = 0;
        var _checkFileType = true;
        var _randNo = Math.floor(Math.random() * 9000) + 1000;

        $.each($('#equipmentUpload')[0].files, function (key, value) {
            var _newFilename = _randNo + "-" + value.name;

            if (value.size > 2000000)
                alert('File (' + value.name + ') exceed 2Mb limit.');

            fileData.append('file', value, _newFilename);
            _countFile++;
            var ext = _newFilename.split('.').pop().toLowerCase();
            if ($.inArray(ext, ['csv']) == -1) {
                _checkFileType = false;
                return;
            }
        });

        if (!_checkFileType) {
            alert('Invalid file type\nOnly allow file type .csv');
            return;
        }
    }

    $.ajax({
        headers: {
            "Authorization": "Bearer " + authToken
        },
        url: reqUploadEquipmentUrl,
        processData: false,
        contentType: false,
        data: fileData,
        type: 'POST'
    }).done(function (result) {
        tblEq.ajax.reload();

        if (result["Message"])
            alert(result["Message"]);
        else
            alert("Data Added Successfully");

        $('#equipmentUpload').val("");
        $('#btnUploadEquipment span').text("Upload Equipment");
        $('#btnUploadEquipment').removeAttr("disabled");
    }).fail(function (xhr, ajaxOptions, thrownError) {
        if (xhr.responseJSON["Message"])
            alert('Failed to add data.\nReason : ' + xhr.responseJSON["Message"]);
        else
            alert('Failed to add data');
        $('#equipmentUpload').val("");
        $('#btnUploadEquipment span').text("Upload Equipment");
        $('#btnUploadEquipment').removeAttr("disabled");
    });
}
/*ajax end*/

/* form validation*/
function setSendForCalibration(value) {
    var days = prompt("Estimated lead time (Days)", "");
    while (days != null) {
        if (Number.isNaN(Number(days))) {
            alert("Input must be a number.")
            days = prompt("Estimated lead time (Days)", "");
        }
        else {
            //alert("Exit loop.");
            sendForCalibrationEquipment(value, days)
            break;
        }
    }
}

function setEditSequence(value) {
    $("#hdnID").val(value);
    $("#lblModal").text("Edit Equipment");
    //$("#divCalibrationForm").attr("hidden", "hidden");
    retrieveData(value);
}

function setHiddenFieldDelete(value) {
    $("#hdnID").val(value);

    var proceed = confirm('Are you sure you want to delete data?');
    if (proceed)
        deleteData();
}

var validateData = $("#infoForm").validate({
    rules: {
        txtEQID: {
            required: true,
            maxlength: 100
        },
        txtEquipmentName: {
            required: true,
            maxlength: 200
        },
        selProject: {
            required: true,
        },
        selAssemblyLine: {
            required: true,
        },
        selEquipmentType: {
            required: true,
        },
        txtDescription: {
            required: true,
            maxlength: 500
        },
        txtSupplier: {
            required: true,
            maxlength: 200
        },
        txtRegisterDate: {
            required: true
        },
        txtPIC: {
            required: true,
            maxlength: 200
        },
        txtCalibrationStatus: {
            required: true
        },
        txtCalibrationFrequency: {
            required: function () {
                if ($("#txtCalibrationStatus").val() == 1)
                    return true;
                else
                    return false;
            },
            maxlength: 100
        },
        txtCalibrationDate: {
            required: function () {
                if ($("#txtCalibrationStatus").val() == 1)
                    return true;
                else
                    return false;
            },
        },
        txtCalibrationSupplier: {
            required: function () {
                if ($("#txtCalibrationStatus").val() == 1)
                    return true;
                else
                    return false;
            },
            maxlength: 200
        },
    },
    messages: {
        txtEQID: {
            required: "Please enter the equipment ID.",
            maxlength: "You have reached maximum length 100."
        },
        txtEquipmentName: {
            required: "Please enter the equipment name.",
            maxlength: "You have reached maximum length 200."
        },
        selProject: {
            required: "Please select project."
        },
        selAssemblyLine: {
            required: "Please select assembly line."
        },
        selEquipmentType: {
            required: "Please select Equipment Type.",
        },
        txtDescription: {
            required: "Please enter the description.",
            maxlength: "You have reached maximum length 500."
        },
        txtSupplier: {
            required: "Please enter the supplier.",
            maxlength: "You have reached maximum length 200."
        },
        txtRegisterDate: {
            required: "Please select register date."
        },
        txtPIC: {
            required: "Please enter the PIC.",
            maxlength: "You have reached maximum length 200."
        },
        txtCalibrationStatus: {
            required: "Please select calibration status."
        },
        txtCalibrationFrequency: {
            required: "Please enter calibration frequency.",
            maxlength: "You have reached maximum length 100."
        },
        txtCalibrationDate: {
            required: "Please select calibration status."
        },
        txtCalibrationSupplier: {
            required: "Please enter calibration supplier.",
            maxlength: "You have reached maximum length 200."
        },
    },
    submitHandler: function (form) {
        if (modalMode == 0) {
            $("#txtEquipmentStatus").attr("disabled", false);
            $("#txtCalibrationStatus").attr("disabled", false);
            addNewData();
        }
        else if (modalMode == 1) {
            $("#txtEquipmentStatus").attr("disabled", false);
            $("#txtCalibrationStatus").attr("disabled", false);
            editData();
        }
    },
});

function childRowFileFormat(d) {
    // `d` is the original data object for the row
    var returnStr = "";
    returnStr += '<div class="row">'
    $.each(d, function (key, value) {
        if (key.indexOf('ID') == -1) {
            if (key.toLowerCase().substring(key.length - 4) == "date") {
                returnStr += '<div class="col-md-2">' + key.replace(/([a-z][A-Z])/g, '  b$1').trim() + ':' + (value ? value.replace("T", " ") : "") + '</div>';
            }
            else if (isGuid(value)) {
                //do nothing
            }
            else {
                returnStr += '<div class="col-md-2">' + key.replace(/([A-Z])/g, ' $1').trim() + ':' + value + '</div>';
            }
        }
    });
    returnStr += '</div>'
    return returnStr;
}

function isGuid(value) {
    var regex = /[a-f0-9]{8}(?:-[a-f0-9]{4}){3}-[a-f0-9]{12}/i;
    var match = regex.exec(value);
    return match != null;
}

function setMode(mode) {
    validateData.resetForm();
    resetMessage();
    resetField();
    showModal();
    modalMode = mode;
}

function showModal() {
    $('#newModal').modal("show");
    $('#newModal').addClass("show");
    $('#newModal').css("display", "block");
}

function closeModal() {
    $('#newModal').modal('hide');
}
/* form validation end*/

/*reset messages*/
function resetMessage() {
    $('#lblMsgSuccess').text('');
    $('#lblMsgError').text('');
}

function resetField() {
    var resetForm = [$("#txtEQID"), $("#txtEquipmentName"), $("#selProject"), $("#selAssemblyLine"), $("#txtDescription"), $("#txtSupplier"), $("#txtRegisterDate"),
        $("#txtPIC"), $("#txtCalibrationStatus"), $("#txtCalibrationDate"), $("#txtCalibrationSupplier"), $("#txtEquipmentRange"), $("#txtBrand"),
        $("#txtRemarks"), $("#txtModel"), $("#txtSerialNumber"), $("#txtResolution"), $("#txtPermissibleError"), $("#txtUncertainty"), $("#txtLocation"),
        $("#txtProperty"), $("#txtCalibrationFrequency"), $("#selEquipmentType")];

    $.each(resetForm, function (key, value) {
        value.val("");
        value.removeClass("error");
    });
}
/*reset messages*/

//project dropdown
function assemblyLineDropdown(AssemblyLineID) {
    if ($("#selProject").val() == "") {
        $("#selAssemblyLine option").remove();
        $("#selAssemblyLine").append('<option value="">Select Project First</option>');
        $("#selAssemblyLine").attr("disabled", "disabled");
    }
    else {
        $("#selAssemblyLine").removeAttr("disabled");
        getAssemblyLine(AssemblyLineID);
    }
}

function calibrationShowForm() {
    if ($('#txtCalibrationStatus').val() == "1") {
        $("#txtCalibrationDate").parent().removeAttr("hidden");
        $("#txtCalibrationSupplier").parent().removeAttr("hidden");
        $("#txtCalibrationFrequency").parent().removeAttr("hidden");
    }
    else {
        $("#txtCalibrationDate").parent().attr("hidden", "hidden");
        $("#txtCalibrationSupplier").parent().attr("hidden", "hidden");
        $("#txtCalibrationFrequency").parent().attr("hidden", "hidden");
    }
}

$(document).ajaxComplete(function () {
    $('[data-toggle="tooltip"]').tooltip();
});