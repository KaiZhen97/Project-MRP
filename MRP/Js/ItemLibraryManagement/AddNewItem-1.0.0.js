var ItemLibraryList;
var modalMode;

$(document).ready(function () {
    modalMode = $("#hdnMode").val();
    handledID = $("#hdnID").val();

    getCategoryList();
    getSupplierList();
    getPurchaserList();
    //retrieveData();

    //$("select").val("0");

    $.contextMenu({
        selector: "#inputIPN",
        className: "css-title-toolbox",
        items: {
            "Unlock Input": {
                name: "Unlock Input",
                icon: "fa-unlock-keyhole",
                callback: function () {
                    if ($("#inputIPN").attr("disabled")) {
                        $("#inputIPN")
                            .removeAttr("disabled")
                            .css("background", "#ECECEC");
                        $("#lock").remove();
                    }
                }
            }
        }
    });

    $(".btn-close")
    .add("#btnCancelClose")
    .add("#btnNoExistAdd")
    .add("#btnNoExistEdit")
    .add("#btnNoConfirmEdit")
    .click(function () {
        $("#Close").modal("hide");
        $("#ExistAdd").modal("hide");
        $("#ExistEdit").modal("hide");
        $("#ConfirmEdit").modal("hide");
    });

    $("#btnDiscardClose").click(function () {
        $("#Close").modal("hide");
        window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
    });

    $("#btnConfirm").click(function () {
        if (modalMode == 0) {
            addItemLibrary()
            //if ($("#inputIPN").val() == 0) {
            //    addItemLibrary();
            //    console.log("1", $("#inputIPN").val("1"));
            //}
            //else if ($("#inputIPN").val() == 1) {
            //    $("#ExistAdd").modal("show");
            //    $("#btnYesExistAdd").click(function (){
            //        addItemLibrary();
            //        console.log("2", $("#inputIPN").val(2));
            //    })
            //}
            //var dataStr = {
            //    IsDraft: 0,
            //    CategoryID: $("#inputCategory").val(),
            //    IPN: $("#inputIPN").val(),
            //    Manufacturer: $("#inputManufacturer").val(),
            //    MPN: $("#inputMPN").val(),
            //    ItemDescription: $("#inputItemDescription").val(),
            //    SupplierName: $("#inputSupplierName").val(),
            //    UOM: $("#inputUOM").val(),
            //    UnitPrice: $("#inputUnitPrice").val(),
            //    UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
            //    MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
            //    RequiredSN: $("#inputRequiredSN").val(),
            //    Tariff: $("#inputTariff").val(),
            //    RequiredCalibration: $("#inputRequiredCalibration").val(),
            //    MoreDetails: $("#inputMoreDetails").val(),
            //    DeliveryTerm: $("#inputDeliveryTerm").val(),
            //    QuotationDate: $("#inputQuotationDate").val(),
            //    QuotationValidity: $("#inputQuotationValidity").val(),
            //    Std_LeadTime_Days: $("#inputStdLeadTime").val(),
            //    Purchaser1: $("#inputPurchaser1").val(),
            //    Purchaser2: $("#inputPurchaser2").val(),
            //    IsDefault: $("#inputIsDefault").val(),
            //    Remark: $("#inputRemark").val(),
            //};

            .then((data) => {
                ShowPass("Add Item Success", data.Message || "Add successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Add Item Library", error.responseText || error);
            })
        }
        else if (modalMode == 1) {
            editItemLibrary()
            //if ($("#inputIPN").val() == 0) {
            //    $("#ConfirmEdit").modal("show");
            //    $("#btnYesConfirmEdit").click(function () {
            //        editItemLibrary();
            //    })
            //}
            //else if ($("#inputIPN").val() == 1) {
            //    $("#ExistEdit").modal("show");
            //    $("#btnYesExistEdit").click(function () {
            //        editItemLibrary();
            //    })
            //}
            .then((data) => {
                ShowPass("Edit Item Success", data.Message || "Edit successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Edit Item Library", error.responseText || error);
            })
        }
        else if (modalMode == 2) {
            addItemLibrary()
            //if ($("#inputIPN").val() == 0) {
            //    addItemLibrary();
            //}
            //else if ($("#inputIPN").val() == 1) {
            //    $("#ExistAdd").modal("show");
            //    $("#btnYesExistAdd").click(function () {
            //        addItemLibrary();
            //    })
            //}
            .then((data) => {
                ShowPass("Add Item Success", data.Message || "Add successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Add Item Library", error.responseText || error);
            })
        }
    });

    $("#btnSaveDraft")
    .add("#btnSaveDraftClose")
    .click(function () {
        if (modalMode == 0) {
            addDraftItem()
            .then((data) => {
                ShowPass("Add Draft Item Success", data.Message || "Add successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Add Draft Item", error.responseText || error);
            })
        }
        else if (modalMode == 1) {
            saveDraftItem()
            .then((data) => {
                ShowPass("Save Draft Item Success", data.Message || "Edit successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Save Draft Item", error.responseText || error);
            })
        }
        else if (modalMode == 2) {
            addDraftItem()
            .then((data) => {
                ShowPass("Add Draft Success", data.Message || "Add successfully!");
                ShowInfo("Item Library List Reload", "Item Library List reloaded with latest data");
                window.location.href = "/Views/ItemLibraryManagement/ItemLibraryList";
            })
            .catch((error) => {
                ShowError("Fail To Add Draft Item", error.responseText || error);
            })
        }
    });

    $("#btnAdd").click(function () {
        $("#KeyTechSpec").append(
            '<div class="input col-6 mb-3">' +
            '<div class="input-group row">' +
            '<input class="spec_left" style="color: white; width: 20%; flex-grow: 1"/>' +
            '<input class="spec_mid" style="width: 30%; flex-grow: 1"/>' +
            '<div class="spec_right" style="width: auto;">' +
            '<i class="fa-solid fa-circle-minus" id="btnRemoveKTS" style="color: red;"></i>' +
            '</div></div></div>')
    });

    $("#KeyTechSpec").on("click", "#btnRemoveKTS", function () {
        $(this).parents(".input").remove();
    });

    $("#btnAddPWPItem").click(function () {
        Drawer_Show("#AddPWPItem");
    });

    //$.ajax({
    //    url: reqPurchaserListUrl,
    //    type: 'GET',
    //    dataType: 'json',
    //    headers: {
    //        "Content-Type": "application/json; charset=utf-8",
    //        "Authorization": "Bearer " + authToken,
    //    },
    //    success: function (data) {
    //        $.each(data, function (i, val) {
    //            console.log(val);
    //            $("#inputPurchaser1").append("<option value=" + data.data[i].Purchaser1AccessID + ">" + data.data[i].Purchaser1 + "</option>");
    //            $("#inputPurchaser2").append("<option value=" + data.data[i].Purchaser2AccessID + ">" + data.data[i].Purchaser2 + "</option>");
    //        })
    //    },
    //    //})
    //});


    //if (modalMode == 1) {
    //    $(document).attr("title", "Edit Item");
    //    $("#txtTitle").text("Edit Item - " + handledID);
    //    retrieveData()
    //    //getCategoryList()
    //    .then((data) => {
    //        getCategoryList()
    //        getSupplierList()
    //        getPurchaserList()
    //        //retrieveData();
    //        console.log("data", data);
    //    })
    //    //.catch((error) => {
    //    //    console.log("error", error);
    //    //})

    //    //getSupplierList()
    //    //.then((data) => {
    //    //    retrieveData();
    //    //})

    //    //getPurchaserList()
    //    //.then((data) => {
    //    //    retrieveData();
    //    //})
    //}
    ////else if (modalMode == 2) {
    ////    duplicateData();
    ////};


    if (modalMode == 1) {
        $(document).attr("title", "Edit Item");
        $("#txtTitle").text("Edit Item - " + handledID);
        retrieveData();
    }
    else if (modalMode == 2) {
        duplicateData();
    };
        //Promise.all([getCategoryList(), getSupplierList(), getPurchaserList(),retrieveData()])
        //////retrieveData()

        //////getCategoryList()
        //////.then((data) => {
        //.then((data) => {
        //    //getCategoryList()
        //    //getSupplierList();
        //    //getPurchaserList()
        //    //retrieveData();

        //    console.log("sdata", data);
        //})
        //.catch((error) => {
        //    console.log("edata", data);
        //})
        //.finally(() => {
        //    console.log("fdata", data);
        //})
        //.then((data) => {
        //    getPurchaserList();
        //})
        //.then((data) => {
        //    retrieveData();
        //})

        //.catch((error) => {
        //    console.log("error", error);
        //})

        //getSupplierList()
        //.then((data) => {
        //    //retrieveData();
        //    console.log("data", data);
        //})

        //getPurchaserList()
        //.then((data) => {
        //    //retrieveData();
        //    console.log("data", data);
        //})

        //retrieveData()
        //.then((data) => {
        //    console.log("data", data);
        //})
    //}

    $("#btnReset").click(function () {
        if (modalMode == 0)
            $("input, select, textarea").val("");
        else if (modalMode == 1)
            retrieveData();
        else if (modalMode == 2)
            duplicateData();
    });
});

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
                console.log("success purchaser2", data);
            },
            error: function (xhr) {
                console.log("error purchasing", data.data);
            }
        })
    })
}

