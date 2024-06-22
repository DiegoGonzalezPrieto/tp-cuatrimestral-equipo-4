<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DatosUsuario.aspx.cs" Inherits="webform.DatosUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Datos Personales</h1>
        
    </div>
    <div>
        <h3>Datos de Cursos</h3>
        <div>
            <asp:GridView ID="gvCursosUsuario" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Capitulos.Count" HeaderText="Capitulos" />
                    <asp:BoundField DataField="NombresCategorias" HeaderText="Categorías" />
                    <asp:BoundField DataField="Disponible" HeaderText="Estado" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
