using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<int> AddPaymentAsync(PaymentDto paymentDto)
    {
        var client = await _context.Clients.FindAsync(paymentDto.ClientId);
        if (client == null)
        {
            throw new Exception("Client not found");
        }
        var subscription = await _context.Subscriptions.FindAsync(paymentDto.SubscriptionId);
        if (subscription == null)
        {
            throw new Exception("Subscription not found");
        }
        if (subscription.EndDate < DateTime.Now)
        {
            throw new Exception("Subscription is not active");
        }
        var nextPaymentDate = subscription.CreatedAt.AddMonths(subscription.RenewalPeriod);
        if (DateTime.Now < nextPaymentDate)
        {
            throw new Exception("Payment not due yet");
        }
        var existingPayment = await _context.Payments
            .Where(p => p.IdClient == paymentDto.ClientId && p.IdSubscription == paymentDto.SubscriptionId)
            .FirstOrDefaultAsync();
        if (existingPayment != null)
        {
            throw new Exception("Payment already exists for this period");
        }
        var discount = await _context.Discounts
            .Where(d => d.IdSubscription == paymentDto.SubscriptionId && d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now)
            .OrderByDescending(d => d.Value)
            .FirstOrDefaultAsync();
        var amountToPay = subscription.TotalPaidAmount;
        if (discount != null)
        {
            amountToPay -= amountToPay * discount.Value / 100;
        }
        if (paymentDto.Amount != amountToPay)
        {
            throw new Exception("Incorrect payment amount");
        }
        var payment = new Payment();
        payment.IdClient = client.Id;
        payment.IdSubscription = subscription.Id;
        payment.Date = DateTime.Now;
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment.IdPayment;
    }
}