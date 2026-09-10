using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Lessons;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Lessons;
using Microsoft.AspNetCore.Mvc;

namespace Acadimia.Api.Controllers
{
    public class LessonController : BaseController
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService) => _lessonService = lessonService;

        [HttpPost]
        public async Task<OperationResult> Create(LessonInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return result;
            }
            return await _lessonService.CreateAsync(input);
        }

        [HttpPut]
        public async Task<OperationResult> Update(LessonInputDto input) => await _lessonService.UpdateAsync(input);

        [HttpPut]
        public async Task<OperationResult> ConfigureMeeting(MeetingConfigInputDto input) => await _lessonService.ConfigureMeetingAsync(input);

        [HttpPut]
        public async Task<OperationResult> Cancel(LessonCancelDto input) => await _lessonService.CancelAsync(input);

        [HttpGet]
        public async Task<IActionResult> GetSchedule(int? courseId, int? groupId)
            => Ok(await _lessonService.GetScheduleAsync(courseId, groupId));
    }
}