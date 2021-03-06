﻿
function msgSucesso(msg) {
    $.confirm({
        title: 'Sucesso!',
        content: msg,
        type: 'green',
        typeAnimated: true,
        buttons: {
            ok: {
                text: 'Ok',
                btnClass: 'btn-success',
                action: function () {
                    window.location.reload();
                }
            },
        }
    });
}

function msgErro(msg) {
    $.confirm({
        title: 'Erro!',
        content: msg,
        type: 'red',
        typeAnimated: true,
        buttons: {
            ok: {
                text: 'Ok',
                btnClass: 'btn-danger',
                action: function () {
                }
            },
        }
    });
}

function msgInfo(msg) {
    $.confirm({
        title: 'Informação!',
        content: msg,
        type: 'blue',
        typeAnimated: true,
        buttons: {
            ok: {
                text: 'Ok',
                btnClass: 'btn-info',
                action: function () {
                }
            },
        }
    });
}