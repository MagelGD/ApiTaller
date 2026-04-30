using ApiTaller.Domain.Dtos.Vehicle;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<GetVehicleDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetVehicleDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetVehicleDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetVehicleDto?> ValidateExist(string plate, CancellationToken cancellation);
        Task<bool> CreateAsync(Vehicle create, CancellationToken cancellation);
        Task<bool> UpdateAsync(Vehicle update, CancellationToken cancellation);
    }
}
