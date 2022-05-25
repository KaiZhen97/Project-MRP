var table = null;

function loadTable(reqTableUrl, authToken) {
    table = $('#PlatformSetupTable').DataTable(
        {
            "processing": true,
            "serverSide": false,
            //"columnDefs": [
            //    { "className": "center", "targets": [0, 1] },
            //    { "targets": 0, "orderable": false }
            //],
            "ajax":
            {
                "url": reqTableUrl,
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
                    "data": "PlatformID",
                    "visible": false,
                },
                { "data": "PlatformName" },
            ],
            "initComplete": function (settings, json) {
                //draw tooltip after the table complete
                $('[data-toggle="tooltip"]').tooltip();
            },
            "searchable": true,
            "orderable": true,
            "responsive": true,
        });


    $(".dataTables_filter input").attr("placeholder", "Filter");
    $('.form-control input-sm').attr('style', 'width:auto;');
}

function loadEditModalTable(reqEditTableUrl, authToken, targetPlatform) {
    // Reset Table
    $("#SelectAll").prop('checked', false);
    $("#Modal_EditPlatform tbody").html("");
    $("#Modal_EditPlatform ._table-title").html(targetPlatform);

    // Load Table
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditTableUrl,
            type: 'Get',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                if (data.data !== null)
                {
                    var firstLevel = data.data;
                    var construcStr = "";
                    var countfirstlevel = 0;

                    firstLevel.map((parentModule) => {
                        var firstlevelChkboxName = "firstlvl" + countfirstlevel.toString();

                        construcStr = construcStr +
                            `<tr>` +
                                `<td>${CheckBoxWrapper(`<input class="table-ckb" type="checkbox" value="${parentModule.RoleID}" name="${firstlevelChkboxName}" id="${firstlevelChkboxName}" />`)}</td>` +
                                `<td colspan="3">${parentModule.RoleName}</td>` +
                            `</tr>`;

                        countfirstlevel++;
                    })

                    $("#Modal_EditPlatform tbody").html(construcStr);
                }
                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

function initEditModalTable() {
    $("input[name^='first']").change(function () {
        CheckBox_CheckModify(this, $(this).prop("checked"));

        generateValueData();
    });
}

function loadCheckedItem(reqCheckedItemUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqCheckedItemUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $.each(data, function(key, value) {
                    var target = $("input[type=checkbox][value='" + value.RoleID + "']");
                    target.prop('checked', true);
                    target.addClass('curr-db-val');
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

function generateValueData() {

    var contrutStr = "";
    $("input[type='checkbox']").each(function (i, obj) {
        if (this.checked) {
            if (contrutStr === "")
                contrutStr = $(this).val();
            else
                contrutStr = contrutStr + "," + $(this).val();
        }
    });
    console.log(contrutStr);
    $("#" + hiddenChkBoxValue_ID).val(contrutStr);
}

function SaveEditPlatform(reqSaveEditUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqSaveEditUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                console.log(data);
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                error(xhr);
            }
        })
    })
}

