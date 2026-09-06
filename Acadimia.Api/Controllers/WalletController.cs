using Acadimia.Api.Helper.Files;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Wallet;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Wallets;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;
        private readonly IFileService _fileService;

        public WalletController(IWalletService walletService, IFileService fileService)
        {
            _walletService = walletService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> MyWallet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wallet = await _walletService.GetMyWalletAsync(userId);
            return Ok(wallet);
        }

        [HttpPost]
        public async Task<IActionResult> GetTransactionHistory([FromBody] DataTableRequestDto? request = null)
        {
            request ??= new DataTableRequestDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _walletService.GetTransactionHistoryAsync(userId, request);

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        // الطالب يرسل طلب شحن رصيد مع صورة إشعار التحويل البنكي
        // ملاحظة: [FromForm] لازم لأنه multipart/form-data (فيه ملف)
        [HttpPost]
        public async Task<OperationResult> SubmitTopUpRequest([FromForm] TopUpRequestInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            // رفع صورة الإشعار أولاً
            var fileName = await _fileService.SaveFile(input.ReceiptFile, "WalletReceipts");
            if (string.IsNullOrEmpty(fileName))
            {
                result.Message = Messages.Failed;
                return result;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await _walletService.SubmitTopUpRequestAsync(userId, input.Amount, input.BankReferenceNo, fileName);
        }

        [HttpPost]
        public async Task<OperationResult> SubmitWithdrawalRequest(WithdrawalRequestInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _walletService.SubmitWithdrawalRequestAsync(userId, input);
        }

        // Admin/Finance: عرض طلبات الشحن المعلّقة (فيها رابط الصورة للمراجعة)
        [HttpGet]
        public async Task<IActionResult> GetPendingTopUpRequests()
        {
            var requests = await _walletService.GetPendingTopUpRequestsAsync();
            return Ok(requests);
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingWithdrawalRequests()
        {
            var requests = await _walletService.GetPendingWithdrawalRequestsAsync();
            return Ok(requests);
        }

        // Admin/Finance: يشوف الصورة ويقرر قبول أو رفض
        [HttpPost]
        public async Task<OperationResult> VerifyTopUpRequest(VerifyTopUpRequestDto input)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _walletService.VerifyTopUpRequestAsync(adminId, input);
        }

        [HttpPost]
        public async Task<OperationResult> DecideWithdrawalRequest(WithdrawalDecisionDto input)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _walletService.DecideWithdrawalRequestAsync(adminId, input);
        }

        [HttpPost]
        public async Task<OperationResult> CompleteWithdrawal(CompleteWithdrawalDto input)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _walletService.CompleteWithdrawalAsync(adminId, input);
        }

        // Admin/Finance: عرض صورة إشعار تحويل معين للمراجعة
        [HttpGet]
        public async Task<IActionResult> GetReceiptImage(string fileName)
        {
            var fileBytes = await _fileService.GetFile("WalletReceipts", fileName);
            if (fileBytes == null || fileBytes.Length == 0)
                return NotFound();

            return File(fileBytes, "image/jpeg"); // أو تحديد الـ content-type حسب امتداد الملف
        }
    }
}