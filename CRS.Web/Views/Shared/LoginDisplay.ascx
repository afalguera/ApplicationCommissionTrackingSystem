@{ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" }
@{
    if (Request.IsAuthenticated) 
    {

        Welcome <strong> @Page.User.Identity.Name </strong>!
        [ @Html.ActionLink("Logout", "Logout", "Account") ]

    }
    else 
    {
 
        [ @Html.ActionLink("LogIn", "Login", "Account") ]
    }
}
