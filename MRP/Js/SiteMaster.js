$(document).ready(function () {
    var loadingIconConstruct = "<i class='fa-solid fa-spinner loading-icon'></i>";

    // Check SidePanel State and Initialize Side Panel
    if (localStorage.getItem("SidePanel_PinState") === "pin" && localStorage.getItem("SidePanel_ExpandState") === "expand") {
        $("#SidePanel_Main").addClass("pin");
        ShowSidePanel()
    }

    // Toggle Side Panel
    $("#Button_ExpandSidePanel").on("click", function (e) {
        if ($("#SidePanel_Main").hasClass("collapsed")) {
            ShowSidePanel();
        }
        else {
            CollapseSidePanel();
        }
    })

    // Overlay Collapse Side Panel
    $("#SidePanel_Overlay, #Button_CollapseSidePanel").on("click", function (e) {
        CollapseSidePanel();
    });

    // Pin Side Panel
    $("#Button_PinSidePanel").on("click", function (e) {
        if ($("#SidePanel_Main").hasClass("pin")) {
            UnpinSidePanel();
        }
        else {
            PinSidePanel();
        }
    });

    // Load Nav Menu
    loadNavMenu(reqURL, authToken, redirectLoginURL);

    // Load User Profile
    loadUserProfile(reqProfileURL, authToken);
    retrieveProfileIcon(reqProfileIconURL, authToken);

    // Check Active Nav Item
    CheckActiveNavItem();

    loadNavPath();

    // Append Loading Icon
    $("button").append(loadingIconConstruct);
});

function SignOut() {
    window.location.href = "/Login";
}



/* ============================================================================== */
/* === Side Panel ===*/
/* ============================================================================== */
function PinSidePanel() {
    localStorage.setItem("SidePanel_PinState", "pin");

    // Remove Side Panel Overlay
    $("#SidePanel_Overlay").addClass("disabled");

    // Apply Pin Class
    $("#SidePanel_Main").addClass("pin");
    $("#MainPanel").addClass("expand");
}

function UnpinSidePanel() {
    localStorage.setItem("SidePanel_PinState", "unpin");

    // Show Side Panel Overlay
    $("#SidePanel_Overlay").removeClass("disabled");

    // Remove Pin Class
    $("#SidePanel_Main").removeClass("pin");
    $("#MainPanel").removeClass("expand");
}

function ShowSidePanel() {
    if (localStorage.getItem("SidePanel_PinState") === "pin") {
        $("#MainPanel").addClass("expand");
    }

    if (localStorage.getItem("SidePanel_PinState") === "unpin" || localStorage.getItem("SidePanel_PinState") === null) {
        $("#MainPanel").removeClass("pin");
        $("#SidePanel_Overlay").removeClass("disabled");
    }

    $("#SidePanel_Main").removeClass("collapsed");

    localStorage.setItem("SidePanel_ExpandState", "expand");
}

function CollapseSidePanel() {
    if (localStorage.getItem("SidePanel_PinState") === "pin") {
        $("#MainPanel").removeClass("expand");
    }

    $("#SidePanel_Overlay").addClass("disabled");
    $("#SidePanel_Main").addClass("collapsed");

    $("#MainPanel").removeClass("pin");

    localStorage.setItem("SidePanel_ExpandState", "collapse");
}

function CheckActiveNavItem() {
    var currPath = window.location.pathname.toString();

    $(".nav-item").each((key, value) => {
        if ($(value).attr("href").toString() === currPath) {
            $(value).addClass("active");

            if ($(value).parent().hasClass("collapse")) {
                $(`.nav-item.lv1[href="#${$(value).parent().attr("id")}"]`).removeClass("collapsed");
                $(value).parent().collapse("show");
            }
        }
    });
}



/* ============================================================================== */
/* === Load Menu & User Profile ===*/
/* ============================================================================== */
function loadNavMenu(reqURL, authToken, failRedirectUrl) {
    return $.ajax({
        url: reqURL,
        type: "Get",
        async: false,
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            if (data.data.length == 0) {
                alert("Invalid Login");
                window.location = failRedirectUrl;
                return;
            }

            //1st level menu
            $.each(data.data, function (key, value) {
                if (value.ParentModuleID == null) {
                    var constructStr = "";

                    if (value.ModuleLink == null || value.ModuleLink == "") {
                        // Children Available, dropdown button & dropdown section instead
                        constructStr = `<a class="nav-item lv1 collapsed" data-bs-toggle="collapse" href="#${value.HTMLTagID}" aria-expanded="false" aria-controls="${value.HTMLTagID}">
                                            <i class="${value.HTMLTagIcon}"></i>
                                            <span>${value.ModuleName}</span>
                                            <i class="fa-solid fa-chevron-up _arrow"></i>
                                        </a>`;
                        constructStr += `<div class="collapse" id="${value.HTMLTagID}"></div>`;
                    }
                    else {
                        // No Children, redirect to target page if click
                        constructStr = `<a id="${value.HTMLTagID}" class="nav-item" href="${value.ModuleLink}">
                                            <i class="${value.HTMLTagIcon}"/>
                                            <span>${value.ModuleName}</span>
                                        </a>`;
                    }

                    // Append To Nav
                    $("#SidePanel_Nav").append(constructStr);
                }
            });

            //2nd level menu
            $.each(data.data, function (key, value) {
                if (value.ParentModuleID != null) {
                    var constructStr = "";

                    constructStr = `<a id="${value.HTMLTagID}" class="nav-item lv2" href="${value.ModuleLink}">`;
                    constructStr += value.HTMLTagIcon && `<i class="${value.HTMLTagIcon}"/>`;
                    constructStr += `<span>${value.ModuleName}</span></a>`;

                    // Append To Collapse Section
                    $(`#${value.ParentTagID}`).append(constructStr);
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("Load Menu Error:");
            console.log(xhr);
            console.log(thrownError);
        }
    });
}

function loadUserProfile(reqProfileURL, authToken) {
    return $.ajax({
        url: reqProfileURL,
        type: "Get",
        async: false,
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            $("#TopPanel_Username, #SidePanel_Username").html(data.StaffName);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Fail to load user profile");
            console.log(xhr);

            $("#TopPanel_Username, #SidePanel_Username").html("user");
        }
    });
}

function retrieveProfileIcon(reqProfileIconURL, authToken) {
    $.ajax({
        url: reqProfileIconURL,
        type: "Get",
        dataType: "json",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": "Bearer " + authToken
        },
        success: function (data, status, xhr) {
            if (data === null) {
                $("#SidePanel_ProfileIcon").attr("style", "display: block !important");
                $("#SidePanel_ProfilePic").attr("style", "display: none !important");
            }
            else {
                $("#SidePanel_ProfileIcon").attr("style", "display: none !important");
                $("#SidePanel_ProfilePic").attr("style", "display: block !important");
                $("#SidePanel_ProfilePic").attr("src", "Content/images/Aimflex_Logo_White.png");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


/* ============================================================================== */
/* === Load Navigation Path ===*/
/* ============================================================================== */
function loadNavPath() {
    var path = window.location.pathname.replace("/Views/", "");

    if (path === "/")
        path = "Home";

    $("#Navigation_Path").html(path.replace("/", " / "));
}