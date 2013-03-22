using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;

namespace CloudClaims.Data.Portal {
    public static class ETDW_CCSetting_CustomFormSections {

        //public static int addFormSection(int clientID, int formID, string sectionName, int sortOrder = 0) {
        //    var db = new CCPortalEntities();
        //    CCSetting_CustomFormSections fs = new CCSetting_CustomFormSections {
        //        clientID = clientID,
        //        formID = formID,
        //        sectionName = sectionName,
        //        sortOrder = sortOrder
        //    };
        //    db.CCSetting_CustomFormSections.Add(fs);
        //    db.SaveChanges();
        //    return fs.sectionID;
        //}

        //public static IEnumerable<CCSetting_CustomFormSections> getFormSections(int clientId, int formID) {
        //    var db = new CCPortalEntities();
        //    return db.CCSetting_CustomFormSections.Where(c => c.clientID == clientId && c.formID == formID).OrderBy(c => c.sortOrder);
        //}
    }
}
