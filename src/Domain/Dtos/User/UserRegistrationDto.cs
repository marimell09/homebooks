using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.User
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Primeiro nome é campo obrigatório.")]
        [StringLength(60, ErrorMessage = "Primeiro nome deve ter no máximo {1} caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Último nome é campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Último deve ter no máximo {1} caracteres.")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Nome de usuário é campo obrigatório.")]
        [StringLength (60, ErrorMessage = "Nome de usuário deve ter no máximo {1} caracteres.")]
        public string UserName { get; set; }

        [Required (ErrorMessage = "Email é campo obrigatório.")]
        [EmailAddress (ErrorMessage = "E-mail em formato inválido.")]
        [StringLength (100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}