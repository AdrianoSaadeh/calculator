using Newtonsoft.Json;

namespace WCCalculator.Models
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("hasError")]
        public bool HasError { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }
    }
}