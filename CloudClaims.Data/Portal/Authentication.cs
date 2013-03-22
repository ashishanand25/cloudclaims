using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CloudClaims.Data.Portal {
    public class Authentication {
        public static  CCPerson DoClientLogin(string username, string password) {
            return new CCPortalEntities().CCPersons.Where(p => p.personUserName == username && p.personPassword == password).SingleOrDefault();
        }
    }
}
