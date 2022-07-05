//var ItemLibraryList;
var modalMode;
var handledID;

function TableItemLibraryList(reqTableItemLibraryListUrl, authToken) {
    ItemLibraryList = $('#Table_ItemLibraryList').DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqTableItemLibraryListUrl,
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
                    "data": "ID",
                    "visible": true
                },
                { "data": "CategoryName", },
                { "data": "IPN", },
                { "data": "Manufacturer", },
                { "data": "MPN" },
                { "data": "ItemDescription", },
                //{ "data": "SupplierName", },
                //{ "data": "Currency", },
                //{ "data": "UnitPrice", },
                //{
                //    "data": "Std_LeadTime_Days",
                //    "mRender": function (data, type, full) {
                //        return data + " Days";
                //    }
                //},
                //{ "data": "IsDefault", },
                { "data": "CreatedDate", },
                { "data": "LastUpdatedDate", },
            ],

            "initComplete": function (settings, json) {
                $('[data-toggle="tooltip"]').tooltip();
            },

            "searchable": true,
            "orderable": true,
            "responsive": true,
        }
    )
};

$(document).ready(function () {
    TableItemLibraryList(reqTableItemLibraryListUrl, authToken);
    draftItemNo();
    handledID = $("#hdnID").val();

    $.contextMenu({
        selector: "#Table_ItemLibraryList tbody tr",
        className: "css-title-toolbox",
        items: {
            "Item Details": {
                name: "Item Details",
                icon: "fa-circle-info",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    handledID = rowData.ID;

                    window.location.href = "/Views/ItemLibraryManagement/ItemDetails/" + handledID;
                }
            },
            "Duplicate": {
                name: "Duplicate",
                icon: "fa-copy",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    handledID = rowData.ID;

                    setMode(2);
                    window.location.href = "/Views/ItemLibraryManagement/AddNewItem/" + modalMode + '/' + rowData.ID;
                }
            },
            "edit": {
                name: "Edit",
                icon: "fa-edit",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    handledID = rowData.ID;

                    setMode(1);
                    window.location.href = "/Views/ItemLibraryManagement/EditItem/" + modalMode + '/' + rowData.ID;
                }
            },
            "delete": {
                name: "Delete",
                icon: "fa-trash",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    handledID = rowData.ID;

                    $("#deleteItem").modal("show");
                }
            }
        }
    });

    $("#btnYesDeleteItem").click(function () {
        $("#deleteItem").submit();
        var dataStr = {
            ID: handledID,
            DeletedRemark: $("#DeleteItemRemark").val(),
        }
        $("#deleteItem").modal("hide");

        deleteItemLibrary(reqDeleteItemLibraryUrl, authToken, dataStr)
        .then((data) => {

            ItemLibraryList.ajax.reload();

            ShowInfo("Item Library List Reload", "ItemLibrary list has been reloaded with latest data.");
            ShowPass("Delete ItemLibrary Success", data.Message || "Delete successfully!");
        })
        .catch((error) => {
            ShowError("Fail To Delete Item Library", error.responseText || error);
        })
        $("input").val("");
    });

    
    $("#btnYesDeleteDraft").click(function () {
        var dataStr = {
            ID: handledID,
        }

        $("#deleteDraft").modal("hide");

        deleteDraftItem(reqDeleteDraftItemUrl, authToken, dataStr)
        .then((data) => {

            draftItemNo();

            ShowInfo("Draft Item List Reload", "Draft Item list has been reloaded with latest data.");
            ShowPass("Delete Draft Item Success", data.Message || "Delete successfully!");
        })
        .catch((error) => {
            ShowError("Fail To Delete Draft Item Library", error.responseText || error);
        })
    });

    $("#keywordSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#draftItemList").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        })
    });

    $(".btn-close").click(function () {
        $("#draftList").modal("hide");
        $("#deleteDraft").modal("hide");
        $("#deleteItem").modal("hide");
        $("input, select").val("");
    });

    $("#btnNoDeleteItem").click(function () {
        $("#deleteItem").modal("hide");

        $("input").val("");
    });

    $("#btnNoDeleteDraft").click(function () {
        $("#deleteDraft").modal("hide");
        $("#draftList").modal("show");
    });

    $("#btnDeleteDraft").click(function () {
        deleteDrafts(data);
    });

    $("#btnDraftItemLibrary").click(function () {
        draftItemLists();
    });
})

