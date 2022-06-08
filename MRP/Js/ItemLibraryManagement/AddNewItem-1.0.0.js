var ItemLibraryList;

$(document).ready(function () {
    $("#btnResetAdd").click(function () {
        $("input, select").val("");
    });

    $(".btn-close").click(function () {
        $("#closeAdd").modal("hide");
        $("#confirmAdd").modal("hide");
    })

    $("#btnSaveDraftCloseAdd").click(function () {

        $("#closeAdd").modal("hide");
    });

    $("#btnDiscardCloseAdd").click(function () {
        //$("input, select").val("");
        $("#closeAdd").modal("hide");
        //document.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
        window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
    });

    $("#btnCancelCloseAdd").click(function () {
        $("#closeAdd").modal("hide");
    });


    $("#btnSaveDraftAdd").click(function () {

        var dataStr = {
            IsDraft: 1,
            CategoryID: 1,
            IPN: $("#inputIPN").val(),
            Manufacturer: $("#inputManufacturer").val(),
            MPN: $("#inputMPN").val(),
            ItemDescription: $("#inputItemDescription").val(),
            SupplierName: $("#inputSupplierName").val(),

            UOM: $("#inputUOM").val(),

            //
            UnitPrice: $("#inputUnitPrice").val(),
            //
            UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
            //
            MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
            RequiredSN: $("#inputRequiredSN").val(),
            Tariff: $("#inputTariff").val(),
            RequiredCalibration: $("#inputRequiredCalibration").val(),
            MoreDetails: $("#inputMoreDetails").val(),
            DeliveryTerm: $("#inputDeliveryTerm").val(),
            //
            QuotationDate: $("#inputQuotationDate").val(),
            QuotationValidity: $("#inputQuotationValidity").val(),
            //
            Std_LeadTime_Days: $("#inputStdLeadTime").val(),

            Purchaser1: $("#inputPurchaser1").val(),
            Purchaser2: $("#inputPurchaser2").val(),
            IsDefault: $("#inputIsDefault").val(),
            Remark: $("#inputRemark").val(),
        }

        //console.log(dataStr);


        addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr)
        .then((data) => {

            window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            
            ShowPass("Save Draft Success", data.Message || "Save successfully!");
            ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
        })
        .catch((error) => {
            ShowError("Fail To Save Draft Item Library", error.responseText || error);
        })
    });

    
    $("#btnNoConfirmAdd").click(function () {
        $("#confirmAdd").modal("hide");
    });


    $("#btnYesConfirmAdd").click(function () {
        
        var dataStr = {
            CategoryID: 1,
            IPN: $("#inputIPN").val(),
            Manufacturer: $("#inputManufacturer").val(),
            MPN: $("#inputMPN").val(),
            ItemDescription: $("#inputItemDescription").val(),
            SupplierName: $("#inputSupplierName").val(),

            UOM: $("#inputUOM").val(),

            //
            UnitPrice: $("#inputUnitPrice").val(),
            //
            UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
            //
            MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
            RequiredSN: $("#inputRequiredSN").val(),
            Tariff: $("#inputTariff").val(),
            RequiredCalibration: $("#inputRequiredCalibration").val(),
            MoreDetails: $("#inputMoreDetails").val(),
            DeliveryTerm: $("#inputDeliveryTerm").val(),
            //
            QuotationDate: $("#inputQuotationDate").val(),
            QuotationValidity: $("#inputQuotationValidity").val(),
            //
            Std_LeadTime_Days: $("#inputStdLeadTime").val(),
            
            Purchaser1: $("#inputPurchaser1").val(),
            Purchaser2: $("#inputPurchaser2").val(),
            IsDefault: $("#inputIsDefault").val(),
            Remark: $("#inputRemark").val(),

            ////SupplierCode: $("#inputCurrency").val(),
            //Currency: $("#inputCurrency").val(),
            //KeyTech: $("#inputKeyTech").val(),
            //IsDraft: $("#inputIsDraft").val(),

            //CreateDate: $("#inputCreateDate").val(),
            //CreateBy: $("#inputCreateBy").val(),
            //LastUpdateDate: $("#inputLastUpdateDate").val(),
            //LastUpdateBy: $("#inputLastUpdateBy").val(),
            //DeleteDate: $("#inputDeleteDate").val(),
            //DeleteBy: $("#inputDeleteBy").val(),
            //AppKey: $("#inputAppKey").val(),
        }

        console.log(dataStr);

        addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr)
        .then((data) => {
            ShowPass("Add Item Success", data.Message || "Add successfully!");

            ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");

            window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
        })
        .catch((error) => {
            ShowError("Fail To Add Item Library", error.responseText || error);
        })
        $("#confirmAdd").modal("hide");
        //ItemLibraryList.ajax.reload();
    })


    $("#btnAddPWPItem").click(function () {
        //("#AddPWPItem").toggleClass("hidden");
        Drawer_Show("#AddPWPItem");

    })


    
    
    //$("#inputPuchaser1").click(function () {
        $.ajax({
            url: reqPurchasingListUrl,
            type: 'GET',
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data) {

                $.each(data, function (i, data) {
                    //debugger
                    //var select = $("#inputPurchaser1");
                    //var option = "<option value=" + data.AccessID + ">" + data.StaffName + "</option>";
                    //select.append(option);

                    $("#inputPurchaser1").append("<option value=" + data.AccessID + ">" + data.StaffName + "</option>");
                    $("#inputPurchaser2").append("<option value=" + data.AccessID + ">" + data.StaffName + "</option>");
                    console.log(data);
                    //console.log(select);
                    //console.log(option);
                })
            }
            //error: function () {
            //    error(xhr);
            //}

            //success: function (data, status, xhr) {
            //    resolve(data);
            //},
            //error: function (xhr, ajaxOptions, thrownError) {
            //    error(xhr);
            //}
        })
    })

