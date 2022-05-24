using Microsoft.AspNetCore.Mvc.Rendering;

namespace CertificatesSystem.WebUI.Views.Shared;

public static class ManageNavPages
{
    public static string Home => "Home";

    public static string RejectedSuggestions => "RejectedSuggestions";

    public static string CategoriesList => "CategoriesList";

    public static string StatesList => "StatesList";

    public static string UsersList => "UsersList";


    public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);

    public static string RejectedSuggestionsNavClass(ViewContext viewContext) => PageNavClass(viewContext, RejectedSuggestions);

    public static string CategoriesListNavClass(ViewContext viewContext) => PageNavClass(viewContext, CategoriesList);

    public static string StatesListNavClass(ViewContext viewContext) => PageNavClass(viewContext, StatesList);

    public static string UsersListNavClass(ViewContext viewContext) => PageNavClass(viewContext, UsersList);


    private static string PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
                         ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "";
    }
}