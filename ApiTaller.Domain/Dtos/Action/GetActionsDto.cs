using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.Operation;

namespace ApiTaller.Domain.Dtos.Action
{
    public class GetActionsDto
    {
        public int Id { get; set; }
        public GetModuleDto Module { get; set; } = null!;
        public GetOperationDto Operation { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ResponsibleUser { get; set; } = null!;
    }
}

