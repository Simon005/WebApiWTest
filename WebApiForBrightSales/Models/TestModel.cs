using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForBrightSales.Models
{
    public class TestModel
    {

        public string ViewAsString { get; set; }
        public ContentResult JavascriptTest { get; set; }

        public ContentResult JsonCss { get; set; }
    }
}
