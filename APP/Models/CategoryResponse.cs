using CORE.APP.Models;
using System.ComponentModel;

namespace APP.Models
{
    public class CategoryResponse : Response
    {
        [DisplayName("Category Title")]
        public string Title { get; set; } 

        public string Description { get; set; }

        [DisplayName("Product Count")]
        public int ProductCount { get; set; }

        public string Products { get; set; }

        public List<ProductResponse> ProductList { get; set; }
    }
}
