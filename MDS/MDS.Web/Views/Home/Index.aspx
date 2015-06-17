<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    我的博客
</asp:Content>


<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <%foreach (var art in ViewBag.Articles)
      { %>
    <div class="blog-post">
        <h3 class="blog-post-title"><%: art.Title%></h3>
        <p class="blog-post-meta"><%: art.PostTime%> <span></span><span>阅读(<%: art.Reads%>)</span></p>
        <div class="blog-post-body">

            <%: art.Content%>
        </div>
    </div>
    <%} %>
</asp:Content>
