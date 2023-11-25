using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SendFirebaseNotificationC_.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISendNotification sendNotification;
        public IndexModel(ILogger<IndexModel> logger, ISendNotification sendNotification)
        {
            _logger = logger;
            this.sendNotification = sendNotification;
        }

        public void OnGet()
        {
            sendNotification.SendNotification("Test Notify","Test Notify",Types.Token,"");
        }
    }
}