function retrieveData() {
    return new Promise((resolve, error) => {
        var dataStr = {
            ID: handledID,
        };
        $.ajax({
            url: reqItemLibraryIdUrl,
            type: "Post",
            data: JSON.stringify(dataStr),
            //await: true,
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
                console.log("data", data);
            },
            error: function (xhr) {
                error(xhr);
            }
        })
    })
};

function duplicateData() {
    return new Promise((resolve, error) => {
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
                $("#inputIPN").val("");
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
                console.log("data", data);
            },
            error: function (xhr) {
                error(xhr);
            }
        })
    })
};

function addItemLibrary() {
    var dataStr = {
        IsDraft: 0,
        CategoryID: $("#inputCategory").val(),
        IPN: $("#inputIPN").val(),
        Manufacturer: $("#inputManufacturer").val(),
        MPN: $("#inputMPN").val(),
        ItemDescription: $("#inputItemDescription").val(),
        SupplierName: $("#inputSupplierName").val(),
        UOM: $("#inputUOM").val(),
        UnitPrice: $("#inputUnitPrice").val(),
        UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
        MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
        RequiredSN: $("#inputRequiredSN").val(),
        Tariff: $("#inputTariff").val(),
        RequiredCalibration: $("#inputRequiredCalibration").val(),
        MoreDetails: $("#inputMoreDetails").val(),
        DeliveryTerm: $("#inputDeliveryTerm").val(),
        QuotationDate: $("#inputQuotationDate").val(),
        QuotationValidity: $("#inputQuotationValidity").val(),
        Std_LeadTime_Days: $("#inputStdLeadTime").val(),
        Purchaser1: $("#inputPurchaser1").val(),
        Purchaser2: $("#inputPurchaser2").val(),
        IsDefault: $("#inputIsDefault").val(),
        Remark: $("#inputRemark").val(),
    }
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddItemLibraryUrl,
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
            },
        })
    })
};

