$(document).ready(function () {


    var javascriptToExecute;


    $.ajax({
        url: "https://localhost:44319/api/widget",
        method: "GET",
        type: "json",
        data: {},
        headers: { 'Access-Control-Allow-Origin': "http://localhost:5106" },
        success: function (newModel) {
            console.log("test");
            console.log(newModel);
            console.log(newModel.viewAsString);
            $("#example-widget-container").css({ "background-color": "red", "width": "200px", "height": "100px" });



            const htmlView = newModel.viewAsString;
            javascriptToExecute = newModel.javascriptTest.content;
            $("#example-widget-containerResult").html(htmlView);


            var f = new Function(javascriptToExecute);



            


            return new Function(javascriptToExecute)();




        },
        complete: function (newModel) {
            console.log("COMPLETE TESTING ");


            var htmlTesting = "<img src='../images/loaded.gif' alt='' onload='alert('Now that I have your attention...');' />";
            //return new Function(javascriptToExecute)();

        },
        error: function () {
            console.log("ERROR");
        }


    });


});
function run(newModel) {

    var javascriptToExecute;

    //$.ajax({
    //    url: "https://localhost:44319/api/widget",
    //    method: "GET",
    //    type: "json",
    //    data: { },
    //    headers: { 'Access-Control-Allow-Origin': "*" },
    //    success: function (newModel) {
    //        console.log("test");
    //        console.log(newModel);
    //        console.log(newModel.viewAsString);
    //        $("#example-widget-container").css({ "background-color": "red", "width": "200px", "height": "100px" });


    //        //var test = $('<button/>',
    //        //    {
    //        //        text: 'Test',
    //        //        click: function () { alert('hi'); },
    //        //        id: "buttonId"
    //        //    });

    //        //$("#example-widget-container").append(test);
            


    //        const htmlView = newModel.viewAsString;
    //        javascriptToExecute = newModel.javascriptTest.content;
    //        $("#example-widget-containerResult").html(htmlView);


    //        //var newScript = document.createElement("script");
    //        //newScript.onerror = loadError;
    //        //if (onloadFunction) { newScript.onload = onloadFunction; }
    //        //document.head.appendChild(newScript);
    //        //newScript.src = url;

    //    },
    //    complete: function (newModel) {
    //        console.log("COMPLETE TESTING ");



    //        $("#example-widget-containerResult").append(script_tag);


    //        //$("#example-widget-containerResult").appendTo(result);

    //    },
    //    error: function () {
    //        console.log("ERROR");
    //    }


    //});


    //(function (document) {
    //            var s = document.createElement('script');
    //            s.id = "external_content";
    //            s.async = true;
    //            s.src = javascriptToExecute;
    //            document.getElementsByTagName('head')[0].appendChild(s);
    //            s.onload = function () {
    //                document.write(example_content);
    //            }
    //        }(document));


}

run();




//FOR VIEW

//<script src="~/js/testWidgetRenderView.js" type="text/javascript"></script>
//    <script id="testScriptTag" type="text/javascript"></script>
//    <div id="example-widget-container">Widget</div>
//    <div id="example-widget-containerResult">ResultEShop</div>