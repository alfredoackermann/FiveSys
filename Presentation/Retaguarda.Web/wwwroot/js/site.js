$(document).ready(function () {
    $('.datatable').DataTable({
        pageLength: 20,
        responsive: true,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'copy', text: 'Copiar' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },
            {
                extend: 'print',
                text: 'Imprimir',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        language: {
            url: '/i18n/dataTables.json',
        }
    });

    // Sobrescreve os valores default do DatePicker
    $.fn.datepicker.defaults.format = "mm/dd/yyyy";
    $.fn.datepicker.defaults.autoclose = true;
    $.fn.datepicker.defaults.language = 'pt-BR';
    $.fn.datepicker.defaults.allowInputToggle = true;

    $(".select2").select2({
        placeholder: "Selecione"
    });

    $('.jquery-marcar-todos').on('click', function () {
        $($(this).val()).not(this).prop('checked', this.checked);
    });
});


// Forca o select2 a chamar o validator quando um item eh selecionado
$(document).on("select2:select", ".select2", function (e) {
    if (e.params.data != null) {
        $("span[aria-labelledby=select2-" + e.currentTarget.id + "-container]").removeClass('input-validation-error');
        $("span[data-valmsg-for=" + e.currentTarget.id + "]").removeClass('field-validation-error').addClass('field-validation-valid').html('');
    }
});

// Sobrescreve os valores default do Validate
// Nao funciona dentro do $(document).ready()
$.validator.setDefaults({
    highlight: function (element, errorClass, validClass) {
        var elem = $(element);
        if (elem.hasClass("select2-hidden-accessible")) {
            $("span[aria-labelledby=select2-" + elem.attr("id") + "-container]").addClass('input-validation-error');
        } else {
            if (element.type === "radio") {
                this.findByName(elem.name).addClass('input-validation-error').removeClass('valid');
            } else {
                $(element).addClass('input-validation-error').removeClass('valid');
            }
        }
    },

    //When removing make the same adjustments as when adding
    unhighlight: function (element, errorClass, validClass) {
        var elem = $(element);
        if (elem.hasClass("select2-hidden-accessible")) {
            $("span[aria-labelledby=select2-" + elem.attr("id") + "-container]").removeClass('input-validation-error');
        } else {
            if (element.type === "radio") {
                this.findByName(elem.name).removeClass('input-validation-error').addClass('valid');
            } else {
                $(element).removeClass('input-validation-error').addClass('valid');
            }
        }
    }
});
