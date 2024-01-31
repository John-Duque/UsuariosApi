using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuariosApi.Controllers
{
    [Route("api/[controller]")]
    public class AcessoController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<RetornoDto> Get()
        {
            return Ok(new RetornoDto { Mensagem = "Acesso Permitido", Sucesso = true });
        }
    }
}

