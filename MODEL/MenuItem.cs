
using System.Collections.Generic;

namespace Model
{
    public class MenuItem
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int PreparationTime { get; set; }
        public int Quantity { get; set; }
        public WorkPlace WorkPlace
        {
            get
            {
                if(Category >= Category.SoftDrinks)
                {
                    return WorkPlace.Bar;
                }
                else
                {
                    return WorkPlace.Kitchen;
                }
            }
        }

    }
    public enum WorkPlace { Kitchen = 1, Bar}
    public enum Category { LunchMain = 1, LunchSpecials, LunchBites, DinnerStarters, DinnerMains, DinnerDesserts, SoftDrinks, HotDrinks, Beers, Wines, unspecified }
}
