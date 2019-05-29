
/**
* @class fw-delete-item
* - atributos
* - [data-fw-delete-item]: ativa a funcionalidade
* - [data-fw-delete-item-confirm-message]: mensagem de confirmação
* - [data-fw-delete-item-success-event]: dispara um evento
*/
(function() {
    $(function() { $(document).on('click', 'a[data-fw-delete-item]', excluirRegistro) })

    var attributes = {
        active: 'data-fw-delete-item',
        message: 'data-fw-delete-item-confirm-message',
        event: 'data-fw-delete-item-success-event'
    }
    var options = {
        mensagemPadrao: 'Deseja relamente excluir este registro?'
    };

    function excluirRegistro(e) {
        e.preventDefault();

        var $a = $(e.target).closest('a');

        var url = $a.attr('href');
        var msg = $a.attr(attributes.message) || options.mensagemPadrao;
        var event = $a.attr(attributes.event);

        fw.alert.confirm(msg, function() {
            $.post(url).done(function(data) {
                if(data.success) {
                    if(event)
                        $(document).trigger(event);
                    else {
                        fw.alert.schedule(data.message, 'success');
                        window.location = data.urlRedirect;
                    }
                }
                else
                    fw.alert.error(data.message);
            })
        })
    }
})()