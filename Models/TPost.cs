using System;
using System.Collections.Generic;

namespace DataVisualiser.Models
{
    public partial class TPost
    {
        public int PkPostId { get; set; }
        public string FkEmailD { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public DateTime LastModified { get; set; }
    }
}
