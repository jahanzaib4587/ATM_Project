using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel
{
    public class TransactionObj
    {
        public string TransactionType { get; set; }
        public string ID { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public decimal amount { get; set; }

    }
}
