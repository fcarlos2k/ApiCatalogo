using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings; 

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Produto, ProdutoDto>().ReverseMap();   
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
    }
}
