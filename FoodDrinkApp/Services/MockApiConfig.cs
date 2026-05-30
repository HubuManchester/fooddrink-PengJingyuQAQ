namespace FoodDrinkApp.Services
{
    public partial class MockApiConfig
    {
        public const string EndpointUrl = "https://6a1ae55ebc2f94475492c786.mockapi.io/api/v1/foods";

        public static bool IsConfigured => !string.IsNullOrWhiteSpace(EndpointUrl) &&
                                           EndpointUrl.StartsWith("http");
    }
}