function deleteDrafts(data) {
    $("#deleteDraft").modal("show");
    $("#draftList").modal("hide");

    setHandledID(data);
};

function setHandledID(data) {
    handledID = data;
};

function setMode(mode) {
    modalMode = mode;
};

function AddNewItem() {
    window.location.href = "/Views/ItemLibraryManagement/AddNewItem";

    var dataStr = {
        CategoryID: $("#inputCategory").val(""),
        IPN: $("#inputIPN").val(""),
        Manufacturer: $("#inputManufacturer").val(""),
        MPN: $("#inputMPN").val(""),
        ItemDescription: $("#inputItemDescription").val(""),
        SupplierName: $("#inputSupplierName").val(""),
        UOM: $("#inputUOM").val(""),
        UnitPrice: $("#inputUnitPrice").val(""),
        UnitPriceDiscount: $("#inputUnitPriceDiscount").val(""),
        MinAmountPerOrder: $("#inputMinAmountPerOrder").val(""),
        RequiredSN: $("#inputRequiredSN").val(""),
        Tariff: $("#inputTariff").val(""),
        RequiredCalibration: $("#inputRequiredCalibration").val(""),
        MoreDetails: $("#inputMoreDetails").val(""),
        DeliveryTerm: $("#inputDeliveryTerm").val(""),
        QuotationDate: $("#inputQuotationDate").val(""),
        QuotationValidity: $("#inputQuotationValidyity").val(""),
        Std_LeadTime_Days: $("#inputStdLeadTime").val(""),
        Purchaser1: $("#inputPurchaser1").val(""),
        Purchaser2: $("#inputPurchaser2").val(""),
        IsDefault: $("#inputIsDefault").val(""),
        Remark: $("#inputRemark").val(""),
    }
};

function addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddItemLibraryUrl,
            type: "POST",
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
};

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

function draftItemLists() {
    $.ajax({
        url: reqDraftItemLibraryUrl,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        dataType: 'JSON',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken,
        },
        data: function (d) {
            return d;
        },
        success: function (data) {
            setMode(1);
            for (i in data.data) {
                $("#draftItemList")
                .append('<li><div class="d-flex justify-content-between">' +
                    '<div class="d-flex flex-column p-1 m-0 ms-2">' +
                    '<p class="d-flex m-0 mt-2" id="draftDetail" style="font-size: 20px; font-weight: 500;">' + data.data[i].ID + ' : ' + data.data[i].IPN + ' | ' + data.data[i].SupplierName + '</p>' +
                    '<p class="d-flex m-0 mb-1" id="draftDescription" style="font-size: 16px;">' + data.data[i].ItemDescription + '</p>' +
                    '<p class="d-flex m-0 mt-1" id="draftCreatedDate" style="font-size: 14px;">Create on ' + data.data[i].CreatedDate + '</p></div>' +
                    '<div class="d-flex flex-column justify-content-center m-0 me-2">' +
                    '<a class="clickableLink" id="btnEditDraft" href="/Views/ItemLibraryManagement/EditItem/' + modalMode + '/' + data.data[i].ID + '"><i class="fa fa-edit"></i>' +
                    '<a class="clickableLink" id="btnDeleteDraft" style="" href="#" onclick="deleteDrafts(' + data.data[i].ID + ');"><i class="fa-solid fa-trash-can">' +
                    '</i></div></div></li>'
                );
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            error(xhr);
        }
    })
    $("#draftItemList").empty();
    $("#draftList").modal("show");
};

function draftItemNo() {
    $.ajax({
        url: reqDraftItemLibraryUrl,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        dataType: 'JSON',
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken,
        },
        data: function (d) {
            return d;
        },
        success: function (data) {
            $("#DraftNumber").text(data.data.length);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            error(xhr);
        }
    })
};

function addDraftItem(reqAddDraftItemUrl, authToken, dataStr) {
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

function saveDraftItem(reqSaveDraftItemUrl, authToken, dataStr) {
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

function deleteDraftItem(reqDeleteDraftItemUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteDraftItemUrl,
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
