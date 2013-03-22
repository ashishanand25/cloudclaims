using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {
    
    public class CCPersonANDCCGenericContactCard {
        public CCPerson person { get; set; }
        public CCGenericContactCard contactCard { get; set; }
    }

    public class CCPersonANDCCClientOrganization {
        public CCPerson person { get; set; }
        public CCClientOrganization org { get; set; }
    }

    public static class EverythingToDoWith_CCPersons {

        public static List<CCPerson> getAllInternalPersons(int clientID) {
            return new CCPortalEntities().CCPersons.Where(p => p.clientID == clientID && p.personClientOrgID == null).ToList();
        }
        
        public static List<CCPerson> getAllExternalPersons(int clientID) {
            return new CCPortalEntities().CCPersons.Where(p => p.clientID == clientID && p.personClientOrgID != null).ToList();
        }

        public static List<CCPersonANDCCClientOrganization> getAllExternalPersonsANDClientOrganizations(int clientID) {
            var db = new CCPortalEntities();
            List<CCPersonANDCCClientOrganization> ccpaccco = new List<CCPersonANDCCClientOrganization>();
            foreach (CCPerson p in EverythingToDoWith_CCPersons.getAllExternalPersons(clientID)) {
                ccpaccco.Add(new CCPersonANDCCClientOrganization { person = p,
                                                                   org = EverythingToDoWith_CCClientOrganizations.getOrganizationByID(p.personClientOrgID.Value, clientID) 
                });
            }
            return ccpaccco;
        }

        public static CCPerson getPersonByID(Guid id, int clientID) {
            return new CCPortalEntities().CCPersons.Where(p => p.clientID == clientID && p.personGUID == id).SingleOrDefault();
        }

        public static CCPersonANDCCGenericContactCard getInternalPersonANDContactCardByID(Guid id, int clientID) {
            var db = new CCPortalEntities();
            CCPersonANDCCGenericContactCard p = new CCPersonANDCCGenericContactCard();
            p.person = db.CCPersons.Where(x => x.clientID == clientID && x.personGUID == id).SingleOrDefault();
            p.contactCard = db.CCGenericContactCards.Where(x => x.clientID == clientID && x.genericContactCardID == p.person.personGenericContactCardID && p.person.personClientOrgID == null).SingleOrDefault();
            return p;
        }

        public static CCPersonANDCCGenericContactCard getExternalPersonANDContactCardByID(Guid id, int clientID) {
            var db = new CCPortalEntities();
            CCPersonANDCCGenericContactCard p = new CCPersonANDCCGenericContactCard();
            p.person = db.CCPersons.Where(x => x.clientID == clientID && x.personGUID == id).SingleOrDefault();
            p.contactCard = db.CCGenericContactCards.Where(x => x.clientID == clientID && x.genericContactCardID == p.person.personGenericContactCardID && p.person.personClientOrgID != null).SingleOrDefault();
            return p;
        }

        public static void updateInternalPerson(CCPersonANDCCGenericContactCard p) {
            try {
                var db = new CCPortalEntities();
                db.Entry(p.person).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(p.contactCard).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }

        public static void updateExternalPerson(CCPersonANDCCGenericContactCard p) {
            try {
                var db = new CCPortalEntities();
                db.Entry(p.person).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(p.contactCard).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }
        
        public static void createInternalPerson(CCPersonANDCCGenericContactCard p) {
            var db = new CCPortalEntities();
            p.contactCard.clientID = p.person.clientID;
            p.contactCard.genericContactCardTypeID = (int) CustomEnums.GenericContactCardType.USER;    //1 for clients, 2 for users
            db.CCGenericContactCards.Add(p.contactCard);
            db.SaveChanges();
            
            p.person.personGUID = Guid.NewGuid();
            p.person.personInactive = false;
            p.person.personCreationDate = DateTime.Now;
            p.person.personRoleID = (int)CustomEnums.PersonType.INTERNAL;
            p.person.personGenericContactCardID = p.contactCard.genericContactCardID;
            db.CCPersons.Add(p.person);
            db.SaveChanges();
        }
        
        public static int createExternalPerson(CCPersonANDCCGenericContactCard p) {
            var db = new CCPortalEntities();
            p.contactCard.clientID = p.person.clientID;
            p.contactCard.genericContactCardTypeID = (int) CustomEnums.GenericContactCardType.USER;    //1 for clients, 2 for users
            db.CCGenericContactCards.Add(p.contactCard);
            db.SaveChanges();

            p.person.personGUID = Guid.NewGuid();
            p.person.personInactive = false;
            p.person.personCreationDate = DateTime.Now;
            p.person.personRoleID = (int) CustomEnums.PersonType.EXTERNAL;
            p.person.personGenericContactCardID = p.contactCard.genericContactCardID;
            db.CCPersons.Add(p.person);
            db.SaveChanges();
            return p.person.personID;
        }
    }
}
