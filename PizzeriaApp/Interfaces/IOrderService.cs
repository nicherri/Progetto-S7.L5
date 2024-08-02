using ViewModels;

namespace Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId);
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(OrderViewModel model);
        Task UpdateOrderAsync(OrderViewModel model);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
    }
}
