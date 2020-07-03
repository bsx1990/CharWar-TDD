using System;

namespace Service.Utility.Exceptions
{
    public class PlaceException : Exception
    {
        public PlaceException(string message):base(message) { }
    }
}