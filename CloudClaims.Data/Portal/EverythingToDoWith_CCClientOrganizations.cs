using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {

    public class CCClientOrganizationANDCCGenericContactCard {
        public CCClientOrganization org { get; set; }
        public CCGenericContactCard contactCard { get; set; }
    }

    public class CCClientOrganizationDropDown {
        public CCClientOrganization org { get; set; }
        public bool selectedFlag { get; set; } 
    }
    public static class EverythingToDoWith_CCClientOrganizations {

        public static List<CCClientOrganization> getAllOrganizations(int clientID) {
            return new CCPortalEntities().CCClientOrganizations.Where(o => o.clientID == clientID).ToList();
        }

        public static List<CCClientOrganizationANDCCGenericContactCard> getAllOrganizationsAndContactCardsFor(int clientID) {
            List<CCClientOrganizationANDCCGenericContactCard> vmList = new List<CCClientOrganizationANDCCGenericContactCard>();
            List<CCClientOrganization> orgs = EverythingToDoWith_CCClientOrganizations.getAllOrganizations(clientID);
            foreach (CCClientOrganization o in orgs) {
                CCClientOrganizationANDCCGenericContactCard tempOrg = new CCClientOrganizationANDCCGenericContactCard();
                tempOrg.org = o;
                if (o.clientOrgGenericContactCardID.HasValue) {
                    tempOrg.contactCard = EverythingToDoWith_CCGenericContactCards.GetContactCardByID(clientID, o.clientOrgGenericContactCardID.Value);
                }
                vmList.Add(tempOrg);
            }
            return vmList;
        }

        public static CCClientOrganization getOrganizationByID(int id, int clientID) {
            return new CCPortalEntities().CCClientOrganizations.Where(o => o.clientID == clientID && o.clientOrgID == id).SingleOrDefault();
        }
        public static CCClientOrganization getOrganizationByGUID(Guid id, int clientID) {
            return new CCPortalEntities().CCClientOrganizations.Where(o => o.clientID == clientID && o.clientOrgGUID == id).SingleOrDefault();
        }
        public static CCClientOrganizationANDCCGenericContactCard getOrganizationANDContactCardByID(Guid id, int clientID) {
            var db = new CCPortalEntities();
            CCClientOrganizationANDCCGenericContactCard o = new CCClientOrganizationANDCCGenericContactCard();
            o.org = db.CCClientOrganizations.Where(x => x.clientID == clientID && x.clientOrgGUID == id).SingleOrDefault();
            o.contactCard = db.CCGenericContactCards.Where(x => x.clientID == clientID && x.genericContactCardID == o.org.clientOrgGenericContactCardID).SingleOrDefault();
            return o;
        }

        public static void updateOrganization(CCClientOrganizationANDCCGenericContactCard o) {
            try {
                var db = new CCPortalEntities();
                db.Entry(o.org).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(o.contactCard).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }
        public static void createOrganization(CCClientOrganizationANDCCGenericContactCard o) {
            try {
                var db = new CCPortalEntities();
                o.contactCard.clientID = o.org.clientID;
                o.contactCard.genericContactCardTypeID = 3;    //1 for clients, 2 for users, 3 for clientOrganizations
                db.CCGenericContactCards.Add(o.contactCard);
                db.SaveChanges();
                
                o.org.clientOrgGUID = Guid.NewGuid();
                o.org.clientOrgTypeID = 1;  //Temp
                o.org.clientOrgGenericContactCardID = o.contactCard.genericContactCardID;
                db.CCClientOrganizations.Add(o.org);
                db.SaveChanges();
            } catch (Exception ex) {
                string x = ex.Message;
            }

        }
    }
}
