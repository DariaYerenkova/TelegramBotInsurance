using InsurantSales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Application.MindeeService
{
    public interface IMindeeService
    {
        Task<List<ExtractedFieldDTO>> ExtractTextFromFileAsync(string filePaths, CancellationToken cancellationToken);
    }
}
