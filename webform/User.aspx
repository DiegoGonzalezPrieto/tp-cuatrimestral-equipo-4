﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="webform.User" %>
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
                       <asp:Button ID="btnEditarLogin" runat="server" CssClass="btn btn-outline-primary btn-sm" OnClick="btnEditarLogin_Click" Text="Editar datos de login" />
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
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre de usuario</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblUsuario2" runat="server" CssClass="text-muted"></asp:Label>
                                <asp:TextBox ID="txtUsuario2" runat="server" CssClass="form-control text-muted" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre y apellido</p>
                            </div>
                            <div class="col-sm-9">
                                 <asp:Label ID="LblNombreApellido2" runat="server" CssClass="text-muted"></asp:Label>
                                <asp:TextBox ID="txtNombreApellido2" runat="server" CssClass="form-control text-muted" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Label ID="lblEmail1" runat="server" Text="Email" CssClass="mb-0"></asp:Label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblEmail2" runat="server" CssClass="text-muted"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Profesion</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="LblProfesion2" runat="server" CssClass="text-muted"></asp:Label>
                                <asp:TextBox ID="txtProfesion2" runat="server" CssClass="form-control text-muted" Visible="false"></asp:TextBox>

                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Ubicacion</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="LblUbicacion2" runat="server" CssClass="text-muted"></asp:Label>
                                <asp:TextBox ID="txtUbicacion2" runat="server" CssClass="form-control text-muted" Visible="false"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>







</asp:Content>
