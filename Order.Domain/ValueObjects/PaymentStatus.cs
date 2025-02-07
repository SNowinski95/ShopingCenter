﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects
{
    public enum PaymentStatus
    {
        WaitingForReservation,
        WaitingForPayment,
        Paid
    }
}
