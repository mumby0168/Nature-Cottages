using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.General
{
    public class ImageViewerViewModel
    {
        public ImageGroup ImageGroup { get; set; }

        public bool IsEditable { get; set; }  
        
        public bool IsCottage { get; set; }
    }
}
