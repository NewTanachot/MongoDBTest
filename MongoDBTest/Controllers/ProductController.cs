using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBTest.Models;
using MongoDBTest.MongoDB;
using MongoDBTest.Services;

namespace MongoDBTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CRUDServices<Product> productServices;

        public ProductController(CRUDServices<Product> crudServices)
        {
            this.productServices = crudServices;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct()
        {
            return Ok(await productServices.LoadRecordAsync(CollectionsList.Product));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            return Ok(await productServices.LoadRecordAsync(CollectionsList.Product, id));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertProduct(string productName, string? productDiscription = null, string? productType = null)
        {
            await productServices.InsertRecordAsync(CollectionsList.Product, new Product
            {
                ProductName = productName,
                ProductDescription = productDiscription,
                ProductType = productType
            });

            return Ok(new { Success = "Create product done." });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertManyProduct(int insertAmoung)
        {
            var products = new List<Product>();
            await productServices.ClearRecordAsync(CollectionsList.Product);

            for (var i = 1; i <= insertAmoung; i++)
            {
                products.Add(new Product
                {
                    ProductName = "Product_" + i,
                    ProductDescription = "Test_" + i,
                    ProductType = "Type_" + i
                });
            }

            await productServices.InsertRecordAsync(CollectionsList.Product, products);
            return Ok(new { Success = "Create product done." });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct(string id, string? productName = null, string? productDescription = null)
        {
            await productServices.CreateOrUpdateRecordAsync(CollectionsList.Product, id, new Product
            {
                ProductId = id,
                ProductName = productName,
                ProductDescription = productDescription
            });

            return Ok(new { Success = "Update or Create product done." });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await productServices.DeleteRecordAsync(CollectionsList.Product, id);
            return Ok(new { Success = "Delete product done." });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> ClearProduct()
        {
            await productServices.ClearRecordAsync(CollectionsList.Product);
            return Ok(new { Success = "Clear product done." });
        }
    }
}
