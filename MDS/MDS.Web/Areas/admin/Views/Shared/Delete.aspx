<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/admin.Master" Inherits="System.Web.Mvc.ViewPage<MDS.Model.Model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<h3 class="alert alert-warning">您确定要删除这篇文章吗?</h3>
<fieldset>
    <legend>删除文章</legend>
    <%:Html.DisplayForModel() %>
</fieldset>
<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <p>
        <input type="submit" value="删除" /> |
        <%: Html.ActionLink("返回列表", "list") %>
    </p>
<% } %>

</asp:Content>
