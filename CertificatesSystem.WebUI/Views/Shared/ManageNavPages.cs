using Microsoft.AspNetCore.Mvc.Rendering;

namespace CertificatesSystem.WebUI.Views.Shared
{
    public static class ManageNavPages
    {
        public static string Home => "Home";

        public static string Certificates => "Certificates";
    
        public static string Enrollment => "Enrollment";
    
        public static string StudentsList => "StudentsList";

        public static string ManagersList => "ManagersList";

        public static string GradesList => "GradesList";

        public static string UsersList => "UsersList";


        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);
    
        public static string CertificatesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Certificates);
    
        public static string EnrollmentNavClass(ViewContext viewContext) => PageNavClass(viewContext, Enrollment);
    
        public static string StudentsListNavClass(ViewContext viewContext) => PageNavClass(viewContext, StudentsList);

        public static string ManagersListNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManagersList);
    
        public static string GradesListNavClass(ViewContext viewContext) => PageNavClass(viewContext, GradesList);

        public static string UsersListNavClass(ViewContext viewContext) => PageNavClass(viewContext, UsersList);


        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "";
        }
    }
}