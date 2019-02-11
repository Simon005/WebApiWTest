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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiForBrightSales.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("allowTest")]
    public class WidgetController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private ICompositeViewEngine _viewEngine;


        public WidgetController(IHostingEnvironment hostingEnvironment, ICompositeViewEngine compositeViewEngine)
        {
            _viewEngine = compositeViewEngine;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {


            //Auth

            //Get script 

            string webRootPath = _hostingEnvironment.WebRootPath;
            string result = System.IO.File.ReadAllText(webRootPath + "/js/AjaxFromVidgetTest.js");
            string test = result.Replace(Environment.NewLine, string.Empty);
            var testParse = Json(test); 
            var javascript = new JavaScriptResult(result);
            //javascript.Content = test;
            //var javaToString = 
            
            var renderdView = await RenderPartialViewToString("WidgetTestView", new WidgetViewModel() { js = javascript });
            var newModel = new TestModel() { ViewAsString = renderdView, JavascriptTest = javascript};

            return Json(newModel);
            return View("WidgetTestView");
            //return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("/api/[controller]/SendPhoneNumber")]
        public IActionResult SendPhoneNumber(WidgetViewModel model)
        {
            var x = 10;
            return Content(model.Name);
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
