using System;

namespace VendingMachine
{
    public interface IProduct
    {
        string name { get; set; }
        decimal price { get; set; }
        bool wrapped { get; set; }

        void Unwrap();
        bool IsWrapped();
    }
}