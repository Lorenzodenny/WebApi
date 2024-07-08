using WebApi.Abstract;
using WebApi.Controllers;
using WebApi.Model;

namespace WebApi.Design_Pattern.Factory
{
    public class FilmFactory : IFilmFactory
    {
        public Film CreateFilm(Tipologia tipologia, string titolo, int anno, string genere, string creatore, int numeroEpisodi = 0, int numeroStagioni = 0)
        {
            switch (tipologia)
            {
                case Tipologia.Azione:
                    return new FilmAzione(titolo, anno, genere, creatore);
                case Tipologia.Commedia:
                    return new FilmCommedia(titolo, anno, genere, creatore);
                case Tipologia.Drammatico:
                    return new FilmDrammatico(titolo, anno, genere, creatore);
                case Tipologia.Fantascienza:
                    return new FilmFantascienza(titolo, anno, genere, creatore);
                case Tipologia.Horror:
                    return new FilmHorror(titolo, anno, genere, creatore);
                case Tipologia.Documentario:
                    return new Documentario(titolo, anno, genere, creatore); // creatore qui sarebbe il narratore
                case Tipologia.Animazione:
                    return new FilmAnimazione(titolo, anno, genere, creatore); // creatore qui sarebbe lo studio di animazione
                case Tipologia.Storico:
                    return new FilmStorico(titolo, anno, genere, creatore);
                case Tipologia.SerieTV:
                    return new SerieTV(titolo, anno, genere, creatore, numeroEpisodi, numeroStagioni);
                default:
                    throw new ArgumentException("Tipologia non riconosciuta");
            }
        }
    }
}
