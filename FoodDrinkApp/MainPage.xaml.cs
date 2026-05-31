using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDrinkApp.Models;
using FoodDrinkApp.Services;

namespace FoodDrinkApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync(string.Empty);
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            await LoadDataAsync(RecipeSearchBar.Text);
        }

        private async Task LoadDataAsync(string query)
        {
            var items = await FoodCatalogService.SearchAsync(query);

            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.ImageUrl))
                {
                    item.ImageUrl = GetDefaultImageByCategory(item.Category);
                }
            }

            RecipesCollectionView.ItemsSource = items;
        }

        private string GetDefaultImageByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Chicken_curry_05.JPG/320px-Chicken_curry_05.JPG";

            return category.ToLower() switch
            {
                "breakfast" => "https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Avocado_toast.jpg/320px-Avocado_toast.jpg",
                "lunch" => "https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Avocado_toast.jpg/320px-Avocado_toast.jpg",
                "drink" => "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Iced_Matcha_Latte.jpg/320px-Iced_Matcha_Latte.jpg",
                "dinner" => "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Pound_layer_cake.jpg/320px-Pound_layer_cake.jpg",
                _ => "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Chicken_curry_05.JPG/320px-Chicken_curry_05.JPG"
            };
        }

        private async void OnRecipeCardTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is FoodItem selectedFood)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectedFoodItem", selectedFood }
                };
                await Shell.Current.GoToAsync(nameof(FoodDetailPage), navigationParameter);
            }
        }
    }
}