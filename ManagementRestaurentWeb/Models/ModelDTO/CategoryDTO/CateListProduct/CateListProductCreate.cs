using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO.CateListProduct
{
    public class CateListProductCreate
    {
        public CategoryCreateDTO categoryCreateDTO { get; set; }
        public CateListProductCreate()
        {
            categoryCreateDTO = new CategoryCreateDTO();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> productList { get; set; }
    }
}
