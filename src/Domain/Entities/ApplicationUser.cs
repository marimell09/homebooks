using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class ApplicationUser: IdentityUser<Guid>
	{
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        private DateTime? _createdAt;

        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == null ? DateTime.UtcNow : value); }
        }
        public DateTime? UpdatedAt { get; set; }

        public IEnumerable<AddressEntity> Addresses { get; set; }

    }
}