using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Api.Parcial3.Models;
using Newtonsoft.Json;

namespace WebServicesParcial1.Classes
{
	public class clsTorneo
    {
        private DBExamenEntities db = new DBExamenEntities();
        public Torneo _torneo { get; set; }
        public async Task<HttpResponseMessage> Insertar()
        {
            try
            {
                db.Torneos.Add(_torneo);
                await db.SaveChangesAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Torneo insertado correctamente", Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error al insertar el torneo: " + ex.Message, Encoding.UTF8, "text/plain")
                };
            }
        }

        public async Task<HttpResponseMessage> Actualizar()
        {
            try
            {
                var torneoExistente = await db.Torneos.FindAsync(_torneo.idTorneos);

                if (torneoExistente == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(
                            $"El torneo con el código {_torneo.idTorneos} no existe, por lo tanto no se puede actualizar.",
                            Encoding.UTF8,
                            "text/plain"
                        )
                    };
                }

                db.Entry(torneoExistente).CurrentValues.SetValues(_torneo);
                await db.SaveChangesAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Se actualizó el torneo correctamente", Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("No se pudo actualizar el torneo: " + ex.Message, Encoding.UTF8, "text/plain")
                };
            }
        }


        public async Task<HttpResponseMessage> ConsultarTorneos(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            try
            {
                var query = db.Torneos.AsQueryable();

                if (!string.IsNullOrEmpty(tipo))
                {
                    query = query.Where(t => t.TipoTorneo.Contains(tipo));
                }
                else if (!string.IsNullOrEmpty(nombre))
                {
                    query = query.Where(t => t.NombreTorneo.Contains(nombre));
                }
                else if (fecha.HasValue)
                {
                    query = query.Where(t => DbFunctions.TruncateTime(t.FechaTorneo) == fecha.Value.Date);
                }

                List<Torneo> torneos = await query.ToListAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(torneos), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error: " + ex.Message)
                };
            }
        }

        public async Task<HttpResponseMessage> Eliminar()
        {
            try
            {
                var torneo = await Consultar(_torneo.idTorneos);

                if (torneo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(
                            $"El torneo con el código {_torneo.idTorneos} no existe, por lo tanto no se puede eliminar.",
                            Encoding.UTF8,
                            "text/plain"
                        )
                    };
                }

                db.Torneos.Remove(torneo);
                await db.SaveChangesAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Se eliminó el torneo correctamente", Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("No se pudo eliminar el torneo: " + ex.Message, Encoding.UTF8, "text/plain")
                };
            }
        }

        public async Task<HttpResponseMessage> Eliminar(int id)
        {
            try
            {
                var torneo = await Consultar(id);

                if (torneo == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(
                            "El torneo con el código ingresado no existe, por lo tanto no se puede eliminar.",
                            Encoding.UTF8,
                            "text/plain"
                        )
                    };
                }

                db.Torneos.Remove(torneo);
                await db.SaveChangesAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Se eliminó el torneo correctamente", Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("No se pudo eliminar el torneo: " + ex.Message, Encoding.UTF8, "text/plain")
                };
            }
        }

        private Task<Torneo> Consultar(int id)
        {
            return db.Torneos.FirstOrDefaultAsync(T => T.idTorneos == id);
        }
    }
}