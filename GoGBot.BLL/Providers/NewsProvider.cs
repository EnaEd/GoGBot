using GoGBot.BLL.Models;
using GoGBot.BLL.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Configurations;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationEnum = Shared.Enums;

namespace GoGBot.BLL.Providers
{
    public class NewsProvider : INewsProvider
    {
        private readonly IRandomProvider _randomProvider;
        private readonly IConfiguration _configuration;

        public NewsProvider(IConfiguration configuration, IRandomProvider randomProvider)
        {
            _configuration = configuration;
            _randomProvider = randomProvider;
        }

        public async Task<ForismaticModel> GetCurrentNewsAsync(Shared.Enums.Enum.NewsThemeType theme)//TODO EE:improve by add content type specific
        {
            using HttpClient httpClient = new HttpClient();

            string url = _configuration[nameof(ForismaticApiConfig.ForismaticApi)];
            var result = await httpClient.GetAsync(url);
            ForismaticModel model = JsonConvert.DeserializeObject<ForismaticModel>(await result.Content.ReadAsStringAsync());
            return model;
        }

        public ApplicationEnum.Enum.NewsThemeType GetCurrentTheme()
        {
            var themeCount = Enum.GetNames(typeof(ApplicationEnum.Enum.NewsThemeType)).Length;
            var currentTheme = _randomProvider.GetRandomInt(themeCount);
            var parseResult = Enum.TryParse(typeof(ApplicationEnum.Enum.NewsThemeType), currentTheme.ToString(), out object theme);
            if (!parseResult || (ApplicationEnum.Enum.NewsThemeType)theme == default)//if parse fail or enum is None set first enum
            {
                theme = ApplicationEnum.Enum.NewsThemeType.Beard;
            }
            return (ApplicationEnum.Enum.NewsThemeType)theme;
        }
    }
}
