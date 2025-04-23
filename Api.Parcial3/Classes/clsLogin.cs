using Api.Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Parcial3.Classes
{
    public class clsLogin
    {
        public clsLogin() {
            loginRespuesta = new LoginRespuesta();
        }
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                AdministradorITM usuario = dbExamen.AdministradorITMs.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                //string ClaveCifrada = cifrar.HashPassword(login.Clave);
                //login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                AdministradorITM usuario = dbExamen.AdministradorITMs.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                //Se genera el token
                string Token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in dbExamen.Set<AdministradorITM>()
                       where U.Usuario == login.Usuario &&
                               U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.Usuario,
                           Autenticado = true,
                           Token = Token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> listRpta = new List<LoginRespuesta>();
                listRpta.Add(loginRespuesta);
                return listRpta.AsQueryable();
            }
        }
    }
}