using AutoMapper;
using ManagementRestaurantLocation.Global;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO.ProductDTO;
using ManagementRestaurantLocation.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurantLocation.Models.RestaurantDTO;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagementRestaurantLocation.Controllers
{
    [Route("API/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRestaurentRepository _restaurentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly APIRespone _APIRespone;

        public ProductController(IRestaurentRepository restaurentRepository, IProductRepository productRepository, IMapper mapper )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _restaurentRepository = restaurentRepository;
            this._APIRespone = new();
        }

        
        [HttpGet]
        public async Task<ActionResult<APIRespone>> GetAllProduct()
        {
            try
            {
                IEnumerable<Products> products = await _productRepository.GetAllAsycn(includeProperties: "Restaurent");
                _APIRespone.Result = _mapper.Map<List<ProductDTO>>(products);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }


        [HttpPost]
        public async Task<ActionResult<APIRespone>> ProductCreate([FromBody] ProductCreatDTO  productDTO)
        {
            try
            {
                if(await _restaurentRepository.GetAsycn(p => p.Id == productDTO.RetaurentID) == null)
                {
                    ModelState.AddModelError("ErrorsMessge", "Restaurent is not exits ");
                    return BadRequest(ModelState);
                }
                if (await _productRepository.GetAsycn(res => res.Name.ToLower() == productDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorsMessge", "Product is already exits ");
                    return BadRequest(ModelState);
                }
                if (productDTO == null)
                {
                    return BadRequest(productDTO);
                }

                var model = _mapper.Map<Products>(productDTO);
                model.Creat_At = DateTime.Now;
                model.Update_At = DateTime.Now;
                model.Slug = Slug.convertToUnSign2(model.Name);
                await _productRepository.CreateAsycn(model);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                _APIRespone.Result = _mapper.Map<Products>(productDTO);

                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }


        [HttpGet("Id", Name = "GetProduct")]
        public async Task<ActionResult<APIRespone>> GetProduct(int Id)
        {
            try
            {
                if (Id == 0) return BadRequest();
                var model = await _productRepository.GetAsycn(pro => pro.Id == Id);
                if (model == null) return NotFound();
                model.Restaurent = await _restaurentRepository.GetAsycn(pro => pro.Id == model.RetaurentID);

                _APIRespone.Result = _mapper.Map<ProductDTO>(model);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }

        [HttpPut]
        public async Task<ActionResult<APIRespone>> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO, int Id)
        {
            try
            {
                if (productUpdateDTO == null || Id != productUpdateDTO.Id) return BadRequest();
                var model = _mapper.Map<Products>(productUpdateDTO);
                model.Slug = Slug.convertToUnSign2(model.Name);
                model.Update_At = DateTime.Now;

                await _productRepository.UpdateAsycn(model);
                _APIRespone.Result = model;
                _APIRespone.StatusCode = HttpStatusCode.OK;
                return Ok(_APIRespone);
            }

            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }


        [HttpDelete("Id", Name = "DeleteProduct")]
        public async Task<ActionResult<APIRespone>> DeleteProduct(int Id)
        {
            if (Id == 0) return BadRequest();
            var model = await _productRepository.GetAsycn(res => res.Id == Id);
            if (model == null) return NotFound();

            await _productRepository.DeleteAsycn(model);
            _APIRespone.StatusCode = HttpStatusCode.NoContent;
            _APIRespone.IsSuccess = true;
            _APIRespone.ErrorsMessge = new List<string>
            {
                "Delete Succesful"
            };
            return Ok(_APIRespone);
        }
    }
}
