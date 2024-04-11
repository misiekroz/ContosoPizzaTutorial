namespace ContosoPizza.Models
{
    public class Order : IContosoModel
    {
        public int ID { get; set; }

        public List<int> PizzaIDs { get; set; } = [];

        public int UserID { get; set; }

        public int PremiumPoints { get; set; }

        public bool Received { get; set; }
    }
}
