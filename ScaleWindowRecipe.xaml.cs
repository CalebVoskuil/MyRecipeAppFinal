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
    /// Interaction logic for ScaleWindowRecipe.xaml
    /// </summary>
    public partial class ScaleWindowRecipe : Window
    {
        private MyIngredients myIngredients;

        public ScaleWindowRecipe(MyIngredients ingredients)
        {
            InitializeComponent();
            myIngredients = ingredients;
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text;
            float factor = float.Parse(ScalingFactorTextBox.Text);
            myIngredients.ScaleRecipe(recipeName, factor);
            MessageBox.Show("Recipe scaled.");
            Close();
        }
    }
}
