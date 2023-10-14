using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Models.Dto;

namespace ProductsAPI.Controllers
{
    [Route("api/ProductAPI")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductAPIController(ApplicationDbContext db)
        {

            _db = db;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult< IEnumerable<ProductDTO>> GetProducts()
        {
            return Ok(_db.Products.ToList());
            
         }

        [HttpGet("{id:int}",Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200 ,Type = typeof(ProductDTO))]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(u=>u.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<ProductDTO> CreateProduct ([FromBody]ProductDTO productDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(_db.Products.FirstOrDefault(u=>u.Name.ToLower() == productDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Product already exists!");
                return BadRequest(ModelState);
            }
            if(productDTO == null)
            {
                return BadRequest(productDTO);
            }
            if(productDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Product model = new()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                ImageUrl = productDTO.ImageUrl
            };
            _db.Products.Add(model);
            _db.SaveChanges();
            

            return CreatedAtRoute("GetProduct",new {id = productDTO.Id}, productDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        
        public IActionResult DeleteProduct(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(u=>u.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
    
        }

        [HttpPut("{id:int}", Name ="UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateProduct(int id,[FromBody]ProductDTO productDTO) 
        { 
            if(productDTO == null || id != productDTO.Id)
            {
                return BadRequest();
            }
            //var product = ProductStore.ProductList.FirstOrDefault(u=>u.Id == id);
            //product.Name = productDTO.Name;
            //product.Price = productDTO.Price;
            //product.Quantity = productDTO.Quantity;

            Product model = new()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                Description = productDTO.Description,
                ImageUrl = productDTO.ImageUrl
            };
            _db.Products.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name ="UpdatePartialProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialProduct(int id, JsonPatchDocument<ProductDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.AsNoTracking().FirstOrDefault(u=>u.Id==id);

            ProductDTO productDTO = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };

            if(product == null)
            {
                return BadRequest();

            }
            patchDTO.ApplyTo(productDTO,ModelState);
            Product model = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };
            _db.Products.Update(model);
            _db.SaveChanges();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
