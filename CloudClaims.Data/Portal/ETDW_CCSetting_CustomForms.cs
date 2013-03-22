using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudClaims.Model;
using System.Data;
using CloudClaims.Data;

namespace CloudClaims.Data.Portal {
    public static class ETDW_CCSetting_CustomForms {

        public static int addCustomForm(int clientID, string formType, string formName, string serializedForm) {
            var db = new CCPortalEntities();
            CCSetting_CustomForms cf = new CCSetting_CustomForms { clientID = clientID, formType = formType, formName = formName, serializedForm = serializedForm };
            db.CCSetting_CustomForms.Add(cf);
            db.SaveChanges();
            return cf.formID;
        }

        public static IEnumerable<CCSetting_CustomForms> getAllCustomForms(int clientID) {
            var db = new CCPortalEntities();
            return db.CCSetting_CustomForms.Where(c => c.clientID == clientID);
        }

        public static CCSetting_CustomForms getCustomFormByFormID(int clientID, int formID) {
            var db = new CCPortalEntities();
            return db.CCSetting_CustomForms.Where(f => f.clientID == clientID && f.formID == formID).SingleOrDefault();
        }

        public static CCSetting_CustomForms getCustomFormByFormType(int clientID, CustomEnums.FormType formType) {
            var db = new CCPortalEntities();
            string fType = formType.ToString();
            return db.CCSetting_CustomForms.Where(f => f.clientID == clientID && f.formType == fType).SingleOrDefault();
        }

        public static void updateCustomForm(int clientID, int formID, string formName, string serializedForm) {
            var db = new CCPortalEntities();
            CCSetting_CustomForms cf = db.CCSetting_CustomForms.Where(f => f.clientID == clientID && f.formID == formID).SingleOrDefault();
            if (cf != null) {
                cf.formName = formName;
                cf.serializedForm = serializedForm;
                db.Entry(cf).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
