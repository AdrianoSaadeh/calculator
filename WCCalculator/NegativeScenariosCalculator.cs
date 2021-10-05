using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;
using WCCalculator.Models;

namespace WCCalculator
{
    class NegativeScenariosCalculator
    {
        RestClient client;
        string uri;
        string headerKey;
        string headerValue;

        [SetUp]
        public void Initialize()
        {
            client = new RestClient("https://sf-api-gateway.azure-api.net");
            uri = "/credit-limit/staging/workingcapital/loan-calculator";
            headerKey = "Ocp-Apim-Subscription-Key";
            headerValue = "3cc7e8194f4d462093c4a71282fac62d";
        }

        [TestCase(10, 1000000, 2, "monthly", false, "Error on LoanCalculator: value for terms not allowed, set a value between 3 and 12.", TestName = "Pay Interest should be invalid in 2 Term and Monthly")]
        [TestCase(10, 1000000, 2, "biweekly", false, "Error on LoanCalculator: value for terms not allowed, set a value between 3 and 12.", TestName = "Pay Interest should be invalid in 2 Term and Biweekly")]
        [TestCase(10, 1000000, 13, "biweekly", false, "Error on LoanCalculator: value for terms not allowed, set a value between 3 and 12.", TestName = "Pay Interest should be invalid in 13 Term and Monthly")]
        [TestCase(10, 1000000, 13, "monthly", false, "Error on LoanCalculator: value for terms not allowed, set a value between 3 and 12.", TestName = "Pay Interest should be invalid in 13 Term and Biweekly")]
        [TestCase(10, 1000000, 3, "quarter", false, "Error: Invalid Loan Type. The Loan Type should be either 'monthly' or 'biweekly'. BadRequest", TestName = "Pay Interest should be invalid when frequency is different from monthly or biweekly")]
        [TestCase(10, 0, 3, "monthly", false, "Error: The 'amount', 'number_of_terms' and 'rate' fields are required to be greater than 0. BadRequest", TestName = "Pay Interest should be invalid when amount is 0")]
        [TestCase(10, 100000000, 3, "monthly", false, "Error on LoanCalculator: value exceeds the limit.", TestName = "Pay Interest should be invalid when amount is greater than 99999999,99")]
        [TestCase(14, 5000, 6, "biweekly", false, "Error on LoanCalculator: loan type not is permitted for the simulator.", TestName = "Pay Interest should be invalid when loan type is different from 10 or 11")]
        public void TermLoanNegativeConditionsTest(int loanType, double value, int totalPayments, string frequency, bool flagInterest, string messageExpected)
        {
            // arrange
            var request = new RestRequest(uri + $"/{loanType}/{value}/{totalPayments}/{frequency}/{flagInterest}", Method.GET);
            request.AddHeader(headerKey, headerValue);

            // act
            IRestResponse response = client.Execute(request);
            CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsFalse(calculatorResponse.IsSuccess);
            Assert.IsTrue(calculatorResponse.Error[0].HasError);
            Assert.AreEqual(messageExpected, calculatorResponse.Error[0].Message);
        }
    }
}
