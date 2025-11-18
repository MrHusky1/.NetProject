using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    // TODO: Explain in lecture
    public class Store : Entity
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        public bool IsVirtual { get; set; }

        public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>();

        [NotMapped]
        public List<int> ProductIds
        {
            get => ProductStores.Select(ps => ps.ProductId).ToList();
            set => ProductStores = value.Select(v => new ProductStore() { ProductId = v }).ToList();
        }
    }
}
