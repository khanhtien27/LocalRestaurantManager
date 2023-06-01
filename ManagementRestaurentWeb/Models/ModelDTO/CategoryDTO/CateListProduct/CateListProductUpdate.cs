using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO.CateListProduct
{
    public class CateListProductUpdate
    {
        public CategoryUpdateDTO categoryUpdateDTO { get; set; }
        public CateListProductUpdate()
        {
            categoryUpdateDTO = new CategoryUpdateDTO();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> productList { get; set; }
    }
}
