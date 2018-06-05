﻿using System.Globalization;
using System.Windows.Data;
namespace Paint_0._5
{
    public class CultureResources
    {
        private static ObjectDataProvider resourceProvider =
          (ObjectDataProvider)App.Current.FindResource("Resources");

        public Properties.Resources GetResourceInstance()
        {
            return new Properties.Resources();
        }

        public static void ChangeCulture(CultureInfo culture)
        {
            Properties.Resources.Culture = culture;
            resourceProvider.Refresh();
        }
    }
}
