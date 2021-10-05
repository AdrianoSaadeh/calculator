using System;
using System.Collections.Generic;
using System.Text;

namespace WCCalculator.Models
{
    public class CalculatorRequest
    {
        public int LoanType { get; set; }
        public int Value { get; set; }
        public int TotalPayments { get; set; }
        public string Frequency { get; set; }
        public string PaymentTimesExpected { get; set; }

        public bool FlagInterest { get; set; }

        public CalculatorRequest(int lt, int val, int tp, string freq, string pte, bool fi)
        {
            LoanType = lt;
            Value = val;
            TotalPayments = tp;
            Frequency = freq;
            PaymentTimesExpected = pte;
            FlagInterest = fi;
        }
    }
}
