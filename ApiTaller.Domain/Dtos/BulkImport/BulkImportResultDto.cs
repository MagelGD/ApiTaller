using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.BulkImport
{
    public class BulkImportResultDto
    {
        public bool Success { get; set; }
        public int TotalRows { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public int CreatedCategoriesCount { get; set; }
        public int SkippedDuplicates { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<BulkImportErrorDto> Errors { get; set; } = new();
    }

    public class BulkImportErrorDto
    {
        public int RowNumber { get; set; }
        public string Field { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
