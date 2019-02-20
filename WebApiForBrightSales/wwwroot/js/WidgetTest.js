(function () {
    // Localize jQuery variable
    var jQuery;
    console.log('new query object' + jQuery);

    /******** Load jQuery if not present *********/
    if (window.jQuery === undefined || window.jQuery.fn.jquery !== "3.3.1") {
        var script_tag = document.createElement("script");
        script_tag.setAttribute("type", "text/javascript");
        script_tag.setAttribute(
            "src",
            "https://code.jquery.com/jquery-3.3.1.min.js"

        );
    console.log('new query object1' + jQuery);
        if (script_tag.readyState) {
            script_tag.onreadystatechange = function () {
                // For old versions of IE
                if (this.readyState == "complete" || this.readyState == "loaded") {
                    scriptLoadHandler();

                }
            };
        } else {
            // Other browsers
            script_tag.onload = scriptLoadHandler;
        }
        // Try to find the head, otherwise default to the documentElement
        (
            document.getElementsByTagName("head")[0] || document.documentElement
        ).appendChild(script_tag);
    } else {
        // The jQuery version on the window is the one we want to use
        jQuery = window.jQuery;
        main();
    }

    /******** Called once jQuery has loaded ******/
    function scriptLoadHandler() {
        // Restore $ and window.jQuery to their previous values and store the
        // new jQuery in our local jQuery variable
        jQuery = window.jQuery.noConflict(true);
        // Call our main function
        main();
    }

    /******** Our main function ********/
    function main() {
        jQuery(document).ready(function ($) {
            console.log('new query object2' + jQuery);

            $.ajax({
                url: "https://localhost:44319/TestView",
                type: "GET",
                success: function (result) {
                    console.log("test");
                    $("#example-widget-container").css({ "background-color": "red", "width": "200px", "height": "100px" });
                },
                complete: function () {
                    console.log("onComplete");
                    }


            });

            var container = $("#example-widget-container").css({ "background-color": "yellow", "width":"100px", "height":"100px"});
            
            // We can use jQuery 1.4.2 here
        });
    }
})(); // We call our anonymous function immediately