//(function () {
//    $(window)
//        .load(function () {
//            $(".loader").fadeOut("slow");
//        });
//})();
(function() {
    $('#billingToggle')
        .change(function () {
            if ($(this).prop('checked')) {
                $('#billingToggleDiv *').attr("disabled", "disabled");
            } else {
                $('#billingToggleDiv *').removeAttr("disabled");
            }
        });
})();
