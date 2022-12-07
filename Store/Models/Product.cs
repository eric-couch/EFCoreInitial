using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Column(TypeName="decimal(6,2)")]
        public decimal Price { get; set; }

        public override string? ToString()
        {
            return $"\nProduct Name:\t{Name}\nPrice:\t{Price}";
        }
    }
}
