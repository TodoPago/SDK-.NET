using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace TodoPagoConnector.Model
{
    [JsonObject]
    [DataContract(Name = "Currency")]
    public class Currency
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }
}
