<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="ATS.Webforms.UI.Chart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
    <asp:Chart ID="LeaveChart" runat="server" CssClass="graph bar">
        <Series>
            <asp:Series Name="Leaves" XValueType="DateTime" YValueType="Int32" XValueMember="StartDate" YValueMembers="Duration"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    </asp:Content>
