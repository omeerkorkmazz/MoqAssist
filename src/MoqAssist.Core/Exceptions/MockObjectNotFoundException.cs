using System;

namespace MoqAssist.Core.Exceptions
{
    public class MockObjectNotFoundException : Exception
    {
        public MockObjectNotFoundException(string message) : base(message) { }
    }
}