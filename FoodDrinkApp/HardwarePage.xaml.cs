using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Linq;
using System.Threading.Tasks;
using FoodDrinkApp.Services;

namespace FoodDrinkApp
{
    public partial class HardwarePage : ContentPage
    {
        public HardwarePage()
        {
            InitializeComponent();
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    var photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        var stream = await photo.OpenReadAsync();
                        FoodPhoto.Source = ImageSource.FromStream(() => stream);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnLocationClicked(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        LocationLabel.Text = $"{placemark.Locality}, {placemark.AdminArea}, {placemark.CountryName}\nLat: {location.Latitude:F4}, Lng: {location.Longitude:F4}";
                    }
                    else
                    {
                        LocationLabel.Text = $"Lat: {location.Latitude:F4}, Lng: {location.Longitude:F4}";
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnSpeakClicked(object sender, EventArgs e)
        {
            try
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));
                await SpeechService.SpeakAsync("Demonstrating hardware integration capabilities.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnStopSpeakClicked(object sender, EventArgs e)
        {
            SpeechService.Stop();
        }

        private async void OnFlashlightOnClicked(object sender, EventArgs e)
        {
            try
            {
                await Flashlight.Default.TurnOnAsync();
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Notice", "Flashlight is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnFlashlightOffClicked(object sender, EventArgs e)
        {
            try
            {
                await Flashlight.Default.TurnOffAsync();
            }
            catch (Exception)
            {
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            SpeechService.Stop();
            try
            {
                await Flashlight.Default.TurnOffAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}