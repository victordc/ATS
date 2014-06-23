<%@ Page Language="C#" MasterPageFile="~/Webforms/Site.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="ATS.Webforms.UI.Chart" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="head">
  <script src="../Scripts/jquery-1.7.1.min.js"></script>
  <script src="../Scripts/raphael-min.js"></script>
  <script src="../Scripts/morris.js"></script>
  <script src="../Scripts/prettify.min.js"></script>
  <script src="../Scripts/chart.js"></script>
  <link href="../Content/chart.css" rel="stylesheet" />
  <link href="../Content/prettify.min.css" rel="stylesheet" />
  <link href="../Content/morris.css" rel="stylesheet" />
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="body">
<h3>Time Events</h3>
<div id="graph"></div>
<pre id="code" class="prettyprint linenums">
var week_data = [
  {"period": "2014-06-11", "Annual": 7, "MC": 0},
  {"period": "2014-06-12", "Annual": 0, "MC": 0},
  {"period": "2014-06-13", "Annual": 0, "MC": 0},
  {"period": "2014-06-14", "Annual": 0, "MC": 0},
  {"period": "2014-06-15", "Annual": 0, "MC": 0},
  {"period": "2014-06-16", "Annual": 0, "MC": 0},
  {"period": "2014-06-17", "Annual": 0, "MC": 0},
  {"period": "2014-06-18", "Annual": 0, "MC": 0},
  {"period": "2014-06-19", "Annual": 0, "MC": 4},
  {"period": "2014-06-20", "Annual": 0, "MC": 0},
];
Morris.Line({
  element: 'graph',
  data: week_data,
  xkey: 'period',
  ykeys: ['Annual', 'MC'],
  labels: ['Annual', 'MC'],
  events: [
    '2014-06-01',
    '2014-06-30'
  ]
});
</pre>
    </asp:Content>
