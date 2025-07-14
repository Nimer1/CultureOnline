using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryTipoPromocion
    {
        Task<ICollection<TipoPromocion>> ListAsync();
        Task<TipoPromocion> FindByIdAsync(int id);
        Task<TipoPromocion> FindByNameAsync(string name);
        Task<int> AddAsync(TipoPromocion tipoPromocion);
        
        Task UpdateAsync(int id, TipoPromocion tipoPromocion);
        Task DeleteByIdAsync(int id);
        Task<ICollection<TipoPromocion>> GetTipoPromocionByName(string name);

    }
}
