<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Este bloque vacío es porque hay un error en VisualStudio al tener una expresión condicional parece
        https://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context
    --%>
    <%="" %>

    <% if (!indice)
        { %>

    <div class="row m-4 shadow-lg p-5">
        <div class="col">
            <div class="row justify-content-end ">
                <div class="col-4 py-3 text-muted">
                    <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a>
                </div>
            </div>


            <div class="row">
                <h2 class="mb-3"><%: capitulo.Orden %>.<%: contenido.Orden %> <%: contenido.Nombre %></h2>
                <p class="text-muted px-4"><%: contenido.Tipo != null ? contenido.Tipo.Nombre : "" %></p>
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
                    <asp:Button Text="Descargar Pdf" ID="btnPdf" runat="server" OnClick="btnPdf_Click" CssClass="btn btn-dark"></asp:Button>

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
    <%}
        else
        { %>

    <%-- INDICE --%>
    <h3>Indice de <%= curso.Nombre %></h3>
    <ul class="list-group">
        <% foreach (dominio.CapituloIndice capIndice in curso.Indice.Capitulos)
            { %>
        <li class="list-group-item"><%: capIndice.Orden + ". " + capIndice.Nombre %>
            <ul class="list-group-flush">
                <% foreach (dominio.ContenidoIndice conIndice in capIndice.Contenidos)
                    { %>
                    <li class="list-group-item" >
                        <span data-bs-toggle="tooltip" data-bs-title="Visto" data-bs-placement="left">
                        <input id="contenido-<%: capIndice.Orden + "-" + conIndice.Orden %>" 
                            class="form-check-input me-1" type="checkbox" value="">
                        <label class="form-check-label" for="contenido-<%: capIndice.Orden + "-" + conIndice.Orden %>"><%: capIndice.Orden + "." + conIndice.Orden + ". " %></label> 

                        </span>
                        <a href="VerCurso.aspx?curso=<%: curso.Id %>&capitulo=<%: capIndice.Orden %>&contenido=<%: conIndice.Orden %>" 
                            class="text-muted"><%: conIndice.Nombre %></a> 
                    </li>
                <% } %>
            </ul>
        </li>
        <% } %>
    </ul>
    <%} %>


</asp:Content>
