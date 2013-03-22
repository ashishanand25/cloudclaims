using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;

namespace CloudClaims.Data.Portal {
    public static class EverythingToDoWith_CCSetting_ClaimDropdownValues {

        public static List<CCSetting_ClaimDropdownValues> getClaimDropdownValues(int clientID, int dropdownTypeID) {
            var db = new CCPortalEntities();
            return db.CCSetting_ClaimDropdownValues.Where(c => c.clientID == clientID && c.claimDropdownTypeID == dropdownTypeID).ToList();
        }

        public static int addClaimDropdownValue(int clientID, int dropdownTypeID, string ddValueItem) {
            var db = new CCPortalEntities();
            CCSetting_ClaimDropdownValues cdv = new CCSetting_ClaimDropdownValues {
                clientID = clientID,
                claimDropdownTypeID = dropdownTypeID,
                ddValueItem = ddValueItem,
                sortOrder = 0,
                itemDisabled = false,
                itemDeleted = false
            };
            db.CCSetting_ClaimDropdownValues.Add(cdv);
            db.SaveChanges();
            return cdv.claimDropdownTypeID;
        }
    }
}
