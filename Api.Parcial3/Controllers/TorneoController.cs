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
    [Authorize]
    public class TorneoController : ApiController
    {
        [HttpGet]
        [Route("consultarxtipo")]
        public async Task<HttpResponseMessage> ConsultarXTipo(string tipo)
        {
            clsTorneo tor = new clsTorneo();
            return await tor.ConsultarTorneos(tipo);
        }

        [HttpGet]
        [Route("consultarxnombre")]
        public async Task<HttpResponseMessage> ConsultarXNombre(string nombre )
        {
            clsTorneo tor = new clsTorneo();
            return await tor.ConsultarTorneos(null, nombre);
        }

        [HttpGet]
        [Route("consultarxfecha")]
        public async Task<HttpResponseMessage> ConsultarXFecha(DateTime fecha)
        {
            clsTorneo tor = new clsTorneo();
            return await tor.ConsultarTorneos(null, null, fecha);
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