//})

//function loadItemLibraryID(reqItemLibraryIdUrl, authToken, dataStr) {
//    return new Promise((resolve, error) => {
//        $.ajax({
//            url: reqItemLibraryIdUrl,
//            type: "Post",
//            dataType: "json",
//            data: JSON.stringify(dataStr),
//            headers: {
//                "Content-Type": "application/json; charset=utf-8",
//                "Authorization": "Bearer " + authToken,
//            },
//            success: function (data, status, xhr) {

//                $("#inputCategoryID").val(data.CategoryID);
//                $("#inputIPN").val(data.IPN || "");
//                $("#inputManufacturer").val(data.Manufacturer || "");
//                $("#inputMPN").val(data.MPN || "");
//                $("#inputItemDescription").val(data.ItemDescription);
//                $("#inputSupplierName").val(data.SupplierName || "");
//                $("#inputUOM").val(data.UOM);
//                $("#inputUnitPrice").val(data.UnitPrice);
//                $("#inputUnitPriceDiscount").val(data.UnitPriceDiscount || "");
//                $("#inputMinAmountPerOrder").val(data.MinAmountPerOrder);
//                $("#inputRequiredSN").val(data.RequiredSN || "");
//                $("#inputTariff").val(data.Tariff || "");
//                $("#inputRequiredCalibration").val(data.RequiredCalibration || "");
//                $("#inputMoreDetails").val(data.MoreDetails || "");
//                $("#inputDeliveryTerm").val(data.DeliveryTerm || "");
//                $("#inputQuotationDate").val(data.QuotationDate || "");
//                $("#inputQuotationValidyity").val(data.QuotationValidity || "");
//                $("#inputStdLeadTime").val(data.Std_LeadTime_Days || "");
//                $("#inputPurchaser1").val(data.Purchaser1 || "");
//                $("#inputPurchaser2").val(data.Purchaser2 || "");
//                $("#inputIsDefault").val(data.IsDefault || "");
//                $("#inputRemark").val(data.Remark || "");

//                resolve();
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                error(xhr);
//            }
//        })
//    })
//}

function addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddItemLibraryUrl,
            type: "Post",
            dataType: "JSON",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer "+authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
}

function editItemLibrary(reqEditItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditItemLibraryUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
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
}
