using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCGenericContactCards {
        
        public static CCGenericContactCard GetContactCardByID(int clientID, int id) {
            var db = new CCPortalEntities();
            return db.CCGenericContactCards.SingleOrDefault(c => c.clientID == clientID && c.genericContactCardID == id);
        }

        public static CCGenericContactCard createGenericContactCard(int clientID, CustomEnums.GenericContactCardType cardType) {
            var db = new CCPortalEntities();
            CCGenericContactCard c = new CCGenericContactCard {
                clientID = clientID,
                genericContactCardTypeID = (int) cardType
            };
            db.CCGenericContactCards.Add(c);
            db.SaveChanges();
            return c;
        }
    }
}
