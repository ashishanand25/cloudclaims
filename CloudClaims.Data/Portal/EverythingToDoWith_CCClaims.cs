using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCClaims {

        public static CCClaim getClaimByGuid(Guid id, int clientID) {
            
                var db = new CCPortalEntities();
                return db.CCClaims.Where(c => c.claimGUID == id && c.clientID == clientID).SingleOrDefault();
            
        }
        public static CCClaim getClaimByID(int id, int clientID) {
            try {
                var db = new CCPortalEntities();
                return db.CCClaims.Where(c => c.claimID == id && c.clientID == clientID).SingleOrDefault();
            } catch {
                throw new Exception();
            }
        }

        public static IEnumerable<CCClaim> getAllClaimsFor(int clientID) {
            try {
                var db = new CCPortalEntities();
                return db.CCClaims.Where(c => c.clientID == clientID);
            } catch {
                throw new Exception();
            }
        }

        public static Guid addClaim(CCClaim c) {
            try {
                var db = new CCPortalEntities();
                c.claimGUID = Guid.NewGuid();
                db.CCClaims.Add(c);
                db.SaveChanges();
                return c.claimGUID;
            } catch {
                throw new Exception();
            }
        }
        public static void updateClaim(CCClaim c) {
            var db = new CCPortalEntities();
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static void deleteClaim(CCClaim c) {
            var db = new CCPortalEntities();
            db.CCClaims.Remove(db.CCClaims.Find(c.claimID));
            db.SaveChanges();
        }

    }
}
