using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace FoodDrinkApp.Services
{
    public static class SpeechService
    {
        private static CancellationTokenSource _cancellationTokenSource;

        public static async Task SpeakAsync(string text)
        {
            Stop();

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await TextToSpeech.Default.SpeakAsync(text, cancelToken: _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
        }

        public static void Stop()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}