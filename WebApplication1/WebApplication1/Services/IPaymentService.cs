using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IPaymentService
{
    Task<int> AddPaymentAsync(PaymentDto paymentDto);
}