//(function () {
//    $(window)
//        .load(function () {
//            $(".loader").fadeOut("slow");
//        });
//})();
(function() {
    $("#billingToggle")
        .change(function () {
            if ($(this).prop("checked")) {
                $("#billingToggleDiv *").attr("disabled", "disabled");
            } else {
                $("#billingToggleDiv *").removeAttr("disabled");
            }
        });

    $("#showOrderItem").on("click", function () {
        $("#orderItemList").slideToggle("fast");
    });

    //$(".top").on("click", function () {
    //    var $parentBox = $(this).closest(".box");
    //    $parentBox.siblings().find(".bottom").slideUp();
    //    $parentBox.find(".bottom").slideToggle(500, "swing");
    //});
})();
