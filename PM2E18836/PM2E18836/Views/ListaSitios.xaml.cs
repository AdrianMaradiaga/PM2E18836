using PM2E18836.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E18836.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaSitios : ContentPage
    {
        private Sitios sitio;
        public static string Descripcion;
        public static byte[] Imagen;

        public ListaSitios()
        {
            InitializeComponent();
        }

        private void ListaSitios_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            sitio = e.CurrentSelection.FirstOrDefault() as Sitios;
            Descripcion = sitio.descripcion;
            Imagen = sitio.imagen;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListaSitio.ItemsSource = await App.Instancia.ObtenerlistaSitios();
        }

        private async void btneliminarsitio(object sender, EventArgs e)
        {
            if (sitio != null)
            {
                bool confirmacion = await DisplayAlert("Confirmación", "¿Desea eliminar el sitio seleccionado?", "SI", "NO");

                if (confirmacion)
                {
                    try
                    {
                        var EliminarSitio = await App.Instancia.EliminarSitio(sitio);

                        if (EliminarSitio != 0)
                        {
                            await DisplayAlert("Aviso", "Sitio eliminado de manera correcta!", "Ok");
                            ListaSitio.ItemsSource = await App.Instancia.ObtenerlistaSitios();
                        }
                        else
                        {
                            await DisplayAlert("Aviso", "Ha ocurrido un error al eliminar el sitio.", "Ok");
                        }
                    }
                    catch
                    {
                        await DisplayAlert("Aviso", "Ha ocurrido un error al eliminar el sitio.", "Ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Por favor, seleccione un sitio para eliminar.", "Ok");
            }
        }


        private async void btnvermapa(object sender, EventArgs e)
        {
            if (sitio != null)
            {
                bool confirmacion = await DisplayAlert("Confirmación", "¿Desea ver el sitio seleccionado en el mapa?", "SI", "NO");

                if (confirmacion)
                {
                    try
                    {
                        await Navigation.PushAsync(new VerMapa());
                    }
                    catch
                    {
                        await DisplayAlert("Aviso", "Ha ocurrido un error al ver el sitio en el mapa.", "Ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Por favor, seleccione un sitio para ver en el mapa.", "Ok");
            }
        }
    }
}