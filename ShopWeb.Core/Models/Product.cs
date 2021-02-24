using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWeb.Core.Models
{
    public class Product : BaseEntity
    {
        
        [StringLength(20)]
        [DisplayName("ProductName")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,1000)]
        public double Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

     
    }
}
