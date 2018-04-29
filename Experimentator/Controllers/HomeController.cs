using Microsoft.AspNetCore.Mvc;

namespace Experimentator.Controllers
{
    public class HomeController:Controller
    {
        public ViewResult Index() => View();
    }
}
