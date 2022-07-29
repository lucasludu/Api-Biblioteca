using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtenderBaseController<TCreation, TEntity, TDTO> : ControllerBase where TEntity: class, IHaveId
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _controllerName;
        public ExtenderBaseController(ApplicationDbContext context, IMapper mapper, string controllerName)
        {
            _context = context;
            _mapper = mapper;
            _controllerName = controllerName;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDTO>>> Get()
        {
            var entidades = await _context.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDTO>>(entidades);
        }


        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDTO>> Get(int id)
        {
            var entidades = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entidades == null)
            {
                return NotFound();
            }
            return _mapper.Map<TDTO>(entidades);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post(TCreation entidadCreate)
        {
            var entidad = _mapper.Map<TEntity>(entidadCreate);
            _context.Add(entidad);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<TDTO>(entidad);
            return new CreatedAtActionResult(nameof(Get), _controllerName, new { id = entidad.Id }, dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, TCreation entidadCreate)
        {
            var entidad = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            _mapper.Map(entidadCreate, entidad);

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var entidad = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            _context.Entry(entidad).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
