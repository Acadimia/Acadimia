using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acadimia.Data.Resources;
using Acadimia.Data.DbContext;

namespace Acadimia.Infrastructure.Services.Modules
{
    public class ModulesService : IModulesService
    {
        private readonly ApplicationDbContext _context;
        public ModulesService(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public async Task<List<Module>> GetAllAsync()
        {
            return await _context.Modules.ToListAsync();
        }

        public async Task<OperationResult> SwitchStatusAsync(Module input)
        {
            var result = new OperationResult(false, Messages.Failed);
            
            var module = _context.Modules.SingleOrDefault(m => m.Id == input.Id);
            if (module != null)
            {
                module.Status = input.Status;
                _context.Modules.Update(module);
                await _context.SaveChangesAsync();
                result.Success = true;
                if (module.Status)
                {
                    result.Message = Messages.Activation;
                }
                else
                {
                    result.Message = Messages.Deactivation;
                }

            }
            return result;
        }
    }
}
