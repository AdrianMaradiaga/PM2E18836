using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PM2E18836.Models;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PM2E18836.Controllers
{
    public class DBSitios
    {
        readonly SQLiteAsyncConnection dbSitio;
        public DBSitios(string Path) 
        { 
            dbSitio = new SQLiteAsyncConnection(Path);
            //Creación de las tablas en la BD 
            dbSitio.CreateTableAsync<Sitios>(); 
        }

        /* CRUD de la DB Sitios */
        public Task<int> GuardarSitio(Sitios sitios) 
        {
            if (sitios.id != 0)
            {
                return dbSitio.UpdateAsync(sitios); //Actualización
            }
            else
            {
                return dbSitio.InsertAsync(sitios); //Inserción
            }
        }

        //Lectura
        public Task<List<Sitios>> ObtenerlistaSitios()
        {
            return dbSitio.Table<Sitios>().ToListAsync();
        }

        //Eliminación
        public Task<int> EliminarSitio(Sitios sitios)
        {
            return dbSitio.DeleteAsync(sitios);
        }

        // Obtener Longitud de UbicacionesDB
        public Task<Sitios> ObtenerLongitud(string Longitude)
        {
            return dbSitio.Table<Sitios>().Where(i => i.longitud == Longitude).FirstOrDefaultAsync();
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Sitios> ObtenerLatitud(string Latitude)
        {
            return dbSitio.Table<Sitios>().Where(i => i.latitud == Latitude).FirstOrDefaultAsync();
        }

        // Obtener Descripcion de UbicacionesDB
        public Task<Sitios> ObtenerDescripcion(String uDescripcion)
        {
            return dbSitio.Table<Sitios>().Where(i => i.descripcion == uDescripcion).FirstOrDefaultAsync();
        }
    }
}
