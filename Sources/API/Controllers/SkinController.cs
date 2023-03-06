using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        private readonly ILogger _logger;

        public SkinController(IDataManager dataManager, ILogger logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        // GET: api/<SkinController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 0, [FromQuery] int offset = 10)
        {
            try
            {
                if (offset > 30 || offset < 0)
                {
                    _logger.LogInformation("Le nombre d'item demandé ne peut être supérieur à 30 ou inférieur à 0");
                    return Forbid();
                }



                var nbItem = await _dataManager.ChampionsMgr.GetNbItems();
                var nbPage = Math.Ceiling((double)nbItem / offset);
                if (page > 0 || page > nbPage)
                {
                    _logger.LogInformation("Le numero de page est incorrect");
                    return Forbid();
                }

                var startIndex = page * offset;
                IEnumerable<Skin> skins = await _dataManager.SkinsMgr.GetItems(startIndex, offset);
                if (skins.Any())
                {
                    _logger.LogInformation("Aucun skin n'a été trouvé");
                    return NoContent();
                }

                var result = new
                {
                    nbItem,
                    offset,
                    items = skins.ToDtos(),
                };

                return Ok(result);

            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // GET api/<SkinController>/test
        [HttpGet("{nom}")]
        public async Task<IActionResult> Get(string nom)
        {
            try
            {
                IEnumerable<Skin> skin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0,
                    await _dataManager.SkinsMgr.GetNbItemsByName(nom), null);
                if (skin.Count() == 0)
                {
                    _logger.LogInformation("Aucun skin n'a été trouvé");
                    return NoContent();
                }
                return Ok(skin.Single().ToDto());

            }
            catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // POST api/<SkinController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SkinDto skin)
        {
            try
            {
                if (await _dataManager.SkinsMgr.GetNbItemsByName(skin.Name) == 0)
                {
                    await _dataManager.SkinsMgr.AddItem(skin.ToSkin());
                    return CreatedAtAction(nameof(Get), skin.Name, skin);
                }
                _logger.LogInformation("Le skin voulu est déja existant");
                return BadRequest();
            }
            catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // PUT api/<SkinController>/test
        [HttpPut("{nom}")]
        public async Task<IActionResult> Put(string nom, [FromBody] SkinDto skin)
        {
            try
            {
                if (!nom.Equals(skin.Name))
                {
                    _logger.LogInformation("Vous ne pouvez pas modifier le nom d'un skin");
                    return BadRequest();
                }
                var oldSkin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0,
                    await _dataManager.SkinsMgr.GetNbItemsByName(nom));

                var newSkin = await _dataManager.SkinsMgr.UpdateItem(oldSkin.FirstOrDefault(), skin.ToSkin());
                return Ok(newSkin.ToDto());

            }
            catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // DELETE api/<SkinController>/test
        [HttpDelete("{nom}")]
        public async Task<IActionResult> Delete(string nom)
        {
            try
            {
                var skin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0, await _dataManager.SkinsMgr.GetNbItemsByName(nom));

                bool res = await _dataManager.SkinsMgr.DeleteItem(skin.FirstOrDefault());
                if (res)
                {
                    return Ok(nom);
                }
                _logger.LogInformation("Le skin n'a pas été supprimer");
                return BadRequest();
            }
            catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }
    }
}
