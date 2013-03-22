using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace CloudClaims.Data.Portal
{
    public static class ETDW_CCSetting_ClaimCheckboxTypes {
        public static CCSetting_ClaimCheckboxTypes getCheckboxTypeByID(int clientID, int CheckboxTypeID) {
            var db = new CCPortalEntities();
            CCSetting_ClaimCheckboxTypes cdt = db.CCSetting_ClaimCheckboxTypes.Find(CheckboxTypeID);
            if (cdt.clientID == clientID) {
                return cdt;
            } else return null;
        }

        public static string getCheckboxTypeName(int clientID, int CheckboxTypeID) {
            var db = new CCPortalEntities();
            return db.CCSetting_ClaimCheckboxTypes.Where(d => d.clientID == clientID && d.checkboxTypeID == CheckboxTypeID).SingleOrDefault().checkboxName;
        }

        public static IEnumerable<CCSetting_ClaimCheckboxTypes> getAllClaimCheckboxTypes(int clientID, bool activeOnly = true) {
            var db = new CCPortalEntities();
            if (activeOnly) {
                return db.CCSetting_ClaimCheckboxTypes.Where(d => d.clientID == clientID && d.checkboxDeleted == false && d.checkboxDisabled == false);
            } else {
                return db.CCSetting_ClaimCheckboxTypes.Where(d => d.clientID == clientID && d.checkboxDeleted == false);
            }
        }

        public static int addClaimCheckboxType(int clientID, string CheckboxName) {
            var db = new CCPortalEntities();
            CCSetting_ClaimCheckboxTypes cdt = new CCSetting_ClaimCheckboxTypes {
                clientID = clientID,
                checkboxName = CheckboxName,
                checkboxDisabled = false,
                checkboxDeleted = false
            };
            db.CCSetting_ClaimCheckboxTypes.Add(cdt);
            db.SaveChanges();
            return cdt.checkboxTypeID;
        }

        public static void disableClaimCheckboxType(int clientID, int claimCheckboxTypeID, bool state) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimCheckboxTypes.Find(claimCheckboxTypeID);
            if (cdt.clientID == clientID) {
                cdt.checkboxDisabled = state;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void updateClaimCheckboxType(CCSetting_ClaimCheckboxTypes cdt) {
            try {
                var db = new CCPortalEntities();
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception e) {
                string x = e.Message;
            }
        }

        public static void deleteClaimCheckboxType(int clientID, int claimCheckboxTypeID) {
            var db = new CCPortalEntities();
            var cdt = db.CCSetting_ClaimCheckboxTypes.Find(claimCheckboxTypeID);
            if (cdt.clientID == clientID) {
                cdt.checkboxDeleted = true;
                db.Entry(cdt).State = EntityState.Modified;
                db.SaveChanges();
            }


        }
    }
    
}
