using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Model;

namespace UsuariosApi.Data
{
	public class UsuarioDbContext : IdentityDbContext<Usuario> // modelo que estamos usando para mapear um usuario para o banco
	{
		public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
		{

		}
	}
}

