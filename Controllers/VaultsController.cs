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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        Vault created = await _vs.Create(newVault);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        return Ok(_vs.Get());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{Id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> GetVaultById(string Id)
    {
      Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
      try
      {
        var result = await _vs.GetVaultById(Id);
        if (result.CreatorId != userInfo.Id)
        {
          if (result.IsPrivate == true)
          {
            return Unauthorized("This doesn't belong to you!");
          }
          else
          {
            return Ok(result);
          }
        }
        else
        {
          return Ok(result);
        }
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Boolean>> DeleteVaultById(string Id)
    {
      try
      {
        var result = await _vs.DeleteVaultById(Id);
        return Ok(result > 0 ? null : false);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Keep>>> Get(int vaultId)
    {
      Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
      try
      {
        var targetVault = await _vs.GetVaultById(vaultId.ToString());
        if (targetVault.CreatorId != userInfo.Id)
        {
          if (targetVault.IsPrivate == true)
          {
            return Unauthorized("This doesn't belong to you!");
          }
          else
          {
            var result = await _vks.GetKeepsByVaultId(vaultId);
            return Ok(result);
          }
        }
        else
        {
          var result = await _vks.GetKeepsByVaultId(vaultId);
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