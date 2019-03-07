using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.LocalArea
{
    public class LocalAreaViewModel
    {
        public List<Attraction> Attractions { get; set; }

        public List<FacebookPost> FacebookPosts { get; set; }
    }
}
