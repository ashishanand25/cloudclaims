using CloudClaims.Data.Portal;
using CloudClaims.Model;
using CloudClaims.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;


namespace CloudClaims {
    public class PortalUtils {
        public static CCPerson readCookieData(System.Security.Principal.IIdentity x) {
            //SignIn loggedInPlayer = new SignIn();
            CCPerson loggedInPlayer = new CCPerson();
            //if (x.IsAuthenticated) {
                //loggedInPlayer.userGUID = new Guid();
                if (x is FormsIdentity) {
                    FormsIdentity identity = (FormsIdentity)x;
                    FormsAuthenticationTicket ticket = identity.Ticket;
                    string[] ticketData = ticket.UserData.Split('|');
                    //loggedInPlayer.userGUID = new Guid(ticketData[0].ToString());
                    //loggedInPlayer.clientID = Convert.ToInt32(ticketData[1]);
                    loggedInPlayer = EverythingToDoWith_CCPersons.getPersonByID(new Guid(ticketData[0].ToString()), Convert.ToInt32(ticketData[1]));
                }
            //} else {
                //loggedInPlayer.userGUID = new Guid();
                //loggedInPlayer.username = "?";
                
            //}
            return loggedInPlayer;
        }
    }
}