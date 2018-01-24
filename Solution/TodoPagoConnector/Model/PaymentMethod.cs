using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TodoPagoConnector.Model
{
    [JsonObject]
    [DataContract]
    public class PaymentMethod
    {
        [DataMember(Name = "CardNumberLengthMax")]
        public int CardNumberLengthMax { get; set; }

        [DataMember(Name = "CardNumberLengthMin")]
        public int CardNumberLengthMin { get; set; }

        [DataMember(Name = "ExpirationDateCheck")]
        public bool ExpirationDateCheck { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "IdTipoMedioPago")]
        public int IdTipoMedioPago { get; set; }

        [DataMember(Name = "Logo")]
        public string Logo { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "SecurityCodeCheck")]
        public bool SecurityCodeCheck { get; set; }

        [DataMember(Name = "SecurityCodeLength")]
        public int SecurityCodeLength { get; set; }
    }
}
