using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Pages;

namespace Acadimia.Infrastructure.Services.Pages
{
    public interface IPagesService
    {
        Task<PagedResultDto<List<Page>>> GetAllAsync(PagedResultRequestDto<Page> input);
        Task<List<Page>> GetPagesListForMenu();
        Task<Page> GetByIdOrDefaultAsync(int id);
        Task<OperationResult> CreateEditAsync(PageInputDto input);
        Task<OperationResult> DeleteAsync(int id);
        Task<List<Page>> GetParentsListAsync();
        Task<List<Module>> GetModulesListAsync();
        Task<List<PageCategory>> GetCategoriesListAsync();
    }
}