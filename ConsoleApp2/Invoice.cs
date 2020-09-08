using System;
using System.Collections.Generic;
using System.Text;

namespace AZ204.MessageEventDemo
{
    public class Invoice
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string ip_address { get; set; }
        public string total { get; set; }
        public DateTime transaction_time { get; set; }
    }
}
