using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{

    [DataContract]
    public class BanksCollection
    {
        [DataMember(Name = "Bank")]
        public List<Bank> Bank { get; set; }
    }
}
