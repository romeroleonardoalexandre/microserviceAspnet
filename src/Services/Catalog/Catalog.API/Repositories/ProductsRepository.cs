using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductsRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();

        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            //mongo db driver to create filter
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryName(string category)
        {
            //mongo db driver to create filter
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
