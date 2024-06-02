<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Ver Curso</h1>
    <p>Id Curso <%: curso.Id %></p>
    <p>Id Capitulo <%: capitulo.Id %></p>
    <p>Id Contenido <%: contenido.Id %></p>
</asp:Content>
