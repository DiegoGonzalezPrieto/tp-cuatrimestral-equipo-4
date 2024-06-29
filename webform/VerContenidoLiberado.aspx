<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerContenidoLiberado.aspx.cs" Inherits="webform.VerContenidoLiberado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row m-4 shadow-lg p-4">
        <div class="container text-end">
            <a href="DetallesCurso.aspx?id=<%: curso.Id %>" class="btn btn-primary">Inscribirse</a>
        </div>
        <div class="col">
            <div class="row justify-content-between ">
                <div class="col-4 py-3 text-muted">
                    <a class="text-muted text-decoration-none" href="VerCursoLiberado.aspx?id=<%: curso.Id %>"><span>📜</span> Contenidos Liberados</a>
                </div>
                <div class="col-4 py-3 text-muted">
                    <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a>
                </div>
            </div>




            <div class="row p-4">
                <h2 class="my-3"><%: capitulo.Orden %>.<%: contenido.Orden %> <%: contenido.Nombre %> <span class="badge rounded-pill bg-success">Liberado</span></h2>
                <p class="text-muted px-4"><%: contenido.Tipo != null ? contenido.Tipo.Nombre : "" %></p>
                <p class="px-5 py-3" style="white-space: pre-line;"><%: contenido.Texto %></p>
            </div>

            <% if (!string.IsNullOrEmpty(contenido.UrlVideo))
                { %>
            <%-- VIDEO (si hay Url) --%>
            <div class="row p-4" style="height: 70vh;">

                <iframe src="<%: contenido.UrlVideo %>"></iframe>

            </div>
            <% }%>

            <% if (contenido.Archivo != null)

                { %>
            <%-- PDF (si hay archivo) --%>

            <div class="row justify-content-center">
                <div class="col-4 d-flex justify-content-center">
                    <asp:Button Text="Descargar Pdf" ID="btnPdf" runat="server" OnClick="btnPdf_Click" CssClass="btn btn-dark"></asp:Button>
                </div>
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
