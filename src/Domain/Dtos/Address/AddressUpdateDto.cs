using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Address
{
    public class AddressUpdateDto
    {
        [Required(ErrorMessage = "Id é campo obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é campo obrigatório.")]
        [StringLength(120, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Telefone é campo obrigatório.")]
        [StringLength(11, ErrorMessage = "Telefone deve ter no máximo {1} caracteres.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "CEP é campo obrigatório.")]
        [StringLength(8, ErrorMessage = "CEP deve ter no máximo {1} caracteres.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Estado é campo obrigatório.")]
        [StringLength(35, ErrorMessage = "Estado deve ter no máximo {1} caracteres.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Cidade é campo obrigatório.")]
        [StringLength(70, ErrorMessage = "Cidade deve ter no máximo {1} caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Bairro é campo obrigatório.")]
        [StringLength(70, ErrorMessage = "Bairro deve ter no máximo {1} caracteres.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Endereço é campo obrigatório.")]
        [StringLength(70, ErrorMessage = "Endereço deve ter no máximo {1} caracteres.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Número é campo obrigatório.")]
        [StringLength(6, ErrorMessage = "Número deve ter no máximo {1} caracteres.")]
        public string AddressNumber { get; set; }

        [StringLength(35, ErrorMessage = "Complemento deve ter no máximo {1} caracteres.")]
        public string Additional { get; set; }

        [StringLength(150, ErrorMessage = "Observação deve ter no máximo {1} caracteres.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Id do usuário é campo obrigatório.")]
        public Guid UserId { get; set; }

    }
}
