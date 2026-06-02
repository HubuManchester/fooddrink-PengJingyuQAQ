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
                LocationLabel.Text = "Locating... Please wait for satellite link.";

                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(8));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location == null)
                {
                    location = await Geolocation.Default.GetLastKnownLocationAsync();
                }

                if (location != null)
                {
                    try
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
                    catch (Exception)
                    {
                        // Strategy 2: If reverse geocoding fails due to network/GMS blocks, always output raw coordinates
                        LocationLabel.Text = $"Lat: {location.Latitude:F4}, Lng: {location.Longitude:F4}";
                    }
                }
                else
                {
                    // Strategy 3: Explicitly prompt user if both scanning and cache are unavailable
                    LocationLabel.Text = "Location service timed out. Please check your phone's GPS switch.";
                }
            }
            catch (PermissionException)
            {
                LocationLabel.Text = "Location permission denied.";
                await DisplayAlert("Permission Required", "Please grant location access in your system app settings.", "OK");
            }
            catch (Exception ex)
            {
                LocationLabel.Text = $"Error: {ex.Message}";
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