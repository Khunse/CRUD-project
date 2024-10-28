using System.Text.Json.Nodes;
using IPAserver.DataAccessLayer;
using IPAserver.Model;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISQLDataAccess,SQLDataAccess>();
builder.Services.AddControllers();
// builder.WebHost.UseUrls("http://0.0.0.0:5000");
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors( options => {
    options.AddPolicy("Frontend", policyBuilder => {
        policyBuilder.WithOrigins(allowedOrigins);
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });

});

var app = builder.Build();

app.UseDefaultFiles( new DefaultFilesOptions
{
    DefaultFileNames = new List<string>{ "index.html" }
});

app.UseStaticFiles( new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"dist")),
    RequestPath = ""
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Frontend");

app.UseHttpsRedirection();


app.MapGet("/Product", async (ISQLDataAccess dataAccess) =>
{   
    var getData = await dataAccess.GetProducts("Default");
    var respData = new RespModal();

    if( getData is not null)
    {
        respData.RespData = JsonConvert.SerializeObject(getData);
        respData.IsSuccess = true;
        respData.RespDesc = "Success!";
    }

    return respData;
})
.WithName("GetAllProducts")
.WithOpenApi();

app.MapGet("/Product/{id}", async (ISQLDataAccess dataAccess,long id) =>
{   
    var respData = new RespModal();
    var getData = await dataAccess.GetProduct(id);

    if( getData is not null)
    {
        respData.RespData = JsonConvert.SerializeObject(getData);
        respData.IsSuccess = true;
        respData.RespDesc = "Success!";
    }
    else
    {
        respData.IsSuccess=false;
        respData.RespDesc = "Product cannot be found!";
    }

    return respData;
})
.WithName("GetProduct")
.WithOpenApi();


app.MapPost("/product", async (ISQLDataAccess dataAccess,Product product) =>
{   
    var respData = new RespModal();

    var  checkData = await dataAccess.CheckDuplicateProductName(product.ProductName);
    if(checkData is false)
    {

    var getData = await dataAccess.AddProduct(product);

    if( getData )
    {
        respData.IsSuccess = true;
        respData.RespDesc = "Successfully Added!";
    }

    }
    else{
        respData.IsSuccess = false;
        respData.RespDesc = "Product is already exist!";
    }

    return respData;
})
.WithName("AddProduct")
.WithOpenApi();

app.MapPost("/products/{id}", async (ISQLDataAccess dataAccess,long id,Product product) =>
{   
    var respData = new RespModal();
    var checkData = await dataAccess.CheckDuplicateProductId(id);
    var oldData = await dataAccess.GetProduct(id);
    var checkNameData = product.ProductName.Equals(oldData.ProductName) ? false : await dataAccess.CheckDuplicateProductName(product.ProductName);
    
    if(checkData is true && checkNameData is false)
    {

    var getData = await dataAccess.EditProduct(product);

    if( getData )
    {
        respData.IsSuccess = true;
        respData.RespDesc = "Successfully Edit!";
    }

    }
    else if( checkNameData is true)
    {
        respData.IsSuccess = false;
        respData.RespDesc = "Product already exist!";
    }
    else{
        respData.IsSuccess = false;
        respData.RespDesc = "Product cannot be found!";
    }

    return respData;
})
.WithName("EditProduct")
.WithOpenApi();

app.MapPost("/product/{id}", async (ISQLDataAccess dataAccess,long id) =>
{   
    var respData = new RespModal();
    var checkData = await dataAccess.CheckDuplicateProductId(id);

    if(checkData is true) 
    {
    
    var getData = await dataAccess.DeleteProduct(id);

    if( getData )
    {
        respData.IsSuccess = true;
        respData.RespDesc = "Successfully Delete!";
    }


    }
    else
    {
        respData.IsSuccess = false;
        respData.RespDesc = "Product cannot be found!";
    }

    return respData;
})
.WithName("DeleteProduct")
.WithOpenApi();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("index.html",new StaticFileOptions{
         FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "dist"))
    });
    
    endpoints.MapControllers();
});

app.Run();

