using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [JsonObject]
    public class PaymentMethodBank
    {
        [DataMember(Name = "BankId")]
        public int BankId { get; set; }

        [DataMember(Name = "PaymentMethodId")]
        public int PaymentMethodId { get; set; }
    }
}
