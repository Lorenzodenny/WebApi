using WebApi.Abstract;
using WebApi.Model;

namespace WebApi.Extension_Method
{
    public static class FilmExtensions
    {
        public static string ExtensionDescrizione(this Film film)
        {
            return film switch
            {
                FilmAnimazione animazione => $"{animazione.Titolo} è un film d'{animazione.Tipologia} prodotto da {animazione.StudioAnimazione}, nell'anno {animazione.Anno}, di genere {animazione.Genere}.",
                Documentario documentario => $"{documentario.Titolo} è un documentario che tratta di {documentario.Genere} narrato da {documentario.Narratore}, dell'anno {documentario.Anno}, di tipologia {documentario.Tipologia}.",
                FilmAzione azione => $"{azione.Titolo} è un film d'{azione.Tipologia} diretto da {azione.Regista}, nell'anno {azione.Anno}, di genere {azione.Genere}.",
                FilmCommedia commedia => $"{commedia.Titolo} è un film d'{commedia.Tipologia} diretto da {commedia.Regista}, nell'anno {commedia.Anno}, di genere {commedia.Genere}.",
                FilmDrammatico drammatico => $"{drammatico.Titolo} è un film d'{drammatico.Tipologia} diretto da {drammatico.Regista}, nell'anno {drammatico.Anno}, di genere {drammatico.Genere}.",
                FilmFantascienza fantascienza => $"{fantascienza.Titolo} è un film d'{fantascienza.Tipologia} diretto da {fantascienza.Regista}, nell'anno {fantascienza.Anno}, di genere {fantascienza.Genere}.",
                FilmHorror horror => $"{horror.Titolo} è un film d'{horror.Tipologia} diretto da {horror.Regista}, nell'anno {horror.Anno}, di genere {horror.Genere}.",
                FilmStorico storico => $"{storico.Titolo} è un film d'{storico.Tipologia} diretto da {storico.Regista}, nell'anno {storico.Anno}, di genere {storico.Genere}.",
                _ => throw new ArgumentException("Tipo di film non riconosciuto")
            };
        }
    }
}
