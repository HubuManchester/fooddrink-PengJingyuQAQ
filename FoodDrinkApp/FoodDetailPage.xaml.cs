using Microsoft.Maui.Controls;
using System;
using FoodDrinkApp.Models;
using FoodDrinkApp.Services;

namespace FoodDrinkApp
{
    [QueryProperty(nameof(FoodData), "SelectedFoodItem")]
    public partial class FoodDetailPage : ContentPage
    {
        private FoodItem _foodData;
        public FoodItem FoodData
        {
            get => _foodData;
            set
            {
                _foodData = value;
                OnPropertyChanged();
                BindingContext = _foodData;
            }
        }

        public FoodDetailPage()
        {
            InitializeComponent();
        }

        private async void OnReadAloudClicked(object sender, EventArgs e)
        {
            if (FoodData != null)
            {
                await SpeechService.SpeakAsync(FoodData.AccessibleSummary);
            }
        }

        private void OnStopSpeechClicked(object sender, EventArgs e)
        {
            SpeechService.Stop();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SpeechService.Stop();
        }
    }
}