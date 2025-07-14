using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryRolUsuario
    {
        Task<ICollection<RolUsuario>> ListAsync();
        Task<RolUsuario> FindByIdAsync(int id);
    }
}
