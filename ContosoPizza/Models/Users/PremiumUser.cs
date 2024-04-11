namespace ContosoPizza.Models.Users
{
    public class PremiumUser : User, IContosoModel
    {
        public int Points { get; set; }
        public DateTime JoinedTime { get; set; } = default;

        public PremiumUser(User baseUser)
        {
            LastOrder = baseUser.LastOrder;
            Address = baseUser.Address;
            City = baseUser.City;
            Email = baseUser.Email;
            Name = baseUser.Name;
            OrdersCount = baseUser.OrdersCount;
        }
    }
}
