using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudClaims.Data {
    public static class StringExtensions {
        public static T ConvertToEnum<T>(this string value) {
            if (typeof(T).BaseType != typeof(Enum)) {
                throw new InvalidCastException();
            }
            if (Enum.IsDefined(typeof(T), value) == false) {
                throw new InvalidCastException();
            }
            return (T)Enum.Parse(typeof(T), value);
        }
    }

    public class CustomEnums {
        public const string CCSetting_ClaimCheckboxTypes = "checkbox";
        public const string CCSetting_ClaimTextboxTypes = "textbox";
        public const string CCSetting_ClaimDropdownTypes = "dropdown";
        public const string CCSetting_ClaimDateTypes = "datefield";
        public const string CCSetting_ClaimPersonnelTypes = "personnel";

        public enum FormType {
            FORMTYPE_CLAIM,
            FORMTYPE_APPRAISAL,
            FORMTYPE_CLAIM_ATTACHMENT,
            FORMTYPE_REVENUES
        }

        public enum NotificationType {
            NONE,
            WARNING,
            INFORMATION,
            SUCCESS,
            FAILURE,
            VALIDATION_ERROR
        }

        public enum GenericContactCardType {
            CLOUD_CLAIMS_CLIENT = 1,
            USER = 2,
            CLAIM_PERSONNEL = 3
        }

        public enum PersonType {
            INTERNAL = 1,           //Internal to your own company, your own staff
            EXTERNAL = 2,           //A client external to your own company. Must set an organization this person belongs to first, to add an external user
            ONTHEFLY = 3            //Random just for recond keeping
        }

        //public enum ClaimMediaType {
        //    PHOTO,
        //    VIDEO,
        //    DOCUMENT
        //}

        public const string MEDIATYPE_PHOTO = "PIC";
        public const string MEDIATYPE_VIDEO = "VID";
        public const string MEDIATYPE_DOCUMENT = "DOC";
        
    }
}
