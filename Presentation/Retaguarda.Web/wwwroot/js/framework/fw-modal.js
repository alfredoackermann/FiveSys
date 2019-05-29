(function() {

    $(function() {
        $(document).on('click', 'a[data-fw-modal]', openModal);
        $(document).on('click', '.fw-modal a[data-fw-modal-close]', closeModal);

        $(document).on('hidden.bs.modal', function() { $('.fw-modal').remove() });

        $(document).on('fw-modal:formSuccess', onSubmitComplete);
    })

    function openModal(e) {
        e.preventDefault();
    
        var $a = $(e.target).closest('a');

        var url = $a.attr('href');
        var modalSize = '';
        var title = $a.attr('data-fw-modal-title');

        $.get(url, function(data) {
            $('body').append(`
                <div class="fw-modal modal fade" tabindex="-1" role="dialog">
                    <div class="modal-dialog ${modalSize}">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">${title}</h4>                                
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            </div>
                            <div class="modal-body">${data}</div>
                        </div>
                    </div>
                </div>
            `);

            $('.fw-modal').modal({ backdrop:'static', focus:true, keyboard:false });
            
            $('.fw-modal').on('shown.bs.modal', function() {
                var $form = $('.fw-modal form');
                if(!!$form.length) {
                    console.log('fw-modal - aplicando fw-form');
                    fw.form.apply($form);
                    //reativa o plugin de mascara 
                    $.applyDataMask();
                }
            })
        })
    }

    function closeModal(e) {
        e.preventDefault();

        $('.fw-modal').modal('hide');
    }

    function onSubmitComplete(e, result) {
         $('.fw-modal').modal('hide');
    }
})()