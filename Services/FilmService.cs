using WebApi.Abstract;
using WebApi.Generics;
using WebApi.Model;
using WebApi.Model.Static;

namespace WebApi.Services
{
    public class FilmService
    {
        private readonly Container<Film> _filmContainer;

        public FilmService()
        {
            _filmContainer = new Container<Film>();
            ConfigureLogging(isProduction: true);
            InitializeFilms();
        }

        private void ConfigureLogging(bool isProduction)
        {
            _filmContainer.LogAction = LogHelper.Log;
            LogHelper.ConfigureLogging(isProduction);
        }

        private void InitializeFilms()
        {
            _filmContainer.AddItem(new Documentario("Planet Earth", 2006, "Documentario", "David Attenborough"));
            _filmContainer.AddItem(new FilmDrammatico("Schindler's List", 1993, "Drammatico", "Steven Spielberg"));
            _filmContainer.AddItem(new FilmAzione("Mad Max: Fury Road", 2015, "Azione", "George Miller"));
            _filmContainer.AddItem(new FilmCommedia("The Grand Budapest Hotel", 2014, "Commedia", "Wes Anderson"));
            _filmContainer.AddItem(new FilmHorror("Get Out", 2017, "Horror", "Jordan Peele"));
            _filmContainer.AddItem(new FilmFantascienza("Blade Runner 2049", 2017, "Fantascienza", "Denis Villeneuve"));
            _filmContainer.AddItem(new FilmAnimazione("Toy Story", 1995, "Animazione", "Pixar Animation Studios"));
            _filmContainer.AddItem(new FilmStorico("Braveheart", 1995, "Storico", "Mel Gibson"));
        }

        public List<Film> GetRecentFilms()
        {
            return _filmContainer.FindFilms(film => film.Anno > 2010);
        }

        public List<Film> GetAllFilms()
        {
            return _filmContainer.GetAllItems();
        }

        public Film GetFilmById(int id)
        {
            return _filmContainer.GetItemById(id);
        }

        public bool AddFilm(Film film)
        {
            _filmContainer.AddItem(film);
            return true;
        }

        public bool DeleteFilm(int id)
        {
            Film film = GetFilmById(id);
            if (film != null)
            {
                _filmContainer.RemoveItem(film);
                return true;
            }
            return false;
        }
    }
}
