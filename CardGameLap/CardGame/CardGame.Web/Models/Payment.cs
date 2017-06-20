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
        /// <summary>
        /// LUHN Algorithmus for Creditcard Payment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns>BOOL isValidExpiration</returns>
        public static bool IsValidExpiration(int month, int year)
        {
            bool isValidExpiration = true;

            if (year < DateTime.Now.Year)
                isValidExpiration = false;
            else if (year == DateTime.Now.Year && month < DateTime.Now.Month)
                isValidExpiration = false;

            return isValidExpiration;
        }

        [Required(ErrorMessage = "Your Creditcard-Number is not valid")]
        public string CreditCardNumber { get; private set; }
        public string CardHolder { get; private set; }
        public int ExpireMonth { get; private set; }
        public int ExpireYear { get; private set; }
        public int SecurityCode { get; private set; }



        /// <summary>
        /// PRIVAT Konstruktor Payment
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <param name="cardHolder"></param>
        /// <param name="expireMonth"></param>
        /// <param name="expireYear"></param>
        /// <param name="securityCode"></param>
        private Payment(string creditCardNumber, string cardHolder, int expireMonth, int expireYear, int securityCode)
        {
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpireMonth = expireMonth;
            ExpireYear = expireYear;
            SecurityCode = securityCode;
        }


        /// <summary>
        /// public static Payment Create
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <param name="cardHolder"></param>
        /// <param name="expireMonth"></param>
        /// <param name="expireYear"></param>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        /// 
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
    