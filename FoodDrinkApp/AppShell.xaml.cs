using Microsoft.Maui.Controls;

namespace FoodDrinkApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FoodDetailPage), typeof(FoodDetailPage));
        }
    }
}