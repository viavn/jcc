using System;

namespace JccApi.Exceptions
{
    public class JccException : Exception
    {
        public JccException(string message) : base(message)
        {
        }
    }
}