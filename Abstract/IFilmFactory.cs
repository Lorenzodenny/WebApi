using WebApi.Controllers;

namespace WebApi.Abstract
{
    public interface IFilmFactory
    {
        Film CreateFilm(Tipologia tipologia, string titolo, int anno, string genere, string creatore, int numeroEpisodi = 0, int numeroStagioni = 0);
    }

}
