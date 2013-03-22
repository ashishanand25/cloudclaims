(function (Notifications, $, undefined) {
    //Private Property
    var isHot = true;
    //Public Property
    Notifications.ingredient = "Bacon";

    //Public Method
    Notifications.displayGlobalNotification = function (vmNotifications, callback) {
        $.post(GLOBAL_NOTIFICATIONS_URL, vmNotifications, function (result) {
            $("#global-notification").html($(result)).show('slide', { direction: 'up' }, 500, function () {
                //TO DO: Do some back-end audit/loggins here
                if (typeof callback === 'function') {
                    callback();
                }
            });
        });
    };

    function htmlEncode(value) {
        //create a in-memory div, set it's inner text(which jQuery automatically encodes)
        //then grab the encoded contents back out.  The div never exists on the page.
        return $('<div/>').text(value).html();
    }

    function htmlDecode(value) {
        return $('<div/>').html(value).text();
    }

    //Private Method
    function deanSucks() {
        //bna;jadoja
    }

}(window.Notifications = window.Notifications || {}, jQuery));