using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    // TODO: Explain in lecture
    public class Product : Entity
    {
        [Required, StringLength(150)]
        public string Name { get; set; } // Reference Type: null can't be assigned since Required is used

        // Way 1: decimal value type
        //public float UnitPrice { get; set; } // Value Type: null can't be assigned, default value is 0.0F if no assignment
        // Way 2: decimal value type
        //public double UnitPrice { get; set; } // Value Type: null can't be assigned, default value is 0.0 if no assignment
        // Way 3: decimal value type
        public decimal UnitPrice { get; set; } // Value Type: null can't be assigned, default value is 0.0M if no assignment

        public int? StockAmount { get; set; } // Value Type: null can be assigned since ? is used, default value is null if no assignment

        public DateTime? ExpirationDate { get; set; } // Value Type: null can be assigned since ? is used, default value is null if no assignment

        // FK
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>();

        [NotMapped]
        public List<int> StoreIds 
        { 
            get => ProductStores.Select(ps => ps.StoreId).ToList(); 
            set => ProductStores = value.Select(v => new ProductStore() { StoreId = v }).ToList(); 
        }
    }
}
