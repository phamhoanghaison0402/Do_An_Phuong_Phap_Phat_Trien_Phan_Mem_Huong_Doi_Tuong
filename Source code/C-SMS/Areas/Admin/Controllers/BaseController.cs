using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Custom notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type"> Type of notification
        ///     - success: successfull notification, green color
        ///     - warning: warning notification, yellow color
        ///     - error: error notification, red color
        /// </param>
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if(type=="success")
                TempData["AlertType"] = "alert-success";
            else if (type == "warning")
                TempData["AlertType"] = "alert-warning";
            else if (type == "error")
                TempData["AlertType"] = "alert-danger";
        }

    }
}