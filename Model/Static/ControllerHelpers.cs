using System.Text.Json;
using WebApi.Controllers;

namespace WebApi.Model.Static
{
    public static class ControllerHelpers
    {
        public static string ExtractCreator(JsonElement data, Tipologia tipologia)
        {
            switch (tipologia)
            {
                case Tipologia.Documentario:
                    return data.GetProperty("narratore").GetString();
                case Tipologia.Animazione:
                    return data.GetProperty("studioAnimazione").GetString();
                default:
                    return data.TryGetProperty("regista", out var registaValue) ? registaValue.GetString() : null;
            }
        }

        public static void SetCacheHeader(HttpResponse response, int maxAgeSeconds)
        {
            response.Headers.Add("Cache-Control", $"public, max-age={maxAgeSeconds}");
        }
    }

}
