using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetPlayer() {
            List<Player> players = new List<Player>();

            try {
                if (PlayerData.PalyerTable.Rows.Count > 0)
                {
                    foreach (DataRow row in PlayerData.PalyerTable.Rows) {
                        players.Add(
                            new Player()
                            {
                                Name = row["Name"].ToString(),
                                PlayerNumber = int.Parse(row["PlayerNumber"].ToString()),
                                Phone = row["Phone"].ToString(),
                                Email = row["Email"].ToString(),
                                Tier = row["Tier"].ToString()
                            }
                        );
                    }

                    return Ok(players);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{playerNumber}")]
        public ActionResult GetPlayer(int playerNumber)
        {
            try
            {
                DataRow dr = PlayerData.PalyerTable.Select("PlayerNumber=" + playerNumber.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    Player player = new Player()
                    {
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Tier = dr["Tier"].ToString(),
                        PlayerNumber = playerNumber
                    };

                    return Ok(player);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult PostPlayer(Player player)
        {
            try
            {
                if (ValidatePlayerRequest(player))
                {
                    PlayerData.PalyerTable.Rows.Add(player.PlayerNumber, player.Name, player.Email, player.Phone, player.Tier);

                    return Created("~/api/player", player);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult PutPlayer(Player player)
        {
            try
            {
                DataRow dr = PlayerData.PalyerTable.Select("PlayerNumber=" + player.PlayerNumber.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    dr["Name"] = player.Name;
                    dr["Email"] = player.Email;
                    dr["Phone"] = player.Phone;
                    dr["Tier"] = player.Tier;

                    PlayerData.PalyerTable.AcceptChanges();
                    return Ok(new WebApplication4.Models.Response() { Status = "Complete" });
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{playerNumber}")]
        public ActionResult DeletePlayer(int playerNumber)
        {
            try
            {
                DataRow dr = PlayerData.PalyerTable.Select("PlayerNumber=" + playerNumber.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    dr.Delete(); //changes the Product_name
                    PlayerData.PalyerTable.AcceptChanges();
                    return Ok(new WebApplication4.Models.Response() { Status = "Complete" });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidatePlayerRequest(Player player)
        {
            if (string.IsNullOrWhiteSpace(player.Name) || string.IsNullOrWhiteSpace(player.Phone) || string.IsNullOrWhiteSpace(player.Email) || string.IsNullOrWhiteSpace(player.Tier))
            {
                return false;
            }
            return true;
        }
    }
}
