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

        public void addWrapper()
        {
            this.count++;
        }

        public int getCount()
        {
            return this.count;
        }
    }    
}