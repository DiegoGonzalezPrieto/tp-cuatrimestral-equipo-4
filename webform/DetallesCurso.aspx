<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetallesCurso.aspx.cs" Inherits="webform.DetallesCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .tip {
            width: 0;
            height: 0;
            position: absolute;
            border: 10px solid #ccc;
            border-top-color: transparent;
            border-left-color: transparent;
            border-bottom-color: transparent;
        }

        .tip-left {
            top: 1em;
            left: -24px;
        }

        .dialogbox .body {
            position: relative;
            width: calc(100% - 20px);
            margin: 20px 0;
            padding: 10px;
            background-color: #DADADA;
            border-radius: 3px;
            border: 5px solid #ccc;
        }

        .body .message {
            font-family: Arial, sans-serif;
            font-size: 14px;
            line-height: 1.5;
            color: #797979;
        }

        .resena-panel {
            margin-top: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .flex-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .flex-item-left {
            text-align: left;
        }

        .flex-item-right {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="tituloPagina">
        <h1 style="">Detalles del curso</h1>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="col">
                    <div class="card h-100">

                        <div class="text-center">
                            <asp:Image ID="imgCurso" runat="server" CssClass="card-img-top img-fluid"
                                Style="width: 80%;" onerror="this.src = 'Media/noImage.svg';" />
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <ul class="list-group">
                    <li class="list-group-item">
                        <span class="fw-bold">Nombre:</span>
                        <asp:Label ID="lblNombreCurso" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Descripción:</span>
                        <asp:Label ID="lblDescripcionCurso" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Fecha de Publicacion:</span>
                        <asp:Label ID="lblFechaPublicacion" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Categoria:</span>
                        <asp:Label ID="lblCategoria" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Cantidad de capitulos:</span>
                        <asp:Label ID="lblCantidadCapitulos" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Estado:</span>
                        <asp:Label ID="lblEstado" runat="server"></asp:Label>
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Costo:</span>
                        <asp:Label ID="lblCosto" runat="server"></asp:Label>
                    </li>
                </ul>

                <% if (webform.Seguridad.adquirioCurso(IdCurso) || webform.Seguridad.creoCurso(IdCurso) || webform.Seguridad.esAdmin())
                    { %>
                <a href="VerCurso.aspx?curso=<%: IdCurso %>" class="btn btn-primary my-4">Ver Curso</a>
                <% }
                    else if (webform.Seguridad.UsuarioActual != null)
                    {%>
                <div class="container p-2">
                    <asp:Button ID="btnInscribirse" runat="server" Text="Inscribirse" CssClass="btn btn-primary" OnClick="btnInscribirse_Click" />
                    <% if (webform.Seguridad.parcialmenteLiberado(IdCurso))
                        { %>
                    <a href="#" class="btn btn-success mx-2">Ver contenidos liberados</a>
                    <% } %>
                </div>
                <asp:Label ID="lblMensaje" runat="server" Visible="false" />

                <%}
                    else
                    { %>
                <a href="Login.aspx" class="btn btn-primary my-4">Iniciar sesión para inscribirse</a>
                <%}%>



                <div class="container text-end my-4">
                    <% if (webform.Seguridad.adquirioCurso(IdCurso) && !webform.Seguridad.agregoResena(IdCurso))
                        {%>
                    <asp:Button ID="BtnResena" runat="server" Text="Agregar Reseña" CssClass="btn btn-primary" OnClick="BtnResena_Click" />
                    <% } %>
                    <% if (webform.Seguridad.UsuarioActual != null && !webform.Seguridad.esAdmin()
                                && !webform.Seguridad.creoCurso(IdCurso) && !webform.Seguridad.denuncioCurso(IdCurso))
                        {%>
                    <asp:Button ID="BtnDenunciar" runat="server" Text="Denunciar curso" CssClass="btn btn-danger" OnClick="BtnDenunciar_Click" />
                    <% } %>
                    <% if (!webform.Seguridad.esAdmin() && !webform.Seguridad.creoCurso(IdCurso) && webform.Seguridad.denuncioCurso(IdCurso))
                        {%>
                    <button class="btn btn-warning"
                        onclick="mostrarMensajDenuncia('<%: obtenerMensajeDenuncia() %>'); return false;"
                        data-bs-toggle="modal" data-bs-target="#modalDenuncia">
                        Ver denuncia realizada</button>
                    <% } %>
                </div>
            </div>
        </div>
    </div>


    <asp:Panel ID="pnlResena1" runat="server" CssClass="resena-panel" Visible="false">
        <div class="form-group">
            <label for="txtPuntaje">Puntaje (1 a 5):</label>
            <asp:TextBox ID="txtPuntaje" runat="server" CssClass="form-control" type="number" min="1" max="5"></asp:TextBox>
            <asp:RequiredFieldValidator ErrorMessage="Puntaje requerido" ControlToValidate="txtPuntaje" runat="server" CssClass="text-danger small" />
        </div>
        <div class="form-group">
            <label for="txtMensaje">Mensaje:</label>
            <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ErrorMessage="Mensaje requerido" ControlToValidate="txtMensaje" runat="server" CssClass="text-danger small" />
        </div>
        <asp:Button ID="btnEnviarResena" runat="server" Text="Enviar Reseña" CssClass="btn btn-success" OnClick="btnEnviarResena_Click" />
    </asp:Panel>


    <asp:Panel ID="pnlResena2" runat="server" CssClass="resena-panel" Visible="false">
        <div class="form-group">
            <label for="txtResenaCreador">No puedes agregar una reseña de tu propio curso</label>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlResena3" runat="server" CssClass="resena-panel" Visible="false">
        <div class="form-group">
            <label for="txtDebeInscribirse">Para poder dejar una reseña debe estar inscripto al curso</label>
        </div>
        <asp:Button ID="BtnInscribirse2" runat="server" Text="Inscribirse" CssClass="btn btn-success" OnClick="btnInscribirse_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlResena4" runat="server" CssClass="resena-panel" Visible="false">
        <div class="form-group">
            <label for="txtDebeLoguearse">Para poder dejar una reseña debe iniciar sesion</label>
        </div>
        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesion" CssClass="btn btn-primary" OnClick="btnIniciarSesion_Click" />
    </asp:Panel>



    <div class="container my-4">
        <h3 class="fw-bold text-muted">~ Reseñas ~</h3>
    </div>

    <div class="container">
        <asp:Repeater ID="rptComments" runat="server">
            <ItemTemplate>
                <div class="dialogbox">
                    <div class="body">
                        <span class="tip tip-left"></span>
                        <div class="message flex-container">
                            <div class="flex-item-left">
                                <asp:Label ID="lblPuntaje" runat="server" CssClass="fw-bold" Text='<%#"Calificacion: " + Eval("Puntaje") + "/5" %>'></asp:Label>
                            </div>
                            <div class="flex-item-right">
                                <asp:Label ID="lblFecha" runat="server" Text='<%#"Fecha: " + Eval("FechaCreacion", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="message">
                            <asp:Label ID="lblComment" runat="server" Text='<%#"Opinion: " + Eval("Mensaje") %>'></asp:Label>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="modalDenuncia" class="modal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Denuncia realizada</h5>
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

    <script>
        function mostrarMensajDenuncia(msj) {
            const textoModal = document.getElementById("textoModal");
            textoModal.innerText = msj;
        }
    </script>

</asp:Content>
