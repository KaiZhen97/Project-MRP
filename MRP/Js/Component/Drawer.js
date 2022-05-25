$(document).ready(function () {
    var overlayConstruct = "<div class='_overlay'></div>";
    var loadingConstruct = "<div class='_loading'><i class='fa-solid fa-spinner'></i></div>"

    var drawerGroup = $(".drawer-container");

    $.each(drawerGroup, function (key, value) {
        var drawer = $(value);
        var drawerHtml = $(value).html();
        var drawerTitle = drawer.attr("title");

        drawer.html("");
        drawer.tooltip('disable');

        drawer.append(overlayConstruct);
        drawer.append(loadingConstruct);

        drawer.append(
            `<div class='_body'>
                <div class='_title'>
                    <h5>${drawerTitle}</h5>
                    <button class="text-button"><i class="fa-solid fa-xmark"></i></button>
                </div>
                <div class='_content'>
                    ${drawerHtml}
                </div>
            </div>
            `);

        $(drawer.find('.text-button, ._overlay')).on('click', function () {
            drawer.removeClass('show');
        });
    });
});

function Drawer_ChangeTitle(targetDrawer, title) {
    $(targetDrawer).find('._title h5').html(title);
}

function Drawer_Show(targetDrawer, isLoading) {
    $(targetDrawer).addClass("show");

    if (isLoading) {
        $(targetDrawer).addClass("loading");
    }
    else {
        $(targetDrawer).removeClass("loading");
    }
}

function Drawer_Hide(targetDrawer) {
    $(targetDrawer).removeClass("show");
    $(targetDrawer).removeClass("loading");
}

function Drawer_ResetInput(targetDrawer) {
    $(targetDrawer).find('input').val('');
}