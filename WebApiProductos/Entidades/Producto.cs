namespace WebApiProductos.Entidades
{
    public class Producto
    {

        public int Id { get; set; }

        public string Nombre { get; set; }
        public List<Empresa> Empresas { get; set; }
    }
}
