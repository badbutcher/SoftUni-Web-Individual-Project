namespace StarCraft.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstats.AdminArea)]
    [Authorize(Roles = WebConstats.AdministratorRole)]
    public class AdminBaseController : Controller
    {
    }
}