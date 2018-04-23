using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [JsonObject]
    public class TodoPagoResult
    {
        [DataMember(Name = "Result")]
        public Result Result { get; set; }
    }
}
