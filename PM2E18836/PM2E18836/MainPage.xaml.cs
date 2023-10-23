using PM2E18836.Models;
using PM2E18836.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Media;

namespace PM2E18836
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ObtenerLatitudyLongitud();
        }

        public async void ObtenerLatitudyLongitud()
        {
            try
            {
                var geolocarequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                var Cancelatoken = new System.Threading.CancellationTokenSource();
                var ubicacion = await Geolocation.GetLocationAsync(geolocarequest, Cancelatoken.Token);

                if (ubicacion != null)
                {
                    txtLatitud.Text = ubicacion.Latitude.ToString();
                    txtLongitud.Text = ubicacion.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Alerta", "Este dispositivo no soporta el proceso de Geolocalización" + fnsEx, "Ok");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Alerta", "El GPS se encuentra desactivado, por favor activar el GPS!", "Ok");
                System.Diagnostics.Process.GetCurrentProcess().Kill(); //Se cierra la app, el usuario debe activar el GPS

            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Alerta", "La aplicación no cuenta con los permisos para acceder al GPS" + pEx, "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ocurrió un error al acceder a su ubicación" + ex, "Ok");
            }
        }

        byte[] GuardarImagen;
        private async void btnTomarFoto(object sender, EventArgs e)
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MyApp",
                Name = DateTime.Now.ToString() + "_foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null)
            {
                GuardarImagen = null;
                MemoryStream memoryStream = new MemoryStream();

                photo.GetStream().CopyTo(memoryStream);
                GuardarImagen = memoryStream.ToArray();

                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
            ObtenerLatitudyLongitud();
            txtDescripcion.Focus(); //Se coloca el puntero en Descripción
        }

        private async void btnListarSitios(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaSitios());
        }

        private async void btnAgregarSitio(object sender, EventArgs e)
        {
            if (GuardarImagen == null)
            {
                await DisplayAlert("Aviso", "Por favor, tome una imagen del sitio!", "OK");
            }
            else if (txtDescripcion.Text == null)
            {
                await DisplayAlert("Aviso", "Por favor, ingrese la descripción del sitio!", "OK");
            }
            else 
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(foto.Source.ToString()) ||
                string.IsNullOrEmpty(txtLatitud.Text) || string.IsNullOrEmpty(txtLongitud.Text))
                {
                    // Si falta alguno de los campos requeridos, muestra una alerta.
                    await DisplayAlert("Error", "Por favor complete todos los campos antes de guardar.", "OK");
                }
                else
                {
                    var sitio = new Sitios
                    {
                        imagen = GuardarImagen,
                        latitud = txtLatitud.Text,
                        longitud = txtLongitud.Text,
                        descripcion = txtDescripcion.Text
                    };
                    var resultado = await App.Instancia.GuardarSitio(sitio);

                    if (resultado != 0)
                    {
                        await DisplayAlert("Aviso", "¡Sitio ingresado con exito!", "OK");
                        // Restablecer los campos después de guardar.
                        foto.Source = "camera2.png";
                        txtLatitud.Text = "";
                        txtLongitud.Text = "";
                        txtDescripcion.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "Ha Ocurrido un Error", "OK");
                    }
                    await Navigation.PopAsync();
                }
            }
        }

        private void btnSalirApp(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}