using System;
using System.Collections.Generic;

namespace HiAcademyAPI.Models
{
    public partial class AppCourse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
