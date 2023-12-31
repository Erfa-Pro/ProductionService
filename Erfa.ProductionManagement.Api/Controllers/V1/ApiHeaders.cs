using Microsoft.AspNetCore.Mvc;

namespace Erfa.ProductionManagement.Api.Controllers.V1
{
    public class ApiHeaders
    {
        [FromHeader]
        public string UserName { get; set; } = string.Empty;
    }
}
