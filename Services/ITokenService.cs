using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalRecipes.Services
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string username);
    }
}