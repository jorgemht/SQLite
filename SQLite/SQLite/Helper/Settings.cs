﻿namespace SQLite.Helper
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants
        private const string AccessTokenKey = "accessToken_ Key";
        private static readonly string AccessTokenDefault = string.Empty;
        #endregion

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);

            set => AppSettings.AddOrUpdateValue(AccessTokenKey, value);
        }

    }
}

