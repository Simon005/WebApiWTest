using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using WebApiForBrightSales.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForBrightSales.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("allowTest")]
    public class WidgetController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private ICompositeViewEngine _viewEngine;
        IConfiguration _configuration;


        public WidgetController(IHostingEnvironment hostingEnvironment, ICompositeViewEngine compositeViewEngine, IConfiguration configuration)
        {
            _viewEngine = compositeViewEngine;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;

            //Get script 

            string result = System.IO.File.ReadAllText(webRootPath + "/js/AjaxFromVidgetTest.js");
            string test = result.Replace(Environment.NewLine, string.Empty);
            var javascript = new JavaScriptResult(result);

            //Get css

            string cssResult = System.IO.File.ReadAllText(webRootPath + "/css/widgetCss.css");
            string cssWithoutEmptyLines = cssResult.Replace(Environment.NewLine, string.Empty);
            var cssWELAndJsResult = new JavaScriptResult(cssWithoutEmptyLines);

            //var jsCssJsonResult = JsonConvert.SerializeObject(cssResult);
            //var jsCss = new JavaScriptResult(cssResult);
            
            var renderdView = await RenderPartialViewToString("WidgetTestView", new WidgetViewModel());
            var newModel = new TestModel() { ViewAsString = renderdView, JavascriptTest = javascript, JsonCss = cssWELAndJsResult};

            return Json(newModel);
        }

        [HttpPost]
        [Route("/api/[controller]/SendPhoneNumber")]
        public IActionResult SendPhoneNumber(WidgetViewModel data)
            {

            var accountSid = _configuration.GetConnectionString("TwilioAccountSid");
            var authToken = _configuration.GetConnectionString("TwilioAuthToken");
            TwilioClient.Init(accountSid, authToken);


            //Trial account
            var to = new PhoneNumber("+46736142577");
            var from = new PhoneNumber("+46705448629");

            var call = CallResource.Create(to, from,
                url: new Uri("http://demo.twilio.com/docs/voice.xml"));

            return Content(data.PhoneNumber);
        }

        [HttpGet]
        [Route("widget/SendPhoneNumberGET")]
        public IActionResult SendPhoneNumber()
        {
            var x = 10;
            return Content("<h1> SENDPHONENUMBER</h1>");
        }


        private async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult =
                    _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }


        //[HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    var body = RenderViewToString("Values", "~/Views/Home/Index.cshtml", new object());
        //    return Request.CreateResponse(HttpStatusCode.OK, new { content = body });
        //}

        //public static string RenderViewToString(string controllerName, string viewName, object viewData)
        //{
        //    using (var writer = new StringWriter())
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values.Add("controller", controllerName);
        //        var fakeControllerContext = new ControllerContext(new ActionContext(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
        //        var razorViewEngine = new RazorViewEngine();
        //        var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

        //        var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
        //        razorViewResult.View.Render(viewContext, writer);
        //        return writer.ToString();
        //    }

        //}

        //public class FakeController : ControllerBase { protected override void ExecuteCore() { } }



        public class JavaScriptResult : ContentResult
        {
            public JavaScriptResult(string script)
            {
                this.Content = script;
                this.ContentType = "application/javascript";
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
