using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class TransactionManager
    {
        private IDictionary<string, Candy> priceList;
        private decimal totalCash;

        public TransactionManager(IDictionary<string, Candy> priceList)
        {
            this.priceList = priceList;
            this.totalCash = 0.00m;
        }

        public bool AttemptPayment(string candyName, decimal payment)
        {
            Candy candyChoice;

            if (payment <= 0.00m)
            {
                return false;
            }
            else if (!priceList.TryGetValue(candyName, out candyChoice)) 
            {
                return false;
            }
            else if (payment < candyChoice.price)
            {
                return false;
            } 
            else
            {
                totalCash += payment;
                return true;
            }
        }

        public decimal GetTotalCash()
        {
            return this.totalCash;
        }
    }
}