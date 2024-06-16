<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModeracionDenunciasCursos.aspx.cs" Inherits="webform.ModeracionDenunciasCursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="my-5">Denuncias de Cursos</h1>

    <table class="table table-striped">
    <thead>
        <tr>
            <th>Curso</th>
            <th>Denunciante</th>
            <th>Ver Mensaje</th>
            <th>Acciones</th>
            <th>Resuelto</th>
        </tr>
    </thead>
    <tbody>
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

        <asp:Repeater ID="repDenunciasCursos" runat="server">
            <ItemTemplate>
                <tr>
                    <td><a class="text-decoration-none" href='<%# "DetallesCurso.aspx?id=" + Eval("IdCurso")%>'>Nombre de curso...</a></td>
                    <td><%#Eval("IdDenunciante") %></td>
                    <td><%#Eval("MensajeDenuncia") %></td>
                    <%--<td id="btnAccion">
                        <!--Boton de Editar -->
                        <asp:Button ID="btnEditarCurso" Text="Editar" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnEditarCurso_Click" runat="server" />
                        <!--Boton de Desactivar -->

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnActivarCurso" Text='<%# (bool)Eval("Disponible") ? "Desactivar" : "Activar" %>' CssClass='<%# (bool)Eval("Disponible") ? "btn btn-sm btn-outline-warning" : "btn btn-sm btn-outline-success" %>'
                                    CommandArgument='<%# Eval("Id") %>' OnClick="btnActivarCurso_Click" data-bs-toggle="modal" data-bs-target="#ModalPublicacion" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <!--Boton de Eliminar -->
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnEliminarCurso" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger" CommandArgument='<%# Eval("Id") %>'
                                    data-bs-toggle="modal" data-bs-target="#ModalEliminar" OnClick="btnEliminarCurso_Click" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <%#Eval("NombresCategorias") %>
                    </td>
                    <td><%# (bool)Eval("Disponible") ? "Disponible" : "No Disponible" %></td>
                </tr>--%>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

</asp:Content>
