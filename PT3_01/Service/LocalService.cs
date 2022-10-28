using PT3_01.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PT3_01.Service
{


    public class LocalService : IDatabase
    {
        private Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();

        public void Delete(Guid id)
        {
            products.Remove(id);
        }

        public Task<List<Product>> GetAll()
        {
            return Task.FromResult(products.Values.ToList());
        }

        public Task<Product> GetOne(Guid id)
        {
            return Task.FromResult(products[id]);
        }

        public void Initialise()
        {
            
        }

        public void Insert(Product p)
        {
            products[p.Id] = p;
        }

        public void Update(Product p)
        {
            Insert(p);
        }
    }
}
