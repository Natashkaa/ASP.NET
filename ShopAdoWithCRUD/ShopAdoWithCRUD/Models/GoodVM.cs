using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAdoWithCRUD.Models
{
    public class GoodVM
    {
        public int Id { get; set; }
        [Required]
        public string GoodName { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public List<string> ImagePath { get; set; }
    }
}