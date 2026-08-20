using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Modules
{
    public interface IModulesService
    {
        Task<List<Module>> GetAllAsync();
        Task<OperationResult> SwitchStatusAsync(Module input);
    }
}
