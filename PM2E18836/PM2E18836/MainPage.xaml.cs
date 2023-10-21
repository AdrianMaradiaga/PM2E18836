using PM2E18836.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E18836
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }
        private  void btnTomarFoto(object sender, EventArgs e)
        {

        }

        private void btnListaSitios(object sender, EventArgs e)
        {

        }

        private void btnAgregarSitio(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(foto.Source.ToString()) ||
                string.IsNullOrEmpty(txtLatitud.Text) || string.IsNullOrEmpty(txtLongitud.Text))
            {
                // Si falta alguno de los campos requeridos, muestra una alerta.
                DisplayAlert("Error", "Por favor complete todos los campos antes de guardar.", "OK");
            }
            else
            {
                // Aquí deberías implementar la lógica para guardar el sitio en la base de datos SQLite.
                // Puedes usar SQLite-net-pcl u otra biblioteca para interactuar con la base de datos.
                // Después de guardar, puedes mostrar un mensaje de éxito y restablecer los campos si es necesario.

                // Ejemplo:
                // var sitio = new Sitio
                // {
                //     Imagen = foto.Source.ToString(),
                //     Latitud = txtLatitud.Text,
                //     Longitud = txtLongitud.Text,
                //     Descripcion = txtDescripcion.Text
                // };
                // // Lógica para guardar el sitio en la base de datos.

                // Restablecer los campos después de guardar.
                foto.Source = "camera2.png";
                txtLatitud.Text = "";
                txtLongitud.Text = "";
                txtDescripcion.Text = "";
            }
        }


        private void btnSalirApp(object sender, EventArgs e)
        {
            // Aquí deberías implementar la lógica para salir de la aplicación.
            // Puedes usar la función App.Current.MainPage.Navigation.PopAsync() para volver a la página anterior o App.Current.Quit() para cerrar la aplicación, dependiendo de tus necesidades.
        }


    }
}
