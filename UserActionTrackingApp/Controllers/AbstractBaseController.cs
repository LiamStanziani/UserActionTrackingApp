using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Xml.Schema;

namespace UserActionTrackingApp.Controllers
{
    public abstract class AbstractBaseController : Controller
    {
        // Generates the tracking message by running the methods to get the cookie total count and
        // session count which will returned as a viewbag to be shown on the footer in layout
        public string GenerateUserTrackingMessage(string pageName)
        {
            string fullOutput = ""; 
            fullOutput = $"You have visited this page a total of {GetAndIncrementTotalPageVisitCount(pageName)} times, and {GetAndIncrementPageVisitCountForSession(pageName)} of those visits are from this current session.";
            return ViewBag.fullOutput = fullOutput;
        }

        // Generates cookies that expire in 90 days and checks to see if cookies already exist,
        // which it will get the current ammount of cookies. But if they don't exist then
        // it will add a cookie and then it will return the counter
        private int GetAndIncrementTotalPageVisitCount(string pageName)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(90)
            };

            string homeRequest = HttpContext.Request.Cookies["home_cookie_count"];
            string otherRequest = HttpContext.Request.Cookies["other_cookie_count"];
            int nullCounter = 0;
            int cookieHomeNullCounter = 0;
            int cookieOtherNullCounter = 0;

            if (homeRequest != null)
            {
                cookieHomeNullCounter = Convert.ToInt32(homeRequest);
            }
            if (otherRequest != null)
            {
                cookieOtherNullCounter = Convert.ToInt32(otherRequest);
            }
            else
            {
                if (pageName == "HomeIndex")
                {
                    Response.Cookies.Append("home_cookie_count", nullCounter.ToString(), options);
                }
                else if (pageName == "OtherIndex")
                {
                    Response.Cookies.Append("other_cookie_count", nullCounter.ToString(), options);
                }
            }

            if (pageName == "HomeIndex")
            {
                nullCounter = cookieHomeNullCounter;
                nullCounter++;
                Response.Cookies.Append("home_cookie_count", nullCounter.ToString());
            }
            else if (pageName == "OtherIndex")
            {
                nullCounter = cookieOtherNullCounter;
                nullCounter++;
                Response.Cookies.Append("other_cookie_count", nullCounter.ToString());
            }

            return nullCounter;
        }

        // Checks to see if a session already exists, if it does it will add 1 to the current amount and it
        // will return the amount
        private int GetAndIncrementPageVisitCountForSession(string pageName)
        {

            int tempHomeCount = 0;
            int sessionHomeCount = 0;
            int tempOtherCount = 0;
            int sessionOtherCount = 0;

            if (HttpContext.Session.GetInt32("home_session_visit_count").HasValue)
            {
                tempHomeCount = HttpContext.Session.GetInt32("home_session_visit_count").Value;
            }
            else
            {
                sessionHomeCount++;
                HttpContext.Session.SetInt32("home_session_visit_count", sessionHomeCount);
            }
            if (HttpContext.Session.GetInt32("other_session_visit_count").HasValue)
            {
                tempOtherCount = HttpContext.Session.GetInt32("other_session_visit_count").Value;
            }
            else
            {
                sessionOtherCount++;
                HttpContext.Session.SetInt32("other_session_visit_count", sessionOtherCount);
            }

            sessionHomeCount = tempHomeCount;
            sessionOtherCount = tempOtherCount;

            if (pageName == "HomeIndex")
            {
                sessionHomeCount++;
                HttpContext.Session.SetInt32("home_session_visit_count", sessionHomeCount);
                return sessionHomeCount;
            }
            if (pageName == "OtherIndex")
            {
                sessionOtherCount++;
                HttpContext.Session.SetInt32("other_session_visit_count", sessionOtherCount);
                return sessionOtherCount;
            }
            return -1;
        }

        //if (HttpContext.Session.GetInt32("home_session_visit_count").HasValue)
        //{
        //    sessionHomeCount = HttpContext.Session.GetInt32("home_session_visit_count").Value;
        //}
        //else
        //{
        //    HttpContext.Session.SetInt32("home_session_visit_count", sessionHomeCount++);
        //}

        //if (HttpContext.Session.GetInt32("other_session_visit_count").HasValue)
        //{
        //    sessionOtherCount = HttpContext.Session.GetInt32("other_session_visit_count").Value;
        //}
        //else
        //{
        //    HttpContext.Session.SetInt32("other_session_visit_count", sessionOtherCount++);
        //}

        //if (pageName == "HomeIndex")
        //{
        //    HttpContext.Session.SetInt32("home_session_visit_count", sessionHomeCount++);
        //    return sessionHomeCount;
        //}
        //else if (pageName == "OtherIndex")
        //{
        //    HttpContext.Session.SetInt32("home_session_visit_count", sessionOtherCount++);
        //    return sessionOtherCount;
        //}
        //return -1;
    }
}
