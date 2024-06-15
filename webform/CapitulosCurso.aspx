<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CapitulosCurso.aspx.cs" Inherits="webform.CapitulosCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            border: 1px, solid, grey;
            border-radius: 25px;
            margin: 5px;
        }

        .panel-control {
            border: 1px, solid, grey;
            border-radius: 25px;
            margin: 5px;
        }

        .centrar {
            text-align: center;
        }
        #acciones{
            display:flex;
        }
        .btn-outline-primary{
            margin-right:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-title" style="display: flex; justify-content: center; align-items: center; margin: 20px; font-size: 30px; color: dimgrey;">
        <asp:Label ID="lblTituloCurso" Text="Sin titulo" runat="server" />
    </div>

    <div class="col-md-12 mb-2">
        <!--<asp:Button ID="btnNuevoCapitulo" Text="Nuevo Capitulo" OnClick="btnNuevoCapitulo_Click" CssClass="btn btn-success" runat="server" />
             <br />-->
        <button id="btnNuevoCapitul" type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#ModalNuevoCapitulo" runat="server">Nuevo Capitulo</button>
    </div>

    <asp:Label ID="lblCapitulo" Text="No hay capitulos. Debe agregar uno." Style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />

    <div style="display: flex; justify-content: space-between;">
        <div id="tablaCapitulos" class="col-md-8 table-responsive" runat="server">

            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Orden</th>
                        <th>Nombre</th>
                        <th>Cantidad</th>
                        <th class="centrar">Acciones</th>
                        <th>Contenidos</th>
                        <th>Liberado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ScriptManager ID="ScriptManager" runat="server" />
                    <asp:Repeater ID="repCapitulos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="centrar"><%#Eval("Orden") %></td>
                                <td><%#Eval("Nombre") %></td>
                                <td class="centrar"><%#Eval("Contenidos.Count") %> </td>
                                <td id="acciones">
                                    <!--Boton de Editar -->
                                    <asp:Button ID="Editar" Text="Editar" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("Id") %>' OnClick="Editar_Click" runat="server" />
                                    <!--Boton de Eliminar -->
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnEliminarCapitulo" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger" CommandArgument='<%# Eval("Id") %>'
                                                data-bs-toggle="modal" data-bs-target="#ModalEliminar" OnClick="btnEliminarCapitulo_Click" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Button ID="btnVer" Text="Ver Contenidos" CssClass="btn btn-sm btn-outline-warning" CommandArgument='<%# Eval("Id") %>' OnClick="btnVer_Click" runat="server" />
                                </td>
                                <td><%# (bool)Eval("Liberado") ? "Disponible" : "No Disponible" %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <!--Modal de Eliminar -->
            <div class="modal fade" id="ModalEliminar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalEliminar">Eliminar</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="txtEliminar" Text="“Confirme si desea proceder con la eliminación del capitulo seleccionado. 
                                         Los usuarios que previamente lo hayan adquirido mantendrán el acceso. 
                                         Sin embargo, el capitulo quedará inaccesible para nuevas adquisiciones o modificaciones futuras.”"
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

        <div id="panelControl" class="col-md-4 panel-control" runat="server">
            <div style="margin: 10px;">
                <div style="align-items: center; margin: 10px;">
                    <asp:Label Text="Orden Capitulo" runat="server" />
                    <asp:TextBox ID="txtOrden" Text="" CssClass="form-control" runat="server" />
                </div>
                <div style="align-items: center; margin: 10px;">
                    <asp:Label Text="Nombre Capitulo" runat="server" />
                    <asp:TextBox ID="txtNombreCapitulo" Text="" CssClass="form-control" runat="server" />
                </div>
                <div style="align-items: center; margin: 10px;">
                    <asp:Label Text="Estado Capitulo" runat="server" />
                    <asp:CheckBox ID="chkEstado" CssClass="form-check-input" runat="server" />
                </div>
            </div>
            <div style="display: flex; justify-content: center; margin: 5px;">
                <asp:Button ID="btnGuardarCambios" Text="Guardar Cambios" CssClass="btn btn-sm btn-success" OnClick="btnGuardarCambios_Click" runat="server" />
            </div>
            <div style="display: flex; justify-content: center; margin: 5px; color: forestgreen;">
                <asp:Label ID="txtMensajeGuardado" Text="Capitulo guardado correctamente!!" runat="server" />
            </div>
        </div>
    </div>

    <div style="display: flex; justify-content: center; align-items: center;">
        <asp:Button ID="Volver" Text="Volver" CssClass="btn btn-secondary" OnClick="Volver_Click" runat="server" />
    </div>



    <div class="modal fade" id="ModalNuevoCapitulo" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Curso Agregado</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <label>Nombre Nuevo Capitulo</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control form-control-sm" runat="server" />

                </div>
                <div class="modal-footer">
                    <asp:Button Text="Agregar Capitulo" CssClass="btn btn-sm btn-success" ID="btnModalAceptar" OnClick="btnNuevoCapitulo_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
