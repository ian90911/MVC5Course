using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Exceptions
{
    public class TestException:Exception
    {
        public TestException() : base("測試例外錯誤") { }
        public TestException(string message) : base(message) { }
        public TestException(string message, System.Exception inner) : base(message, inner) { }

        protected TestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}