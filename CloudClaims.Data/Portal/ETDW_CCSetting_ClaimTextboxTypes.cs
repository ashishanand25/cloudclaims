using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {
    public static class ETDW_CCSetting_ClaimTextboxTypes {
        public static CCSetting_ClaimTextboxTypes getTextboxTypeByID(int clientID, int textboxTypeID) {
            var db = new CCPortalEntities();
            CCSetting_ClaimTextboxTypes cdt = db.CCSetting_ClaimTextboxTypes.Find(textboxTypeID);
            if (cdt.clientID == clientID) {
                return cdt;
            } else return null;
        }

        public static string getTextboxTypeName(int clientID, int textboxTypeID) {
            var db = new CCPortalEntities();
            return db.CCSetting_ClaimTextboxTypes.Where(d => d.clientID == clientID && d.textboxTypeID == textboxTypeID).SingleOrDefault().textboxName;
        }

        public static IEnumerable<CCSetting_ClaimTextboxTypes> getAllClaimTextboxTypes(int clientID, bool activeOnly = true) {
            var db = new CCPortalEntities();
            if (activeOnly) {
                return db.CCSetting_ClaimTextboxTypes.Where(d => d.clientID == clientID && d.textboxDeleted == false && d.textboxDisabled == false);
            } else {
                return db.CCSetting_ClaimTextboxTypes.Where(d => d.clientID == clientID && d.textboxDeleted == false);
            }
        }

        public static int addClaimTextboxType(int clientID, string textboxName) {
            var db = new CCPortalEntities();
            CCSetting_ClaimTextboxTypes cdt = new CCSetting_ClaimTextboxTypes {
                clientID = clientID,
                textboxName = textboxName,
                textboxDisabled = false,
                textboxDeleted = false
            };
            db.CCSetting_ClaimTextboxTypes.Add(cdt);
            db.SaveChanges();
            return cdt.textboxTypeID;
        }

        public static void disableClaimTextboxType(int clientID, int claimTextboxTypeID, bool state) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimTextboxTypes.Find(claimTextboxTypeID);
            if (cdt.clientID == clientID) {
                cdt.textboxDisabled = state;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void updateClaimTextboxType(CCSetting_ClaimTextboxTypes cdt) {
            try {
                var db = new CCPortalEntities();
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }

        public static void deleteClaimTextboxType(int clientID, int claimTextboxTypeID) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimTextboxTypes.Find(claimTextboxTypeID);
            if (cdt.clientID == clientID) {
                cdt.textboxDeleted = true;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
