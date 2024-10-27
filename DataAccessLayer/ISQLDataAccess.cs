using IPAserver.Model;

namespace IPAserver.DataAccessLayer;

public interface ISQLDataAccess
{
    Task<List<Product>> GetProducts(string connectionId);
    Task<Product> GetProduct(long Id);
    Task<bool> AddProduct(Product product);
    Task<bool> EditProduct(Product product);
    Task<bool> DeleteProduct(long id);
    Task<bool> CheckDuplicateProductName(string productName);
    Task<bool> CheckDuplicateProductId(long Id);
}