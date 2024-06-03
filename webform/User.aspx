<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="webform.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .rounded-circle {
    height:150px;
    width:150px;
     object-fit: cover;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <div class="row">
            <div class="col-lg-4 d-flex">
                <div class="card mb-4 w-100 d-flex justify-content-center">
                    <div class="card-body text-center">
                        <div>
                            <asp:Image ID="ImgAvatar" runat="server" CssClass="rounded-circle" />
                        </div>
                        <button type="button" class="btn btn-outline-secondary btn-sm mt-2" data-bs-toggle="modal" data-bs-target="#uploadModal">
                            Cambiar Imagen
                        </button>
                        <h5 class="my-3">Juan Perez</h5>
                        <p class="text-muted mb-1">Full Stack Developer</p>
                        <p class="text-muted mb-4">General Pacheco, Buenos Aires</p>
                    </div>
                </div>
            </div>

            <div class="col-lg-8 d-flex">
                <div class="card mb-4 w-100">
                    <div class="card-body d-flex flex-column justify-content-center">
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Usuario</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblUsuario" runat="server" CssClass="text-muted">Juancito123</asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Nombre y apellido</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">Juan Perez</p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Email</p>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblEmail" runat="server" CssClass="text-muted">jperez@utn.com</asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Celular</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">(011) 1234-5678</p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Direccion</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">Calle falsa 123</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="uploadModal" tabindex="-1" aria-labelledby="uploadModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="uploadModalLabel">Subir nueva imagen</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mb-2" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 text-danger"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="BtnAvatar" runat="server" Text="Subir Imagen" CssClass="btn btn-primary" OnClick="BtnAvatar_Click" />
                </div>
            </div>
        </div>
    </div>



        <ul class="nav nav-tabs  my-4" id="myTab" role="tablist">
        <li class="nav-item col-md-6" role="presentation">
            <a class="nav-link active" id="categorias-tab" data-bs-toggle="tab" href="#Cursos" role="tab" aria-controls="Cursos" aria-selected="true" style="font-size: medium; text-align: center; font-weight: bold; color: white; background-color: black">- Cursos inscripto -</a>
        </li>
        <li class="nav-item col-md-6" role="presentation">
            <a class="nav-link" id="marcas-tab" data-bs-toggle="tab" href="#MisCursos" role="tab" aria-controls="MisCursos" aria-selected="false" style="font-size: medium; text-align: center; font-weight: bold; color: white; background-color: black">- Mis Cursos -</a>
        </li>
    </ul>


    <div class="tab-content" id="myTabContent">

        <div class="tab-pane fade show active" id="Cursos" role="tabpanel" aria-labelledby="Cursos-tab">
            <div class="row">
                <p>Curso de duendeologia avanzada</p>
            </div>
        </div>

        <div class="tab-pane fade" id="MisCursos" role="tabpanel" aria-labelledby="MisCursos-tab">
            <div class="row">
                <p>No hay cursos creados</p>
            </div>
        </div>
    </div>



</asp:Content>
