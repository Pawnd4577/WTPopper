using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace WTPopper;

internal class UrlMonitor : IDisposable
{
    private readonly Dispatcher _dispatcher;
    private readonly HttpClient _httpClient;
    private readonly Timer _timer;
    private readonly string _url;
    private byte[] _imageData;
    private string _lastPostUrl;

    public UrlMonitor(string url)
    {
        _url = url;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WTPopper");
        _dispatcher = Application.Current.Dispatcher;

        _timer = new Timer(15000);
        _timer.Elapsed += async (s, e) => await CheckUrl();
        _dispatcher.Invoke(async () => { await CheckUrl(true); });
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _httpClient?.Dispose();
    }

    public event EventHandler<string> OnPostUrlChanged;
    public event EventHandler<ImageSource> OnNewImage;

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private async Task CheckUrl(bool isInitialCheck = false)
    {
        try
        {
            if (Configuration.LinkId == "1234")
                return;

            var jsonResponse = await _httpClient.GetStringAsync(_url + "links/" + Configuration.LinkId + ".json");

            using var document = JsonDocument.Parse(jsonResponse);
            var currentPostUrl = document.RootElement
                .GetProperty("post_url")
                .GetString();

            if (isInitialCheck || currentPostUrl != _lastPostUrl)
            {
                _lastPostUrl = currentPostUrl;

                // Check if a real image and not a video 
                string[] validExtensions = { ".png", ".jpg", ".jpeg", ".gif" };
                string extension = Path.GetExtension(currentPostUrl).ToLower();
                if (!validExtensions.Contains(extension))
                {
                    MessageBox.Show("Format file not supported. Only PNG, JPG, JPEG and GIF are supported.",
                        "Format invalid",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                OnPostUrlChanged?.Invoke(this, currentPostUrl);

                await DownloadImage(currentPostUrl);
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing json: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking url: {ex.Message}");
        }
    }

    private async Task DownloadImage(string imageUrl)
    {
        try
        {
            var response = await _httpClient.GetAsync(imageUrl);
            var stream = await response.Content.ReadAsStreamAsync();

            await _dispatcher.InvokeAsync(() =>
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                OnNewImage?.Invoke(this, bitmapImage);
            });
        }
        catch (Exception ex)
        {
            await _dispatcher.InvokeAsync(() => { Console.WriteLine($"Error image: {ex.Message}"); });
        }
    }

    public Image GetCurrentImage()
    {
        if (_imageData == null) return null;

        using (var ms = new MemoryStream(_imageData))
        {
            return Image.FromStream(ms);
        }
    }
}