namespace ContosoPizza.Models
{
    public class Pizza : IContosoModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
