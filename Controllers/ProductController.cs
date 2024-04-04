using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productApi.DataAccess;
using productApi.Model;
using System;
using System.Linq;
using System.Threading.Tasks; // Import Task namespace for async methods

namespace productApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;

        // Constructor injection to inject DbContext
        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        // Endpoint to create a product
        [HttpPost]
        public async Task<IActionResult> CreatProduct([FromBody] Product model)
        {
            try
            {
                int result;
                _db.Products.Add(model);
                result = _db.SaveChanges();
                if (result == 0)
                {
                    return Ok(new { value = "Do not create" });
                }
                else
                {
                    return Ok(new { value = "created successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to create a product purchase
        [HttpPost("purchase")]
        public async Task<IActionResult> CreatProductPurchase([FromBody] ProductPurchase model)
        {
            try
            {
                int result;
                _db.ProductPurchases.Add(model);
                result = _db.SaveChanges();
                if (result == 0)
                {
                    return Ok(new { value = "Do not create" });
                }
                else
                {
                    return Ok(new { value = "created successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to create a product sale
        [HttpPost("sale")]
        public async Task<IActionResult> CreatProductSale([FromBody] ProductSale model)
        {
            try
            {
                int result;
                _db.ProductSales.Add(model);
                result = _db.SaveChanges();

                // Update product quantity after sale
                var product = await _db.Products.FindAsync(model.ProductId);
                product.ProductQuontity -= model.SaleQuontity ?? 0;
                _db.Products.Update(product);

                // Save changes to the database
                result = await _db.SaveChangesAsync();

                if (result == 0)
                {
                    return Ok(new { value = "Do not create" });
                }
                else
                {
                    return Ok(new { value = "created successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to get all product purchases
        [HttpGet("GetAllPurchase")]
        public async Task<IActionResult> GetAllPurchase()
        {
            try
            {
                var productList = _db.ProductPurchases.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to get all product sales
        [HttpGet("GetAllSale")]
        public async Task<IActionResult> GetAllSale()
        {
            try
            {
                var productList = _db.ProductSales.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to get all products
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var productList = _db.Products.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to get a specific product by ID
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to delete a product by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return NotFound(new { msg = "Product Not Found" });
                }
                else
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();
                    return Ok(new { msg = "Delete Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update a product
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product model)
        {
            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == model.Id);
                if (product == null)
                {
                    return NotFound("Product Not Found");
                }

                // Update product properties
                product.ProductName = model.ProductName;
                product.ProductDescription = model.ProductDescription;
                product.ProductQuontity = model.ProductQuontity;

                // Save changes
                _db.Products.Update(product);
                _db.SaveChanges();

                return Ok(new { msg = "Update Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
