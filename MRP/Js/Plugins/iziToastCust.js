function ShowInfo(title, msg, position, timeout_ms) {
    iziToast.show({
        theme: 'customDark',
        icon: "fas fa-info",
        title: title,
        message: msg,
        position: position ? position : "topRight",
        transitionIn: "flipInX",
        layout: 2,
        zindex: 3000,
        backgroundColor: '#BCE2FF',
        iconColor: '#0E4168',
        titleColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        timeout: timeout_ms ? timeout_ms: 3000,
    });
}

function ShowPass(title, msg, position, timeout_ms) {
    iziToast.show({
        theme: 'customDark',
        icon: "fas fa-check",
        title: title,
        message: msg,
        position: position ? position : "topRight",
        transitionIn: "flipInX",
        layout: 2,
        zindex: 3000,
        backgroundColor: '#BBFFD6',
        iconColor: '#17FC73',
        titleColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        timeout: timeout_ms ? timeout_ms : 3000,
    });
}

function ShowAlert(title, msg, position, timeout_ms) {
    iziToast.show({
        theme: 'customDark',
        icon: "fas fa-exclamation-triangle",
        title: title,
        message: msg,
        position: position ? position : "topRight",
        transitionIn: "flipInX",
        layout: 2,
        zindex: 3000,
        backgroundColor: '#FFF0C7',
        iconColor: '#FFC934',
        titleColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        timeout: timeout_ms ? timeout_ms : 3000,
    });
}

function ShowError(title, msg, position, timeout_ms) {
    iziToast.show({
        theme: 'customDark',
        icon: "fas fa-exclamation-triangle",
        title: title,
        message: msg,
        position: position ? position : "topRight",
        transitionIn: "flipInX",
        layout: 2,
        zindex: 3000,
        backgroundColor: '#FFE1E1',
        iconColor: '#FF0D0D',
        titleColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        messageColor: '#1C1C1C',
        timeout: timeout_ms ? timeout_ms : 3000,
    });
}