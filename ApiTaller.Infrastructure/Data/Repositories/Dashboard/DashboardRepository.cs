using ApiTaller.Domain.Dtos.Dashboard;
using ApiTaller.Domain.Interfaces.Repositories;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTaller.Infrastructure.Data.Repositories.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DataContext _context;

        public DashboardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> GetActiveWorkOrdersCountAsync(CancellationToken ct)
        {
            return await _context.WorkOrder
                .AsNoTracking()
                .Where(x => x.IsActive && x.Status != "Entregado" && x.Status != "Cancelado")
                .CountAsync(ct);
        }

        public async Task<int> GetTotalCustomersCountAsync(CancellationToken ct)
        {
            return await _context.Customer
                .AsNoTracking()
                .Where(x => x.IsActive)
                .CountAsync(ct);
        }

        public async Task<int> GetTotalVehiclesCountAsync(CancellationToken ct)
        {
            return await _context.Vehicle
                .AsNoTracking()
                .Where(x => x.IsActive)
                .CountAsync(ct);
        }

        public async Task<decimal> GetOperatingAvailabilityPercentAsync(CancellationToken ct)
        {
            // Total active products
            var totalActiveProducts = await _context.Product
                .AsNoTracking()
                .Where(p => p.IsActive)
                .CountAsync(ct);

            if (totalActiveProducts == 0) return 100m; // If no products, technically we have 100% availability of nothing.

            // Count of active products that have an inventory record with StockQuantity > 0
            var productsWithStock = await _context.Product
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Join(_context.Inventory.AsNoTracking(),
                      p => p.Id,
                      i => i.ProductId,
                      (p, i) => new { p, i })
                .Where(x => x.i.StockQuantity > 0)
                .CountAsync(ct);

            var percentage = (decimal)productsWithStock / totalActiveProducts * 100m;
            return Math.Round(percentage, 1);
        }

        public async Task<IEnumerable<DashboardActivityDto>> GetRecentActivityAsync(int limit, CancellationToken ct)
        {
            var activities = await _context.WorkOrderHistory
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Take(limit)
                .Select(x => new DashboardActivityDto
                {
                    OrderId = x.WorkOrderId,
                    Status = x.Status,
                    Description = x.Observations ?? $"Se actualizó el estado a {x.Status}",
                    ActionBy = x.ActionBy,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(ct);

            return activities;
        }

        public async Task<int> GetActiveWorkOrdersCountByTypeAsync(string vehicleType, CancellationToken ct)
        {
            return await _context.WorkOrder
                .AsNoTracking()
                .Include(x => x.VehicleNavigation)
                .Where(x => x.IsActive && x.Status != "Entregado" && x.Status != "Cancelado" && x.VehicleNavigation != null && x.VehicleNavigation.VehicleType == vehicleType)
                .CountAsync(ct);
        }

        public async Task<int> GetTotalVehiclesCountByTypeAsync(string vehicleType, CancellationToken ct)
        {
            return await _context.Vehicle
                .AsNoTracking()
                .Where(x => x.IsActive && x.VehicleType == vehicleType)
                .CountAsync(ct);
        }
    }
}
