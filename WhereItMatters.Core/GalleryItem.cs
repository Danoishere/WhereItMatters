using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class GalleryItem : IEntity
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ItemUrl { get; set; }
        public string EmbeddedHtml { get; set; }


        public bool IsEmbeddedHtml
        {
            get
            {
                if (!string.IsNullOrEmpty(EmbeddedHtml))
                {
                    return true;
                }
                return false;
            }
        }


        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    return ImageName;
                }
                else
                {
                    return ItemUrl;
                }
            }
        }

        public int DonationRequestId { get; set; }
        public DonationRequest DonationRequest { get; set; }
    }
}
