using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewDto
{
    public class ManufactureDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int QuantityManu { get; set; }
    }
}