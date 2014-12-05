using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Core.Domain
{
    public static class AppConstants
    {
        // roles
        public const string Role_Admin = "Admin";
        public const string Role_Author = "Author";

        // tempdata keys
        public const string TempData_Key_MessageForView = "MessageForView";
        public const string TempData_Key_EmailConfirm = "EmailConfirm";

        // editor template names
        public const string EditorDisplayTemplate_HttpPostedFileBase = "_HttpPostedFileBase";
        public const string EditorDisplayTemplate_CKEditorTextArea = "_CKEditorTextArea";
        public const string EditorDisplayTemplate_AutoCompleteListBox = "_AutoCompleteListBox";
        public const string EditorDisplayTemplate_AutoCompleteDropDownList = "_AutoCompleteDropDownList";
    }
}
