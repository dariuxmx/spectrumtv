using System;
namespace SpectrumTV.Models.Exceptions
{
    public class TypeRegistrationException : Exception
    {
        public TypeRegistrationException(Type  type) : base($"Type '{type}' is not registered.")
        { }
    }
}
