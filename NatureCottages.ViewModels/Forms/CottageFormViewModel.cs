using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Forms
{
    public class CottageFormViewModel
    {
        public Cottage Cottage { get; set; }        
    }
}
