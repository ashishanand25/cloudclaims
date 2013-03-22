using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCClaimPersons {
        public static int addClaimPerson(int clientID, int claimID, int personID, int claimPersonnelTypeID) {
            var db = new CCPortalEntities();
            CCClaimPerson cp = new CCClaimPerson {
                clientID = clientID,
                claimID = claimID,
                personID = personID,
                claimPersonnelTypeID = claimPersonnelTypeID,
                claimPersonDeleted = false,
                creationDate = DateTime.Now
            };
            db.CCClaimPersons.Add(cp);
            db.SaveChanges();
            return cp.claimPersonID;
        }

        public static IEnumerable<CCClaimPerson> getAllClaimPersons(int clientID, int claimID, int claimPersonnelTypeID = 0) {
            var db = new CCPortalEntities();
            if (claimPersonnelTypeID == 0) {
                return db.CCClaimPersons.Where(c => c.clientID == clientID && c.claimID == claimID);
            } else {
                return db.CCClaimPersons.Where(c => c.clientID == clientID && c.claimID == claimID && c.claimPersonnelTypeID == claimPersonnelTypeID);
            }
        }

        

        public static void deleteClaimPersonByPersonID(int clientID, int claimID, int personID) {
            var db = new CCPortalEntities();
            var p = db.CCClaimPersons.Where(c => c.clientID == clientID && c.claimID == claimID && c.personID == personID).SingleOrDefault();
            p.claimPersonDeleted = true;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void deleteClaimPersonByClaimPersonID(int clientID, int claimID, int claimPersonID) {
            var db = new CCPortalEntities();
            var p = db.CCClaimPersons.Find(claimPersonID);
            if (p.clientID == clientID && p.claimID == claimID) {
                p.claimPersonDeleted = true;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
