using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryEtiqueta
    {
        Task<ICollection<Etiquetas>> ListAsync();
        Task<Etiquetas> FindByIdAsync(int id);
        Task<ICollection<Etiquetas>> FindByProductoIdAsync(int productoId);
      


    }
}
