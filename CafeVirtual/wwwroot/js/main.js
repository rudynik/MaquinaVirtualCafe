var main = function () {

    var inserir = function () {

        $('#alerta').hide();

        $("#required").each(function () {
            var formGroup = $(this);
            var campos = formGroup.find("input");
            if (campos.length > 0) {
                campos.each(function () {
                    var campo = $(this);

                    if (campo[0].checked) {

                        if (campo.val() === '0.05' || campo.val() === '0.01') {

                            $('#alerta').html("Defeito na leitora de Moeda, insira outro valor de moeda");
                            $('#alerta').show();
                        } else {

                            $('#alerta').hide();

                            var produto = {
                                Valor: $('#valor').val().replace('.', ','),
                                Total: $('#total').val().replace('.', ','),
                                Moeda: campo.val().replace('.', ',')
                            };

                            $.ajax({
                                url: "/Home/DadosPagamento",
                                method: "POST",
                                data: { produto },
                                success: function (result) {
                                    console.log(result);

                                    $('#valor').val(result.valor);
                                    $('#total').val(result.total);
                                    $('#troco').val(result.troco);
                                    $('#totalProduto').html(result.total.toFixed(2));
                                }
                            });
                        }
                    }
                });
            }
        });
    }

    var finalizar = function () {

        var troco = $('#troco').val();

        if (troco == -1 || troco == "") {
            
        $('#alerta').html("Valor insuficiente! insira mais moedas");
        $('#alerta').show();
        return;
    }

        var produto = {
            Valor: $('#valor').val().replace('.', ','),
            Total: $('#total').val().replace('.', ','),
            Id: $('#Id').val()
        };

        $.ajax({
            url: "/Home/DadosFinalizar",
            method: "POST",
            data: { produto },
            success: function (result) {

                window.location.href = "/Home/Resumo/" + result;
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
            
        });

    }
    return {
        inserir: inserir,
        finalizar: finalizar
    }

}();