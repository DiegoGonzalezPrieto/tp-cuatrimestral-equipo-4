<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministracionCategorias.aspx.cs" Inherits="webform.AdministracionCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Repeater ID="repCategorias" runat="server" >
        <HeaderTemplate>
            Nombre
        </HeaderTemplate>
        <ItemTemplate>
            <h5><%# Eval("Nombre") %></h5>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
