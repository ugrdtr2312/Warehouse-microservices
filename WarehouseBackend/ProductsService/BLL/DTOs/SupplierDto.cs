using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BLL.DTOs
{
    public class SupplierDto : BaseDto
    {
        [Required, NotNull]
        public string CompanyName { get; set; }
    }
}