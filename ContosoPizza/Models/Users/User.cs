namespace ContosoPizza.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public DateTime LastOrder { get; set; } = default;

        public int OrdersCount { get; set; }

    }
}
