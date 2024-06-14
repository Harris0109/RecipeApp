using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    //Back-end for MainWindow
    public partial class MainWindow : Window
    {
        //Create list to store recipes
        private List<Recipe> recipes = new List<Recipe>();

        //Inicializing MainWindow
        public MainWindow()
        {
            InitializeComponent();
        }

        //Ensuring Add Recipe button works when clicked
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            if (addRecipeWindow.ShowDialog() == true)
            {
                Recipe newRecipe = addRecipeWindow.NewRecipe;
                recipes.Add(newRecipe);
            }
        }

        //Ensuring Recipe details button works when clicked
        private void RecipeDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeDetailsWindow recipeDetailsWindow = new RecipeDetailsWindow(recipes);
            recipeDetailsWindow.Show();
        }
    }

    //Creating class ingredients to get user input
    public class Ingredient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        //To temporarily store user input 
        public Ingredient(string name, string unit, double quantity, int calories, string foodGroup)
        {
            Name = name;
            Unit = unit;
            Quantity = quantity;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    //Creating class recipe to get user input
    public class Recipe
    {
        public string Name { get; set; }

        //List used to store multiple details of user input for ingredients
        public List<Ingredient> Ingredients { get; set; }

        //To temporarily store user input 
        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
        }

        //Use to calculate total calories
        public int CalcCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }
    }
}

