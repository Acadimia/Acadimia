 using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Lessons;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Lessons;
using Acadimia.Infrastructure.Services.Ownership;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
    {
        public class LessonController : BaseController
        {
            private readonly ILessonService _lessonService;
            private readonly IOwnershipService _ownershipService;

            public LessonController(ILessonService lessonService, IOwnershipService ownershipService)
            {
                _lessonService = lessonService;
                _ownershipService = ownershipService;
            }

            [HttpPost]
            public async Task<OperationResult> Create(LessonInputDto input)
            {
                var result = new OperationResult(false, Messages.Invalid);
                if (!ModelState.IsValid)
                {
                    result.Message = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return result;
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var owns = input.GroupId != null
                    ? await _ownershipService.OwnsGroupAsync(userId, input.GroupId.Value)
                    : input.CourseId != null && await _ownershipService.OwnsCourseAsync(userId, input.CourseId.Value);

                if (!owns) return new OperationResult(false, Messages.Failed);
                return await _lessonService.CreateAsync(input);
            }

            [HttpPut]
            public async Task<OperationResult> Update(LessonInputDto input)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!await _ownershipService.OwnsLessonAsync(userId, input.Id))
                    return new OperationResult(false, Messages.Failed);

                return await _lessonService.UpdateAsync(input);
            }

            [HttpPut]
            public async Task<OperationResult> ConfigureMeeting(MeetingConfigInputDto input)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!await _ownershipService.OwnsLessonAsync(userId, input.LessonId))
                    return new OperationResult(false, Messages.Failed);

                return await _lessonService.ConfigureMeetingAsync(input);
            }

            [HttpPut]
            public async Task<OperationResult> Cancel(LessonCancelDto input)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!await _ownershipService.OwnsLessonAsync(userId, input.LessonId))
                    return new OperationResult(false, Messages.Failed);

                return await _lessonService.CancelAsync(input);
            }

            [HttpGet]
            public async Task<IActionResult> GetSchedule(int? courseId, int? groupId)
                => Ok(await _lessonService.GetScheduleAsync(courseId, groupId));
        }
    }
