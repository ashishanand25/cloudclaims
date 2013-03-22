using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmStickyFooter {
        public bool pickLayout { get; set; }
        public bool addRow { get; set; }
        public bool personPicker { get; set; }
        public bool textboxPicker { get; set; }
        public bool dropdownPicker { get; set; }
        public bool dateFieldPicker { get; set; }
        public bool checkboxPicker { get; set; }
        public bool personnelPicker { get; set; }

        public vmStickyFooter() {
            pickLayout = false;
            addRow = false;
            personPicker = false;
            textboxPicker = false;
            dropdownPicker = false;
            dateFieldPicker = false;
            checkboxPicker = false;
            personnelPicker = false;
        }
    
    }
}