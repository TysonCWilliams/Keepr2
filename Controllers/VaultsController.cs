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
    // private readonly VaultKeepsService _vks;
    public VaultsController(VaultsService vs)
    // ,VaultKeepsService vks
    {
      _vs = vs;
      // _vks = vks;
    }

    // [HttpPost]
    // [Authorize]
    // public ActionResult<Vault> Create([FromBody] Vault newVault)
    // {
    //   try
    //   {
    //     Vault created = _vs.Create(newVault);
    //     return Ok(created);
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        Vault created = _vs.Create(newVault);
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
    public async Task<ActionResult<Keep>> GetVaultById(string Id)
    {
      try
      {
        var result = await _vs.GetVaultById(Id);
        return Ok(result);
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

    // [HttpGet("{vaultId}/vaultKeeps")]
    // public ActionResult<IEnumerable<Keep>> Get(int vaultId)
    // {
    //   try
    //   {
    //     return Ok(_vks.GetKeepsByVaultId(vaultId));
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}