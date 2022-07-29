using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entities;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //----------------- CATEGORIAS ---------------------
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
            //----------------- EDITORIALES ---------------------
            CreateMap<Editorial, EditorialDTO>().ReverseMap();
            CreateMap<Editorial, EditorialCreateDTO>().ReverseMap();
            //----------------- AUTORES ---------------------
            CreateMap<Autor, AutorDTO>().ReverseMap();

            CreateMap<AutorCreateDTO, Autor>()
                .ForMember(m => m.Image, option => option.Ignore());

        }
    }
}
