using System.Text.Json.Serialization;

namespace Blog.API.Models
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }

        // Construtor primário, se usa ela para passar todos os parametros, incluindo o ID
        //public Category() { }


        // Construtor de parametros para POST <> INSERT INTO
        [JsonConstructor] // Serve para o JSON interpretar corretamente os dados que chegam na sua API.
        public Category(string name, string slug)  // slug é cadeia de caracteres do site que vem aleátorio,
                                                   // porém convertido pra algo mais visual
        {
            Name = name;
            Slug = slug;
        }

    }
}
