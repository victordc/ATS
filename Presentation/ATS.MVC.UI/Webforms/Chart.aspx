<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="ATS.Webforms.UI.Chart" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../Scripts/raphael-min.js"></script>
    <script src="../Scripts/morris.js"></script>
    <script src="../Scripts/chart.js"></script>
    <link href="../Content/chart.css" rel="stylesheet" />
    <link href="../Content/morris.css" rel="stylesheet" />
    <div id="graph"></div>
    <script type="text/javascript">
        var newHistory = <%=JsonResult %>;
        Morris.Line({
            element: 'graph',
            data: newHistory,
            xkey: 'StartDate',
            ykeys: ['MC','Annual','Exams'],
            labels: ['MC','Annual','Exams']
        });
    </script>
</asp:Content>
