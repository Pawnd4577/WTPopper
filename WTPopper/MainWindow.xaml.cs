﻿using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WTPopper;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int WS_EX_TRANSPARENT = 0x20;
    private const int WS_EX_LAYERED = 0x80000;
    private const int GWL_EXSTYLE = -20;

    private DispatcherTimer fadeTimer;

    public MainWindow()
    {
        InitializeComponent();

        SourceInitialized += (s, e) =>
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, new IntPtr(extendedStyle.ToInt64() | WS_EX_TRANSPARENT | WS_EX_LAYERED));
        };

        var monitor = new UrlMonitor("https://walltaker.joi.how/api/");

        monitor.OnPostUrlChanged += (sender, postUrl) => { Console.WriteLine($"New image detected: {postUrl}"); };

        monitor.OnNewImage += (sender, image) =>
        {
            Console.WriteLine($"New Image downloaded: {image.Width}x{image.Height}");
            DisplayImage(image);
        };

        monitor.Start();
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    private void DisplayImage(ImageSource image)
    {
        try
        {
            WPPopperImage.BeginAnimation(OpacityProperty, null);
            WPPopperImage.Opacity = Configuration.InitialOpacity;

            WPPopperImage.Source = image;
            Show();

            fadeTimer = new DispatcherTimer();
            fadeTimer.Interval = TimeSpan.FromSeconds(15);
            fadeTimer.Tick += (s, e) =>
            {
                fadeTimer.Stop();
                StartFadeOut();
            };
            fadeTimer.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while loading image: {ex.Message}");
        }
    }

    private void StartFadeOut()
    {
        var fadeOutAnimation = new DoubleAnimation
        {
            From = Configuration.InitialOpacity,
            To = Configuration.FinalOpacity,
            Duration = TimeSpan.FromSeconds(Configuration.FadeDuration)
        };

        fadeOutAnimation.Completed += (s, e) =>
        {
            WPPopperImage.Opacity = 1.0;
            Hide();
        };
        WPPopperImage.BeginAnimation(OpacityProperty, fadeOutAnimation);
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    private void Options_Click(object sender, RoutedEventArgs e)
    {
        var options = new OptionsWindows();
        options.Show();
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}