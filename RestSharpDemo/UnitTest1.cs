using NUnit.Framework;
using RestSharp;
using System;

namespace RestSharpDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(11, 1000000, 12, "biweekly", TestName = "1")]
        [TestCase(11, 1000000, 12, "monthly", TestName = "2")]
        [TestCase(11, 1000000, 6, "biweekly", TestName = "3")]
        [TestCase(11, 1000000, 6, "monthly", TestName = "4")]
        public void PartitionSerializationTest(int loanType, int value, int totalPayments, string frequency)
        {
            // arrange
            var client = new RestClient("https://sf-api-gateway.azure-api.net");
            var request = new RestRequest($"/credit-limit/staging/workingcapital/loan-calculator/{loanType}/{value}/{totalPayments}/{frequency}/false", Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3cc7e8194f4d462093c4a71282fac62d");

            // act
            IRestResponse response = client.Execute(request);
            CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(calculatorResponse.IsSuccess, Is.EqualTo("True"));
            Assert.AreEqual(calculatorResponse.Error[0].HasError, "False");

        }
    }
}