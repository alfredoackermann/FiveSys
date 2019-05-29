/**
* @class fw-form-ajax
* - [data-fw-ajax] - ativa o submit do form por ajax
* - [data-fw-disable-fields=true] - desabilita os campos para somente leitura
* - [data-fw-success-event] - dispara um evento
*
* - depende do plugin  jquery-form https://github.com/jquery-form/form
* - CDN <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.form/4.2.2/jquery.form.min.js" integrity="sha384-FzT3vTVGXqf7wRfy8k4BiyzvbNfeYjK+frTVqZeNDFl8woCbF0CYG6g2fMEFFo/i" crossorigin="anonymous"></script>
* - depende do plugin jquery validate
* - CDN <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.17.0/jquery.validate.min.js"></script>
* - depende do plugin jquery validate unobtrusive
* - CDN <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.6/jquery.validate.unobtrusive.min.js"></script>
*/

if(!window.fw)
    window.fw = {};

window.fw.form = (function() {

    $(function() {
        apply();
    })

    function apply($form) {

        $form = $form || $('form[data-fw-ajax]');
        
        configJqueyValidator($form);
        disableFields($form);
        aplicarFoco($form);
        configurarFormAjax($form);
    }

    function configurarFormAjax($form) {
        $form.ajaxForm({
            beforeSubmit: validateForm,
            success: onComplete
        });
    }

    function aplicarFoco($form) {
        
        $form.find('input:visible, select:visible, textarea:visible').first().focus();
    }

    function disableFields($form) {
        if($form.attr('data-fw-disable-fields') == 'true') {

            $form
            .find('input, select, textarea')
            .each(function() {
                if($(this).closest('.dataTables_wrapper').length == 0)
                    $(this).attr('disabled', true);
            })
        }
    }

    function configJqueyValidator($form) {
        $.validator.unobtrusive.parse($form);

        jQuery.extend(jQuery.validator.messages, {
            required: "Campo obrigatório.",
            remote: "Please fix this field.",
            email: "Please enter a valid email address.",
            url: "Please enter a valid URL.",
            date: "Please enter a valid date.",
            dateISO: "Please enter a valid date (ISO).",
            number: "Please enter a valid number.",
            digits: "Please enter only digits.",
            creditcard: "Please enter a valid credit card number.",
            equalTo: "Please enter the same value again.",
            accept: "Please enter a value with a valid extension.",
            maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
            minlength: jQuery.validator.format("Please enter at least {0} characters."),
            rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
            range: jQuery.validator.format("Please enter a value between {0} and {1}."),
            max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
            min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
        });
    }

    function validateForm(arr, $form, options) {
        var validator = $form.validate();
        if(!validator.valid()) {
            fw.alert.error('Existem campos inválidos.');
            return false;  
        }
    };

    function onComplete(result, status, xhr, $form) {
        if(result.success) {
            var event = $form.data('fw-success-event');

            var isModal = $form.closest('.fw-modal').length > 0;

            if(isModal)
                $(document).trigger('fw-modal:formSuccess');

            if(event) {
                fw.alert.success(result.message);
                $(document).trigger(event, result);
                return;
            }

            if (result.urlRedirect) {
                fw.alert.schedule(result.message, 'success');
                window.location = result.urlRedirect;
                return;
            }

            if(result.message) {
                fw.alert.success(result.message);
            }

        }
        else
            fw.alert.error(result.message);
    }
    
    return {
        apply: apply
    };
})()