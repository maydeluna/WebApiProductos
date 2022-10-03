using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProductos.Entidades;

namespace WebApiProductos.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await dbContext.Productos.Include(x=> x.Empresas).ToListAsync();
  
  
        }

        [HttpPost]

        public async Task<ActionResult> Post(Producto producto)
        {
            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")] // api/productos/1

        public async Task<ActionResult> Put(Producto producto, int id)
        {
            if(producto.Id !=id)
            {
                return BadRequest("El id del producto no coincide con el establecido en la url");
            }

            dbContext.Update(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Productos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Producto()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
