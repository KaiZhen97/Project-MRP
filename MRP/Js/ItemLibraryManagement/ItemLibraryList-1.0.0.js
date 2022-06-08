var ItemLibraryList;
//var PurchasingList;
var ModalMode = 0;

function loadTable_ItemLibraryList(reqTableItemLibraryListUrl, authToken) {
    ItemLibraryList = $('#Table_ItemLibraryList').DataTable(
        {
            "processing": true,
            "serverSide": false,
            //"columnDefs": [
            //    { "targets": 0, "searchable": false, "className": "never" },
            //    { "className": "center", "targets": 1, "width": "2%", "orderable": false },
            //    { "targets": 2, "orderable": true, "width": "20%", "className": "left" }
            //],
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
                { "data": "SupplierName", },
                { "data": "Currency", },
                { "data": "UnitPrice", },
                {
                    "data": "Std_LeadTime_Days",
                    "mRender": function (data, type, full) {
                        return data + " Days";
                    }
                },
                { "data": "IsDefault", },
                { "data": "CreatedDate", },
                { "data": "LastUpdatedDate", },
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


$(document).ready(function () {
    loadTable_ItemLibraryList(reqTableItemLibraryListUrl, authToken);
    //ItemLibraryList.ajax.reload();
    
    $.contextMenu({
        selector: "#Table_ItemLibraryList tbody tr",
        className: "css-title-toolbox",
        items: {
            "Item Details": {
                name: "Item Details",
                icon: "fa-solid fa-circle-info",
                //id: "ItemDetails",

                //callback: function (key, options) {

                //}
            },

            "Duplicate": {
                name: "Duplicate",
                icon: "fa-solid fa-copy",

                //callback: function (key, options) {

                //}
            },
            "edit": {
                name: "Edit",
                icon: "fa-edit",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenItemLibraryID}`).val(rowData.ID)

                    setMode(0);
                    window.location.href = "/Views/ItemLibraryManagement/AddNewItem/" + rowData.ID;

                    loadItemlibraryID(reqItemLibraryIdUrl, authToken, itemLibrary)
                    .catch((error) => {
                        ShowError("Load Item Library Failed", "Fail");
                    })
                }
            },
            "delete": {
                name: "Delete",
                icon: "fa-solid fa-trash",
                callback: function (key, options) {

                    var rowData = ItemLibraryList.row(options.$trigger).data();

                    var itemLibrary = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenItemLibraryID}`).val(rowData.ID);

                    $("#deleteItem").modal("show");

                    //console.log("rowdata " + rowData);
                    //console.log("hdn" + $(`#${hiddenItemLibraryID}`));

                    loadItemLibraryID(reqItemLibraryIdUrl, authToken, itemLibrary)
                    .catch((error) => {
                        ShowError("Load Item Details Failed", "Fail"); 
                    })
                }
            }
        }
    })


    $("#btnYesDeleteItem").click(function () {
        $("#deleteItem").submit();

        var dataStr = {
            ID: $(`#${hiddenItemLibraryID}`).val(),
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
    })

    
    $("#btnYesDeleteDraft").click(function () {
        $("#deleteDraft").submit();

        var dataStr = {
            ID: $(`#${hiddenItemLibraryID}`).val(),
            //DeletedRemark: $("#DeleteItemRemark").val(),
        }
        $("#deleteDraft").modal("hide");

        deleteDraftItem(reqDeleteDraftItemUrl, authToken, dataStr)
        .then((data) => {

            ItemLibraryList.ajax.reload();

            ShowInfo("Item Library List Reload", "ItemLibrary list has been reloaded with latest data.");
            ShowPass("Delete Draft Item Library Success", data.Message || "Delete successfully!");
        })
        .catch((error) => {
            ShowError("Fail To Delete Draft Item Library", error.responseText || error);
        })
        //$("input").val("");
    })


    //$("#keywordSearch").("keyup", function() {
    //    var value = $(this.val();
    //    $("#ulDraftList li").filter(function() {
    //        $(this).toggle($(this).text().indexOf(value) > -1)
    //    })
    //})


    //$("#search").on("keyup", function () {
    //    $(".users").html("");
    //    var val = $.trim(this.value);
    //    if (val) {
    //        val = val.toLowerCase();
    //        $.each(usersArray, function (_, obj) {
    //            // console.log(val,obj.name.toLowerCase().indexOf(val),obj)
    //            if (obj.name.toLowerCase().indexOf(val) != -1) {
    //                $(".users").append('<div class="user-card"><span class="user-info">' + obj.name + '</span><br/><img class="user-image" src="' + obj.image + '"/></div>');
    //            }
    //        });
    //    }
    //    $(".not-found").toggle($(".users").find("img").length == 0);
    //});


    //$("#myInput").on("keyup", function() {
    //    var value = $(this).val().toLowerCase();
    //    $("#myTable tr").filter(function() {
    //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    //    });
    //});


    //$("#btnDraftItem").click(function () {
    //    $.ajax
    //    eleteItemLibrary(reqDeleteItemLibraryUrl, authToken, dataStr)
    //})


    //$("#btnDraftItem").click(function () {
    //    $("#deleteDraft").modal("show");
    //})

    //$("#btnDraftItem").click(function () {
    //    $("#draftList").modal("show");
    //})

    $(".btn-close").click(function () {
        $("#draftList").modal("hide");
        $("#deleteDraft").modal("hide");
        $("#deleteItem").modal("hide");
        $("input, select").val("");
    })

    $("#btnNoDeleteItem").click(function () {
        $("#deleteItem").modal("hide");

        //$("input").val("");
    });

    $("#btnNoDeleteDraft").click(function () {
        $("#deleteDraft").modal("hide");
        $("#draftList").modal("show");
    });

    $("#btnDeleteDraft").click(function () {
        $("#deleteDraft").modal("show");
    })
})



function AddNewItem() {

    setMode(1);
    window.location.href = "/Views/ItemLibraryManagement/AddNewItem";

    var dataStr = {
        CategoryID: $("#inputCategoryID").val(""),
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

        //Currency: $("#inputCurrency").val(""),
        //IsDraft: $("#inputIsDraft").val(""),
        //KeyTech: $("#inputKeyTech").val(""),
        //CreateDate: $("#inputCreateDate").val(""),
        //CreateBy: $("#inputCreateBy").val(""),
        //LastUpdateDate: $("#inputLastUpdateDate").val(""),
        //LastUpdateBy: $("#inputLastUpdateBy").val(""),
        //DeleteDate: $("#inputDeleteDate").val(""),
        //DeleteBy: $("#inputDeleteBy").val(""),
        //AppKey: $("#inputAppKey").val(""),
    }
}

function deleteDraft() {
    //document.getElementById("deleteDraft").Modal(show);
    //$("#delete").click(function () {
    $("#btnDeleteDraft").click(function () {
        $("#deleteDraft").modal("show");
        $("#draftList").modal("hide");
    })
}





function setMode(mode) {
    modalMode = mode;
}

//function searchDraft() {
//    var input, filter, ul, li, a, i, txtValue;
//}

function loadItemLibraryID(reqItemLibraryIdUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqItemLibraryIdUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#inputCategoryID").val(data.CategoryID);
                $("#inputIPN").val(data.IPN || "");
                $("#inputManufacturer").val(data.Manufacturer || "");
                $("#inputMPN").val(data.MPN || "");
                $("#inputItemDescription").val(data.ItemDescription);
                $("#inputSupplierName").val(data.SupplierName || "");
                $("#inputUOM").val(data.UOM);
                $("#inputUnitPrice").val(data.UnitPrice);
                $("#inputUnitPriceDiscount").val(data.UnitPriceDiscount || "");
                $("#inputMinAmountPerOrder").val(data.MinAmountPerOrder);
                $("#inputRequiredSN").val(data.RequiredSN || "");
                $("#inputTariff").val(data.Tariff || "");
                $("#inputRequiredCalibration").val(data.RequiredCalibration || "");
                $("#inputMoreDetails").val(data.MoreDetails || "");
                $("#inputDeliveryTerm").val(data.DeliveryTerm || "");
                $("#inputQuotationDate").val(data.QuotationDate || "");
                $("#inputQuotationValidyity").val(data.QuotationValidity || "");
                $("#inputStdLeadTime").val(data.Std_LeadTime_Days || "");
                $("#inputPurchaser1").val(data.Purchaser1 || "");
                $("#inputPurchaser2").val(data.Purchaser2 || "");
                $("#inputIsDefault").val(data.IsDefault || "");
                $("#inputRemark").val(data.Remark || "");
                
                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
                
            }
        })
    })
}

function addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr) {
    //var dataStr = { ID: id };
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

function draftItemLibrary(reqDraftItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDraftItemLibraryUrl,
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

$("#btnDraftItemLibrary").click(function () {
    $.ajax({
        url: reqDraftItemLibraryUrl,
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        dataType: 'JSON',
        //beforeSend: function (xhr) {
        //    xhr.setRequestHeader("Authorization",
        //        "Bearer " + authToken);
        //},
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken,
        },
        data: function (d) {
            return d;
        },
        success: function (data) {
            for (i in data.data) {
                $("#DraftList")
                    .append('<li><div class=" d-flex justify-content-between "><div class=" d-flex flex-column p-2 ">' +
                    '<p style="margin-bottom:0px;font-weight:500; font-size:20px;">' + data.data[i].ID + ' : ' + data.data[i].IPN + ' | ' + data.data[i].SupplierName + '</p>' +
                    '<p class="mb-2" style="font-size:16px; font-weight:400">' + data.data[i].ItemDescription + '</p>' + 
                    '<p style="font-size:14px; color: rgba(80, 80, 80, 0.5);">Create on ' + data.data[i].CreatedDate + '</p></div>' +
                    '<div class=" d-flex flex-column justify-content-center p-2 "><a class="clickableLink" href="AddNewItem/' + data.data[i].ID +
                    '" style="color:#505050;" data-toggle="tooltip" data-placement="top" title="Edit"><i class="fa fa-edit"></i>' +
                    '<a class="clickableLink" href="#" id="btnDeleteDraft" onclick="deleteDraft();" style="color:#505050;" data-toggle="tooltip" data-placement="top" title="Delete"><i class="fa-solid fa-trash-can"></i></div></div></li>');
            }
    
            console.log(data.data);
            console.log(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            error(xhr);
        }
    });
    //$("#ulDraftList").empty();
    //var b = "#draftList";

    //Drawer_ResetInput(b);
    //Drawer_Show(b);
    $("#draftList").modal("show");
})


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
}

function deleteDraftItem(reqDeleteDraftItemUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteIDrafttemUrl,
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
}