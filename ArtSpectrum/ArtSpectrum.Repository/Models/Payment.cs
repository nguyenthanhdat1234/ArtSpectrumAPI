﻿using System;
using System.Collections.Generic;

namespace ArtSpectrum.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public int OrderId { get; set; }
    }
}
