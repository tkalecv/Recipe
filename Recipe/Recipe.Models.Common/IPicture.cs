namespace Recipe.Models.Common
{
    public interface IPicture
    {
        byte[] Image { get; set; }
        int PictureID { get; set; }
        int RecipeID { get; set; }
    }
}