using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr2.Models;
using keepr2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace keepr2.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        Keep created = _ks.Create(newKeep);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //helpful to check in service if creator is whoever is logged in
        editData.Id = id;
        // return Ok(_ks.Edit(editData, userInfo.Id));
        return Ok(null);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}/views")]
    [Authorize]
    public async Task<ActionResult<Keep>> IncrementViews(int id)
    {
      try
      {
        var result = await _ks.IncrementViewCount(id);
        return Ok(result);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Keep>> GetKeepById(string Id)
    {
      try
      {
        var result = await _ks.GetKeepById(Id);
        return Ok(result);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Boolean>> DeleteKeepById(string Id)
    {
      try
      {
        var result = await _ks.DeleteKeepById(Id);
        return Ok(result > 0 ? null : false);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [Authorize]
    // [HttpDelete("{id}")]
    // public async Task<ActionResult<string>> Delete(int id)
    // {
    //   try
    //   {
    //     Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
    //     return Ok(_ks.Delete(id, userInfo.Id));
    //   }
    //   catch (System.Exception e)
    //   {

    //     return BadRequest(e.Message);

    //   }
    // }
  }
}