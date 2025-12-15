using Microsoft.AspNetCore.Mvc;

namespace Payments.Api.Controllers;

[ApiController]
[Route("payments")]
public class PaymentsController : ControllerBase
{
    [HttpPost("charge")]
    public IActionResult Charge([FromBody] ChargePaymentRequest request)
    {
        // Simula fallo aleatorio (resiliencia)
        if (Random.Shared.Next(1, 5) == 1)
            return StatusCode(500, "Payment gateway error");

        return Ok(new
        {
            request.OrderId,
            Status = "CHARGED"
        });
    }

    [HttpPost("refund")]
    public IActionResult Refund([FromBody] RefundPaymentRequest request)
    {
        return Ok(new
        {
            request.OrderId,
            Status = "REFUNDED"
        });
    }
}

public record ChargePaymentRequest(Guid OrderId, decimal Amount);
public record RefundPaymentRequest(Guid OrderId);