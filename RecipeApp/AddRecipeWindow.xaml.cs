using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

//Back-end coding for AddRecipeWindow
namespace RecipeApp
{
    public partial class AddRecipeWindow : Window
    {
        //Saving ingredients in a list
        private List<Ingredient> ingredients = new List<Ingredient>();
        public Recipe NewRecipe { get; private set; }

        public AddRecipeWindow()
        {
            InitializeComponent();
        }

        //Activating text box for user input and as a condition for amount of ingredients
        private void IngredientCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(IngredientCountTextBox.Text, out int count))
            {
                IngredientsPanel.Children.Clear();
                for (int i = 0; i < count; i++)
                {
                    AddIngredientFields(i + 1);
                }
            }
        }

        //Displaying form for user input for recipe details
        private void AddIngredientFields(int index)
        {
            var ingredientPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 10, 0, 10) };

            var ingredientHeader = new TextBlock { Text = $"Ingredient {index}", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) };
            ingredientPanel.Children.Add(ingredientHeader);

            var nameLabel = new TextBlock { Text = "Name:" };
            var nameTextBox = new TextBox { Width = 300, Margin = new Thickness(0, 0, 0, 5) };
            nameTextBox.Name = $"NameTextBox_{index}";

            var unitLabel = new TextBlock { Text = "Unit:" };
            var unitTextBox = new TextBox { Width = 100, Margin = new Thickness(0, 0, 0, 5) };
            unitTextBox.Name = $"UnitTextBox_{index}";

            var quantityLabel = new TextBlock { Text = "Quantity:" };
            var quantityTextBox = new TextBox { Width = 100, Margin = new Thickness(0, 0, 0, 5) };
            quantityTextBox.Name = $"QuantityTextBox_{index}";

            var caloriesLabel = new TextBlock { Text = "Calories:" };
            var caloriesTextBox = new TextBox { Width = 100, Margin = new Thickness(0, 0, 0, 5) };
            caloriesTextBox.Name = $"CaloriesTextBox_{index}";
            
            //Creating ComboBox for foodgroup choice
            var foodGroupLabel = new TextBlock { Text = "Food Group:" };
            var foodGroupComboBox = new ComboBox { Width = 150, Margin = new Thickness(0, 0, 0, 10) };
            foodGroupComboBox.Name = $"FoodGroupComboBox_{index}";
            foodGroupComboBox.Items.Add("Vegetables");
            foodGroupComboBox.Items.Add("Fruits");
            foodGroupComboBox.Items.Add("Proteins");
            foodGroupComboBox.Items.Add("Grains");

            ingredientPanel.Children.Add(nameLabel);
            ingredientPanel.Children.Add(nameTextBox);
            ingredientPanel.Children.Add(unitLabel);
            ingredientPanel.Children.Add(unitTextBox);
            ingredientPanel.Children.Add(quantityLabel);
            ingredientPanel.Children.Add(quantityTextBox);
            ingredientPanel.Children.Add(caloriesLabel);
            ingredientPanel.Children.Add(caloriesTextBox);
            ingredientPanel.Children.Add(foodGroupLabel);
            ingredientPanel.Children.Add(foodGroupComboBox);

            IngredientsPanel.Children.Add(ingredientPanel);
        }

        //Save Button to store user input from recipe details
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ingredients.Clear();

            for (int i = 0; i < IngredientsPanel.Children.Count; i++)
            {
                var ingredientPanel = IngredientsPanel.Children[i] as StackPanel;

                var nameTextBox = ingredientPanel.Children[1] as TextBox;
                var unitTextBox = ingredientPanel.Children[3] as TextBox;
                var quantityTextBox = ingredientPanel.Children[5] as TextBox;
                var caloriesTextBox = ingredientPanel.Children[7] as TextBox;
                var foodGroupComboBox = ingredientPanel.Children[9] as ComboBox;

                if (nameTextBox != null && unitTextBox != null && quantityTextBox != null &&
                    caloriesTextBox != null && foodGroupComboBox != null &&
                    !string.IsNullOrWhiteSpace(nameTextBox.Text) &&
                    !string.IsNullOrWhiteSpace(unitTextBox.Text) &&
                    double.TryParse(quantityTextBox.Text, out double quantity) &&
                    int.TryParse(caloriesTextBox.Text, out int calories) &&
                    foodGroupComboBox.SelectedItem != null)
                {
                    string foodGroup = foodGroupComboBox.SelectedItem.ToString();
                    ingredients.Add(new Ingredient(nameTextBox.Text, unitTextBox.Text, quantity, calories, foodGroup));
                }
            }

            //Error message for incase user leaves a box unfilled in the details for recipe
            if (string.IsNullOrEmpty(RecipeNameTextBox.Text) || ingredients.Count == 0)
            {
                MessageBox.Show("Please enter a recipe name and fill in all ingredient details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewRecipe = new Recipe(RecipeNameTextBox.Text) { Ingredients = ingredients };
            DialogResult = true;
            Close();
        }

        //Cancel button for when user wants to cancel
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
