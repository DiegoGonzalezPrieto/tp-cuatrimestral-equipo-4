<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PanelAdministracion.aspx.cs" Inherits="webform.PanelAdministracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="m-5">Panel de Administración</h1>
    <div class="row p-5">
        <div class="col">
            <a href="AdministracionCategorias.aspx" class="btn btn-info">Adm. de Categorías</a>
        </div>
        <div class="col">
            <a href="AdministrarDenuncias.aspx" class="btn btn-danger">Moderación de Denuncias</a>
        </div>
        <div class="col">
            <a href="AdministrarUsuarios.aspx" class="btn btn-warning">Adm. de Usuarios</a>
        </div>
    </div>
</asp:Content>
