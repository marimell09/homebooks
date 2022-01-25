using Domain.Security;
using System;

namespace Domain.Dtos.User
{
    public class UserRegistrationResponseDto : AuthResult
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }    

        public DateTime CreatedAt { get; set; }
    }
}