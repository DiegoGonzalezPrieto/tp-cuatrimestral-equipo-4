<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MisCursos.aspx.cs" Inherits="webform.MisCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Panel de Usuario</h1>
    <div class="container mt-5">
        <ul class="nav nav-tabs" id="Panel">
            <li class="nav-item">
                <a class="nav-link active" id="home-tab" data-bs-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Cursos Inscripto</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="profile-tab" data-bs-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Mis Cursos</a>
            </li>
        </ul>
        <div class="tab-content mt-3">
            <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="card">
                            <img src="https://www.staffcreativa.pe/blog/wp-content/uploads/HTML5.jpg" class="card-img-top" alt="">
                            <div class="card-body">
                                <h5 class="card-title">Curso HTML5</h5>
                                <asp:Button ID="btnVerCurso" Text="Ver" CssClass="btn btn-primary" OnClick="btnVerCurso_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnNuevoCurso" Text="Nuevo" CssClass="btn btn-success" OnClick="btnNuevoCurso_Click" runat="server" />
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Nombre</th>
                                    <th>Cantidad</th>
                                    <th>Acciones</th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Curso C# Nivel 1</td>
                                    <td>10</td>
                                    <td>
                                        <asp:Button ID="btnEditarCurso" Text="Editar" CssClass="btn btn-sm btn-primary" runat="server" />
                                        <asp:Button ID="btnActivarCurso" Text="Deactivar" CssClass="btn btn-sm btn-warning" runat="server" />
                                        <asp:Button ID="btnEliminarCurso" Text="Eliminar" CssClass="btn btn-sm btn-danger" runat="server" />
                                    </td>
                                    <td>En linea</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
