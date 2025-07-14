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
<<<<<<< HEAD
        Task AddAsync(Reseñas reseña);
        Task UpdateAsync(Reseñas reseña);
        Task DeleteAsync(int id);
=======
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
    }
}
