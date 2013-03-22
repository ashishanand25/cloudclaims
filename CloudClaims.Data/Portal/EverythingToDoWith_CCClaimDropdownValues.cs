using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCClaimDropdownValues {
        public static IEnumerable<CCClaimDropdownValue> getAllClaimDropdownValues(int clientID, int claimID) {
            var db = new CCPortalEntities();
            return db.CCClaimDropdownValues.Where(c => c.clientID == clientID && c.claimID == claimID);
        }
        
        public static int addClaimDropdownValue(int clientID, int claimID, int dropdownTypeID, int dropdownValueID) {
            var db = new CCPortalEntities();
            CCClaimDropdownValue cdv = new CCClaimDropdownValue {
                clientID = clientID,
                claimID = claimID,
                dropdownTypeID = dropdownTypeID,
                dropdownValueID = dropdownValueID
            };
            db.CCClaimDropdownValues.Add(cdv);
            db.SaveChanges();
            return cdv.recordID;
        }

        public static void updateClaimDropdownValue(int clientID, int claimID, int dropdownTypeID, int dropdownValueID) {
            var db = new CCPortalEntities();
            CCClaimDropdownValue cdv = db.CCClaimDropdownValues.Where(d => d.clientID == clientID && d.claimID == claimID && d.dropdownTypeID == dropdownTypeID).SingleOrDefault();
            if (cdv != null) {
                cdv.dropdownValueID = dropdownValueID;
                db.Entry(cdv).State = EntityState.Modified;
                db.SaveChanges();
            } else {
                //The user thinks he is updating - but if it never existed in the database, let's create it now
                int newID = addClaimDropdownValue(clientID, claimID, dropdownTypeID, dropdownValueID);
            }
        }
    }
}
