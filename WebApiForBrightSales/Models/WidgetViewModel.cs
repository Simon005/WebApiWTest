using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForBrightSales.Models
{
    public class WidgetViewModel
    {
        public ContentResult js { get; set; }

        public string Name { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
