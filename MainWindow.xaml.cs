using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MyRecipeAppFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Caleb Voskuil
    /// ST10397320
    /// Prog6221
    /// group 1
    /// References:https://youtu.be/GhQdlIFylQ8?si=cKIYSWunHDC1csWj
    /// https://youtu.be/gfkTfcpWqAY?si=3BredEtmLRK-IVXr
    /// https://youtu.be/wxznTygnRfQ?si=YdSPdyKekHStCUFz
    /// </summary>
    public partial class MainWindow : Window
    {
        MyIngredients myIngredients = new MyIngredients();

        public MainWindow()
        {
            InitializeComponent();
            myIngredients.OnCaloriesExceeded += Message => MessageBox.Show(Message);
        }

        private void EnterRecipe_Click(object sender, RoutedEventArgs e)
        {
            var enterRecipeWindow = new EnterRecipeWindow(myIngredients);
            enterRecipeWindow.ShowDialog();
        }

        private void DisplayAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            RecipeListBox.Items.Clear();
            var sortedRecipes = myIngredients.GetAllRecipes().OrderBy(r => r.Name).ToList();
            foreach (var recipe in sortedRecipes)
            {
                RecipeListBox.Items.Add(recipe.Name);
            }
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            var scaleWindowRecipe = new ScaleWindowRecipe(myIngredients);
            scaleWindowRecipe.ShowDialog();
        }

        private void ResetAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            myIngredients.Reset();
            RecipeListBox.Items.Clear();
            MessageBox.Show("All recipes have been reset.");
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            string filter = FilterTextBox.Text.ToLower();
            RecipeListBox.Items.Clear();
            var filteredRecipes = myIngredients.GetAllRecipes().Where(r =>
                r.Ingredients.Any(i => i.Name.ToLower().Contains(filter)) ||
                r.Ingredients.Any(i => i.FoodGroup.ToLower().Contains(filter)) ||
                r.TotalCalories() <= int.Parse(filter)).OrderBy(r => r.Name).ToList();
            foreach (var recipe in filteredRecipes)
            {
                RecipeListBox.Items.Add(recipe.Name);
            }
        }
    }
}