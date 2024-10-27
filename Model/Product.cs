using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAserver.Model;

public class Product
{
    public long Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set;} = null!;
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
    public bool IsDelete { get; set; }
}