function editItemLibrary() {
    var dataStr = {
        ID: handledID,
        IsDraft: 0,
        CategoryID: $("#inputCategory").val(),
        IPN: $("#inputIPN").val(),
        Manufacturer: $("#inputManufacturer").val(),
        MPN: $("#inputMPN").val(),
        ItemDescription: $("#inputItemDescription").val(),
        SupplierName: $("#inputSupplierName").val(),
        UOM: $("#inputUOM").val(),
        UnitPrice: $("#inputUnitPrice").val(),
        UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
        MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
        RequiredSN: $("#inputRequiredSN").val(),
        Tariff: $("#inputTariff").val(),
        RequiredCalibration: $("#inputRequiredCalibration").val(),
        MoreDetails: $("#inputMoreDetails").val(),
        DeliveryTerm: $("#inputDeliveryTerm").val(),
        QuotationDate: $("#inputQuotationDate").val(),
        QuotationValidity: $("#inputQuotationValidity").val(),
        Std_LeadTime_Days: $("#inputStdLeadTime").val(),
        Purchaser1: $("#inputPurchaser1").val(),
        Purchaser2: $("#inputPurchaser2").val(),
        IsDefault: $("#inputIsDefault").val(),
        Remark: $("#inputRemark").val(),
    }
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
};

function addDraftItem() {
    var dataStr = {
        IsDraft: 1,
        CategoryID: $("#inputCategory").val(),
        IPN: $("#inputIPN").val(),
        Manufacturer: $("#inputManufacturer").val(),
        MPN: $("#inputMPN").val(),
        ItemDescription: $("#inputItemDescription").val(),
        SupplierName: $("#inputSupplierName").val(),
        UOM: $("#inputUOM").val(),
        UnitPrice: $("#inputUnitPrice").val(),
        UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
        MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
        RequiredSN: $("#inputRequiredSN").val(),
        Tariff: $("#inputTariff").val(),
        RequiredCalibration: $("#inputRequiredCalibration").val(),
        MoreDetails: $("#inputMoreDetails").val(),
        DeliveryTerm: $("#inputDeliveryTerm").val(),
        QuotationDate: $("#inputQuotationDate").val(),
        QuotationValidity: $("#inputQuotationValidity").val(),
        Std_LeadTime_Days: $("#inputStdLeadTime").val(),
        Purchaser1: $("#inputPurchaser1").val(),
        Purchaser2: $("#inputPurchaser2").val(),
        IsDefault: $("#inputIsDefault").val(),
        Remark: $("#inputRemark").val(),
    }
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddDraftItemUrl,
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

function saveDraftItem() {
    var dataStr = {
        ID: handledID,
        IsDraft: 1,
        CategoryID: $("#inputCategory").val(),
        IPN: $("#inputIPN").val(),
        Manufacturer: $("#inputManufacturer").val(),
        MPN: $("#inputMPN").val(),
        ItemDescription: $("#inputItemDescription").val(),
        SupplierName: $("#inputSupplierName").val(),
        UOM: $("#inputUOM").val(),
        UnitPrice: $("#inputUnitPrice").val(),
        UnitPriceDiscount: $("#inputUnitPriceDiscount").val(),
        MinAmountPerOrder: $("#inputMinAmountPerOrder").val(),
        RequiredSN: $("#inputRequiredSN").val(),
        Tariff: $("#inputTariff").val(),
        RequiredCalibration: $("#inputRequiredCalibration").val(),
        MoreDetails: $("#inputMoreDetails").val(),
        DeliveryTerm: $("#inputDeliveryTerm").val(),
        QuotationDate: $("#inputQuotationDate").val(),
        QuotationValidity: $("#inputQuotationValidity").val(),
        Std_LeadTime_Days: $("#inputStdLeadTime").val(),
        Purchaser1: $("#inputPurchaser1").val(),
        Purchaser2: $("#inputPurchaser2").val(),
        IsDefault: $("#inputIsDefault").val(),
        Remark: $("#inputRemark").val(),
    }
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqSaveDraftItemUrl,
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
