using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data {
    
    public class CCClientANDCCGenericContactCard {
        public CCClient client { get; set; }
        public CCGenericContactCard contactCard { get; set; }
    }

    public static class EverythingToDoWith_CCClient {
        
        public static List<CCClient> getAllClients() {
            return new CCPortalEntities().CCClients.ToList();
        }

        public static CCClient getClientByID(int id) {
            return new CCPortalEntities().CCClients.Find(id);
        }

        public static CCClientANDCCGenericContactCard getClientANDContactCardByID(int id) {
            CCClientANDCCGenericContactCard c = new CCClientANDCCGenericContactCard();
            c.client = new CCPortalEntities().CCClients.Find(id);
            c.contactCard = new CCPortalEntities().CCGenericContactCards.Where(x => x.genericContactCardID == c.client.clientGenericContactCardID).SingleOrDefault();
            return c;
        }

        public static void updateClient(CCClientANDCCGenericContactCard c) {
            try {
                var db = new CCPortalEntities();
                db.Entry(c.contactCard).State = EntityState.Modified;
                db.Entry(c.client).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string ex = e.Message;
            }
        }
        public static void createClient(CCClient c) {
            try {
                var db = new CCPortalEntities();
                c.clientGUID = Guid.NewGuid();
                c.clientTimeZone = 0;
                c.clientInactive = false;
                c.clientGroup = "DEFAULT";
                c.clientGenericContactCardID = -1;  //Initialize
                db.CCClients.Add(c);
                db.SaveChanges();


                CCGenericContactCard cc = new CCGenericContactCard();
                cc.clientID = c.clientID;
                cc.genericContactCardTypeID = 2;    //1 for clients, 2 for users
                cc.genericContactAddrLine1 = "155 Mark Tree Road";
                db.CCGenericContactCards.Add(cc);
                db.SaveChanges();

                c.clientGenericContactCardID = cc.genericContactCardID;
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();

                //Create admin account card for admin user as well
                CCGenericContactCard cc2 = new CCGenericContactCard();
                cc2.clientID = c.clientID;
                cc2.genericContactCardTypeID = 2;    //1 for clients, 2 for users
                db.CCGenericContactCards.Add(cc2);
                db.SaveChanges();

                
                CCPerson clientAdministrator = new CCPerson();
                clientAdministrator.personParentID = c.clientID;
                clientAdministrator.personCreationDate = DateTime.Now;
                clientAdministrator.clientID = c.clientID;
                clientAdministrator.personRoleID = 1;
                clientAdministrator.personInactive = false;
                clientAdministrator.personParentTable = "CCClients";
                clientAdministrator.personGUID = Guid.NewGuid();
                clientAdministrator.personInactive = false;
                clientAdministrator.personFName = "Administrator";
                clientAdministrator.personLName = "Administrator";
                clientAdministrator.personUserName = c.clientGUID.ToString();
                clientAdministrator.personPassword = c.clientGUID.ToString();
                clientAdministrator.personGenericContactCardID = cc2.genericContactCardID;
                db.CCPersons.Add(clientAdministrator);
                db.SaveChanges();
                
            } catch (Exception e) {
                string x = e.Message;
            }
        }
    }
}
