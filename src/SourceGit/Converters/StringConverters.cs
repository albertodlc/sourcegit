﻿using System;
using System.Globalization;

using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;

namespace SourceGit.Converters
{
    public static class StringConverters
    {
        public class ToLocaleConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return Models.Locale.Supported.Find(x => x.Key == value as string);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (value as Models.Locale).Key;
            }
        }

        public static ToLocaleConverter ToLocale = new ToLocaleConverter();

        public class ToThemeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var theme = (string)value;
                if (theme.Equals("Light", StringComparison.OrdinalIgnoreCase))
                {
                    return ThemeVariant.Light;
                }
                else if (theme.Equals("Dark", StringComparison.OrdinalIgnoreCase))
                {
                    return ThemeVariant.Dark;
                }
                else
                {
                    return ThemeVariant.Default;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var theme = (ThemeVariant)value;
                return theme.Key;
            }
        }

        public static ToThemeConverter ToTheme = new ToThemeConverter();

        public class FormatByResourceKeyConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var key = parameter as string;
                return App.Text(key, value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public static FormatByResourceKeyConverter FormatByResourceKey = new FormatByResourceKeyConverter();

        public static FuncValueConverter<string, string> ToShortSHA =
            new FuncValueConverter<string, string>(v => v.Length > 10 ? v.Substring(0, 10) : v);

        public class ToFontFamilyConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var name = value as string;
                if (string.IsNullOrEmpty(name))
                {
                    return FontManager.Current.DefaultFontFamily;
                }
                else
                {
                    return new FontFamily(name);
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var fontFamily = value as FontFamily;
                return fontFamily == null ? string.Empty : fontFamily.ToString();
            }
        }

        public static ToFontFamilyConverter ToFontFamily = new ToFontFamilyConverter();
    }
}