<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="webform.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .title {
            height: 150px;
        }

        .subtitle {
            height: 100px;
        }

        .profile-circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title" style="display: flex; justify-content: center; align-items: center; color: dimgrey;">
        <h2>
            <asp:Label Text="Panel de Administracion de General" runat="server" />
        </h2>
    </div>
    <div class="container mt-5">
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <a class="nav-link active" id="usuarios-tab" data-bs-toggle="tab" href="#usuarios" role="tab" aria-controls="usuarios" aria-selected="true">Usuarios</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="cursos-tab" data-bs-toggle="tab" href="#cursos" role="tab" aria-controls="cursos" aria-selected="false">Cursos</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="estadisticas-tab" data-bs-toggle="tab" href="#estadisticas" role="tab" aria-controls="estadisticas" aria-selected="false">Estadísticas</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="administradores-tab" data-bs-toggle="tab" href="#administradores" role="tab" aria-controls="administradores" aria-selected="false">Administradores</a>
            </li>
        </ul>

        <div class="tab-content mt-3" id="myTabContent">
            <div class="tab-pane fade show active" id="usuarios" role="tabpanel" aria-labelledby="usuarios-tab">
                <div class="row">
                    <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: cadetblue;">
                        <h1>Administracion Usuarios</h1>
                    </div>

                    <div>
                        <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Foto de Perfil">
                                    <ItemTemplate>
                                        <asp:Image ID="imgFotoPerfil" runat="server" ImageUrl='<%# Eval("UrlFotoPerfil") %>' CssClass="profile-circle" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" />
                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# (bool)Eval("Estado") ? "Activo" : "Fuega de Linea" %>' CssClass='<%# Eval("Estado").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="cursos" role="tabpanel" aria-labelledby="cursos-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: lightslategrey;">
                    <h1>Administracion Cursos</h1>
                </div>
            </div>
            <div class="tab-pane fade" id="estadisticas" role="tabpanel" aria-labelledby="estadisticas-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: darkcyan;">
                    <h1>Datos Estadisticos</h1>
                </div>
            </div>
            <div class="tab-pane fade" id="administradores" role="tabpanel" aria-labelledby="administradores-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: cornflowerblue;">
                    <h1>Administradores</h1>
                </div>
                <div>
                    <asp:GridView ID="gvAdmin" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Foto de Perfil">
                                <ItemTemplate>
                                    <asp:Image ID="imgFotoPerfil" runat="server" ImageUrl='<%# Eval("UrlFotoPerfil") %>' CssClass="profile-circle" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" />
                            <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# (bool)Eval("Estado") ? "Activo" : "Fuega de Linea" %>' CssClass='<%# Eval("Estado").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
