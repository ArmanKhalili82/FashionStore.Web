﻿using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.ProductsService;

public interface IProductsService
{
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetFeaturedProducts();
    Task<List<Product>> GetPopularProducts();
    Task<List<Product>> GetCarouselProducts();
    Task<Product> GetProductById(int id);
    Task Create(Product products);
    Task Update(Product products);
    Task Delete(int productId);
}
