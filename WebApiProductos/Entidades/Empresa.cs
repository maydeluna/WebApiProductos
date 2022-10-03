namespace WebApiProductos.Entidades
{
    public class Empresa
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
