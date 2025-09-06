using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs.Reservation_DTOs;
using Stripe.Forwarding;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManger serviceManger) : ApiController
    {
        // Create or Update PaymentIntent for Reservation
        [HttpPost("{reservationId:int}")]
        public async Task<ActionResult<ReservationReturnDTO>> CreateOrUpdatePayment(int reservationId)
        {
            var result = await serviceManger.PaymentServices.CreateOrUpdatePaymentIntentAsync(reservationId);

            return Ok(result);
        }

        // Stripe Webhook
        [HttpPost("WebHook")] // Example: https://localhost:7130/api/Payments/WebHook
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeHeader = Request.Headers["Stripe-Signature"];

            await serviceManger.PaymentServices.UpdateOrderPaymentStatus(json, stripeHeader!);

            return new EmptyResult();
        }
    }
    }
