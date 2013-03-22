using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCSetting_ClaimDropdownTypes {

        public static CCSetting_ClaimDropdownTypes getDropdownTypeByID(int clientID, int dropdownTypeID) {
            var db = new CCPortalEntities();
            CCSetting_ClaimDropdownTypes cdt = db.CCSetting_ClaimDropdownTypes.Find(dropdownTypeID);
            if (cdt.clientID == clientID) {
                return cdt;
            } else return null;
        }
        
        public static string getDropdownTypeName(int clientID, int dropdownTypeID) {
            var db = new CCPortalEntities();
            return db.CCSetting_ClaimDropdownTypes.Where(d => d.clientID == clientID && d.dropdownTypeID == dropdownTypeID).SingleOrDefault().dropdownName;
        }

        public static IEnumerable<CCSetting_ClaimDropdownTypes> getAllClaimDropdownTypes(int clientID, bool activeOnly = true) {
            var db = new CCPortalEntities();
            if (activeOnly) {
                return db.CCSetting_ClaimDropdownTypes.Where(d => d.clientID == clientID && d.dropdownDeleted == false && d.dropdownDisabled == false);
            } else {
                return db.CCSetting_ClaimDropdownTypes.Where(d => d.clientID == clientID && d.dropdownDeleted == false);
            }
        }

        public static int addClaimDropdownType(int clientID, string dropdownName, bool hideOnNewClaim = false) {
            var db = new CCPortalEntities();
            CCSetting_ClaimDropdownTypes cdt = new CCSetting_ClaimDropdownTypes {
                clientID = clientID,
                dropdownName = dropdownName,
                hideOnNewClaim = hideOnNewClaim,
                dropdownDisabled = false,
                dropdownDeleted = false
            };
            db.CCSetting_ClaimDropdownTypes.Add(cdt);
            db.SaveChanges();
            return cdt.dropdownTypeID;
        }

        public static void disableClaimDropdownType(int clientID, int claimDropdownTypeID, bool state) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimDropdownTypes.Find(claimDropdownTypeID);
            if (cdt.clientID == clientID) {
                cdt.dropdownDisabled = state;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void updateClaimDropdownType(CCSetting_ClaimDropdownTypes cdt) {
            try {
                var db = new CCPortalEntities();
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }

        public static void deleteClaimDropdownType(int clientID, int claimDropdownTypeID) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimDropdownTypes.Find(claimDropdownTypeID);
            if (cdt.clientID == clientID) {
                cdt.dropdownDeleted = true;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
