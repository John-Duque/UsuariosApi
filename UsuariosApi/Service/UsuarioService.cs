using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Service
{
	public class UsuarioService
	{
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<CreateUsuarioDto> Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            }

            return _mapper.Map<CreateUsuarioDto>(usuario);

            
        }

        public async Task<TokenDto> Login(LoginUsuarioDto dto)
        {
            SignInResult resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false); // Usando esse metodo e possivel fazer o login por usuario e senha

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            Usuario? usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

            TokenDto token = new TokenDto
            {
                Token = _tokenService.GenerateToken(usuario),
                Mensagem = "Token Gerado com sucesso",
                Sucesso = true
            };


            return token;
        }
    }
}

