using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.SubCategoryService;

public interface ISubCategoryService
{
    Task<List<SubCategory>> GetSubCategories();
    Task<SubCategory> GetSubCategoryById(int id);
    Task CreateSubCategory(SubCategory subCategory);
    Task UpdateSubCategory(SubCategory subCategory);
    Task DeleteSubCategory(int id);
}
