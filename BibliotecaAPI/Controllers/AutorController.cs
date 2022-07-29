using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ExtenderBaseController<AutorCreateDTO, Autor, AutorDTO>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;

        public AutorController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos) : base(context, mapper, "Autor")
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }

        public async override Task<ActionResult> Post([FromForm]AutorCreateDTO entidadCreate)
        {
            var entity = _mapper.Map<Autor>(entidadCreate);

            if(entidadCreate.Image != null)
            {
                string fotoUrl = await GuardarFoto(entidadCreate.Image);
                entity.Image = fotoUrl;
            }
            _context.Add(entity);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<AutorDTO>(entity);

            return new CreatedAtActionResult(nameof(Get), "Autor", new {id = entity.Id}, dto);
        }

        public async override Task<ActionResult> Put(int id,[FromForm] AutorCreateDTO entidadCreate)
        {
            var entity = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if(entity == null)
            {
                return NotFound();
            }

            _mapper.Map(entidadCreate, entity);

            if(entidadCreate.Image != null)
            {
                if(!string.IsNullOrEmpty(entity.Image))
                {
                    await _almacenadorArchivos.Borrar(entity.Image, ConstanteDeAplicaciones.ContenedorDeArchivos.ContenedorDeAutores);
                }
                string fotoUrl = await GuardarFoto(entidadCreate.Image);
                entity.Image = fotoUrl;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> GuardarFoto(IFormFile image)
        {
            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);

            var fileBytes = stream.ToArray();
            return await _almacenadorArchivos.Crear(
                fileBytes, 
                image.ContentType, 
                Path.GetExtension(image.FileName), 
                ConstanteDeAplicaciones.ContenedorDeArchivos.ContenedorDeAutores, 
                Guid.NewGuid().ToString()
            );
        }

    }
}
