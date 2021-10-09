using System;

namespace RavenDb.Tryouts.DataLayer.Models
{
    public class RavenItem : TryoutDoc
    {
        public string Name { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}