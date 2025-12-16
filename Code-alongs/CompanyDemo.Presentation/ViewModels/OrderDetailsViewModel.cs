using CompanyDemo.Domain;
using CompanyDemo.Infrastructure.Data.Model;
using System.Collections.ObjectModel;

namespace CompanyDemo.Presentation.ViewModels
{
    internal class OrderDetailsViewModel
    {
        public ObservableCollection<OrderDetail> Details { get; set; }
        public OrderDetailsViewModel(OrderSummary order)
        {
            LoadOrderDetails(order.Id);
        }

        private void LoadOrderDetails(int orderId)
        {
            using var db = new CompanyContext();

            Details = new ObservableCollection<OrderDetail>(
                db.OrderDetails.Where(o => o.OrderId == orderId).ToList()
            );
        }
    }
}
