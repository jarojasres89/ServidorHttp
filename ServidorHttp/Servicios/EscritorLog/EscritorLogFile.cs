using System;
using System.IO;
using System.Text;

namespace ServidorHttp.Servicios.EscritorLog
{
    public class EscritorLogFile : IEscritorLog
    {
        public EscritorLogFile()
        {
            Console.WriteLine($"Ruta archivo de Log: {RutaArchivoLog}");
        }
        private string RutaArchivoLog = Path.Combine(Environment.CurrentDirectory, "Log.txt");
        public void RegistrarLog(string mensaje)
        {
            Console.WriteLine(mensaje);
            EscribirArchivo(mensaje);
        }

        private void EscribirArchivo(string mensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append(mensaje);
            
            File.AppendAllText(RutaArchivoLog, sb.ToString());
            sb.Clear();
        }
    }
}
