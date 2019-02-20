

        //$("#buttonId").on('click', function () {
        //    alert("TESTONCLICK");
        //})();

$("#submitForm").on('submit', function (event) {
    event.preventDefault();

    $.ajax({
        url: "https://localhost:44319/api/widget/SendPhoneNumber",
        method: "POST",
        type: "json",
        data: $("#submitForm").serialize(),
        headers: { 'Access-Control-Allow-Origin': "http://localhost:5106" },
        success: function (newModel) {
            console.log("test");
            //console.log(phoneNumber);
            $("#example-widget-containerResult");

            console.log(newModel);
        }

    });


});

