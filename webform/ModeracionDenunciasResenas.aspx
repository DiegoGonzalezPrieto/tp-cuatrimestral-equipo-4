<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModeracionDenunciasResenas.aspx.cs" Inherits="webform.ModeracionDenunciasResenas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="my-5">Denuncias de Reseñas</h1>

    <table class="table table-striped">
        <thead>
            <tr>
                <th>Fecha</th>
                <th>Curso</th>
                <th>Denunciante</th>
                <th>Mensaje</th>
                <th>Ver Reseña</th>
                <th>Acciones</th>
                <th>Resuelta / Pendiente</th>
            </tr>
        </thead>
        <tbody>
            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

            <asp:Repeater ID="repDenunciasCursos" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("FechaCreacion") %></td>
                        <td><a class="text-decoration-none" href='<%# "DetallesCurso.aspx?id=" + getIdCurso((int)Eval("IdReseña"))%>'><%# getNombreCurso((int)Eval("IdReseña")) %></a></td>
                        <td><%# getNombreUsuario((int)Eval("IdDenunciante")) %></td>
                        <td><%#  (((string)Eval("MensajeDenuncia")).Length > 30 ? ((string)Eval("MensajeDenuncia")).Substring(0,30) + "..." : Eval("MensajeDenuncia"))%></td>
                        <td>
                            <!--Boton de Ver Mensaje -->
                            <button class="btn btn-sm btn-outline-dark"
                                onclick="mostrarResena('<%# getResena((int)Eval("IdReseña")) %>'); return false;"
                                data-bs-toggle="modal" data-bs-target="#modalResena">
                                Ver Reseña</button>
                        </td>
                        <td>
                            <!--Boton de Ver Mensaje -->
                            <button class="btn btn-sm btn-outline-primary"
                                onclick="mostrarMensaje('<%# Eval("MensajeDenuncia") %>'); return false;"
                                data-bs-toggle="modal" data-bs-target="#modalMensaje">
                                Ver mensaje</button>

                            <!--Boton de Marcar Resuelto / No Resuleto -->
                            <asp:Button ID="btnMarcarResuelto" Text="Marcar Resuelta" CssClass="btn btn-sm btn-outline-success"
                                CommandArgument='<%# Eval("Id") %>' OnClick="btnMarcarResuelto_Click" runat="server" Visible='<%# !(bool)Eval("Resuelta")%>' />
                            <asp:Button ID="btnMarcarPendiente" Text="Marcar Pendiente" CssClass="btn btn-sm btn-outline-dark"
                                CommandArgument='<%# Eval("Id") %>' OnClick="btnMarcarPendiente_Click" runat="server" Visible='<%# Eval("Resuelta")%>' />
                        </td>
                        <td class='<%# (bool)Eval("Resuelta") ? "fw-bold text-success" : "fw-bold"  %>'>
                            <%# (bool)Eval("Resuelta") ? "Resuelta" : "Pendiente" %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>



    <div id="modalMensaje" class="modal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Mensaje de denuncia</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p id="textoModal">Modal body text goes here.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    
    <div id="modalResena" class="modal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Reseña denunciada</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p id="textoResena">Modal body text goes here.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarMensaje(msj) {
            const textoModal = document.getElementById("textoModal");
            textoModal.innerText = msj;
        }
        function mostrarResena(msj) {
            const textoResena = document.getElementById("textoResena");
            textoResena.innerText = msj;
        }
    </script>


</asp:Content>
