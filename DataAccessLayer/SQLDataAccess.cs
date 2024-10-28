using System.Data;
using System.Data.SqlClient;
using Dapper;
using IPAserver.Model;

namespace IPAserver.DataAccessLayer;

public class SQLDataAccess : ISQLDataAccess
{
    private readonly IConfiguration _configuration;

    public SQLDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> AddProduct(Product product)
    {
        using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));

        string query = "INSERT INTO Product ( ProductName, ProductDescription,Created_at,IsDelete)  VALUES ( @ProductName, @ProductDescription, @Created_at, @IsDelete )";

        var myanmarDatetime = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
        var myanmarTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, myanmarDatetime);

        var obj = new{
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Created_at = myanmarTime,
            IsDelete = false
        };

        var queryData = await dbConnection.ExecuteAsync(query,obj);

        if(queryData > 0) return true;

        return false;
        
    }

    public async Task<bool> CheckDuplicateProductId(long Id)
    {
       using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));

            string query = "SELECT * FROM Product WHERE Id=@Id and IsDelete=0";

            var obj = new { Id = Id };

            var queryData = await dbConnection.QueryAsync<Product>(query,obj);
            var dataList = queryData.ToList();

            if( dataList.Count > 0 ) return true;

            return  false;
    }

    public async Task<bool> CheckDuplicateProductName(string productName)
    {
            using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));

            string query = "SELECT * FROM Product WHERE ProductName=@ProductName and IsDelete=0";

            var obj = new { ProductName = productName };

            var queryData = await dbConnection.QueryAsync<Product>(query,obj);
            var dataList = queryData.ToList();

            if( dataList.Count > 0 ) return true;

            return  false;

    }

    public async Task<bool> DeleteProduct(long id)
    {
        using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));

        string query = "UPDATE Product SET IsDelete=1, Updated_at = @Updated_at WHERE Id=@Id and IsDelete=0";

        var myanmarDatetime = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
        var myanmarTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, myanmarDatetime);
        
        var obj = new { Id = id, Updated_at = myanmarTime };

        var queryData = await dbConnection.ExecuteAsync(query,obj);

        if(queryData > 0) return true;

        return false;
    }

    public async Task<bool> EditProduct(Product product)
    {
        using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));
        
        string query ="UPDATE Product SET ProductName=@ProductName,ProductDescription=@ProductDescription,Updated_at=@Updated_at WHERE Id=@Id and IsDelete=0";

        var myanmarDatetime = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
        var myanmarTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, myanmarDatetime);

        var obj = new { ProductName = product.ProductName, ProductDescription = product.ProductDescription, Updated_at = myanmarTime, Id = product.Id};

        var queryData = await dbConnection.ExecuteAsync(query,obj);

        if( queryData > 0 ) return true;

        return false;
    }

    public async Task<Product> GetProduct(long Id)
    {
        using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString("Default"));
        
        string query = "SELECT * FROM Product WHERE Id=@Id and IsDelete=0";

        var obj = new { Id = Id };

        var queryData = await dbConnection.QueryFirstOrDefaultAsync<Product>(query,obj);

        return queryData;
    }

    public async Task<List<Product>> GetProducts(string connectionId="Default")
    {   
        using IDbConnection dbConnection= new SqlConnection(_configuration.GetConnectionString(connectionId));
        
        string query = "SELECT * FROM Product WHERE IsDelete=0";
        var queryData = await dbConnection.QueryAsync<Product>(query);
        return queryData.ToList();
    }
}
