using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using StubLib;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class ChampionsController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        public ChampionsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // GET: api/<ChampionsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItems(0, await _dataManager.ChampionsMgr.GetNbItems());
            if (champ.Count() == 0)
            {
                return NoContent();
            }
            return Ok(champ.ToDtos());
        }

        // GET api/<ChampionsController>/5
        [HttpGet("{nom}")]
        public async Task<IActionResult> Get(string nom)
        {
            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(nom), null);
            if (champ.Count() == 0)
            {
                return NoContent();
            }
            return Ok(champ.ToDtos());
        }

        // POST api/<ChampionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChampionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChampionsController>/5
        [HttpDelete("{nom}")]
        public async Task<IActionResult> Delete(string nom)
        {
            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0,
                await _dataManager.ChampionsMgr.GetNbItemsByName(nom), null);

            bool res = await _dataManager.ChampionsMgr.DeleteItem(champ.FirstOrDefault());
            if (res)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
