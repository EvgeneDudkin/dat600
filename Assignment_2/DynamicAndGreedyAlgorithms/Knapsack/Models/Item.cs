namespace Knapsack.Models
{
    public class Item
    {
        public Item( int weight, int price )
        {
            Weight = weight;
            Price = price;
        }

        public int Weight { get; }
        public int Price { get; }
    }
}