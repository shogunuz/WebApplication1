using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Exceptions
{
    public class MyException :ApplicationException
    {
        public MyException():base() {   }
        public MyException(string msg) : base(msg) { }
        public MyException(string msg, Exception innerException)
            : base(msg, innerException) { }
        public MyException(System.Runtime.Serialization.SerializationInfo serialization,
            System.Runtime.Serialization.StreamingContext context)
            : base(serialization,context)
        {

        }
    }
}
