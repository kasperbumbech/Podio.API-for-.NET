using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class ImageItemField : ItemField
    {
        private List<FileAttachment> _images;

        public IEnumerable<FileAttachment> Images
        {
            get
            {
                return this.valuesAs<FileAttachment>(_images);
            }
        }
    }
}
