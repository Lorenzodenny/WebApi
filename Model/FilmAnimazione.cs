using Microsoft.Win32;
using WebApi.Abstract;
using WebApi.Controllers;
using WebApi.Extension_Method;

namespace WebApi.Model
{
    public class FilmAnimazione : Film
    {
        public string StudioAnimazione { get; set; }

        public FilmAnimazione(string titolo, int anno, string genere, string studioAnimazione) : base(titolo, anno, genere, Tipologia.Animazione)
        {
            StudioAnimazione = studioAnimazione;
        }

        public override string GetDescription()
        {
            // return FilmHelper.Descrizione(this); // classe e metodo static

            return this.ExtensionDescrizione(); // Usa l'extension method
        }

        public override string ToString()
        {
            return $"{Titolo} dello studio {StudioAnimazione} ({Anno})";
        }
    }
}
