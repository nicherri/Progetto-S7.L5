﻿using ViewModels;

namespace Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductViewModel model);
        Task UpdateProductAsync(ProductViewModel model);
        Task DeleteProductAsync(int id);
    }
}
