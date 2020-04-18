﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingTest.Domain.Models
{
    public class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
