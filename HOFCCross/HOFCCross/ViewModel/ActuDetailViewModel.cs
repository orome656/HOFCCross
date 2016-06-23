using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class ActuDetailViewModel
    {
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }

        public static implicit operator ActuDetailViewModel(Actu actu)
        {
            return new ActuDetailViewModel()
            {
                Content = actu.Detail,
                Date = actu.Date,
                ImageUrl = actu.ImageUrl,
                PostId = actu.PostId,
                Title = actu.Title,
                Url = actu.Url
            };
        }
    }
}
