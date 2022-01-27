using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public abstract class BaseEntity
	{
        [Key]
        [Required]
        public Guid Id { get; set; }

        private DateTime? _createAt;

        [Required]
        public DateTime? CreateAt
        {
            get { return _createAt; }
            set { _createAt = (value == null ? DateTime.UtcNow : value); }
        }

        [Required]
        public DateTime? UpdateAt { get; set; }
    }
}
