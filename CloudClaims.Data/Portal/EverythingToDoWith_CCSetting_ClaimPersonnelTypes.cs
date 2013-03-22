using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCSetting_ClaimPersonnelTypes {
        public static IEnumerable<CCSetting_ClaimPersonnelTypes> getAllClaimPersonnelTypes(int clientID, bool activeOnly = true) {
            var db = new CCPortalEntities();
            if (activeOnly) {
                return db.CCSetting_ClaimPersonnelTypes.Where(c => c.clientID == clientID && c.claimPersonnelTypeDeleted == false && c.claimPersonnelTypeDisabled == false);
            } else {
                return db.CCSetting_ClaimPersonnelTypes.Where(c => c.clientID == clientID && c.claimPersonnelTypeDeleted == false);
            }
        }
        
        public static int addClaimPersonnelType(int clientID, string personnelType, bool allowMultiple = false) {
            var db = new CCPortalEntities();
            CCSetting_ClaimPersonnelTypes cpt = new CCSetting_ClaimPersonnelTypes {
                clientID = clientID,
                allowMultiple = allowMultiple,
                claimPersonnelType = personnelType,
                claimPersonnelTypeDeleted = false,
                claimPersonnelTypeDisabled = false
            };
            db.CCSetting_ClaimPersonnelTypes.Add(cpt);
            db.SaveChanges();
            return cpt.claimPersonnelTypeID;
        }

        public static void deleteClaimPersonnelType(int clientID, int claimPersonnelTypeID) {
            var db = new CCPortalEntities();
            var cpt = db.CCSetting_ClaimPersonnelTypes.Find(claimPersonnelTypeID);
            if (cpt.clientID == clientID) {
                cpt.claimPersonnelTypeDeleted = true;
                db.Entry(cpt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void disableClaimPersonnelType(int clientID, int claimPersonnelTypeID, bool state) {
            var db = new CCPortalEntities();
            var cpt = db.CCSetting_ClaimPersonnelTypes.Find(claimPersonnelTypeID);
            if (cpt.clientID == clientID) {
                cpt.claimPersonnelTypeDisabled = state;
                db.Entry(cpt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void updateClaimPersonnelType(CCSetting_ClaimPersonnelTypes cpt) {
            try {
                var db = new CCPortalEntities();
                db.Entry(cpt).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }

        public static string getClaimPersonnelTypeNameByID(int clientID, int claimPersonnelTypeID) {
            var db = new CCPortalEntities();
            return db.CCSetting_ClaimPersonnelTypes.Where(p => p.clientID == clientID && p.claimPersonnelTypeID == claimPersonnelTypeID).SingleOrDefault().claimPersonnelType;
        }
    }
}
