using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstract;
using WebApi.Generics;
using WebApi.Model;
using System.Linq;
using System.Text.Json;
using WebApi.Model.Static;
using WebApi.Services;

namespace WebApi.Controllers
{
    public enum Tipologia
    {
        Azione,
        Commedia,
        Drammatico,
        Documentario,
        Fantascienza,
        Horror,
        Animazione,
        Storico
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }


        [HttpGet("RecentFilms/{year}")]
        public ActionResult<List<Film>> ListFilmsByYear(int year)
        {
            var films = _filmService.GetFilmsByYear(year);
            return Ok(films);
        }

        // endpoint che utilizza il metodo con Func
        [HttpGet("FilmTitleLengths")]
        public ActionResult<List<int>> GetFilmTitleLengths()
        {
            var titleLengths = _filmService.CalculateFilmTitleLengths();
            return Ok(titleLengths);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Film>> Get([FromQuery] string genere = null, [FromQuery] int? anno = null)
        {
            try
            {
                var films = _filmService.FilterFilms(genere, anno);
                return Ok(films);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            try
            {
                var film = _filmService.GetFilmById(id);
                if (film == null)
                {
                    return NotFound();
                }
                return Ok(film);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }        
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_filmService.DeleteFilm(id))
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }

        //{
        //  "titolo": "Planet Earth II",
        //  "anno": 2016,
        //  "genere": "Natura",
        //  "tipologia": "Documentario",
        //  "narratore": "David Attenborough"
        //}


        [HttpPost]
        public ActionResult<Film> Post([FromBody] JsonElement filmData)
        {
            try
            {
                string titolo = filmData.GetProperty("titolo").GetString();
                int anno = filmData.GetProperty("anno").GetInt32();
                string genere = filmData.GetProperty("genere").GetString();
                string tipologiaString = filmData.GetProperty("tipologia").GetString();

                Tipologia tipologia;
                if (!Enum.TryParse<Tipologia>(tipologiaString, true, out tipologia))
                {
                    return BadRequest("Tipologia non valida.");
                }

                // Estrapolazione delle proprietà comuni
                string regista = filmData.TryGetProperty("regista", out var registaValue) ? registaValue.GetString() : null;
                string narratore = filmData.TryGetProperty("narratore", out var narratoreValue) ? narratoreValue.GetString() : null;
                string studioAnimazione = filmData.TryGetProperty("studioAnimazione", out var studioValue) ? studioValue.GetString() : null;

                Film nuovoFilm = tipologia switch
                {
                    Tipologia.Azione => new FilmAzione(titolo, anno, genere, regista),
                    Tipologia.Commedia => new FilmCommedia(titolo, anno, genere, regista),
                    Tipologia.Drammatico => new FilmDrammatico(titolo, anno, genere, regista),
                    Tipologia.Fantascienza => new FilmFantascienza(titolo, anno, genere, regista),
                    Tipologia.Horror => new FilmHorror(titolo, anno, genere, regista),
                    Tipologia.Documentario => new Documentario(titolo, anno, genere, narratore),
                    Tipologia.Animazione => new FilmAnimazione(titolo, anno, genere, studioAnimazione),
                    Tipologia.Storico => new FilmStorico(titolo, anno, genere, regista),
                    _ => throw new ArgumentException("Tipologia non riconosciuta")
                };

                _filmService.AddFilm(nuovoFilm);
                return CreatedAtAction(nameof(Get), new { id = nuovoFilm.Titolo }, nuovoFilm);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Film> Update(int id, [FromBody] JsonElement filmData)
        {
            try
            {
                string titolo = filmData.GetProperty("titolo").GetString();
                int anno = filmData.GetProperty("anno").GetInt32();
                string genere = filmData.GetProperty("genere").GetString();
                string tipologiaString = filmData.GetProperty("tipologia").GetString();

                Tipologia tipologia;
                if (!Enum.TryParse<Tipologia>(tipologiaString, true, out tipologia))
                {
                    return BadRequest("Tipologia non valida.");
                }

                // Estrapolazione delle proprietà comuni
                string regista = filmData.TryGetProperty("regista", out var registaValue) ? registaValue.GetString() : null;
                string narratore = filmData.TryGetProperty("narratore", out var narratoreValue) ? narratoreValue.GetString() : null;
                string studioAnimazione = filmData.TryGetProperty("studioAnimazione", out var studioValue) ? studioValue.GetString() : null;

                Film updatedFilm = tipologia switch
                {
                    Tipologia.Azione => new FilmAzione(titolo, anno, genere, regista),
                    Tipologia.Commedia => new FilmCommedia(titolo, anno, genere, regista),
                    Tipologia.Drammatico => new FilmDrammatico(titolo, anno, genere, regista),
                    Tipologia.Fantascienza => new FilmFantascienza(titolo, anno, genere, regista),
                    Tipologia.Horror => new FilmHorror(titolo, anno, genere, regista),
                    Tipologia.Documentario => new Documentario(titolo, anno, genere, narratore),
                    Tipologia.Animazione => new FilmAnimazione(titolo, anno, genere, studioAnimazione),
                    Tipologia.Storico => new FilmStorico(titolo, anno, genere, regista),
                    _ => throw new ArgumentException("Tipologia non riconosciuta")
                };

                if (_filmService.UpdateFilm(id, updatedFilm))
                {
                    return Ok(updatedFilm);
                }
                else
                {
                    return NotFound($"Nessun film trovato con ID {id}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore: {ex.Message}");
            }
        }





    }
}
