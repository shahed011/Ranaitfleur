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
        $(this).text($(this).text() === "Show order item(s)" ? "Hide order item(s)" : "Show order item(s)");
    });
})();
