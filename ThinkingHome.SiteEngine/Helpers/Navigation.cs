using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ThinkingHome.SiteEngine.Helpers
{
    public static class Navigation
    {
        private static NavigationItem[] items;

        public static void Load(string basePath)
        {
            var path = Path.Combine(basePath, "navigation.json");
            var json = File.ReadAllText(path);
            items = JsonConvert.DeserializeObject<NavigationItem[]>(json);
        }

        public static NavigationItem[] GetItems(HttpRequest request)
        {
            return items
                .Select(item => new NavigationItem
                {
                    url = item.url,
                    content = item.content,
                    selected = request.Path == item.url
                })
                .ToArray();
        }

        public class NavigationItem
        {
            public string content;

            public string url;

            public bool selected;
        }
    }
}