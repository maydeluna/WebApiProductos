using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProductos.Entidades;
using WebApiProductos.Migrations;

namespace WebApiProductos.Controllers
{
  
        [ApiController]
        [Route("api/empresas")]
        public class EmpresasController : ControllerBase
        {
            private readonly ApplicationDbContext dbContext;

            public EmpresasController(ApplicationDbContext context)
            {
                this.dbContext = context;
            }

        [HttpGet]

        public async Task<ActionResult<List<Empresa>>> GetAll()
        {
            return await dbContext.Empresas.ToListAsync();

        }

        [HttpGet("{id: int}")]

        public async Task<ActionResult<Empresa>> GetById(int id)
        {
            return await dbContext.Empresas.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpPost]

        public async Task<ActionResult> Post(Empresa empresa)
        {
            var existeProducto = await dbContext.Productos.AnyAsync(x => x.Id == empresa.ProductoId);
            if (!existeProducto)
            {
                return BadRequest($"No existe el producto con el id: {empresa.ProductoId}");
            }

            dbContext.Add(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Empresa empresa, int id)
        {
            var exist = await dbContext.Empresas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("La empresa epecificada no existe");
            }
            if (empresa.Id != id)
            {
                return BadRequest("El id de la empresa no coincide con el establecido en la url");
            }

            dbContext.Update(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Empresas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            //var validateRelation = await dbContext.ProductoEmpresa.AnyAsync

            
            dbContext.Remove(new Empresa { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }

}
