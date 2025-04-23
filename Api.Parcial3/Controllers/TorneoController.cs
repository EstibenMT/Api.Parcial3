using Api.Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebServicesParcial1.Classes;

namespace Api.Parcial3.Controllers
{
    [RoutePrefix("api/torneo")]
    public class TorneoController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Consultar(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            clsTorneo tor = new clsTorneo();
            return await tor.ConsultarTorneos(tipo, nombre, fecha);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insertar([FromBody] Torneo torneo)
        {
            clsTorneo tor = new clsTorneo();
            tor._torneo = torneo;
            return await tor.Insertar();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Actualizar([FromBody] Torneo torneo)
        {
            clsTorneo tor = new clsTorneo();
            tor._torneo = torneo;
            return await tor.Actualizar();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Eliminar([FromBody] Torneo torneo)
        {
            clsTorneo tor = new clsTorneo();
            tor._torneo = torneo;
            return await tor.Eliminar();
        }

        [HttpDelete]
        [Route("eliminarxid")]
        public async Task<HttpResponseMessage> EliminarXId(int id)
        {
            clsTorneo tor = new clsTorneo();
            return await tor.Eliminar(id);
        }
    }
}