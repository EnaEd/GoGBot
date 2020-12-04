using System;

namespace Shared.Extension
{
    public static class EnumExtension
    {
    }
    public class EnumDescriptor : Attribute
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public EnumDescriptor(string firstName = null, string lastName = null, string userName = null)
        {
            FirstName = firstName is null ? string.Empty : firstName;
            LastName = lastName is null ? string.Empty : lastName;
            UserName = userName is null ? string.Empty : userName;
        }
    }
}
