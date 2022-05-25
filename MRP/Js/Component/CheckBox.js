$(document).ready(function () {
    var allCheckBox = $("input[type='checkbox']");
    var selectAllCheckBox = $("input[type='checkbox']._select-all");

    $.each(allCheckBox, function (index, element) {
        var t = $(element)

        // Wrap and add icon
        if (!t.parent().hasClass("_checkbox-cont")) {
            t.wrap("<div class='_checkbox-cont'></div>").parent().append("<i class='fa-solid fa-check _checkbox-checkmark'></i>");
        }
    })


    $.each(selectAllCheckBox, function (index, element) {
        var t = $(element);

        t.change(function () {
            console.log('changed');

            var currVal = $(this).prop('checked');
            var parentTbl = t.parents().find('table');

            CheckBox_SelectAll(parentTbl, currVal);
        })
    })
});

function CheckBoxWrapper(inputHtml) {
    return `<div class='_checkbox-cont'>${inputHtml}<i class='fa-solid fa-check _checkbox-checkmark'></i></div>`;
}

function CheckBox_SelectAll(targetTbl, targetValue) {
    var targets = $(targetTbl).find("tbody td input[type='checkbox']");

    targets.map((index, element) => {
        var t = $(element)

        t.prop('checked', targetValue);
        CheckBox_CheckModify(t, targetValue);
    })

}

function CheckBox_Select(name) {
    $("input[type='checkbox'][name^='" + name + "']").each(function (i, obj) {
        $(this).prop('checked', true);
    });
}

function CheckBox_Uncheck(name) {
    $("input[type='checkbox'][name^='" + name + "']").each(function (i, obj) {
        $(this).prop('checked', false);
    });
}

function CheckBox_CheckModify(raw_target, targetValue) {
    var target = $(raw_target)

    if ((target.hasClass("curr-db-val") && !targetValue) || (!target.hasClass("curr-db-val") && targetValue)) {
        target.parent().parent().parent().addClass("modify");
    }
    else {
        target.parent().parent().parent().removeClass("modify");
    }
}