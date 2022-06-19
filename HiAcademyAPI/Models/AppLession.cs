namespace HiAcademyAPI.Models
{
    public partial class AppLession
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public string Sound { get; set; } = null!;
    }
}
