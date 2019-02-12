

    function getClicks() {
        console.log("TESTFROM GET CLICK FUNCTION");

        $("#buttonId").on('click', function () {
            alert("TESTONCLICK");
        })();


    }

    getClicks();
