using ApiTaller.Domain.Dtos.Workshop;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services
{
    public interface IWorkshopOnboardingService
    {
        Task<int> OnboardWorkshopAsync(WorkshopOnboardingRequestDto request);
    }
}
