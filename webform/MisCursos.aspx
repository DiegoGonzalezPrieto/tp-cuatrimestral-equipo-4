<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MisCursos.aspx.cs" Inherits="webform.MisCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            height: 200px;
            border: 1px solid gray;
            border-radius: 15px;
            margin-bottom: 15px;
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
                                        <asp:Button ID="btnVerCurso" Text="Ver" CssClass="btn btn-primary" OnClick="btnVerCurso_Click" runat="server" />
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
                                            <th>Ver Mas</th>
                                            <th>Capitulos</th>
                                            <th>Acciones</th>
                                            <th>Categorías</th>
                                            <th>Estado</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repCursos" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Nombre") %></td>
                                                    <td>
                                                        <asp:Button ID="btnAgregar" Text="+" CommandArgument='<%# Eval("Id") %>' OnClick="btnAgregar_Click" type="button" CssClass="btn btn-sm btn-secondary" href="#capitulos" runat="server" />
                                                    </td>
                                                    <td> <%#Eval("Capitulos.Count") %></td>
                                                    <td>
                                                        <asp:Button ID="btnEditarCurso" Text="Editar" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnEditarCurso_Click" runat="server" />
                                                        <asp:Button ID="btnActivarCurso" Text='<%# (bool)Eval("Disponible") ? "Desactivar" : "Activar" %>' CssClass='<%# (bool)Eval("Disponible") ? "btn btn-sm btn-outline-warning" : "btn btn-sm btn-outline-success" %>'
                                                            CommandArgument='<%# Eval("Id") %>' OnClick="btnActivarCurso_Click" runat="server" />
                                                        <asp:Button ID="btnEliminarCurso" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger" OnClick="btnEliminarCurso_Click" runat="server" />
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
                            </div>
                            <asp:Label ID="lblTituloCapitulo" Text="Capitulo" runat="server" />
                            <div class="col-md-12 mb-2">
                                <!--<asp:Button ID="btnNuevoCapitulo" Text="Nuevo Capitulo" OnClick="btnNuevoCapitulo_Click" CssClass="btn btn-success" runat="server" />
                                <br />-->
                                <button id="btnNuevoCapitul" type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal" runat="server">Nuevo Capitulo</button>
                            </div>

                            <asp:Label ID="lblCapitulo" Text="No hay capitulos. Debe agregar uno." style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />
                            
                            <div class="col-md-12 table-responsive">

                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Orden</th>
                                            <th>Nombre</th>
                                            <th>Cantidad</th>
                                            <th>Contenidos</th>
                                            <th>Liberado</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repCapitulos" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Orden") %></td>
                                                    <td><%#Eval("Nombre") %></td>
                                                    <td>5 </td>
                                                    <td>
                                                        <asp:Button ID="btnVer" Text="Ver Contenido" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnVer_Click" runat="server" />
                                                    </td>
                                                    <td><%# (bool)Eval("Liberado") ? "Disponible" : "No Disponible" %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>



                            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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

                            <asp:Label ID="lblTituloContenido" Text="Contenido" runat="server" />
                            <div class="col-md-12 mb-3">
                                <asp:Button ID="btnNuevoContenido" Text="Nuevo Contenido" CssClass="btn btn-success" runat="server" />
                            </div>
                            <asp:Label ID="lblContenido" Text="No hay contenidos. Agregar nuevo contenido ahora." style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />
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
                        </div>

            </div>
        </div>
    </div>
</asp:Content>
