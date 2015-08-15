using System.Web.Mvc;

namespace pavlikeMVC.Areas.AdminPanel
{
    public class AdminPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "AdminPanel";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "AdminPanel_default",
               "AdminPanel/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               new[] { "pavlikeMVC.Areas.AdminPanel.Controllers" }
           );
        }
    }
}