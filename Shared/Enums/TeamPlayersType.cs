using Shared.Extension;

namespace Shared.Enums
{
    public partial class Enum
    {
        public enum TeamPlayersType
        {
            [EnumDescriptor()]
            None = 0,
            [EnumDescriptor("Andy", "Tolmachev", null, "Main man")]
            MainPlayer = 592902264,
            [EnumDescriptor("Alex", "Ageenko", null, "The Immigrant")]
            BasicFirst = 599707177,
            [EnumDescriptor("Vasja", null, "Fukcman1", "Fukcman1")]
            BasicSecond = 460393099,
            [EnumDescriptor("Yugen", "Kononenko", null, "The Little")]
            BasicThrird = 767456173,
            [EnumDescriptor("Ed", "Ena", null, "just hatzker")]
            Admin = 385595104,
            [EnumDescriptor("Max", "Beard", null, "Beard")]
            BasicFive = 585878720

        }
    }

}
