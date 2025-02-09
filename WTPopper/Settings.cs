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

    [UserScopedSetting]
    [DefaultSettingValue("100")]
    public double NotificationVolume
    {
        get => (double)this["NotificationVolume"];
        set => this["NotificationVolume"] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("false")]
    public bool RepopActive
    {
        get => (bool)this["RepopActive"];
        set => this["RepopActive"] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("300")]
    public double RepopTimer
    {
        get => (double)this["RepopTimer"];
        set => this["RepopTimer"] = value;
    }
}