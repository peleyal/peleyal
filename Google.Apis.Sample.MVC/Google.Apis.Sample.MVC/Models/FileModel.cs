using System;

namespace Google.Apis.Sample.MVC.Models
{
    public class FileModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DownloadUrl { get; set; }
    }
}