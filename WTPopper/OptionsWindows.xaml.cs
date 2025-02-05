using System.Windows;

namespace WTPopper;

/// <summary>
///     Logique d'interaction pour OptionsWindows.xaml
/// </summary>
public partial class OptionsWindows : Window
{
    public OptionsWindows()
    {
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        LinkIdTextBox.Text = Configuration.LinkId;
        OpacitySlider.Value = Configuration.InitialOpacity;
        DelaySlider.Value = Configuration.DelayBeforeFade;
        FadeDurationSlider.Value = Configuration.FadeDuration;
        NotificationVolumeSlider.Value = Configuration.NotificationVolume;
    }


    private void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Configuration.LinkId = LinkIdTextBox.Text;
            Configuration.InitialOpacity = OpacitySlider.Value;
            Configuration.DelayBeforeFade = DelaySlider.Value;
            Configuration.FadeDuration = FadeDurationSlider.Value;
            Configuration.NotificationVolume = NotificationVolumeSlider.Value;

            MessageBox.Show("Parameters saved");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while saving parameters : {ex.Message}");
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}