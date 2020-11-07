using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CafeVirtual.Utilidades
{
    public static class Persistencia
    {
        public static string SerializaParaString<T>(this T valor)
        {
            try
            {
                if (valor == null)
                {
                    return null;
                }

                XmlSerializer xml = new XmlSerializer(valor.GetType());
                StringWriter retorno = new StringWriter();
                xml.Serialize(retorno, valor);
                return retorno.ToString();
            }
            catch
            {
                throw;
            }
        }

        public static object DeserializaParaObjeto(string valor, Type tipo)
        {
            try
            {
                if (valor == null)
                {
                    return null;
                }

                XmlSerializer xml = new XmlSerializer(tipo);
                var valor_serializado = new StringReader(valor);
                return xml.Deserialize(valor_serializado);
            }
            catch
            {
                throw;
            }
        }

        public static T Deserialize<T>(string valor)
        {
            try
            {
                return (T)Convert.ChangeType(DeserializaParaObjeto(valor, typeof(T)), typeof(T));
            }
            catch (ArgumentNullException ex)
            {
                return default(T);
            }
            catch
            {
                throw;
            }
        }
    }
}
