(function () {
    $(window)
        .load(function() {
            $(".loader").fadeOut("slow");
        });

    $("#billingToggle")
        .change(function () {
            if ($(this).prop("checked")) {
                $("#billingToggleDiv *").attr("disabled", "disabled");
            } else {
                $("#billingToggleDiv *").removeAttr("disabled");
            }
        });

    $(".showOrderItemButton").on("click", function () {
        var $parentBox = $(this).parent(".orderRow");
        $parentBox.find(".orderItemRow").slideToggle(200, "swing");
        if ($(this).text().includes("Show order")) {
            $(this).text(" Hide order item(s)");
            $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
        } else {
            $(this).text(" Show order item(s)");
            $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
        }
    });
})();
