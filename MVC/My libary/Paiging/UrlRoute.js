(function () {

    window.onpopstate = function (e) {

        try {
            console.log(e.state.previous_url);
            if (e.state)
                window.location = e.state.previous_url;

        } catch (e) {
            console.log("error onpopstate");
        }
    }

})();