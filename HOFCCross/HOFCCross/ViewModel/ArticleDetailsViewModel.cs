using FreshMvvm;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class ArticleDetailsViewModel: FreshBasePageModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public HtmlWebViewSource Html {
            get {
                return new HtmlWebViewSource { Html = "<html><body>" + this.Content + "</body></html>" };
            }
        }
        public string Content { get; set; }

        private IService _service;

        public ArticleDetailsViewModel(IService service)
        {
            _service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            var details = await _service.GetArticleDetails((string)initData);
            Title = details.Title;
            Date = details.Date;
            Content = details.Article;
        }
    }
}
