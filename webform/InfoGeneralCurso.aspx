<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="InfoGeneralCurso.aspx.cs" Inherits="webform.InfoGeneralCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-title{
            height:150px;
            border:1px solid #800080;
            border-radius: 5px;
            box-shadow: 2px 2px 10px 2px #bcbdb5;
        }
        .mb-3{
            box-shadow: 2px 2px 10px 2px darkgrey;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="modal-title" style="display: flex; justify-content: center; align-items: center; margin: 20px; font-size: 30px; color: dimgrey;">
            <asp:Label ID="lblTituloCurso" Text="Sin titulo" runat="server" />
        </div>
        <div style="display:flex; justify-content:space-around; flex-wrap: wrap; align-items: center;">
            <div class="card border-primary mb-3" style="width: 26%; margin:5px;">
                <div class="card-header text-primary" style="text-align:center;">CANTIDAD de CAPITULOS</div>
                <div class="card-body text-primary" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCantCapitulos" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-secondary mb-3" style="width: 26%; margin:5px;">
                <div class="card-header text-secondary" style="text-align:center;">CANTIDAD de CONTENIDOS</div>
                <div class="card-body text-secondary" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCantContenidos" Text="null" runat="server" />
                    </h1>
                    
                </div>
            </div>
            <div class="card border-success mb-3" style="width: 26%; margin:5px;">
                <div class="card-header text-success" style="text-align:center;">CANTIDAD de INSCRIPTOS</div>
                <div class="card-body text-success" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCantInscriptos" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-danger mb-3" style="width: 20%; margin:10px;">
                <div class="card-header text-danger" style="text-align:center;">CANTIDAD de RESEÑAS</div>
                <div class="card-body text-danger" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCantResenias" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-warning mb-3" style="width: 20%; margin:10px;">
                <div class="card-header text-warning" style="text-align:center;">CANTIDAD de COMENTARIOS</div>
                <div class="card-body text-warning" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCantComentarios" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-info mb-3" style="width: 20%; margin:10px;">
                <div class="card-header text-info" style="text-align:center;">CALIFICACION del CURSO</div>
                <div class="card-body text-info" style="text-align:center;">
                    <h1>
                        <asp:Label ID="lblCalificacion" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <!--
            <div class="card border-light mb-3" style="width: 20%; margin-left:10px;">
                <div class="card-header">Header</div>
                <div class="card-body">
                    <h5 class="card-title">Light card title</h5>
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                </div>
            </div>
            <div class="card border-dark mb-3" style="width: 20%; margin-left:10px;">
                <div class="card-header">Header</div>
                <div class="card-body">
                    <h5 class="card-title">Dark card title</h5>
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                </div>
            </div>
            -->
        </div>
    </div>
</asp:Content>
