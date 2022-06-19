namespace HiAcademyAPI.Models
{
    public partial class AppCourseforuser
    {
        public string Iduser { get; set; } = null!;
        public string Idcourse { get; set; } = null!;
        public bool Status { get; set; }

        public virtual AppCourse IdcourseNavigation { get; set; } = null!;
        public virtual AppUser IduserNavigation { get; set; } = null!;
    }
}
