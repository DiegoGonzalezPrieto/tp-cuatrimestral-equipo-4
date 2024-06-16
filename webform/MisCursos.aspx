<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MisCursos.aspx.cs" Inherits="webform.MisCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #btnAccion {
            display: flex;
            justify-content: space-around;
        }
        .centrar{
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Panel de Usuario</h1>
    <div class="container mt-5">
        <ul class="nav nav-tabs" id="Panel">
            <li class="nav-item">
                <a class="nav-link" id="MisCursosInscripto-tab" data-bs-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Cursos Inscripto</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" id="MisCursosCreados-tab" data-bs-toggle="tab" href="#cursos" role="tab" aria-controls="cursos" aria-selected="false">Mis Cursos</a>
            </li>

        </ul>
        <div class="tab-content mt-3">
            <div class="tab-pane fade " id="home" role="tabpanel" aria-labelledby="home-tab">
                <div class="row">
                    <asp:Repeater ID="repCardsCurso" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4 mb-3">
                                <div class="card">
                                    <img src="<%#Eval("ImagenDataUrl") %>" class="card-img-top" alt="">
                                    <div class="card-body">
                                        <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                        <asp:Button ID="btnVerCurso" Text="Ver" CssClass="btn btn-primary" 
                                            OnClick="btnVerCurso_Click" runat="server" 
                                            CommandArgument='<%#Eval("Id") %>'/>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
            <div class="tab-pane fade show active" id="cursos" role="tabpanel" aria-labelledby="cursos">

                <div class="row">
                    <label>Cursos</label>
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnNuevoCurso" Text="Nuevo Curso" CssClass="btn btn-success" OnClick="btnNuevoCurso_Click" runat="server" />
                    </div>
                    <div class="col-md-12 table-responsive">

                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Nombre</th>
                                    <th class="centrar">Ver Mas</th>
                                    <th class="centrar">Capitulos</th>
                                    <th class="centrar">Acciones</th>
                                    <th>Categorías</th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
                                <asp:Repeater ID="repCursos" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><a class="text-decoration-none" href='<%# "DetallesCurso.aspx?id=" + Eval("Id")%>'><%#Eval("Nombre") %></a></td>
                                            <td class="centrar">
                                                <asp:Button ID="btnAgregar" Text="Capitulos" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregar_Click" type="button" CssClass="btn btn-sm btn-secondary" href="#capitulos" runat="server" />
                                            </td>
                                            <td class="centrar"><%#Eval("Capitulos.Count") %></td>
                                            <td id="btnAccion">
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
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                        <!--Modal Aviso para Activar/Desactivar -->
                        <div class="modal fade" id="ModalPublicacion" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h1 class="modal-title fs-5" id="exampleModalPublicacion">Opciones de Publicacion</h1>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="txtActivarDesactivar" Text="“Está a punto de realizar una acción que modificará la visibilidad del contenido del curso en la plataforma.
                                            ¿Confirma que desea proceder con la activación/desactivación de la publicación del curso?”" runat="server"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-sm btn-danger" data-bs-dismiss="modal">Cerrar</button>
                                        <asp:Button ID="btnDesactivar" Text="Aceptar" CssClass="btn btn-sm btn-success" OnClick="btnDesactivarCurso_Click" runat="server"></asp:Button>
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
                                        <asp:Label ID="txtEliminar" Text="“Confirme si desea proceder con la eliminación del curso seleccionado.
                                            Los usuarios que previamente lo hayan adquirido mantendrán el acceso.
                                            Sin embargo, el curso quedará inaccesible para nuevas adquisiciones o modificaciones futuras.”" runat="server"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-sm btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                        <asp:Button ID="Button1" Text="Aceptar" CssClass="btn btn-sm btn-danger" OnClick="btnEliminar_Click" runat="server"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div> 
                </div>

            </div>
        </div>
    </div>
</asp:Content>
