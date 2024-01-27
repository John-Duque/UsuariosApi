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
        public async Task<IActionResult> CadastraUsuario([FromBody] CreateUsuarioDto dto)
        {
            await _usuarioService.Cadastra(dto);
            return Ok("Usuário Cadastrado!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto dto)
        {
            string token = await _usuarioService.Login(dto);
            return Ok(token);
        } 
    }
}

