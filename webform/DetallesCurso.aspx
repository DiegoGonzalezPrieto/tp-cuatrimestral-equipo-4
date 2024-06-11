<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetallesCurso.aspx.cs" Inherits="webform.DetallesCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <asp:LinkButton ID="BtnMarketing" runat="server" Style="text-decoration: none; color: inherit;">
                            <div class="text-center">
                                <asp:Image ID="imgCurso" runat="server" CssClass="card-img-top img-fluid"
                                    Style="width: 80%;" onerror="this.src = 'Media/noImage.svg';" />
                            </div>
                        </asp:LinkButton>
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
                <% if (webform.Seguridad.UsuarioAcual != null)
                    {%>
                <div class="container p-2">
                    <asp:Button ID="btnInscribirse" runat="server" Text="Inscribirse" CssClass="btn btn-primary" OnClick="btnInscribirse_Click" />
                </div>
                <asp:Label ID="lblMensaje" runat="server" Visible="false" />

                <%} else
            { %>
                <a href="Login.aspx" class="btn btn-primary my-4">Iniciar sesión para inscribirse</a>
            <%}%>

                <div class="container text-end my-4">
                    <asp:Button ID="BtnDenunciar" runat="server" Text="Denunciar curso" CssClass="btn btn-danger" OnClick="BtnDenunciar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
