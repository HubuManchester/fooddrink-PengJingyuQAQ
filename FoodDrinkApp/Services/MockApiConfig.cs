namespace FoodDrinkApp.Services
{
    public static class MockApiConfig
    {
        public const string EndpointUrl = "";
        public static bool IsConfigured => !string.IsNullOrWhiteSpace(EndpointUrl);
    }
}