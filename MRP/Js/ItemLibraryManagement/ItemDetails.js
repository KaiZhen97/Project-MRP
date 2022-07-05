
$(document).ready(function () {
    modalMode = $("#hdnMode").val();
    handledID = $("#hdnID").val();

    getCategoryList();
    getSupplierList();
    getPurchaserList();
    retrieveData();

    $("#btnDuplicate").click(function () {
        window.location.href = "/Views/ItemLibraryManagement/AddNewItem/" + 2 + '/' + handledID;
    });

    $("#btnEdit").click(function () {
        window.location.href = "/Views/ItemLibraryManagement/EditItem/" + 1 + '/' + handledID;
    });

    $("#btnDelete").click(function () {
        $("#deleteItem").modal("show");
    });

    $("#btnNoDeleteItem")
    .add(".btn-close").click(function () {
        $("#deleteItem").modal("hide");
        $("#DeleteItemRemark").val("");
    });
    
    $("#btnYesDeleteItem").click(function () {
        var dataStr = {
            ID: handledID,
            DeletedRemark: $("#DeleteItemRemark").val(),
        }
        $("#deleteItem").modal("hide");

        deleteItemLibrary(reqDeleteItemLibraryUrl, authToken, dataStr)
        .then((data) => {

            window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";

            ShowInfo("Item Library List Reload", "ItemLibrary list has been reloaded with latest data.");
            ShowPass("Delete ItemLibrary Success", data.Message || "Delete successfully!");
        })
        .catch((error) => {
            ShowError("Fail To Delete Item Library", error.responseText || error);
        })
    });

    //$("#btnPrevious").click(function () {
    //    window.location.href = "/Views/ItemLibraryManagement/ItemDetails/" + handledID;
    //})

    //$("#btnNext").click(function () {
    //    //window.location.href = "/Views/ItemLibraryManagement/ItemDetails/" + handledID;
    //})
})

function getCategoryList() {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqCategoryListUrl,
            type: 'Get',
            async: false,
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data) {
                $.each(data.data, function (i, val) {
                    $("#inputCategory").append("<option value=" + data.data[i].ID + ">" + data.data[i].CategoryName + "</option>");
                })
                console.log("success category", data.data);
            },
            error: function (xhr) {
                console.log("error category", data.data);
            }
        })
    })
}

function getSupplierList() {
    return new Promise((resolve, error) => {

        $.ajax({
            url: reqSupplierListUrl,
            type: 'GET',
            async: false,
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data) {
                $.each(data.data, function (i, val) {
                    //$("#inputCategory").append("<option value=" + data.data[i].CategoryID + ">" + data.data[i].CategoryName + "</option>");
                    $("#inputSupplierName").append("<option value=" + data.data[i].SupplierName + ">" + data.data[i].SupplierName + "</option>");
                    //$("#inputPurchaser2").append("<option value=" + data.data[i].Purchaser1AccessID + ">" + data.data[i].Purchaser1 + "</option>");
                    //$("#inputPurchaser1").append("<option value=" + data.data[i].Purchaser2AccessID + ">" + data.data[i].Purchaser2 + "</option>");
                })
                console.log("sucess supplier", data.data);
            },
            error: function (xhr) {
                console.log("error supplier", data.data);
            }
        })
    })
}

function getPurchaserList() {
    return new Promise((resolve, error) => {

        $.ajax({
            url: reqPurchaserListUrl,
            type: 'GET',
            async: false,
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data) {
                $.each(data, function (i, val) {
                //$.each(data.data, function (i, val) {
                    $("#inputPurchaser1").append("<option value=" + data[i].AccessID + ">" + data[i].StaffName + "</option>");
                    $("#inputPurchaser2").append("<option value=" + data[i].AccessID + ">" + data[i].StaffName + "</option>");
                    //$("#inputPurchaser1").append("<option value=" + data.data[i].ID + ">" + data.data[i].Purchaser1 + "</option>");
                })
                //console.log("success purchaser1.", data.data);
                console.log("success purchaser", data);
            },
            error: function (xhr) {
                console.log("error purchasing", data.data);
            }
        })
    })
}

