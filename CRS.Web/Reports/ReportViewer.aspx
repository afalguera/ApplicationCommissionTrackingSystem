<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="MvcApplication1.Reports.ReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server"  style="width:100%; height:100%; text-align:center">
      <div  align="center"> 
       
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer id="ACTSReportViewer" runat="server" Width="100%" BackColor="#FFFBF7"
                            BorderWidth="0px" ShowFindControls="false" Height="100%" ShowBackButton="true" SizeToReportContent="true"
                            ShowPromptAreaButton="false" AsyncRendering="false"></rsweb:ReportViewer>
          
        </div>
    </form> 
</body>
</html>
