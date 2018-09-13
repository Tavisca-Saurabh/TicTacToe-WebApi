using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Model;
using TicTacToe.DataAccess;

namespace TicTacToe.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        // GET: api/Identity
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string Token = PlayerData.GetCurrentUserToken(id);
            return Token;
        }

        //// GET: api/Identity/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Identity
        [HttpPost]
        public ActionResult Post([FromBody]Player Player)
        {
            var isSuccessful = PlayerData.Add(Player);
            if (isSuccessful)
                return Ok("Player Added successfully Your Name is "+Player.Name+"& Your ID is"+ Player.ID +"");
            return BadRequest("Could not add Player");
        }


        // PUT: api/Identity/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
