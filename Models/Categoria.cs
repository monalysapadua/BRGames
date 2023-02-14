using System.ComponentModel.DataAnnotations;

namespace BRGames.Models
{
    public class Categoria
    {
        public int CategoriaJogoID { get; set; }

        [Required(ErrorMessage = "Informe o nome do seu Jogo")]
        public string NomeDoJogo { get; set; }
    }
}
