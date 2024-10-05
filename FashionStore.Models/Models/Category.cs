using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }  // Parent CategoryId (nullable for top-level categories)
    public Category ParentCategory { get; set; }  // Self-referencing relationship
    public ICollection<Category> SubCategories { get; set; }  // Navigation property for subcategories
}
