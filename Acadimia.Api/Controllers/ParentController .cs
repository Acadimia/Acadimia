using Acadimia.Api.Controllers;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Parent;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class ParentController : BaseController
{
    private readonly IParentService _parentService;
    public ParentController(IParentService parentService) => _parentService = parentService;

    [HttpGet]
    public async Task<IActionResult> MyChildren()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(await _parentService.GetMyChildrenAsync(userId));
    }

    [HttpGet]
    public async Task<IActionResult> ChildAttendance(int studentId, DateTime? from, DateTime? to)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(await _parentService.GetChildAttendanceAsync(userId, studentId, from, to));
    }

    [HttpGet]
    public async Task<IActionResult> ChildExamResults(int studentId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(await _parentService.GetChildExamResultsAsync(userId, studentId));
    }
}