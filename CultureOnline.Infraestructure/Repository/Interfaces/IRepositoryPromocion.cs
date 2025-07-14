using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPromocion
    {
<<<<<<< HEAD
        Task<ICollection<Promociones>> ListAsync();
        Task<Promociones> FindByIdAsync(int id);
=======
        Task<ICollection<Etiquetas>> ListAsync();
        Task<Etiquetas> FindByIdAsync(int id);
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
    }
}
