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
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetProfileById(string id)
    {
      Console.WriteLine(id);
      try
      {
        var result = await _ps.GetProfileById(id);

        if (result == null) return NotFound();

        return result;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsByProfile(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        if (userInfo.Id == id)
        {
          var result = await _ks.GetAllKeepsForUser(userInfo.Id);
          return Ok(result);
        }
        else
        {
          var result = await _ks.GetAllPublicKeepsForUser(id);
          return Ok(result);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpGet("{id}/vaults")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfile(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        if (userInfo.Id == id)
        {
          var result = await _vs.GetAllVaultsForUser(userInfo.Id);
          return Ok(result);
        }
        else
        {
          var result = await _vs.GetAllPublicVaultsForUser(id);
          return Ok(result);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }
  }
}