using WTPopper.Properties;

namespace WTPopper;

public static class Configuration
{
    public static double InitialOpacity
    {
        get => Settings.Default.InitialOpacity;
        set
        {
            Settings.Default.InitialOpacity = value;
            Settings.Default.Save();
        }
    }

    public static double FinalOpacity { get; } = 0.0;

    public static double DelayBeforeFade
    {
        get => Settings.Default.DelayBeforeFade;
        set
        {
            Settings.Default.DelayBeforeFade = value;
            Settings.Default.Save();
        }
    }

    public static double FadeDuration
    {
        get => Settings.Default.FadeDuration;
        set
        {
            Settings.Default.FadeDuration = value;
            Settings.Default.Save();
        }
    }

    public static string LinkId
    {
        get => Settings.Default.LinkId;
        set
        {
            Settings.Default.LinkId = value;
            Settings.Default.Save();
        }
    }

    public static double NotificationVolume
    {
        get => Settings.Default.NotificationVolume;
        set
        {
            Settings.Default.NotificationVolume = value;
            Settings.Default.Save();
        }
    }
}