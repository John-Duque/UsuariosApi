using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;
using UsuariosApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuariosApi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("Cadastro")]
        public async Task<ActionResult<CreateUsuarioDto>> CadastraUsuario([FromBody] CreateUsuarioDto dto)
        {
            var retorno = await _usuarioService.Cadastra(dto);
            return Created("",retorno);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginUsuarioDto dto)
        {
            TokenDto token = await _usuarioService.Login(dto);
            return Ok(token);
        } 
    }
}

