/**
* - @LoadByField
* - atributos:
* - [data-fw-load-by-field="seletor"] = seletor para o campo, anotado no container
* - [data-fw-load-by-field-url="url"] = url para carregar o container
* - [data-fw-load-by-field-parameter="parametro"] = nome do parametro que será passado por query string
*/
(function() {
    
    $(apply);

    function apply() {
        $('[data-fw-load-by-field]').each(function(i, elem) {
            loadContainer($(elem))
        })
    }

    function loadContainer($container) {
        var url = $container.data('fw-load-by-field-url');
        var parameter = $container.data('fw-load-by-field-parameter');
        var seletor = $container.data('fw-load-by-field');

        url += '?' + parameter + '='

        $(seletor).change(function() {
            var value = $(this).val();

            if(value)
                $container.load(url + value);
            else
                $container.html('');
        })
    }

})()