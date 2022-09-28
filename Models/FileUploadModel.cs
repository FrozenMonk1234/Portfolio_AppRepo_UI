using Microsoft.AspNetCore.Components.Forms;

namespace Portfolio_AppRepo_UI.Models
{
    public class FileUploadModel
    {
        public bool loaded { get; set; }
        public string? ApplicationName { get; set; }
        public string? DocumentType { get; set; }
        public IBrowserFile? UploadedFile { get; set; } 
    }
}
