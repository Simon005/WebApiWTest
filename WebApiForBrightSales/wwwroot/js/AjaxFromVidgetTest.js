

        //$("#buttonId").on('click', function () {
        //    alert("TESTONCLICK");
        //})();

$("#submitForm").on('submit', function (event) {
    event.preventDefault();

    var datas = $("#submitForm").serialize();

    $.ajax({
        url: "https://localhost:44319/api/widget/SendPhoneNumber",
        method: "POST",
        type: "json",
        data: $("#submitForm").serialize(),
        headers: { 'Access-Control-Allow-Origin': "http://localhost:5106" },
        success: function (newModel) {
            console.log("test");
            //console.log(phoneNumber);
            $("#example-widget-container").css({ "background-color": "green", "width": "200px", "height": "100px" });

            console.log(newModel);
        }

    });


});

//$("#buttonI1qwd").on('submit', function (event) {
//    event.preventDefault();

//    $.ajax({
//        url: "https://localhost:44319/api/widget",
//        method: "GET",
//        type: "json",
//        data: { },
//        headers: { 'Access-Control-Allow-Origin': "*" },
//        success: function (newModel) {
//            console.log(data);
//            console.log("test");
//            console.log(phoneNumber);
//            $("#example-widget-container").css({ "background-color": "green", "width": "200px", "height": "100px" });


//        }

//    });


//});




    //function getClicks() {
    //    console.log("TESTFROM GET CLICK FUNCTION");


    //}

    //getClicks();
