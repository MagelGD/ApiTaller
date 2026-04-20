using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.Operation;

namespace ApiTaller.Domain.Dtos.Action
{
    public class GetActions
    {
        public int Id { get; set; }
        public required GetModule Module { get; set; }
        public required GetOperation Operation { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required string ResponsibleUser { get; set; }
    }
}

