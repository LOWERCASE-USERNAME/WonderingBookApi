using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Utilities
{
    public enum IdeaCardType
    {
        [Display(Name = "Text")]
        Text,
        [Display(Name = "Quote")]
        Quote,
        [Display(Name = "Image")]
        Image
    }
}
