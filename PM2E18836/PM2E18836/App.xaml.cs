using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E18836.Controllers;
using System.IO;

namespace PM2E18836
{
    public partial class App : Application
    {
        static DBSitios instancia;

        public static DBSitios Instancia
        {
            get
            {
                if (instancia == null)
                {
                    string dbname = "SitiosDB.db3";
                    string dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string dbfull = Path.Combine(dbpath, dbname);
                    instancia = new DBSitios(dbfull);
                }
                return instancia;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
