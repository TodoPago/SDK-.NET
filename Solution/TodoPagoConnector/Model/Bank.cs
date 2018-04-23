using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [JsonObject]
    [DataContract(Name = "Bank")]
    public class Bank
    {
        [DataMember(Name = "Code")]
        public string Code { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Logo")]
        public string Logo { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }
}
