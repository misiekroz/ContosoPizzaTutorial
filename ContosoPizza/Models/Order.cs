namespace ContosoPizza.Models
{
    public class Order
    {
        public int ID { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public int UserID { get; set; }

        public int PremiumPoints { get; set; }
    }
}
