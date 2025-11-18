using CORE.APP.Domain;

namespace APP.Domain
{
    public class ProductStore : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
