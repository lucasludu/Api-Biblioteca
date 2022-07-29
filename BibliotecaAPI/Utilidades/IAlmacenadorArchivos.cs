namespace BibliotecaAPI.Utilidades
{
    public interface IAlmacenadorArchivos
    {
        public Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre);
        public Task Borrar(string ruta, string container);
    }
}
