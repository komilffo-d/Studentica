﻿using Microsoft.Extensions.Configuration;

namespace Studentica.Services.Common
{
    public static class SettingsHelper
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration) where TSettings : ISettings
        {
            var type = typeof(TSettings).Name;
            return configuration.GetSection(type).Get<TSettings>()
                   ?? throw new Exception($"'{type}' не найден в файле 'appsettings.json'!");
        }
    }
}
