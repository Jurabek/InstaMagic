// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace InstaMagic.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string api_key = "api_key";
        private static readonly string TokenDefault = string.Empty;

        #endregion


        public static string TokenSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(api_key, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(api_key, value);
            }
        }

    }
}