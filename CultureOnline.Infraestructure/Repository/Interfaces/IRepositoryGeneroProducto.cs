using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryGeneroProducto
    {
        Task<ICollection<GeneroProducto>> ListAsync();
        Task<GeneroProducto> FindByIdAsync(int id);
    }
}
