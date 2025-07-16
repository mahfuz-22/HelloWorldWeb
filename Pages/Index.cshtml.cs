using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloWorldWeb.Pages
{
    public class IndexModel : PageModel
    {
        public string GreetingMessage { get; set; } = "";

        public void OnGet()
        {
            // This runs when the page loads
        }

        public void OnPost(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                GreetingMessage = $"Hello, {userName}! Welcome to ASP.NET Core development!";
            }
            else
            {
                GreetingMessage = "Please enter your name!";
            }
        }
    }
}