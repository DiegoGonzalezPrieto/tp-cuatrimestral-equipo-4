<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DatosUsuario.aspx.cs" Inherits="webform.DatosUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }

        .a1 {
            display: flex;
            justify-content: space-around;
            align-items: center;
            border: 1px solid grey;
            color: darkgray;
            width: 50%;
            margin-top: 20px;
            margin-bottom: 50px;
            padding: 10px;
            border-radius: 5px;
        }

        .inter {
            margin-bottom: 20px;
        }
    </style>
    <script>
        function abrirModal(nombreCurso) {
            // Busca el modal por su ID y ábrelo
            $('#ModalPublicacion').modal('show');

            // Puedes usar el valor de nombreCurso si lo necesitas
            console.log("Curso seleccionado: " + nombreCurso);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="display: flex; justify-content: center; margin: 50px;">
            <h1>Datos Personales</h1>
        </div>

        <div class="a1">
            <asp:Image ID="ImgFotoPerfil" runat="server" CssClass="profile-circle" />
            <asp:Label ID="lblUserName" Text="UserName" runat="server" />
        </div>
        <div style="display: flex; justify-content: space-around; align-items: center; color: darkgray; margin: 20px 20px 40px 20px; width: 50%;">
            <asp:Label ID="lblNombreUsuario" Text="Nombre" runat="server" />
            <asp:Label ID="lblApellidoUsuario" Text="Apellido" runat="server" />
        </div>
        <div style="display: grid; color: darkgray; margin: 20px">
            <asp:Label ID="lblFechaNacimiento" Text="FechaNacimiento" runat="server" CssClass="inter" />
            <asp:Label ID="lblProfesion" Text="Profesion" runat="server" CssClass="inter" />
            <asp:Label ID="lblBibliografia" Text="Bibliografia" runat="server" CssClass="inter" />
        </div>

    </div>
    <div>
        <h3>Datos de Cursos</h3>
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
            <asp:GridView ID="gvCursosUsuario" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Capitulos.Count" HeaderText="Capitulos" />
                    <asp:BoundField DataField="NombresCategorias" HeaderText="Categorías" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# (bool)Eval("Disponible") ? "Disponible" : "No Disponible" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Accion">

                        <ItemTemplate>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSuspender" Text='<%# (bool)Eval("Disponible") ? "Suspender" : "Activar" %>' CssClass='<%# (bool)Eval("Disponible") ? "btn btn-sm btn-outline-secondary" : "btn btn-sm btn-outline-success" %>'
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnSuspender_Click" data-bs-toggle="modal" data-bs-target="#ModalPublicacion" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </ItemTemplate>


                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
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
                    ¿Confirma que desea proceder con la activación/suspencion de la publicación del curso?”"
                            runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnSuspenderActivar" Text="Aceptar" CssClass="btn btn-sm btn-success" OnClick="btnSuspenderActivar_Click" runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
