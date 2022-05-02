using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class MediaLibrary
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
    }
}