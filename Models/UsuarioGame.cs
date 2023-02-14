using System.ComponentModel.DataAnnotations;

namespace BRGames.Models
{
    public class UsuarioGame
    {
        public int UsuarioGameID { get; set; }
        
        [Required(ErrorMessage = "Informe seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu UserName")]
        public string UserName { get; set; }

        [MinLength(6)]
        [Required(ErrorMessage = "Informe sua senha")]  
        public string Senha { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Required(ErrorMessage ="Informe seu Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Informe sua Idade")]
        public int Idade { get; set; }
    }
}
