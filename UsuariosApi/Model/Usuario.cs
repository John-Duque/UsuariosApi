using System;
using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Model
{
    public class Usuario : IdentityUser // Tratar como usuario Identity e tem dados para gravar no banco
    {
		public DateTime DataNascimento { get; set; }
		public Usuario() : base()
		{

		}
	}
}

