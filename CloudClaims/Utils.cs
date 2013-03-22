using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims {
    public class Utils {
        public static string ServerSideValidation(ModelStateDictionary ModelState) {
            string errorMessage = "";
            errorMessage = "<div class=\"\">"
                                + "The following errors occurred in your submission:<ul>";
            foreach (var key in ModelState.Keys) {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null) {
                    errorMessage += "<li class=\"\">" + error.ErrorMessage + "</li>";

                }
            }
            errorMessage += "</ul>";
            return errorMessage;
        
        }

        public static string MinDateReplace(DateTime d) {
            if (d.ToString() == "0001-01-01 00:00:00.0000000")
                return String.Empty;
            else return d.ToString();
        }

        //Removes non-numeric characters from a string and returns the numbers in it (dropdownID-23 => 23)
        public static string RemoveNonNumericChars(string input) {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(input, String.Empty);
        }
    }
}