<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-end ">
        <div class="col-4 py-3 text-muted">
            <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a> 
        </div>

    </div>
    <h2>Capítulo <%: capitulo.Orden %> : <%: capitulo.Nombre %></h2>
    <p>Id Curso: <%: curso.Id %></p>
    <p>N° Contenido: <%: contenido.Orden %></p>
</asp:Content>
