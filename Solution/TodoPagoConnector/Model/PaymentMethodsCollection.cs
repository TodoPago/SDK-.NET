using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [DataContract]
    public class PaymentMethodsCollection
    {
        [DataMember(Name = "PaymentMethod")]
        public List<PaymentMethod> PaymentMethod { get; set; }
    }
}
