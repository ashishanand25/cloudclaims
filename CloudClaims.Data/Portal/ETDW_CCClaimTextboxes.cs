using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal {
    public static class ETDW_CCClaimTextboxes {
        
        public static List<CCClaimTextboxValue> getClaimTextboxValues(int clientID, int claimID) {
            var db = new CCPortalEntities();
            return db.CCClaimTextboxValues.Where(c => c.clientID == clientID && c.claimID == claimID).ToList();
        }

        public static int addClaimTextboxValue(int clientID, int claimID, int textboxTypeID, string textboxValue) {
            var db = new CCPortalEntities();
            CCClaimTextboxValue tbv = new CCClaimTextboxValue {
                clientID = clientID,
                claimID = claimID,
                textboxTypeID = textboxTypeID,
                textboxValue = textboxValue
            };
            db.CCClaimTextboxValues.Add(tbv);
            db.SaveChanges();
            return tbv.recordID;
        }

        public static void updateClaimTextboxValue(int clientID, int claimID, int textboxTypeID, string textboxValue) {
            var db = new CCPortalEntities();
            CCClaimTextboxValue tbv = db.CCClaimTextboxValues.Where(c => c.clientID == clientID && c.claimID == claimID && c.textboxTypeID == textboxTypeID).SingleOrDefault();
            if (tbv != null) {
                if (tbv.clientID == clientID) {
                    tbv.textboxValue = textboxValue;
                    db.Entry(tbv).State = EntityState.Modified;
                    db.SaveChanges();
                }
            } else {
                //If not found, then add a new one to this claim
                int x = addClaimTextboxValue(clientID, claimID, textboxTypeID, textboxValue);
            }
        }
    }
}
