namespace GoGBot.BLL.Models
{
    public class MessageContentModel
    {
        public BaseContentModel ContentData { get; set; }
        public Shared.Enums.Enum.ContentType ContentType { get; set; }
        public Shared.Enums.Enum.NewsThemeType Theme { get; set; }
    }
}
