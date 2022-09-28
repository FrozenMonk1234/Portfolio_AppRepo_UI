namespace Portfolio_AppRepo_UI.Models
{
    public class AddApplicationModel
    {
        public int ID { get; set; }
        public string URL { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? User { get; set; }
        public bool IsActive { get; set; }
        public string Stage { get; set; } = string.Empty;
        public List<ApiUrlModel> ApiList { get; set; } = new List<ApiUrlModel>();
        public List<ApiUrlModel> ServiceList { get; set; } = new List<ApiUrlModel>();
        public ApplicationFileModel FileNameModel { get; set; } = new ApplicationFileModel();

    }
}
