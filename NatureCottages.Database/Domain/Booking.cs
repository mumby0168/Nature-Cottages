using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Booking
    {
        public int Id { get; set; }

        [DisplayName("Date From")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date To")]
        [Required]
        public DateTime DateTo { get; set; }

        public bool IsPendingApproval { get; set; }

        public double TotalPrice { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        
        public int CustomerId { get; set; }

        [ForeignKey("CottageId")]
        public Cottage Cottage { get; set; }

        public int CottageId { get; set; }
    }
}
