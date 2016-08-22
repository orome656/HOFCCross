﻿using HOFCCross.Model;
using PushNotification.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    public interface IService
    {
        Task<List<Actu>> GetActu(bool forceRefresh = false);
        Task<List<Match>> GetMatchs();
        Task<List<ClassementEquipe>> GetClassements();
        Task SendNotificationToken(string token, DeviceType type);

        Task<ArticleDetails> GetArticleDetails(string Url);
        Task<List<string>> GetDiaporama(string initData);
    }
}
