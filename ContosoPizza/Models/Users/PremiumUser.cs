namespace ContosoPizza.Models.Users
{
    public class PremiumUser : User
    {
        public int Points { get; set; }
        public DateTime JoinedTime { get; set; } = default;
    }
}
