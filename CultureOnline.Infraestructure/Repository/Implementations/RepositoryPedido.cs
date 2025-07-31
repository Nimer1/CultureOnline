using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Implementations
{
    public class RepositoryPedido : IRepositoryPedido
    {
        private readonly CultureOnlineContext _context;
        public RepositoryPedido(CultureOnlineContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Pedidos entity)
        {
            try
            {
                
                await _context.Database.BeginTransactionAsync();
                await _context.Set<Pedidos>().AddAsync(entity);
                
                foreach (var item in entity.DetallePedido)
                {
                    //Busca el producto
                    var libro = await _context.Set<Productos>().FindAsync(item.Id);
                    //Actualiza la cantidad en stock
                    libro!.Stock= libro.Stock - item.Cantidad;
                    //Actualiza producto
                    _context.Set<Productos>().Update(libro);
                }
                await _context.SaveChangesAsync();
                // Commit
                await _context.Database.CommitTransactionAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Rollback 
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }

        }

        public async Task<Pedidos> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Pedidos>()
                   .Include(cliente => cliente.Usuario)
                   .Include(detalle => detalle.DetallePedido)
                   .ThenInclude(detalle => detalle.Producto)
                   .Include(p => p.Pago)
                   .Include(p => p.ProductosPersonalizados)       
                   .ThenInclude(pp => pp.Producto)
                   .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }
        public async Task<ICollection<Pedidos>> ListAsync()
        {
            var collection = await _context.Set<Pedidos>()
                .Include(x => x.Usuario)
                .Include(x => x.DetallePedido)
                .ThenInclude(d => d.Producto)
                .AsNoTracking()
                .ToListAsync();
            return collection;
        }
        public async Task<int> GetNextNumberOrden()
        {
            int current = 0;

            string sql = string.Format("SELECT IDENT_CURRENT ('Pedidos') AS Current_Identity;");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);
        }

        public async Task<Pedidos> FindByIdChangeAsync(int id)
        {
            var @object = await _context.Set<Pedidos>()
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }
        public async Task<ICollection<Pedidos>> OrdenByClientId(int id)
        {
            var response = await _context.Set<Pedidos>()
                           .Include(p => p.DetallePedido)
                           .Where(p => p.UsuarioId == id).ToListAsync();

            return response;

        }

        public Task<ICollection<Pedidos>> OrdenByClientId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
