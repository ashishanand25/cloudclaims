using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCClaimDates {

        public static int addClaimDateRecord(CCClaimDate d) {
            var db = new CCPortalEntities();
            db.CCClaimDates.Add(d);
            db.SaveChanges();
            return d.claimDateID;
        }

        public static CCClaimDate getClaimDateByID(int clientID, Guid claimGUID, int claimDateID) {
            var db = new CCPortalEntities();
            return db.CCClaimDates.Where(d => d.clientID == clientID && d.claimGUID == claimGUID && d.claimDateID == claimDateID).SingleOrDefault();
        }

        public static IEnumerable<CCClaimDate> getClaimDateRecords(int clientID, Guid claimGuid) {
            var db = new CCPortalEntities();
            return db.CCClaimDates.Where(d => d.clientID == clientID && d.claimGUID == claimGuid);
        }

        public static void updateClaimDateRecord(CCClaimDate d) {
            var db = new CCPortalEntities();
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void daleteClaimDateRecord(CCClaimDate d) {
            var db = new CCPortalEntities();
            db.CCClaimDates.Remove(d);
            db.SaveChanges();
        }

    }
}
