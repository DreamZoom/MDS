<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    我的博客
</asp:Content>


<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .blog-post {
            margin-bottom: 60px;
        }

        .blog-post-title {
            margin-bottom: 5px;
            font-size: 40px;
        }

        .blog-post-meta {
            margin-bottom: 20px;
            color: #999;
        }
    </style>
    <%foreach (var art in ViewBag.Articles)
      { %>
    <div class="blog-post">
        <h2 class="blog-post-title"><%: art.Title%></h2>
        <p class="blog-post-meta"><%: art.PostTime%> <span></span><span>阅读(<%: art.Reads%>)</span></p>
        <div class="blog-post-body">

            <%: art.Content%>
        </div>
    </div>
    <%} %>
</asp:Content>
