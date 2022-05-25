$(document).ready(function () {

    loadCheckedItem(reqListUrl, authToken);

    $("input[name='first']").change(function () {

        var name = $(this).attr("name");
        if (this.checked) {
            selectCheckBox(name);
        }
        else {
            uncheckCheckBox(name);
        }
    });

    $("input[name^='first']").change(function () {
        checkRowModify(this, $(this).prop("checked"));

        generateValueData();
    });

    $("form button").on("click", (e) => {
        e.preventDefault();
    });

    $("#SelectAll").on("click", function (e) {
        var currVal = $(this).prop('checked');

        $("input[name^='first']").each((key, value) => {
            var target = $(value);

            console.log(target.parent().parent().prop('nodeName'));

            checkRowModify(target, currVal);

            target.prop("checked", currVal);
        });
    });

    $("#btnReset").on("click", function (e) {
        e.preventDefault();

        var btn = $(this);

        var promise = new Promise((ok, error) => {
            btn.addClass("busy");

            $("input[name^='first']").each((key, value) => {
                var target = $(value)

                if (target.hasClass("curr-db-val"))
                    target.prop("checked", true);
                else
                    target.prop("checked", false);

                target.closest("tr").removeClass("modify");
            })

            setTimeout(() => { ok() }, 100);
        })
            .finally(() => {
                btn.removeClass("busy");
            })
    })

    $("#btnSave").on("click", function (e) {
        console.log(this, e);
        $(e).addClass("busy");
    });
});

function loadCheckedItem(reqListUrl, authToken) {
    $.ajax({
        url: reqListUrl,
        type: "GET",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            var contrutStr = "";
            $.each(data.data, function (key, value) {
                var target = $("input[type=checkbox][value='" + value.TableName + "']");
                target.prop('checked', true);
                target.addClass('curr-db-val');

                contrutStr = contrutStr + value.TableName + ",";
            });
            $("#" + hiddenChkBoxID).val(contrutStr.substring(0, contrutStr.length - 1));
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
        }
    });
}

function selectCheckBox(name) {
    $("input[type='checkbox'][name^='" + name + "']").each(function (i, obj) {
        $(this).prop('checked', true);
    });
}

function uncheckCheckBox(name) {
    $("input[type='checkbox'][name^='" + name + "']").each(function (i, obj) {
        $(this).prop('checked', false);
    });
}

function generateValueData() {
    var contrutStr = "";
    $("input[type='checkbox']").each(function (i, obj) {
        if (this.checked) {
            if (contrutStr == "")
                contrutStr = $(this).val();
            else
                contrutStr = contrutStr + "," + $(this).val();
        }
    });

    $("#" + hiddenChkBoxID).val(contrutStr);
}

function checkRowModify(raw_target, currVal) {
    var target = $(raw_target)

    if ((target.hasClass("curr-db-val") && !currVal) || (!target.hasClass("curr-db-val") && currVal)) {
        target.parent().parent().parent().addClass("modify");
    }
    else {
        target.parent().parent().parent().removeClass("modify");
    }
}