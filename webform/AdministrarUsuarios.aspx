<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="webform.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .title {
            height: 150px;
        }

        .subtitle {
            height: 100px;
        }

        .profile-circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title" style="display: flex; justify-content: center; align-items: center; color: dimgrey;">
        <h2>
            <asp:Label Text="Panel de Administracion de General" runat="server" />
        </h2>
    </div>
    <div class="container mt-5">
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <a class="nav-link active" id="usuarios-tab" data-bs-toggle="tab" href="#usuarios" role="tab" aria-controls="usuarios" aria-selected="true">Usuarios</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="cursos-tab" data-bs-toggle="tab" href="#cursos" role="tab" aria-controls="cursos" aria-selected="false">Cursos</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="estadisticas-tab" data-bs-toggle="tab" href="#estadisticas" role="tab" aria-controls="estadisticas" aria-selected="false">Estadísticas</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="administradores-tab" data-bs-toggle="tab" href="#administradores" role="tab" aria-controls="administradores" aria-selected="false">Administradores</a>
            </li>
        </ul>

        <div class="tab-content mt-3" id="myTabContent">
            <div class="tab-pane fade show active" id="usuarios" role="tabpanel" aria-labelledby="usuarios-tab">
                <div class="row">
                    <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: cadetblue;">
                        <h1>Administracion Usuarios</h1>
                    </div>

                    <div>
                        <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true" OnRowCommand="gvUsuarios_RowCommand" DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="Foto de Perfil">
                                    <ItemTemplate>
                                        <asp:Image ID="imgFotoPerfil" runat="server" ImageUrl='<%# string.IsNullOrEmpty((string)Eval("UrlFotoPerfil")) ? "Media/Usuario.png" : Eval("UrlFotoPerfil")%>'  CssClass="profile-circle" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" />
                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# (bool)Eval("Estado") ? "Activo" : "Fuega de Linea" %>' CssClass='<%# Eval("Estado").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary" Text="Ver" CommandName="Redirect" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="cursos" role="tabpanel" aria-labelledby="cursos-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: lightslategrey;">
                    <h1>Administracion Cursos</h1>
                </div>
                
                <div>
                    <asp:GridView ID="gvCursos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Imagen del Curso">
                                <ItemTemplate>
                                    <asp:Image ID="imgCurso" runat="server" ImageUrl='<%# Eval("ImagenDataUrl") %>' CssClass="profile-circle" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Curso" />
                            <asp:BoundField DataField="NombreUsuarioCreador" HeaderText="Creador" />
                            <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha de Publicación" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Costo" HeaderText="Costo" DataFormatString="{0:C}"/>
                            <asp:TemplateField HeaderText="Disponibilidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoCurso" runat="server" Text='<%# (bool)Eval("Disponible") ? "Activo" : "Fuera de Línea" %>' CssClass='<%# Eval("Disponible").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                 <div>
                     <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: lightslategrey;">
                        <h4>Cursos Eliminados</h4>
                    </div>
                     <asp:GridView ID="gvCursosEliminados" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Imagen del Curso">
                                <ItemTemplate>
                                    <asp:Image ID="imgCursoEliminado" runat="server" ImageUrl='<%# Eval("ImagenDataUrl") %>' CssClass="profile-circle" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Curso" />
                            <asp:BoundField DataField="NombreUsuarioCreador" HeaderText="Creador" />
                            <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha de Publicación" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Costo" HeaderText="Costo" DataFormatString="{0:C}"/>
                            <asp:TemplateField HeaderText="Disponibilidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoCursoEliminado" runat="server" Text='<%# (bool)Eval("Disponible") ? "Activo" : "Fuera de Línea" %>' CssClass='<%# Eval("Disponible").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoCurso" runat="server" Text='<%# (bool)Eval("Activo") ? "Activo" : "Eliminado" %>' CssClass='<%# Eval("Activo").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
            </div>
            <!-- DATOS ESTADISTICOS -->
            <div class="tab-pane fade" id="estadisticas" role="tabpanel" aria-labelledby="estadisticas-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: darkcyan;">
                    <h1>Datos Estadisticos</h1>
                </div>
                <div style="display: flex; justify-content: space-around; flex-wrap: wrap; align-items: center;">
                    <!-- USUARIOS TOTALES -->
                    <div class="card border-primary mb-3" style="width: 26%;">
                        <div class="card-header text-primary" style="text-align: center;">Usuarios Totales</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblUsuariosTotales" Text="null" runat="server" />
                            </h1>

                        </div>
                    </div>
                    <!-- USUARIOS ACTIVOS -->
                    <div class="card border-success mb-3" style="width: 26%;">
                        <div class="card-header text-success" style="text-align: center;">Usuarios Activos</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblUsuariosActivos" Text="null" runat="server" />
                            </h1>

                        </div>
                    </div>
                    <!-- USUARIOS SUSPENDIDOS -->
                    <div class="card border-warning mb-3" style="width: 26%;">
                        <div class="card-header text-warning" style="text-align: center;">Usuarios Suspendidos</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblUsuariosSuspendidos" Text="null" runat="server" />
                            </h1>

                        </div>
                    </div>
                    <!-- CURSOS TOTALES-->
                    <div class="card border-secondary mb-3" style="width: 26%;">
                        <div class="card-header text-secondary" style="text-align: center;">Cursos Totales</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblCursosTotales" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                    <!-- CURSOS ACTIVOS-->
                    <div class="card border-info mb-3"" style="width: 26%;">
                        <div class="card-header text-info" style="text-align: center;">Cursos Activos</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblCursosActivos" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                    <!-- CURSOS ELIMINADOS-->
                    <div class="card border-danger mb-3" style="width: 26%;">
                        <div class="card-header text-danger" style="text-align: center;">Cursos Eliminados</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblCursosEliminados" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                    <!-- PORCENTAJE CURSOS x USUARIOS -->
                    <div class="card border-secondary mb-3" style="width: 26%;">
                        <div class="card-header text-secondary" style="text-align: center;">Porcentaje Cursos Usuarios</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblPorcentajeCursoxUsuarios" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                    <!-- INSCRIPCIONES -->
                    <div class="card border-dark mb-3" style="width: 26%;">
                        <div class="card-header text-dark" style="text-align: center;">Inscripciones</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblInscripciones" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                    <!-- CERTIFICACIONES -->
                    <div class="card border-success mb-3" style="width: 26%;">
                        <div class="card-header text-success" style="text-align: center;">Certificaciones</div>
                        <div class="card-body">
                            <h1 class="card-title" style="text-align: center;">
                                <asp:Label ID="lblCertificaciones" Text="null" runat="server" />
                            </h1>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="administradores" role="tabpanel" aria-labelledby="administradores-tab">
                <div class="subtitle" style="display: flex; justify-content: center; align-items: center; color: cornflowerblue;">
                    <h1>Administradores</h1>
                </div>
                <div>
                    <asp:GridView ID="gvAdmin" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Foto de Perfil">
                                <ItemTemplate>
                                    <asp:Image ID="imgFotoPerfil" runat="server" ImageUrl='<%# string.IsNullOrEmpty((string)Eval("UrlFotoPerfil")) ? "Media/Usuario.png" : Eval("UrlFotoPerfil")%>'  CssClass="profile-circle" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" />
                            <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# (bool)Eval("Estado") ? "Activo" : "Fuega de Linea" %>' CssClass='<%# Eval("Estado").ToString() == "True" ? "text-success" : "text-danger" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
