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
                    <td><a class="text-decoration-none" href='<%# "DetallesCurso.aspx?id=" + Eval("IdCurso")%>'><%# getNombreCurso((int)Eval("IdCurso")) %></a></td>
                    <td><%# getNombreUsuario((int)Eval("IdDenunciante")) %></td>
                    <td class="text-truncate"><%#Eval("MensajeDenuncia") %></td>
                    <td>
                        <!--Boton de Editar -->
                        <asp:Button ID="btnVerMensaje" Text="Ver Mensaje" CssClass="btn btn-sm btn-outline-primary" 
                            CommandArgument='<%# Eval("Id") %>' OnClick="btnVerMensaje_Click" runat="server" />
                       
                    </td>
                    <%--
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
