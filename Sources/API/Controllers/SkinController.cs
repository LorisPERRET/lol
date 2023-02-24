using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        public SkinController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // GET: api/<SkinController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Skin> skins = await _dataManager.SkinsMgr.GetItems(0, await _dataManager.SkinsMgr.GetNbItems());
            if (skins.Count() == 0)
            {
                return NoContent();
            }
            return Ok(skins.ToDtos());
        }

        // GET api/<SkinController>/5
        [HttpGet("{nom}")]
        public async Task<IActionResult> Get(string nom)
        {
            IEnumerable<Skin> skin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0,
                await _dataManager.SkinsMgr.GetNbItemsByName(nom), null);
            if (skin.Count() == 0)
            {
                return NoContent();
            }
            return Ok(skin.Single().ToDto());
        }

        // POST api/<SkinController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SkinDto skin)
        {
            if (await _dataManager.SkinsMgr.GetNbItemsByName(skin.Name) == 0)
            {
                var champion = await _dataManager.ChampionsMgr.GetItemsByName(skin.Champion, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(skin.Champion));
                await _dataManager.SkinsMgr.AddItem(skin.ToSkin(champion.SingleOrDefault()));
                return CreatedAtAction(nameof(Get), skin.Name, skin);
            }
            return BadRequest();
        }

        // PUT api/<SkinController>/5
        [HttpPut("{nom}")]
        public async Task<IActionResult> Put(string nom, [FromBody] SkinDto skin)
        {
            if (!nom.Equals(skin.Name)) return BadRequest();
            var oldSkin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0,
                await _dataManager.SkinsMgr.GetNbItemsByName(nom));

            var champion = await _dataManager.ChampionsMgr.GetItemsByName(skin.Champion, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(skin.Champion));

            var newSkin = await _dataManager.SkinsMgr.UpdateItem(oldSkin.FirstOrDefault(), skin.ToSkin(champion.SingleOrDefault()));
            return Ok(newSkin.ToDto());
        }

        // DELETE api/<SkinController>/5
        [HttpDelete("{nom}")]
        public async Task<IActionResult> Delete(string nom)
        {
            var skin = await _dataManager.SkinsMgr.GetItemsByName(nom, 0, await _dataManager.SkinsMgr.GetNbItemsByName(nom));

            bool res = await _dataManager.SkinsMgr.DeleteItem(skin.FirstOrDefault());
            if (res)
            {
                return Ok(nom);
            }
            return BadRequest();
        }
    }
}
