namespace Blog.API.Models.DTOs
{
    public class CategoryRequestDTO
    {
        public string Name { get; init; } = string.Empty; // Nesta classe crio propriedades/construtores conforme minha aplicação necessita, (request)
                                                          // com isso eu deixo minha classe Moldels inauterável (para um ambiente mais organaizado, padrão normalmente seguido)


    }
}
