using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    //Back-end for RecipeDetailsWindow
    public partial class RecipeDetailsWindow : Window
    {
        //Creating List to store recipes
        private List<Recipe> recipes;

        //Initializing RecipeDetailsWindow and list for recipes
        public RecipeDetailsWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
            UpdateRecipeList();
        }

        //Ensuring Filtering boxes work
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRecipeList();
        }

        //Analysing user input and comparing to stored recipe details to get correct outcome
        private void UpdateRecipeList()
        {
            var filteredRecipes = recipes;

            if (!string.IsNullOrEmpty(FilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Name.Contains(FilterTextBox.Text)).ToList();
            }

            if (!string.IsNullOrEmpty(IngredientFilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(IngredientFilterTextBox.Text))).ToList();
            }

            if (FoodGroupFilterComboBox.SelectedItem != null)
            {
                var selectedFoodGroup = (FoodGroupFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (!string.IsNullOrEmpty(selectedFoodGroup))
                {
                    filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup == selectedFoodGroup)).ToList();
                }
            }

            if (int.TryParse(MaxCaloriesTextBox.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(r => r.CalcCalories() <= maxCalories).ToList();
            }

            RecipeListBox.ItemsSource = filteredRecipes;
            RecipeListBox.DisplayMemberPath = "Name";
        }

        //Once correct recipe is chosen the details are displayed
        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                var details = $"Recipe: {selectedRecipe.Name}\nTotal Calories: {selectedRecipe.CalcCalories()}\nIngredients:\n";
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    details += $"{ingredient.Name} - {ingredient.Quantity} {ingredient.Unit} - {ingredient.Calories} calories - {ingredient.FoodGroup}\n";
                }
                MessageBox.Show(details, "Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Close button to stop program
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
