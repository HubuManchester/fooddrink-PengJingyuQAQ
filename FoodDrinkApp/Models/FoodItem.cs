using System;

namespace FoodDrinkApp.Models
{
    public class FoodItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public string AllergyNote { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }

        public string CaloriesLabel => $"{Calories} kcal";
        public string MacroSummary => $"Protein {Protein}g, Carbs {Carbs}g, Fat {Fat}g";
        public string AccessibleSummary => $"{Name}. {Category}. {Calories} kcal. {MacroSummary}. {AllergyNote}";
    }
}