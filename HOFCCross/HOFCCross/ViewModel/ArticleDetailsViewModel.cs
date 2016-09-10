using FreshMvvm;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class ArticleDetailsViewModel: BaseViewModel
    {
        public ArticleDetailsViewModel(IService service) : base(service)
        {
        }

        public string ArticleTitle { get; set; }
        public DateTime Date { get; set; }
        public HtmlWebViewSource Html {
            get {
                return new HtmlWebViewSource { Html = "<html><body>" + this.Content + "</body></html>" };
            }
        }
        public string Content { get; set; }
        
        public override async void Init(object initData)
        {
            base.Init(initData);

            var details = await _service.GetArticleDetails((string)initData);
            ArticleTitle = details.Title;
            Date = details.Date;
            Content = details.Article;
            RaisePropertyChanged(nameof(ArticleTitle));
            RaisePropertyChanged(nameof(Date));
            RaisePropertyChanged(nameof(Html));
        }
    }
}
