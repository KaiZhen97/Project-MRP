$(document).ready(function () {
    var loadingIconConstruct = "<i class='fa-solid fa-spinner loading-icon'></i>";

    $("#form1").submit(function (event) {
        event.preventDefault();
        $("#Button_Login").click();
    })

    $("#Input_Username, #Input_Password").on("keyup", (e) => {
        if (e.keyCode == 13) {
            e.preventDefault;
            $("#Button_Login").click();
        }
    });

    $("#Button_ForgotPassword").on("click", (e) => {
        e.preventDefault();
    });

    $("#Button_Login").on("click", (e) => {
        $("#Button_Login").addClass("busy");
    });

    // Append Loading Icon
    $("button").append(loadingIconConstruct);
});