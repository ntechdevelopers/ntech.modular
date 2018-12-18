using System;
using System.Collections.Generic;
using System.Text;

namespace Ntech.Modules.Core.ViewModels.RoleViewModels
{
    public class RoleViewModel
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string NormalizedName { get; set; }

        public int NumberOfUsers { get; set; }
    }
}
