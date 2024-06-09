<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ContenidoCurso.aspx.cs" Inherits="webform.ContenidoCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-title" style="display: flex; justify-content: center; align-items: center; margin: 20px; font-size: 30px; color: dimgrey;">
        <asp:Label ID="lblTituloContenido" Text="Contenido" runat="server" />
    </div>

    <div class="col-md-12 mb-3">
        <asp:Button ID="btnNuevoContenido" Text="Nuevo Contenido" CssClass="btn btn-success" runat="server" />
    </div>
    <asp:Label ID="lblContenido" Text="No hay contenidos. Agregar nuevo contenido ahora." Style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />
    <div class="col-md-12 table-responsive">

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Orden</th>
                    <th>Nombre</th>
                    <th>Tipo</th>
                    <th>Estado</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repContenido" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Orden") %></td>
                            <td><%#Eval("Nombre") %></td>
                            <td><%#Eval("Tipo") %></td>
                            <td><%# (bool)Eval("Activo") ? "Disponible" : "No Disponible" %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>
