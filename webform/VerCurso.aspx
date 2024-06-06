<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row m-4 shadow-lg p-5">
        <div class="col">
            <div class="row justify-content-end ">
                <div class="col-4 py-3 text-muted">
                    <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a>
                </div>
            </div>


            <div class="row">
                <h2 class="mb-3"><%: capitulo.Orden %>.<%: contenido.Orden %> <%: contenido.Nombre %></h2>
                <p class="text-muted px-4"><%: contenido.Tipo.Nombre %></p>
                <p class="px-5 py-3" style="white-space: pre-line;"><%: contenido.Texto %></p>
            </div>

            <% if (!string.IsNullOrEmpty(contenido.UrlVideo))
                { %>
            <%-- VIDEO (si hay Url) --%>
            <div class="row p-3" style="height: 70vh;">

                <iframe src="<%: contenido.UrlVideo %>"></iframe>

            </div>
            <% }%>

            <% if (contenido.Archivo != null)

                { %>
            <%-- PDF (si hay archivo) --%>
            <div class="row justify-content-center">
                <div class="col-4 d-flex justify-content-center">
                    <asp:Button Text="Descargar Pdf" ID="btnPdf" runat="server" OnClick="btnPdf_Click" CssClass="btn btn-dark">
                    </asp:Button>

                </div>


            </div>
            <% } %>

            <div class="row p-5">
                <div class="col">
                    <% if (!string.IsNullOrEmpty(urlAnterior))
                        { %>

                    <a href="<%: urlAnterior %>" class="btn btn-secondary"><- <%: contenidoAnterior.Nombre %></a>
                    <% } %>
                </div>
                <div class="col d-flex justify-content-end">
                    <% if (!string.IsNullOrEmpty(urlSiguiente))
                        { %>
                    <a href="<%: urlSiguiente %>" class="btn btn-secondary"><%: contenidoSiguiente.Nombre %> -></a>
                    <% } %>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
