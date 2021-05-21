using System.Collections.Generic;

namespace DAL.Entities
{
    public class Supplier : BaseEntity
    {
        public string CompanyName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}