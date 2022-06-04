using System.ComponentModel.DataAnnotations;

namespace KenKata.WebApp.Data
{
    //   IS NOT IN SHARED, BUT DIRECTLY IN WEBAPP

    public class PhotoUpload
    {
        [Display(Name = "Upload File")]
        [Required]
        public IFormFile File { get; set; }
    }
}
