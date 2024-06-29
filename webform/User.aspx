<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="webform.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .rounded-circle {
    height:150px;
    width:150px;
     object-fit: cover;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                <div class="container my-4">
                      <asp:Button ID="btnEditarPerfil" runat="server" class="btn btn-primary btn-sm" Visible="false" OnClick="btnEditarPerfil_Click" Text="Editar Perfil"></asp:Button>
                       <asp:Button ID="btnGuardarPerfil" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="btnGuardarPerfil_Click" Text="Guardar Perfil" />
                       <asp:Button ID="btnEditarLogin" runat="server" CssClass="btn btn-outline-primary btn-sm" OnClick="btnEditarLogin_Click" Text="Cambiar contraseña" />
                        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
                </div>

    <div class="container mt-4">
        <div class="row">
            <div class="col-lg-4 d-flex">
                <div class="card mb-4 w-100 d-flex justify-content-center">
                    <div class="card-body text-center">
                        <div>
                            <asp:Image ID="ImgAvatar" runat="server" CssClass="rounded-circle" />
                        </div>
                        <div>
                        <asp:FileUpload ID="FiCambiarImagen" CssClass="form-control form-control-sml mt-4" type="file" Visible="false" placeholder="Cambiar Imagen" runat="server" />
                        </div>
                        <asp:Label ID="LblUsername1" runat="server" CssClass="my-3" Style="font-size: 1.25rem; font-weight: 500;"></asp:Label>
                        <asp:Label ID="lblProfesion1" runat="server" CssClass="text-muted mb-1" Style="display: block;"></asp:Label>
                        <asp:Label ID="lblUbicacion1" runat="server" CssClass="text-muted mb-4" Style="display: block;"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-lg-8 d-flex">
                <div class="card mb-4 w-100">
                    <div class="card-body d-flex flex-column justify-content-center">

                                                <!-- Panel ver perfil -->

                        <asp:Panel ID="PanelVerPerfil" runat="server">
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre de usuario</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblVerUsername" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre y apellido</p>
                            </div>
                            <div class="col-sm-9">
                                 <asp:Label ID="LblVerNombreApellido" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Email</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblVerEmail" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Profesion</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="LblVerProfesion" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Ubicacion</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="LblVerUbicacion" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                            </div>
                            </asp:Panel>

                        <!-- Panel editar perfil -->

                         <asp:Panel ID="PanelEditarPerfil" runat="server" Visible="false">

                         <div class="row mb-3">
                             <div class="col-sm-3">
                                 <p class="mb-0">Nombre de usuario</p>
                             </div>
                             <div class="col-sm-9">
                                 <asp:TextBox ID="txtEditarUsername" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                             </div>
                         </div>
                         <hr>
                         <div class="row mb-3">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtEditarNombre" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-sm-3">
                                <p class="mb-0">Apellido</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtEditarApellido" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                            </div>
                        </div>

                         <hr>
                         <div class="row">
                             <div class="col-sm-3">
                                 <p class="mb-0">Profesion</p>
                             </div>
                             <div class="col-sm-9">
                                 <asp:TextBox ID="txtEditarProfesion" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                             </div>
                         </div>
                         <hr>
                         <div class="row mb-3">
                            <div class="col-sm-3">
                                <p class="mb-0">Provincia/Departamento</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtEditarProvincia" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-sm-3">
                                <p class="mb-0">Pais</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtEditarPais" runat="server" CssClass="form-control text-muted"></asp:TextBox>
                            </div>
                        </div>
                             </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            







</asp:Content>
