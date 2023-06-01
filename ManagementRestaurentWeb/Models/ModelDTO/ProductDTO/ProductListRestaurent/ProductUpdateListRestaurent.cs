using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementRestaurentWeb.Models.ModelDTO.ProductDTO.ProductListRestaurent
{
    public class ProductUpdateListRestaurent
    {
        public ProductUpdateDTO productUpdateDTO { get; set; }
        public ProductUpdateListRestaurent()
        {
            productUpdateDTO = new ProductUpdateDTO();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> restaurentList { get; set; }
    }
}
