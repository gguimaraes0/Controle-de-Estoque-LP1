// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(document).ready(function () {
//    $('#loginModal').modal('show');
//    $(function () {
//        $('[data-toggle="tooltip"]').tooltip()
//    })
//});

function buscaCEP() {
    debugger;
    var cep = document.getElementById("cep").value;
    var regex = /[^0-9]/;
    cep = cep.replace(regex, '');
    if (cep.length > 0) {
        var linkAPI = 'https://viacep.com.br/ws/' + cep + '/json/';

        $.ajax({
            type: 'GET',
            url: linkAPI,
            datatype: "json",
            cache: false,
            beforeSend: function () {
                document.getElementById("logradouro").value = '...';
                document.getElementById("bairro").value = '...';
                document.getElementById("cidade").value = '...';
                document.getElementById("estado").value = '...';
            },
            success: function (dados) {
                if (dados.erro != undefined)  // quando o CEP não existe...
                {
                    alert('CEP não localizado...');
                    document.getElementById("logradouro").value = '';
                    document.getElementById("bairro").value = '';
                    document.getElementById("cidade").value = '';
                    document.getElementById("estado").value = '';
                }
                else // quando o CEP existe
                {
                    document.getElementById("logradouro").value = dados.logradouro;
                    document.getElementById("bairro").value = dados.bairro;
                    document.getElementById("cidade").value = dados.localidade;
                    document.getElementById("estado").value = dados.uf;
                }
            }
        });
    }
}


function fMasc(objeto, mascara) {
    obj = objeto
    masc = mascara
    setTimeout("fMascEx()", 1)
}

function fMascEx() {
    obj.value = masc(obj.value)
}

function mData(data) {
    data = data.replace(/\D/g, "");

    if (data.length > 7)
        data = data.replace(/(\d{2})(\d{2})(\d{4}).*/g, "$1/$2/$3");
    else if (data.length > 4)
        data = data.replace(/(\d{2})(\d{2})(\d)/g, "$1/$2/$3");
    else
        data = data.replace(/(\d{2})(\d)/g, "$1/$2");

    return data
}

function mCPF(cpf) {
    cpf = cpf.replace(/\D/g, "")
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
    cpf = cpf.replace(/(\d{3})(\d{1,2}).*/, "$1-$2")
    return cpf
}



function mCNPJ(cnpj) {
    debugger;
    var cnpj = cnpj.replace(/\D/g, "");
    cnpj = cnpj.replace(/^0/, "");
    if (cnpj.length > 14) {
        cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2}).*/g, "$1.$2.$3/$4-$5");
    } else if (cnpj.length > 12) {
        cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d*).*/g, "$1.$2.$3/$4-$5");
    } else if (cnpj.length > 8) {
        cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d*).*/, "$1.$2.$3/$4");
    } else if (cnpj.length > 5) {
        cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d*).*/, "$1.$2.$3");
    } else if (cnpj.length > 2) {
        cnpj = cnpj.replace(/^(\d{2})(\d*).*/, "$1.$2");
    }

    return cnpj;
}

function formatarCEP(str) {

    var re = /^([\d]{2}).*([\d]{3})-*([\d]{3})/;

    return str.replace(re, "$1$2-$3");
    return "";
}

function mphone(v) {
    var r = v.replace(/\D/g, "");
    r = r.replace(/^0/, "");
    if (r.length > 10) {
        r = r.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3");
    } else if (r.length > 5) {
        r = r.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1) $2-$3");
    } else if (r.length > 2) {
        r = r.replace(/^(\d\d)(\d{0,5})/, "($1) $2");
    } else {
        r = r.replace(/^(\d*)/, "($1");
    }
    return r;
}

function mask(o, f) {
    setTimeout(function () {
        var v = mphone(o.value);
        if (v != o.value) {
            o.value = v;
        }
    }, 1);
}