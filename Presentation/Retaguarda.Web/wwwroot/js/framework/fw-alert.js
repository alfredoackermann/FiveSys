if(!window.fw)
    window.fw = {};

window.fw.alert = (function() {
    $(function() {
        fireScheduleMessage();
    })

    var KEY_STORAGE = 'schedule-message';
    var options = {
        title: 'FileSys - Retaguarda'
    };

    var factory = {
        success: success,
        info: info,
        error: error,
        schedule: schedule,
        confirm:confirm
    };
    
    function success(msg) {
        toastr.clear();
        toastr.success(breakMessage(msg), options.title, {
            closeButton: true,
            progressBar: true,
        });
    }

    function info(msg) {
        toastr.clear();
        toastr.info(breakMessage(msg), options.title, {
            closeButton: true,
            progressBar: true,
        });
    }

    function error(msg) {
        toastr.clear();
        toastr.error(breakMessage(msg), options.title, {
            closeButton: true,
            progressBar: true,
        });
    }

    function schedule(msg, type) {
        localStorage.setItem(KEY_STORAGE, JSON.stringify({
            message: breakMessage(msg),
            type: type
        }));
    }

    function fireScheduleMessage() {
        var obj = JSON.parse(localStorage.getItem(KEY_STORAGE));

        if(obj) {
            factory[obj.type](obj.message);

            localStorage.removeItem(KEY_STORAGE);
        }

    }

    //https://craftpip.github.io/jquery-confirm/#getting-started
    function confirm(msg, callbackYes, callbackNo) {
        $.confirm({
            title: options.title,
            content: breakMessage(msg),
            buttons: {
                confirm: {
                    text: 'SIM',
                    action: callbackYes,
                },
                cancel: {
                    text: 'NÃO',
                    action: callbackNo
                }
            }
        });
    }
    
    function breakMessage(msg) {
        return msg.replace(/\n/g, '<br/>');
    }

    return factory;
})()