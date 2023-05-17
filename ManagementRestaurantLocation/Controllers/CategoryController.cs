using AutoMapper;
using ManagementRestaurantLocation.Global;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO.CategoryDTO;
using ManagementRestaurantLocation.Models.ModelDTO.ProductDTO;
using ManagementRestaurantLocation.Repository;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagementRestaurantLocation.Controllers
{
    [Route("API/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly APIRespone _APIRespone;

        public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _APIRespone = new ();
        }


        [HttpGet]
        public async Task<ActionResult<APIRespone>> GetAllCategory()
        {
            try
            {
                IEnumerable<Categories> categories = await _categoryRepository.GetAllAsycn(includeProperties: "Product");
                _APIRespone.Result = _mapper.Map<List<CategoryDTO>>(categories);
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
        public async Task<ActionResult<APIRespone>> CategoryCreate([FromBody] CategoryCreateDTO categoryDTO)
        {
            try
            {
                if (await _productRepository.GetAsycn(p => p.Id == categoryDTO.ProductID) == null)
                {
                    ModelState.AddModelError("ErrorsMessge", "Product is not exits ");
                    return BadRequest(ModelState);
                }
                if (await _categoryRepository.GetAsycn(res => res.Name.ToLower() == categoryDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorsMessge", "Product is already exits ");
                    return BadRequest(ModelState);
                }
                if (categoryDTO == null)
                {
                    return BadRequest(categoryDTO);
                }

                var model = _mapper.Map<Categories>(categoryDTO);
                model.Creat_At = DateTime.Now;
                model.Update_At = DateTime.Now;
                model.Slug = Slug.convertToUnSign2(model.Name);
                await _categoryRepository.CreateAsycn(model);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                _APIRespone.Result = _mapper.Map<Categories>(categoryDTO);

                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }

        [HttpGet("Id", Name = "GetCate")]
        public async Task<ActionResult<APIRespone>> GetCate(int Id)
        {
            try
            {
                if (Id == 0) return BadRequest();
                var model = await _categoryRepository.GetAsycn(pro => pro.Id == Id);
                if (model == null) return NotFound();
                model.Product = await _productRepository.GetAsycn(pro => pro.Id == model.ProductID);

                _APIRespone.Result = _mapper.Map<CategoryDTO>(model);
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
        public async Task<ActionResult<APIRespone>> UpdateCategory([FromBody] CategoryUpdateDTO categoryUpdateDTO, int Id)
        {
            try
            {
                if (categoryUpdateDTO == null || Id != categoryUpdateDTO.Id) return BadRequest();
                var model = _mapper.Map<Categories>(categoryUpdateDTO);
                model.Slug = Slug.convertToUnSign2(model.Name);
                model.Update_At = DateTime.Now;

                await _categoryRepository.UpdateAsycn(model);
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

        [HttpDelete("Id", Name = "DeleteCategory")]
        public async Task<ActionResult<APIRespone>> DeleteCategory(int Id)
        {
            if (Id == 0) return BadRequest();
            var model = await _categoryRepository.GetAsycn(res => res.Id == Id);
            if (model == null) return NotFound();

            await _categoryRepository.DeleteAsycn(model);
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
