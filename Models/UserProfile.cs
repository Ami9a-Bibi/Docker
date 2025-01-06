namespace BlazorApp1.Models
{
    public class UserProfile
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty; // Stores the path to the user's image
    }
}
