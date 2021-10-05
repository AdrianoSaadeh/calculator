using Newtonsoft.Json;
using System.Collections.Generic;

namespace WCCalculator.Models
{
    public class Result
    {
        [JsonProperty("paymentAmount")]
        public string PaymentAmount { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("rateMonthly")]
        public string RateMonthly { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("totalPayment")]
        public string TotalPayment { get; set; }

        [JsonProperty("paymentTimes")]
        public string PaymentTimes { get; set; }

        [JsonProperty("paymentSchedule")]
        public List<PaymentSchedule> PaymentSchedule { get; set; }
    }
}