using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeAppFinal
{
    public class Ingredient
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public override string ToString()
        {
            return $"{Quantity} {Unit} of {Name} ({Calories} calories, {FoodGroup})";
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void ConvertUnits(float factor)
        {
            // Define conversion ratios for common unit conversions
            var unitConversions = new Dictionary<string, Dictionary<string, float>>
    {
        { "teaspoon", new Dictionary<string, float> { { "tablespoon", 1.0f / 3 }, { "cup", 1.0f / 48 }, { "ounce", 1.0f / 6 }, { "gram", 5 } } },
        { "tablespoon", new Dictionary<string, float> { { "teaspoon", 3 }, { "cup", 1.0f / 16 }, { "ounce", 1.0f / 2 }, { "gram", 15 } } },
        { "cup", new Dictionary<string, float> { { "teaspoon", 48 }, { "tablespoon", 16 }, { "ounce", 8 }, { "gram", 240 } } },
        { "ounce", new Dictionary<string, float> { { "teaspoon", 6 }, { "tablespoon", 2 }, { "cup", 1.0f / 8 }, { "gram", 28.35f } } },
        { "gram", new Dictionary<string, float> { { "teaspoon", 0.2f }, { "tablespoon", 0.067f }, { "cup", 0.0042f }, { "ounce", 0.035f } } }
        // Add more conversion ratios as needed
    };

            // Check if the current unit has conversion ratios defined
            if (unitConversions.ContainsKey(Unit))
            {
                // Loop through each possible target unit
                foreach (var targetUnit in unitConversions[Unit])
                {
                    // Check if the target unit has a conversion ratio defined
                    if (unitConversions.ContainsKey(targetUnit.Key))
                    {
                        // Calculate the conversion factor from the current unit to the target unit
                        float conversionFactor = unitConversions[Unit][targetUnit.Key];

                        // Convert the quantity to the target unit and scale the quantity
                        float scaledQuantity = Quantity * conversionFactor * factor;

                        // Set the unit to the target unit
                        Unit = targetUnit.Key;
                        Quantity = scaledQuantity;
                        return; // Exit after the first conversion is applied
                    }
                }
                Console.WriteLine($"Conversion to any other unit not supported for unit '{Unit}'.");
            }
            else
            {
                Console.WriteLine($"Unit '{Unit}' not supported for conversion.");
            }
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------//
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public int TotalCalories()
        {
            return Ingredients.Sum(ingredient => ingredient.Calories);
        }

        public override string ToString()
        {
            return Name;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------//
    public class MyIngredients
    {
        public delegate void CalorieNotification(string message);
        public event CalorieNotification OnCaloriesExceeded;

        private List<Recipe> recipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            int totalCalories = recipe.TotalCalories();
            if (totalCalories > 300)
            {
                OnCaloriesExceeded?.Invoke($"Warning: The recipe {recipe.Name} exceeds 300 calories!");
            }
        }

        public List<Recipe> GetAllRecipes()
        {
            return recipes;
        }

        public void Reset()
        {
            recipes.Clear();
        }

        public void ScaleRecipe(string recipeName, float factor)
        {
            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Quantity *= factor;
                }
            }
        }
    

        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        private void HandleCaloriesExceeded(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void DisplayAllRecipes()
        {
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            Console.WriteLine("Recipes List:");
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Console.WriteLine("Enter the number of the recipe you want to view: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice > 0 && choice <= sortedRecipes.Count)
            {
                DisplayRecipe(sortedRecipes[choice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void DisplayRecipe(Recipe recipe, float factor = 1.0f)
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine($"Recipe Name: {recipe.Name}");
            Console.WriteLine("Ingredients: ");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine(ingredient);
            }
            Console.WriteLine("Steps: ");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i]}");
            }
            Console.WriteLine("*************************************************************************");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
       

    }
}
