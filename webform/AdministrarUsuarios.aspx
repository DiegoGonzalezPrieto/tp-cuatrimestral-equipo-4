<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="webform.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .title{
            height:150px;
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
            <asp:Label Text="Panel de Administracion de Usuarios" runat="server" />
        </h2>
    </div>
    <div>
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
                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>' CssClass='<%# Eval("Estado").ToString() == "Activo" ? "text-success" : "text-danger" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
