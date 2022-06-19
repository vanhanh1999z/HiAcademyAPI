namespace HiAcademyAPI.Models
{
    public partial class AppCourseinfo
    {
        public string Idcourse { get; set; } = null!;
        public string Idlession { get; set; } = null!;

        public virtual AppCourse IdcourseNavigation { get; set; } = null!;
        public virtual AppLession IdlessionNavigation { get; set; } = null!;
    }
}
