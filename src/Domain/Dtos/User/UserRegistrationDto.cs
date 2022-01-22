using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.User
{
    public class UserRegistrationDto
    {
        [Required (ErrorMessage = "Nome é campo obrigatório.")]
        [StringLength (60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Email é campo obrigatório.")]
        [EmailAddress (ErrorMessage = "E-mail em formato inválido.")]
        [StringLength (100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}