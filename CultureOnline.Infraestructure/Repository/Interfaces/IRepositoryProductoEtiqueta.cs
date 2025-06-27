using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryProductoEtiqueta
    {
        Task<ICollection<ProductoEtiquetas>> ListAsync();
        Task<ProductoEtiquetas> FindByIdAsync(int id);
    }
}
