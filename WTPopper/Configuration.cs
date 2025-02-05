using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTPopper
{
    public static class Configuration
    {
        public static double InitialOpacity
        {
            get => Properties.Settings.Default.InitialOpacity;
            set
            {
                Properties.Settings.Default.InitialOpacity = value;
                Properties.Settings.Default.Save();
            }
        }

        public static double FinalOpacity { get; } = 0.0;

        public static double DelayBeforeFade
        {
            get => Properties.Settings.Default.DelayBeforeFade;
            set
            {
                Properties.Settings.Default.DelayBeforeFade = value;
                Properties.Settings.Default.Save();
            }
        }

        public static double FadeDuration
        {
            get => Properties.Settings.Default.FadeDuration;
            set
            {
                Properties.Settings.Default.FadeDuration = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string LinkId
        {
            get => Properties.Settings.Default.LinkId;
            set
            {
                Properties.Settings.Default.LinkId = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
