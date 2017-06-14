using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Payment
    {
        public static bool IsValidNumber(string data)
        {
            int sum = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                int add = (data[i] - '0') * (2 - (i + len) % 2);
                add -= add > 9 ? 9 : 0;
                sum += add;
            }
            return sum % 10 == 0;
        }

        public static bool IsValidExpiration(int month, int year)
        {
            bool isValidExpiration = true;

            if (year < DateTime.Now.Year)
                isValidExpiration = false;
            else if (year == DateTime.Now.Year && month < DateTime.Now.Month)
                isValidExpiration = false;

            return isValidExpiration;
        }

        public string CreditCardNumber { get; private set; }
        public string CardHolder { get; private set; }
        public int ExpireMonth { get; private set; }
        public int ExpireYear { get; private set; }
        public int SecurityCode { get; private set; }

        private Payment(string creditCardNumber, string cardHolder, int expireMonth, int expireYear, int securityCode)
        {
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpireMonth = expireMonth;
            ExpireYear = expireYear;
            SecurityCode = securityCode;
        }

        // Factory!!
        public static Payment Create(string creditCardNumber, string cardHolder, int expireMonth, int expireYear, int securityCode)
        {
            Payment cc = null;

            if (IsValidNumber(creditCardNumber) && IsValidExpiration(expireMonth, expireYear))
                cc = new Payment(creditCardNumber, cardHolder, expireMonth, expireYear, securityCode);

            return cc;
        }
    }
}
    