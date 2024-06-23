<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModeracionDenuncias.aspx.cs" Inherits="webform.ModeracionDenuncias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="my-5">Moderación de denuncias</h1>


    <%-- Denuncias Cursos --%>
    <div class="row">
        <div class="col">

            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3 text-white bg-primary">
                    <h4 class="my-0 fw-normal">En Cursos</h4>
                </div>
                <div class="card-body">
                    <h1 class="card-title pricing-card-title">Pendientes: <%: denunciasCursosPendientes %></h1>
                    <ul class="list-unstyled mt-3 mb-4">
                        <li><%: denunciasCursosResueltas %> Resueltas</li>
                        <li><%: denunciasCursosResueltas + denunciasCursosPendientes %> Totales</li>
                    </ul>
                    <a href="ModeracionDenunciasCursos.aspx" class="w-100 btn btn-lg btn-outline-primary">Moderar</a>
                </div>
            </div>
        </div>

        <%-- Denuncias Reseñas --%>
        <div class="col">

            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3 text-white bg-success">
                    <h4 class="my-0 fw-normal">En Reseñas</h4>
                </div>
                <div class="card-body">
                    <h1 class="card-title pricing-card-title">Pendientes: <%: denunciasReseniasPendientes %></h1>
                    <ul class="list-unstyled mt-3 mb-4">
                        <li><%: denunciasReseniasResueltas %> Resueltas</li>
                        <li><%: denunciasReseniasResueltas + denunciasReseniasPendientes %> Totales</li>
                    </ul>
                    <a href="ModeracionDenunciasResenas.aspx" class="w-100 btn btn-lg btn-outline-success">Moderar</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
