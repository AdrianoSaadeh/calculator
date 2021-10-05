using Newtonsoft.Json;

namespace WCCalculator.Models
{
    public class PaymentSchedule
    {
        [JsonProperty("payment")]
        public int Payment { get; set; }

        [JsonProperty("dueDate")]
        public string DueDate { get; set; }

        [JsonProperty("principal")]
        public double Principal { get; set; }

        [JsonProperty("interest")]
        public double Interest { get; set; }

        [JsonProperty("paymentamount")]
        public double Paymentamount { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }
    }
}