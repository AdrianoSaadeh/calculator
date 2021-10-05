using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;
using WCCalculator.Models;

namespace WCCalculator
{
    public class Test
    {
        RestClient client;
        RestRequest request;

        [SetUp]
        public void Initialize()
        {
            // arrange
            client = new RestClient("https://sf-api-gateway.azure-api.net");
            request = new RestRequest("/credit-limit/staging/workingcapital/loan-calculator/{loanType}/{value}/{totalPayments}/{frequency}/false", Method.GET);
            request.AddUrlSegment("loanType", 11);
            request.AddUrlSegment("value", 1000000);
            request.AddUrlSegment("totalPayments", 12);
            request.AddUrlSegment("frequency", "biweekly");
            request.AddHeader("Ocp-Apim-Subscription-Key", "3cc7e8194f4d462093c4a71282fac62d");
        }


        [Test]
        public void CountryAbbreviationSerializationTest()
        {
            // act
            IRestResponse response = client.Execute(request);
            CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(calculatorResponse.IsSuccess, Is.EqualTo("True"));
            Assert.AreEqual(calculatorResponse.Error[0].HasError, "False");
            Assert.AreEqual(calculatorResponse.Result[0].Rate, "6.65");
            Assert.AreEqual(calculatorResponse.Result[0].PaymentTimes, "24");
            Assert.AreEqual(calculatorResponse.Result[0].PaymentAmount, "44633.22");
        }





        //public RestClient client;
        //public RestRequest endpoint;
        //public IRestResponse resp;

        //public RestClient Client(string uri)
        //{
        //    client = new RestClient(uri);
        //    return client;
        //}

        //public RestRequest Endpoint(string rota)
        //{
        //    endpoint = new RestRequest(rota);
        //    return endpoint;
        //}

        //public void Get()
        //{
        //    endpoint.Method = Method.GET;
        //    endpoint.RequestFormat = DataFormat.Json;
        //    endpoint.AddHeader("Ocp-Apim-Subscription-Key", "3cc7e8194f4d462093c4a71282fac62d");
        //}

        //public IRestResponse StatusCode(int code)
        //{
        //    resp = client.Execute(endpoint);

        //    if (resp.IsSuccessful)
        //    {
        //        var status = (int)resp.StatusCode;
        //        Assert.AreEqual(code, status);
        //    }
        //    else
        //    {
        //        var status = (int)resp.StatusCode;
        //        var desc = resp.StatusDescription;
        //        var content = resp.Content;

        //        Console.WriteLine($"{status} - {desc}");
        //        Console.WriteLine(content);
        //        Assert.AreEqual(code, status);
        //    }

        //    return resp;
        //}

        //public void ReturnText()
        //{
        //    JObject obs = JObject.Parse(resp.Content);
        //    Console.WriteLine(obs);
        //}

        //[Test]
        //public void Test1()
        //{
        //    Client("https://sf-api-gateway.azure-api.net");
        //    Endpoint("/credit-limit/staging/workingcapital/loan-calculator/11/500000/12/biweekly/false");
        //    Get();
        //    StatusCode(200);
        //    ReturnText();
        //}





    }
}



