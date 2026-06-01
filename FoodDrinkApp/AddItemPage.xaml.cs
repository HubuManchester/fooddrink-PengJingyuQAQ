using System;
using Microsoft.Maui.Controls;
using FoodDrinkApp.Models;
using FoodDrinkApp.Services;

namespace FoodDrinkApp
{
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(CategoryEntry.Text) ||
                string.IsNullOrWhiteSpace(CaloriesEntry.Text))
            {
                await DisplayAlert("Validation Error", "Name, Category, and Calories are required fields.", "OK");
                return;
            }

            if (!int.TryParse(CaloriesEntry.Text, out int calories) || calories < 0)
            {
                await DisplayAlert("Validation Error", "Please enter a valid positive number for Calories.", "OK");
                return;
            }

            var newItem = new FoodItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = NameEntry.Text.Trim(),
                Category = CategoryEntry.Text.Trim(),
                Calories = calories,
                Protein = (int)(calories * 0.15 / 4),
                Carbs = (int)(calories * 0.50 / 4),
                Fat = (int)(calories * 0.35 / 9),
                Description = string.IsNullOrWhiteSpace(DescriptionEditor.Text) ? "No description provided." : DescriptionEditor.Text.Trim(),
                AllergyNote = string.IsNullOrWhiteSpace(AllergyEntry.Text) ? "None" : AllergyEntry.Text.Trim(),
                Tags = string.IsNullOrWhiteSpace(TagsEntry.Text) ? "General" : TagsEntry.Text.Trim(),
                ImageUrl = string.IsNullOrWhiteSpace(ImageUrlEntry.Text) ? "https://via.placeholder.com/150" : ImageUrlEntry.Text.Trim()
            };

            await FoodCatalogService.AddAsync(newItem);

            await DisplayAlert("Success", "Recipe added successfully!", "OK");

            NameEntry.Text = string.Empty;
            CategoryEntry.Text = string.Empty;
            CaloriesEntry.Text = string.Empty;
            DescriptionEditor.Text = string.Empty;
            AllergyEntry.Text = string.Empty;
            TagsEntry.Text = string.Empty;
            ImageUrlEntry.Text = string.Empty;
        }
    }
}