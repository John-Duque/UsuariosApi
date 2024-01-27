using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
	{
		public IdadeAuthorization()
		{
		}

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dataNascimentoClaim = context
                .User
                .FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);
            if (dataNascimentoClaim is null)
            {
                return Task.CompletedTask;
            }

            DateTime dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);
            int idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
            {
                idadeUsuario--;
            }

            if (idadeUsuario >= requirement.Idade)
            {
                context.Succeed(requirement);
            }


            return Task.CompletedTask;
        }
    }
}

