using System;

namespace VendingMachine
{
    public class TrashCompartment
    {
        private int count;

        public TrashCompartment() 
        {
            this.count = 0;
        }

        public void AddWrapper()
        {
            this.count++;
        }

        public int GetCount()
        {
            return this.count;
        }
    }    
}