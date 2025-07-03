using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.DTOs
{
    public record ExtractedFieldDTO(string FieldName, string? Value, double Confidence);

}
