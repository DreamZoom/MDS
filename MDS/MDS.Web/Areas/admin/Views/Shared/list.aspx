<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MDS.Model.Model>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文章列表
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>文章列表</h2>


    <table class="table">
        <tr>
            <th>标题</th>
            <th>发布时间</th>
            <th>阅读次数</th>
            <th>操作</th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <td><%:item.GetValue("Title") %></td>
            <td><%:item.GetValue("PostTime") %></td>
            <td><%:item.GetValue("Reads") %></td>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { id=item.GetValue("ID")}) %> |
           
            <%: Html.ActionLink("删除", "Delete", new { id=item.GetValue("ID") }) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%: Html.ActionLink("发布文章", "Add", new { }, new { @class="btn btn-success" })%>
    </p>
</asp:Content>
