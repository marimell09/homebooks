using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);
    }
}
