using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCClaimDateTypes {

        public static CCClaimDateType getClaimDateTypeByID(int clientID, int claimDateTypeID) {
            var db = new CCPortalEntities();
            return db.CCClaimDateTypes.Where(d => d.clientID == clientID && d.claimDateTypeID == claimDateTypeID).SingleOrDefault();
        }
        
        public static List<CCClaimDateType> getAllClaimDateTypesByClientID(int clientID, bool activeOnly = false) {
            var db = new CCPortalEntities();
            if (activeOnly)
                return db.CCClaimDateTypes.Where(d => d.clientID == clientID && d.claimDateTypeDisabled == false && d.claimDateTypeDeleted == false).ToList();
            else
                return db.CCClaimDateTypes.Where(d => d.clientID == clientID && d.claimDateTypeDeleted == false).ToList();
        }

        public static void addClaimDateType(int clientID, string claimDateType) {
            var db = new CCPortalEntities();
            db.CCClaimDateTypes.Add(new CCClaimDateType { clientID = clientID, claimDateType = claimDateType, claimDateTypeDisabled = false, claimDateTypeDeleted = false });
            db.SaveChanges();
        }

        public static void deleteClaimDateType(int clientID, int id) {
            var db = new CCPortalEntities();
            //db.CCClaimDateTypes.Remove(db.CCClaimDateTypes.Find(id));
            var d = db.CCClaimDateTypes.Find(id);
            d.claimDateTypeDeleted = true;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void disableClaimDateType(int clientID, int claimDateTypeID, bool state) {
            var db = new CCPortalEntities();
            var cdt = db.CCClaimDateTypes.Find(claimDateTypeID);
            if (cdt.clientID == clientID) {
                cdt.claimDateTypeDisabled = state;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void updateClaimDateType(CCClaimDateType d) {
            try {
                var db = new CCPortalEntities();
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }
    }
}
