using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementRestaurentWeb.Models.ModelDTO.ProductDTO.ProductListRestaurent
{
    public class ProductListRestaurent
    {
        public ProductCreatDTO ProductCreatDTO { get; set; }
        public ProductListRestaurent()
        {
            ProductCreatDTO = new ProductCreatDTO();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> restaurentList { get; set; }
    }
}
