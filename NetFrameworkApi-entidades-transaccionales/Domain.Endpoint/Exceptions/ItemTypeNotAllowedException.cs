using System;

namespace Domain.Endpoint.Exceptions
{
    public class ItemTypeNotAllowedException : Exception
    {
        public ItemTypeNotAllowedException(string type) : base($"Item of type \"{type}\" is not valid") {}
    }
}
