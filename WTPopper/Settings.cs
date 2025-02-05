using System.Configuration;

namespace WTPopper.Properties;

public sealed class Settings : ApplicationSettingsBase
{
    public static Settings Default { get; } = (Settings)Synchronized(new Settings());

    [UserScopedSetting]
    [DefaultSettingValue("1234")]
    public string LinkId
    {
        get => (string)this["LinkId"];
        set => this["LinkId"] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("0.9")]
    public double InitialOpacity
    {
        get => (double)this["InitialOpacity"];
        set => this["InitialOpacity"] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("15")]
    public double DelayBeforeFade
    {
        get => (double)this["DelayBeforeFade"];
        set => this["DelayBeforeFade"] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("3")]
    public double FadeDuration
    {
        get => (double)this["FadeDuration"];
        set => this["FadeDuration"] = value;
    }
}