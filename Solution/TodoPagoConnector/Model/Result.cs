using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [JsonObject]
    [DataContract(Name = "Result")]
    public class Result
    {
        [DataMember(Name = "BanksCollection")]
        public BanksCollection BanksCollection { get; set; }

        [DataMember(Name = "PaymentMethodBanksCollection")]
        public PaymentMethodBanksCollection PaymentMethodBanksCollection { get; set; }

        [DataMember(Name = "PaymentMethodsCollection")]
        public PaymentMethodsCollection PaymentMethodsCollection { get; set; }
    }
}
