using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class UnKnownErrorException:ApplicationException
    {
#nullable enable
        public UnKnownErrorException(string? message) : base(message) { }
    }
}
