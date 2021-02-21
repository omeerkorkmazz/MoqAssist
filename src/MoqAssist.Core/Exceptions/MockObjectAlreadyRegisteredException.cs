using System;

namespace MoqAssist.Core.Exceptions
{
    public class MockObjectAlreadyRegisteredException : Exception
    {
        public MockObjectAlreadyRegisteredException(string message) : base(message) { }
    }
}