using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace UNCDF.CMS
{
    public class CustonActiveDirectory
    {
        public CustonActiveDirectory()
        {

        }

        public CustonActiveDirectory(string domain)
        {
            this.Domain = domain;
        }

        public string Domain
        {
            get;
            private set;
        }

        // Indicamos propiedades solo si se desea cargarlas
        public string Propiedades = "userprincipalname, samaccountname, displayname, mail, sn, givenname";

        private DirectorySearcher BuildUserSearcher(DirectoryEntry de)
        {
            // Agregamos las propiedades (de forma dinamica) que necesitamos obtener
            DirectorySearcher searcher = new DirectorySearcher(de);
            string[] listaProp = Propiedades.Split(Convert.ToChar(","));

            foreach (string prop in listaProp)
                searcher.PropertiesToLoad.Add(prop.Trim());

            return searcher;
            // // searcher.PropertiesToLoad.Add("name");
            // userprincipalname: User name (domain based) Login Name (nombre.apellido@dominio.com) 
            // samaccountname: User name (older systems)  (nombre.apellido)
            // displayname: Display name (Nombre1 Nombre2 Apellido1 Apelledo2)
            // mail: E-mail  

            // sn: Last Name (Surname) (Nombre1 Nombre2) 
            // givenname: Forename , First Name (Apellido1 Apelledo2) 
        }
        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

        public bool AutenticarUsuario(string nombreDominio, string nombreUsuario, string clave)
        {
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + nombreDominio, nombreUsuario, clave);
                DirectorySearcher searcher = new DirectorySearcher(de);
                SearchResult result = searcher.FindOne();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AutenticarUsuario(string nombreDominio, string nombreUsuario, string clave, out Hashtable datosUsuario, out string mensajeError)
        {
            datosUsuario = null;
            mensajeError = "";
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + nombreDominio, nombreUsuario, clave);
                DirectorySearcher searcher = BuildUserSearcher(de);
                searcher.Filter = "samaccountname=" + nombreUsuario;
                SearchResult result = searcher.FindOne();

                if (result == null)
                    return false;
                else
                {
                    datosUsuario = new Hashtable();

                    ResultPropertyCollection listaPropiedades = result.Properties;
                    foreach (string nombreProp in listaPropiedades.PropertyNames)
                    {
                        string separador = "", txt = "";
                        foreach (Object myCollection in listaPropiedades[nombreProp])
                        {
                            txt += separador + myCollection.ToString();
                            separador = "|"; // Normalmente no deberiamos necesitar PROPIEDADES que tengan multiples valores
                        }
                        datosUsuario.Add(nombreProp, txt);
                    }

                    return true;
                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException exDirCom)
            {
                mensajeError = exDirCom.Message.Trim();
            }
            catch (System.Runtime.InteropServices.COMException exCom)
            {
                mensajeError = "No es posible conectarse al Dominio, verifique que el Dominio indicado sea correcto";
            }

            return false;
        }

        public string GetFullUserName(string user, string password)
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://" + Domain, user, password);

            DirectorySearcher searcher = new DirectorySearcher(de);
            searcher.Filter = "samaccountname=" + user;

            SearchResult result = searcher.FindOne();

            var resl = result.Properties["cn"];
            foreach (var item in resl)
            {
                return item.ToString();
            }

            return null;
        }
    }
}