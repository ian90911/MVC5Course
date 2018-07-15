using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.ViewModel
{
    public class ClientQueryCondition
    {
        public string name { get; set; }
        public double? CreditRating { get; set; }
        public int skip { get; set; }
        public int take { get; set; }

        public ClientQueryCondition()
        {
            this.skip = 0;
            this.take = 10;
        }
    }
}