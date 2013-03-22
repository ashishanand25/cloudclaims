using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CloudClaims.Data.Portal
{
    public static class ETDW_CCClaimCheckboxes
    {
         public static List<CCClaimCheckboxValue> getClaimCheckboxValues(int clientID, int claimID) {
            var db = new CCPortalEntities();
            return db.CCClaimCheckboxValues.Where(c => c.clientID == clientID && c.claimID == claimID).ToList();
        }

        public static int addClaimCheckboxValue(int clientID, int claimID, int CheckboxTypeID, bool CheckboxValue) {
            var db = new CCPortalEntities();
            CCClaimCheckboxValue tbv = new CCClaimCheckboxValue {
                clientID = clientID,
                claimID = claimID,
                checkboxTypeID = CheckboxTypeID,
             @checked=CheckboxValue
            };
            db.CCClaimCheckboxValues.Add(tbv);
            db.SaveChanges();
            return tbv.recordID;
        }

        public static void updateClaimCheckboxValue(int clientID, int claimID, int CheckboxTypeID, bool CheckboxValue) {
            var db = new CCPortalEntities();
      
            CCClaimCheckboxValue tbv = db.CCClaimCheckboxValues.Where(c => c.clientID == clientID && c.claimID == claimID && c.checkboxTypeID == CheckboxTypeID).SingleOrDefault();
            if (tbv != null) {
                if (tbv.clientID == clientID) {
                    tbv.@checked = CheckboxValue;
                    db.Entry(tbv).State = EntityState.Modified;
                    db.SaveChanges();
                }
            } else {
                //If not found, then add a new one to this claim
                int x = addClaimCheckboxValue(clientID, claimID, CheckboxTypeID, CheckboxValue);
            }
        } 
    }
}

