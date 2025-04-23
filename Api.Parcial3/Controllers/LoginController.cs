using Api.Parcial3.Classes;
using Api.Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Parcial3.Controllers
{
    [RoutePrefix("api/Login")]
    //[AllowAnonymous]: Directiva para que el servicio no requiera de autenticación
    //[Authorize]: Directiva para que el servicio necesite de autenticación y un token para que se pueda procesar
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Ingresar")]
        public IQueryable<LoginRespuesta> Ingresar([FromBody] Login login)
        {
            clsLogin _login = new clsLogin();
            _login.login = login;
            return _login.Ingresar();
        }
    }
}