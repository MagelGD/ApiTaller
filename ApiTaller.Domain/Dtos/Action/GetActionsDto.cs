using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.Operation;

namespace ApiTaller.Domain.Dtos.Action
{
    public class GetActionsDto
    {
        public int Id { get; set; }
        public  GetModuleDto Module { get; set; }
        public  GetOperationDto Operation { get; set; }
        public  string Name { get; set; }
        public  string Slug { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public  string ResponsibleUser { get; set; }
    }
}

