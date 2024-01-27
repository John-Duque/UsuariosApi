using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
	public class CreateUsuarioDto
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public DateTime DataNascimento { get; set; }
		[Required]
		[DataType(DataType.Password)] // Para Dizer que essa campo e tratado como senha
		public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
		public string RePassword { get; set; }
	}
}

