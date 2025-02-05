using System.Windows;

namespace WTPopper
{
    /// <summary>
    /// Logique d'interaction pour OptionsWindows.xaml
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
            ImageUrlTextBox.Text = Configuration.ImageUrl;
            OpacitySlider.Value = Configuration.InitialOpacity;
            DelaySlider.Value = Configuration.DelayBeforeFade;
            FadeDurationSlider.Value = Configuration.FadeDuration;
        }


        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Uri.TryCreate(ImageUrlTextBox.Text, UriKind.Absolute, out _))
                {
                    Configuration.ImageUrl = ImageUrlTextBox.Text;
                }
                else
                {
                    MessageBox.Show("Url not valid");
                    return;
                }

                Configuration.InitialOpacity = OpacitySlider.Value;
                Configuration.DelayBeforeFade = DelaySlider.Value;
                Configuration.FadeDuration = FadeDurationSlider.Value;

                MessageBox.Show("Parameters saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving parameters : {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
