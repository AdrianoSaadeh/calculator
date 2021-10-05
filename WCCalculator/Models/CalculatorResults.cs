using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WCCalculator.Models
{
    class CalculatorResults
    {
        [JsonProperty("result")]
        public List<Result> Result { get; set; }

        [JsonProperty("error")]
        public List<Error> Error { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

    }
}
