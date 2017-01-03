namespace Ranaitfleur.Model
{
    public class Item
    {
        public int Id { get; set; }
        public int ItemType { get; set; }
        public string Name { get; set; }
        public int NoOfItemInStock { get; set; }
        public int Price { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public float Weight { get; set; }
        public string Dimentions { get; set; }
        public string ImagePath { get; set; }
    }
}
