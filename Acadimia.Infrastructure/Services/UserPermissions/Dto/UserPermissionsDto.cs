using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.UserPermissions.Dto
{
    public class UserPermissionsDto
    {
		public List<UserType> UserTypes { get; set; }
		public List<Page> Pages { get; set; }
	}
}
