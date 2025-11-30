namespace Blog.API.Models.DTOs
{
    public class CategoryResponseDTO  // Nesta classe crio propriedades/construtores conforme minha aplicação necessita, (response)
            // com isso eu deixo minha classe Moldels inauterável (para um ambiente mais organaizado, padrão normalmente seguido)

    {
        public string Name { get; init; } = string.Empty;
        public string Slug { get; init; } = string.Empty;
    }

}
