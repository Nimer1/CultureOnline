using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReseña
    {
        Task<ICollection<Reseñas>> ListAsync();
        Task<Reseñas> FindByIdAsync(int id);
        Task AddAsync(Reseñas reseña);
        Task UpdateAsync(Reseñas reseña);
        Task DeleteAsync(int id);
    }
}
