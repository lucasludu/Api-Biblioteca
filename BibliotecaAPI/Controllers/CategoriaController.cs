using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ExtenderBaseController<CategoriaCreateDTO, Categoria, CategoriaDTO>
    {
        public CategoriaController(ApplicationDbContext context, IMapper mapper) : base(context, mapper, "Categoria")
        {

        }
    }
}
