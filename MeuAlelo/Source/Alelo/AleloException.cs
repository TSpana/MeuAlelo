using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuAlelo.Source.Alelo
{

    [Serializable]
    public class AleloException : Exception
    {
        public AleloException() { }
        public AleloException(string message) : base(message) { }
        public AleloException(string message, Exception inner) : base(message, inner) { }
        protected AleloException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


    [Serializable]
    public class AleloExpiratedException : Exception
    {
        public AleloExpiratedException() { }
        public AleloExpiratedException(string message) : base(message) { }
        public AleloExpiratedException(string message, Exception inner) : base(message, inner) { }
        protected AleloExpiratedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
