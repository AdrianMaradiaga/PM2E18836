using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;

namespace PM2E18836.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerMapa : ContentPage
    {
        public VerMapa()
        {
            InitializeComponent();
        }

        private async void btnCompartir_Clicked(object sender, EventArgs e)
        {
            await CompartirImagen(ListaSitios.Imagen, "LocalizacionImagen.jpg");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var location = await Geolocation.GetLocationAsync();

            if (location == null)
            {
                await DisplayAlert("Advertencia", "Su GPS se encuentra desactivado", "Ok");
            }
            else
            {
                Pin ubicacion = new Pin();
                ubicacion.Label = "Ubicación: "+ListaSitios.Descripcion;
                ubicacion.Type = PinType.Place;
                ubicacion.Position = new Position(location.Latitude, location.Longitude);
                mapa.Pins.Add(ubicacion);
                mapa.IsShowingUser = true;
                mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(500.0)));
            }
        }

        async Task CompartirImagen(byte[] imagen, string filename)
        {
            var file = Path.Combine(FileSystem.CacheDirectory, filename);
            File.WriteAllBytes(file, imagen);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Compartir Imagen de la Ubicación",
                File = new ShareFile(file)
            });
        }
    }
}