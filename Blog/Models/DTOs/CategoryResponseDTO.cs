namespace Blog.API.Models.DTOs
{
    public class CategoryResponseDTO  // Nesta classe crio propriedades/construtores conforme minha aplicação necessita, (response)
            // com isso eu deixo minha classe Moldels inauterável (para um ambiente mais organaizado, padrão normalmente seguido)

    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }

}
