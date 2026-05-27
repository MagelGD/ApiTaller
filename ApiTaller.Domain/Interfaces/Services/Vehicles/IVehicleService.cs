using ApiTaller.Domain.Dtos.Vehicle;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Vehicles
{
    public interface IVehicleService
    {
        Task<IEnumerable<GetVehicleDto>> GetAllAsync(string? vehicleType, CancellationToken cancellation);
        Task<IEnumerable<GetVehicleDto>> GetAllActiveAsync(string? vehicleType, CancellationToken cancellation);
        Task<GetVehicleDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetVehicleDto> CreateOrEditVehicle(GetVehicleDto vehicle, CancellationToken cancellationToken);
    }
}
