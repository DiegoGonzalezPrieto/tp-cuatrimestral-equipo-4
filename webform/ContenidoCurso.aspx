<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ContenidoCurso.aspx.cs" Inherits="webform.ContenidoCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #btnAccion {
            display: flex;
            justify-content: center;
        }
        .btn-primary{
            margin-right:5px;
        }
        .btn-secondary {
            margin-left: 5px;
        }

        .btn-danger {
            margin-left: 5px;
        }

        .centrar {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-title" style="display: flex; justify-content: center; align-items: center; margin: 20px; font-size: 30px; color: dimgrey;">
        <asp:Label ID="lblTituloContenido" Text="Contenido" runat="server" />
    </div>

    <div class="col-md-12 mb-3">
        <asp:Button ID="btnNuevoContenido" Text="Nuevo Contenido" CssClass="btn btn-success" OnClick="btnNuevoContenido_Click" runat="server" />
    </div>
    <asp:Label ID="lblContenido" Text="No hay contenidos. Agregar nuevo contenido ahora." Style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />
    <div class="col-md-12 table-responsive">

        <table class="table table-striped">
            <thead>
                <tr>
                    <th class="centrar">Orden</th>
                    <th>Nombre</th>

                    <th class="centrar">Acciones</th>

                    <th>Tipo</th>
                    <th>Estado</th>
                </tr>
            </thead>
            <tbody>
                <asp:ScriptManager ID="ScriptManager" runat="server" />
                <asp:Repeater ID="repContenido" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="centrar"><%#Eval("Orden") %></td>
                            <td><%#Eval("Nombre") %></td>
                            <td id="btnAccion">
                                <!-- BOTON EDITAR -->
                                <asp:Button ID="btnEditarContenido" Text="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary" OnClick="btnEditarContenido_Click" runat="server" />
                                <!-- BOTON DESACTIVAR/ACTIVAR -->
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnLibrarContenido" Text='<%# (bool)Eval("Liberado") ? "Restringir" : "Liberar" %>' CssClass='<%# (bool)Eval("Liberado") ? "btn btn-sm btn-secondary" : "btn btn-sm btn-success" %>'
                                            CommandArgument='<%# Eval("Id") %>' OnClick="btnLibrarContenido_Click" data-bs-toggle="modal" data-bs-target="#ModalLiberar" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <!-- BOTON ELIMINAR -->
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnEliminarContenido" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandArgument='<%# Eval("Id") %>'
                                            data-bs-toggle="modal" data-bs-target="#ModalEliminar" OnClick="btnEliminarContenido_Click" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td><%#Eval("Tipo.Nombre") %></td>
                            <td><%# (bool)Eval("Liberado") ? "Liberado" : "Restringido" %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!--Modal Aviso para Activar/Desactivar -->
        <div class="modal fade" id="ModalLiberar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalPublicacion">Opciones de Publicacion</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="txtLiberarRestringir" Text="“Está a punto de realizar una acción que modificará la visibilidad del contenido del curso en la plataforma.
                    ¿Confirma que desea proceder con la liberacion/restriccion de la publicación del contenido?”"
                            runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnRestringir" Text="Aceptar" OnClick="btnRestringir_Click" CssClass="btn btn-sm btn-success"  runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <!--Modal de Eliminar -->
        <div class="modal fade" id="ModalEliminar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalEliminar">Eliminar</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="txtEliminar" Text="“Confirme si desea proceder con la eliminación del contenido seleccionado. 
                            Los usuarios que previamente lo hayan adquirido mantendrán el acceso. 
                            Sin embargo, el contenido quedará inaccesible para nuevas adquisiciones o modificaciones futuras.”"
                            runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnEliminar" Text="Aceptar" CssClass="btn btn-sm btn-danger" OnClick="btnEliminar_Click" runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: flex; justify-content: center; align-items: center;">
        <asp:Button ID="Volver" Text="Volver" CssClass="btn btn-secondary" OnClick="Volver_Click" runat="server" />
    </div>

</asp:Content>
