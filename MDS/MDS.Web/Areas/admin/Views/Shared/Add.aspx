<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/admin.Master" Inherits="System.Web.Mvc.ViewPage<MDS.Model.Model>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    发布文章
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <% using (Html.BeginForm())
       { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>发布文章</legend>
        <%: Html.EditorForModel() %>
        <p>
            <input type="submit" value="添加" />
        </p>
    </fieldset>
    <% } %>

    <div>
        <%: Html.ActionLink("返回列表", "List") %>
    </div>

</asp:Content>
