using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ReturnId { get; set; }

        public bool IsNameChanged { get; set; }
        public string? NewName { get; set; }

        public bool IsAvatarChanged { get; set; }
        public string? NewAvatar { get; set; }
        public string? OldAvatar { get; set; }

        public string FileName { get; set; }

        public OperationResult() { }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
