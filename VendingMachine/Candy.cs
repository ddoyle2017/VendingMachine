using System;

namespace VendingMachine
{
    public class Candy : IProduct
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public bool wrapped { get; set; }
        public string flavor { get; set; }
        public string wrapperColor { get; set; }
        
        public Candy(string name, decimal price, string flavor, string wrapperColor)
        {
            this.name = name;
            this.price = price;
            this.flavor = flavor;
            this.wrapperColor = wrapperColor;
            this.wrapped = true;
        }

        public void Unwrap()
        {
            this.wrapped = false;
        }

        public bool IsWrapped()
        {
            return this.wrapped;
        }
    }
}