function retrieveData() {
    var dataStr = {
        ID: handledID,
    };
    $.ajax({
        url: reqItemLibraryIdUrl,
        type: "Post",
        data: JSON.stringify(dataStr),
        dataType: "Json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#inputCategory").val(data.CategoryID);
            $("#inputIPN").val(data.IPN);
            $("#inputManufacturer").val(data.Manufacturer);
            $("#inputMPN").val(data.MPN);
            $("#inputItemDescription").val(data.ItemDescription);
            $("#inputSupplierName").val(data.SupplierName);
            $("#inputUOM").val(data.UOM);
            $("#inputUnitPrice").val(data.UnitPrice);
            $("#inputUnitPriceDiscount").val(data.UnitPriceDiscount);
            $("#inputMinAmountPerOrder").val(data.MinAmountPerOrder);
            $("#inputRequiredSN").val(data.RequiredSN);
            $("#inputTariff").val(data.Tariff);
            $("#inputRequiredCalibration").val(data.RequiredCalibration);
            $("#inputMoreDetails").val(data.MoreDetails);
            $("#inputDeliveryTerm").val(data.DeliveryTerm);
            $("#inputQuotationDate").val(data.QuotationDate);
            $("#inputQuotationValidyity").val(data.QuotationValidity);
            $("#inputStdLeadTime").val(data.Std_LeadTime_Days);
            //$("#inputPurchaser1").val(data.Purchaser1);
            //$("#inputPurchaser2").val(data.Purchaser2);
            $("#inputPurchaser1").val(data.Purchaser1AccessID);
            $("#inputPurchaser2").val(data.Purchaser2AccessID);
            $("#inputIsDefault").val(data.IsDefault);
            $("#inputRemark").val(data.Remark);

            $("#detailItemDescription").text(data.ItemDescription);
            $("#detailIPN").text(data.IPN);
            $("#detailCreatedBy").append(data.CreatedByStaffName);
            $("#detailCreatedBy").append(data.StaffName);
            $("detailLastUpdatedDate").append(data.LastUpdatedDate);

            var createdDate = new Date(data.CreatedDate);
            var updatedDate = new Date(data.LastUpdatedDate);
            var dateNow = new Date();
            var dateBetween1 = new Date(dateNow - createdDate) / (1000 * 60 * 60 * 24);
            var dateBetween2 = new Date(dateNow - updatedDate) / (1000 * 60 * 60 * 24);
            var numberDay1 = Math.round(dateBetween1);
            var numberDay2 = Math.round(dateBetween2);

            $("#detailCreatedDate").text(numberDay1);
            $("#detailLastUpdatedDate").text(numberDay2)

            console.log(data);
        },
        error: function (xhr) {
            error(xhr);
        }
    })
};

//function getUserName() {
//    //debugger;
//    var dataStr = {
//        ID: handledID,
//    };
//    $.ajax({
//        url: reqUserNameUrl,
//        type: "Post",
//        data: JSON.stringify(dataStr),
//        dataType: "json",
//        headers: {
//            "Content-Type": "application/json; charset=utf-8",
//            "Authorization": "Bearer " + authToken,
//        },
//        success: function (data) {
//            $("#detailCreatedBy").append(data.CreatedBy);
//            //$("#detailCreatedBy").text(data.StaffName);
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            error(xhr);
//        }
//    })
//};

//function getUserName(reqUserNameUrl, authToken, dataStr) {
//    return new Promise((resolve, error) => {
//        $.ajax({
//            url: reqUserNameUrl,
//            type: "Post",
//            data: JSON.stringify(dataStr),
//            dataType: "json",
//            headers: {
//                "Content-Type": "application/json; charset=utf-8",
//                "Authorization": "Bearer " + authToken,
//            },
//            success: function (data, status, xhr) {
//                $("#detailCreatedBy").append(data.CreatedBy);
//                //resolve(data);
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                error(xhr);
//            }
//        })
//    })
//};

function deleteItemLibrary(reqDeleteItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteItemLibraryUrl,
            type: "Post",
            data: JSON.stringify(dataStr),
            dataType: "json",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
};
