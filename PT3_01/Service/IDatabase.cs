using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PT3_01.Data;

namespace PT3_01.Service
{
    public interface IDatabase
    {
        void Initialise();
        public Task<List<Product>> GetAll();
        Task<Product> GetOne(Guid id);
        void Delete(Guid id);
        void Update(Product p);
        void Insert(Product p);
    }
}
