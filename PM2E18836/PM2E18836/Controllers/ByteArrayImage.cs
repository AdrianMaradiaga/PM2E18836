using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PM2E18836.Controllers
{
    public class ByteArrayImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retImage = null; //Validación el objeto a convertir no sean nulos
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                retImage = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
