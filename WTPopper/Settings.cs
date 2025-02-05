using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTPopper.Properties
{
    public sealed class Settings : System.Configuration.ApplicationSettingsBase
    {
        private static Settings defaultInstance =
            ((Settings)(System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

        public static Settings Default
        {
            get { return defaultInstance; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1234")]
        public string LinkId
        {
            get { return ((string)this["LinkId"]); }
            set { this["LinkId"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0.9")]
        public double InitialOpacity
        {
            get { return (double)this["InitialOpacity"]; }
            set { this["InitialOpacity"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("15")]
        public double DelayBeforeFade
        {
            get { return (double)this["DelayBeforeFade"]; }
            set { this["DelayBeforeFade"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("3")]
        public double FadeDuration
        {
            get { return (double)this["FadeDuration"]; }
            set { this["FadeDuration"] = value; }
        }
    }
}
