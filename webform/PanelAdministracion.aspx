<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PanelAdministracion.aspx.cs" Inherits="webform.PanelAdministracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .col {
            display: grid;
            justify-content: center;
        }

        .img {
            display: flex;
            justify-content: center;
            margin-bottom:5px;
        }
        #contentPrincipal{
            border: 2px solid grey;
            box-shadow: 5px 5px 5px 5px darkgray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; align-items: center; margin: 25px;">
        <h1 class="m-5">Panel de Administración</h1>
    </div>

    <div class="row p-5" id="contentPrincipal">
        <div class="col">
            <div class="img">
                <img src="Media/categorias.png" alt="AdmCategorias" />
            </div>
            <div>
                <a href="AdministracionCategorias.aspx" class="btn btn-info">Adm. de Categorías</a>
            </div>
        </div>
        <div class="col">
            <div class="img">
                <img src="Media/moderador.png" alt="ModeracionDenuncias" />
            </div>
            <div>
                <a href="ModeracionDenuncias.aspx" class="btn btn-danger">Moderación de Denuncias</a>
            </div>
        </div>
        <div class="col">
            <div class="img">
                <img src="Media/gestion-de-equipos.png" alt="AdmUsuarios" />
            </div>
            <div>
                <a href="AdministrarUsuarios.aspx" class="btn btn-warning">Adm. de Usuarios</a>
            </div>
        </div>
    </div>
</asp:Content>
