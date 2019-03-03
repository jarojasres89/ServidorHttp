namespace ServidorHttp.Modelo
{
    public class Encabezado
    {
        public Encabezado(string nombre, string valor)
        {
            Nombre = nombre;
            Valor = valor;
        }
        public string Nombre { get; }
        public string Valor { get; }
    }
}
