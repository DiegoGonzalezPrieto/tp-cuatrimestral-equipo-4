<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row m-4 shadow-lg p-4">
        <div class="col">
            <div class="row justify-content-end ">
                <div class="col-4 py-3 text-muted">
                    <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a>
                </div>
            </div>
            <div class="row">
                <h2 class="mb-3"><%: capitulo.Orden %>.<%: contenido.Orden %> <%: contenido.Nombre %></h2>
                <p class="p-5"><%: contenido.Texto %></p>
            </div>
            <div class="row">
                <%-- Contenido (video, enlace a pdf) --%>
            </div>

            <div class="row">
                <div class="col">
                    <% if (!string.IsNullOrEmpty(urlAnterior))
                        { %>

                    <a href="<%: urlAnterior %>" class="btn btn-secondary"><- <%: contenidoAnterior.Nombre %></a>
                    <% } %>
                </div>
                <div class="col d-flex justify-content-end">
                    <% if (!string.IsNullOrEmpty(urlSiguiente))
                        { %>
                    <a href="<%: urlSiguiente %>" class="btn btn-secondary"> <%: contenidoSiguiente.Nombre %> -></a>
                    <% } %>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
