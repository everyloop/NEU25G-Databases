using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyDemo.Domain;
using CompanyDemo.Infrastructure.Data.Model;
using CompanyDemo.Presentation.Command;
using Microsoft.EntityFrameworkCore;

namespace CompanyDemo.Presentation.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<string> Regions { get; private set; }

    private string? _selectedRegion;

    public string? SelectedRegion
    {
        get => _selectedRegion;
        set 
        { 
            _selectedRegion = value;

            LoadOrders();

            RaisePropertyChanged();
            RaisePropertyChanged("Orders");
        }
    }
    public ObservableCollection<OrderSummary> Orders { get; private set; }

    private OrderSummary? _selectedOrder;

    public OrderSummary? SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            _selectedOrder = value;
            RaisePropertyChanged();
            ShowOrderDetailsCommand.RaiseCanExecuteChanged();
        }
    }

    public Action ShowOrderDetails { get; set; }
    public DelegateCommand ShowOrderDetailsCommand { get; }

    public Action<string> ShowMessage { get; set; }

    public MainWindowViewModel()
    {
        ShowOrderDetailsCommand = new DelegateCommand(DoShowOrderDetails, CanShowOrderDetails);
        LoadRegions();
    }

    private void DoShowOrderDetails(object obj) => ShowOrderDetails();

//    private void DoShowOrderDetails(object obj) => ShowMessage?.Invoke(SelectedOrder?.Id.ToString() ?? "No order selected!");

    private bool CanShowOrderDetails(object? arg) => SelectedOrder is not null;

    private void LoadRegions()
    {
        using var db = new CompanyContext();

        Regions = new ObservableCollection<string>(
            db.Orders.Select(o => o.ShipRegion).Distinct().ToList()
        );

        SelectedRegion = Regions.FirstOrDefault();
    }

    private void LoadOrders()
    {
        using var db = new CompanyContext();

        Orders = new ObservableCollection<OrderSummary>(
            db.Orders
                .Where(o => o.ShipRegion == SelectedRegion)
                .Include(o => o.Customer)
                .Select(o => new OrderSummary()
                {
                    Id = o.Id,
                    CustomerName = o.Customer.CompanyName,
                    OrderDate = o.OrderDate,
                    City = o.ShipCity,
                    Country = o.ShipCountry,
                    NumberOfItems = o.OrderDetails.Count(),
                    TotalAmount = o.OrderDetails.Sum(o => o.UnitPrice * o.Quantity * (1 - o.Discount)) ?? 0
                })
                .ToList()
        );
    }
}

public class OrderSummary
{
    public int Id { get; set; }

    public string OrderDate { get; set; }

    public string CustomerName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int NumberOfItems { get; set; }
    public double TotalAmount { get; set; }
}
