using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyRecipeAppFinal
{
    /// <summary>
    /// Interaction logic for EnterRecipeWindow.xaml
    /// </summary>
    public partial class EnterRecipeWindow : Window
    {
        private MyIngredients myIngredients;
        private Recipe newRecipe;

        public EnterRecipeWindow(MyIngredients ingredients)
        {
            InitializeComponent();
            myIngredients = ingredients;
            newRecipe = new Recipe();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = new Ingredient
            {
                Name = IngredientNameTextBox.Text,
                Quantity = float.Parse(IngredientQuantityTextBox.Text),
                Unit = IngredientUnitTextBox.Text,
                Calories = int.Parse(IngredientCaloriesTextBox.Text),
                FoodGroup = IngredientFoodGroupTextBox.Text
            };
            newRecipe.Ingredients.Add(ingredient);
            MessageBox.Show("Ingredient added.");
            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            IngredientFoodGroupTextBox.Clear();
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            newRecipe.Steps.Add(StepTextBox.Text);
            MessageBox.Show("Step added.");
            StepTextBox.Clear();
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            newRecipe.Name = RecipeNameTextBox.Text;
            myIngredients.AddRecipe(newRecipe);
            MessageBox.Show("Recipe saved.");
            Close();
        }
    }
}
