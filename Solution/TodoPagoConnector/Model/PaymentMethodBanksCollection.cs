using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace TodoPagoConnector.Model
{
    [JsonObject]
    public class PaymentMethodBanksCollection
    {
        public List<PaymentMethodBank> PaymentMethodBank { get; set; }
    }
}
