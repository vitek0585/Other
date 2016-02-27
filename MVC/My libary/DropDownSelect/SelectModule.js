var SelectModule = {


    select: function (e) {

        function sendAsync(id) {

            return $.ajax({
                type: "Get",
                url: "/City/ShowCities/1",
                data: { region: id },
                dataType: "html",
                contentType: "application/json",
            
                beforeSend: function () {
                    $("#table-cities").animate({ "opacity": ".2" }, 800);
                    $("#loading").show();
                },
                complete: function showContent() {
                    $("#table-cities").animate({ "opacity": "1" }, 200);
                    $("#loading").hide();
                }

            });
        }
        var d = $(e.target).val()||0;
        return sendAsync(d);
    },

}

