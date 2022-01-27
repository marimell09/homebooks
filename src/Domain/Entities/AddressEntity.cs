using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AddressEntity : BaseEntity
    {
        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(8)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(35)]
        public string State { get; set; }

        [Required]
        [MaxLength(70)]
        public string City { get; set; }

        [Required]
        [MaxLength(70)]
        public string District { get; set; }

        [Required]
        [MaxLength(70)]
        public string Street { get; set; }

        [Required]
        [MaxLength(6)]
        public string AddressNumber { get; set; }

        [MaxLength(35)]
        public string Additional { get; set; }

        [MaxLength(150)]
        public string Notes { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
