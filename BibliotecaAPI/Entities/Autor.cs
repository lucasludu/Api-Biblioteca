namespace BibliotecaAPI.Entities
{
    public class Autor : IHaveId
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
