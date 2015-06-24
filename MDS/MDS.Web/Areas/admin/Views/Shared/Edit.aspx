<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/admin.Master" Inherits="System.Web.Mvc.ViewPage<MDS.Model.Model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    修改文章
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>修改文章</legend>
        <%: Html.EditModel() %>
        <p>
            <input type="submit" value="编辑" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("返回列表", "List") %>
</div>

</asp:Content>
