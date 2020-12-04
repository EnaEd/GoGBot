using Shared.Extension;

namespace Shared.Enums
{
    public partial class Enum
    {
        public enum TeamPlayersType
        {
            [EnumDescriptor()]
            None = 0,
            [EnumDescriptor("Andy", "Tolmachev")]
            MainPlayer = 592902264,
            [EnumDescriptor("Alex", "Ageenko")]
            BasicFirst = 599707177,
            [EnumDescriptor("Vasja", null, "Fukcman1")]
            BasicSecond = 460393099,
            [EnumDescriptor("Yugen", "Kononenko")]
            BasicThrird = 767456173,
            [EnumDescriptor("Ed", "Ena")]
            BasicFour = 385595104,
            [EnumDescriptor("Max", "Beard")]
            BasicFive = 585878720

        }
    }

}
