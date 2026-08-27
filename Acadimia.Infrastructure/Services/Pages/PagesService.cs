using Acadimia.Core.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Pages
{
    public class PagesService : BaseService, IPagesService
    {
        public PagesService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        public async Task<PagedResultDto<List<Page>>> GetAllAsync(PagedResultRequestDto<Page> input)
        {
            IQueryable<Page> pages = _context.Pages.Include(x => x.Category).Include(x => x.Module).Include(x => x.Parent)
                .Where(x=> x.Id != 1) // without parent page.  
                .Where(x => string.IsNullOrEmpty(input.SearchValue.Keyword)
                ? true : (x.Name.Contains(input.SearchValue.Keyword))
                || x.NameEn.Contains(input.SearchValue.Keyword)
                || x.Module.Name.Contains(input.SearchValue.Keyword)
                || x.Parent.Name.Contains(input.SearchValue.Keyword));

            if (input.SearchValue.IsActiveSearch != null)
                pages = pages.Where(x => x.IsActive == input.SearchValue.IsActiveSearch);

            if (input.SearchValue.ParentId > 0)
                pages = pages.Where(x => x.ParentId == input.SearchValue.ParentId);

            if (input.SearchValue.ModuleId > 0)
                pages = pages.Where(x => x.ModuleId == input.SearchValue.ModuleId);

            // for sorting
            if (!(string.IsNullOrEmpty(input.SortColumn) && string.IsNullOrEmpty(input.SortColumnDirection)))
                pages = pages.OrderBy(string.Concat(input.SortColumn, " ", input.SortColumnDirection));

            return new PagedResultDto<List<Page>>()
            {
                Data = await pages.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
                TotalCount = await pages.CountAsync()
            };
        }
        
        public async Task<Page> GetByIdOrDefaultAsync(int id)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.Id == id);
            if (page != null)
                return page;

            return new Page();
        }

        // Acadimia.Infrastructure/Services/Pages/PagesService.cs — replace the existing CreateEditAsync
        public async Task<OperationResult> CreateEditAsync(PageInputDto input)
        {
            var result = new OperationResult();
            try
            {
                if (input.Id == 0)
                {
                    var page = new Page
                    {
                        Name = input.Name,
                        NameEn = input.NameEn,
                        Link = input.Link,
                        Icon = input.Icon,
                        InMenu = input.InMenu,
                        IsActive = input.IsActive,
                        IsAjax = input.IsAjax,
                        ParentId = input.ParentId,
                        ModuleId = input.ModuleId,
                        CategoryId = input.CategoryId
                    };

                    await _context.Pages.AddAsync(page);
                }
                else
                {
                    var page = await _context.Pages.SingleOrDefaultAsync(p => p.Id == input.Id);
                    if (page == null)
                    {
                        result.Message = Messages.Failed;
                        return result;
                    }

                    page.Name = input.Name;
                    page.NameEn = input.NameEn;
                    page.Link = input.Link;
                    page.Icon = input.Icon;
                    page.InMenu = input.InMenu;
                    page.IsActive = input.IsActive;
                    page.IsAjax = input.IsAjax;
                    page.ParentId = input.ParentId;
                    page.ModuleId = input.ModuleId;
                    page.CategoryId = input.CategoryId;

                    _context.Pages.Update(page);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }
            return result;
        }
        public async Task<OperationResult> DeleteAsync(int id)
        {
            var result = new OperationResult();
            var page = await _context.Pages.SingleOrDefaultAsync(p => p.Id == id);
            if (page != null)
            {
                var isHasChildren = await _context.Pages.AnyAsync(p => p.ParentId == page.Id);
                if (isHasChildren)
                {
                    result.Message = Messages.PageHasChildren;
                    return result;
                }

                page.IsDeleted = true;
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = Messages.Success;
            }
            return result;
        }

        public async Task<List<Page>> GetParentsListAsync()
        {
            return await _context.Pages.Where(p => p.CategoryId != 3).ToListAsync();
        }

        public async Task<List<Module>> GetModulesListAsync()
        {
            return await _context.Modules.ToListAsync();
        }
        
        public async Task<List<PageCategory>> GetCategoriesListAsync()
        {
            return await _context.PageCategories.ToListAsync();
        }

        public async Task<List<Page>> GetPagesListForMenu()
        {
            var userId = await GetCurrentUserIdAsync();

            var pages = await (from user in _context.Users
                               join userPermission in _context.UserPermissions on user.UserTypeId equals userPermission.UserTypeId
                               join page in _context.Pages on userPermission.PageId equals page.Id
                               join module in _context.Modules on page.ModuleId equals module.Id into pageModules
                               from module in pageModules.DefaultIfEmpty()
                               where user.Id == userId
                               && page.Id != 1
                               && page.CategoryId != (int)GeneralEnums.Tool
                               && page.InMenu
                               && (module == null || module.Status) // Check if module is null or its status is true
                               select new Page
                               {
                                   Id = page.Id,
                                   Name = page.Name,
                                   NameEn = page.NameEn,
                                   Link = page.Link,
                                   Icon = page.Icon,
                                   ParentId = page.ParentId
                               }).ToListAsync();

            return pages;
        }


    }
}
