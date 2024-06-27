<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="InfoGeneralCurso.aspx.cs" Inherits="webform.InfoGeneralCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-title {
            height: 150px;
            border: 1px solid #800080;
            border-radius: 5px;
            box-shadow: 2px 2px 10px 2px #bcbdb5;
        }

        .mb-3 {
            box-shadow: 2px 2px 10px 2px darkgrey;
        }

        .profile-circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }

        .table-striped td, .table-striped th {
            text-align: center;
            vertical-align: middle;
        }

        .table-striped progress {
            display: block;
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="modal-title" style="display: flex; justify-content: center; align-items: center; margin: 20px; font-size: 30px; color: dimgrey;">
            <asp:Label ID="lblTituloCurso" Text="Sin titulo" runat="server" />
        </div>
        <div style="display: flex; justify-content: space-around; flex-wrap: wrap; align-items: center;">
            <div class="card border-primary mb-3" style="width: 26%; margin: 5px;">
                <div class="card-header text-primary" style="text-align: center;">CANTIDAD de CAPITULOS</div>
                <div class="card-body text-primary" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCantCapitulos" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-secondary mb-3" style="width: 26%; margin: 5px;">
                <div class="card-header text-secondary" style="text-align: center;">CANTIDAD de CONTENIDOS</div>
                <div class="card-body text-secondary" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCantContenidos" Text="null" runat="server" />
                    </h1>

                </div>
            </div>
            <div class="card border-success mb-3" style="width: 26%; margin: 5px;">
                <div class="card-header text-success" style="text-align: center;">CANTIDAD de INSCRIPTOS</div>
                <div class="card-body text-success" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCantInscriptos" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-danger mb-3" style="width: 20%; margin: 10px;">
                <div class="card-header text-danger" style="text-align: center;">CANTIDAD de RESEÑAS</div>
                <div class="card-body text-danger" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCantResenias" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-warning mb-3" style="width: 20%; margin: 10px;">
                <div class="card-header text-warning" style="text-align: center;">CANTIDAD de COMENTARIOS</div>
                <div class="card-body text-warning" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCantComentarios" Text="null" runat="server" />
                    </h1>
                </div>
            </div>
            <div class="card border-info mb-3" style="width: 20%; margin: 10px;">
                <div class="card-header text-info" style="text-align: center;">CALIFICACION del CURSO</div>
                <div class="card-body text-info" style="text-align: center;">
                    <h1>
                        <asp:Label ID="lblCalificacion" Text="null" runat="server" />
                    </h1>
                </div>
            </div>

        </div>
        <div class="mt-5">
            <asp:GridView ID="gvUsuariosInscriptos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                <Columns>
                    <asp:TemplateField HeaderText="Foto de Perfil">
                        <HeaderStyle Width="15%" />
                        <ItemStyle Width="15%" />
                        <ItemTemplate>
                            <asp:Image ID="imgFotoPerfil" runat="server" ImageUrl='<%# string.IsNullOrEmpty((string)Eval("UrlFotoPerfil")) ? "Media/Usuario.png" : Eval("UrlFotoPerfil")%>' CssClass="profile-circle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# (bool)Eval("Estado") ? "Activo" : "Fuega de Linea" %>' CssClass='<%# Eval("Estado").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Progreso del curso">
                        <ItemTemplate>
                            <progress max="100"></progress>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="mt-5" style="display: flex; justify-content: space-around; align-items: center;">
            <asp:Button ID="btnVolver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" runat="server" Text="Volver" />
        </div>
    </div>
</asp:Content>
