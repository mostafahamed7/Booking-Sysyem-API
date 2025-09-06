using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Order_Entites
{
    public enum ReservationPaymentStatus
    {
        Pending = 0,
        PymentReceived = 1,
        PaymentFailed = 2,
    }
}
