using System;

namespace Mocksert.Exceptions
{
    [Serializable]
    public class MockException : Exception
    {
        public MockException(string message) : base(message)
        {
        }
    }
}