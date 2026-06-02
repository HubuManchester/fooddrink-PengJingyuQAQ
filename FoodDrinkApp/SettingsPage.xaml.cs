using Microsoft.Maui.Controls;

namespace FoodDrinkApp
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            ThemeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
        }

        private void OnThemeToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}