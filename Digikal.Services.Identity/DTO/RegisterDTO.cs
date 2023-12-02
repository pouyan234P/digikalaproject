using Digikala.Services.Identity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.DTO
{
    public class RegisterDTO
    {
        public string? Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string family { get; set; }
        public string Country { get; set; }
        public string phoneNumber { get; set; }
        public string Role { get; set; }
        public string? Url { get; set; }

        public bool AllowRememberLogin { get; set; } = true;
        public bool EnableLocalLogin { get; set; } = true;

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
