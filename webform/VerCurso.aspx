<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>


    <style>
        body {
            background: #eee;
        }

        hr {
            margin-top: 20px;
            margin-bottom: 20px;
            border: 0;
            border-top: 1px solid #FFFFFF;
        }

        a {
            color: #82b440;
            text-decoration: none;
        }

        .blog-comment::before,
        .blog-comment::after,
        .blog-comment-form::before,
        .blog-comment-form::after {
            content: "";
            display: table;
            clear: both;
        }

        .blog-comment {
            padding-left: 15%;
            padding-right: 15%;
        }

            .blog-comment ul {
                list-style-type: none;
                padding: 0;
            }

            .blog-comment img.avatar {
                position: relative;
                float: left;
                margin-left: 0;
                margin-top: 0;
                width: 65px;
                height: 65px;
            }

            .blog-comment .post-comments {
                border: 1px solid #eee;
                margin-bottom: 20px;
                margin-left: 85px;
                margin-right: 0px;
                padding: 10px 20px;
                position: relative;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
                background: #fff;
                color: #6b6e80;
                position: relative;
            }

            .blog-comment .meta {
                font-size: 13px;
                color: #aaaaaa;
                padding-bottom: 8px;
                margin-bottom: 10px !important;
                border-bottom: 1px solid #eee;
            }

            .blog-comment ul.comments ul {
                list-style-type: none;
                padding: 0;
                margin-left: 85px;
            }

        .blog-comment-form {
            padding-left: 15%;
            padding-right: 15%;
            padding-top: 40px;
        }

            .blog-comment h3,
            .blog-comment-form h3 {
                margin-bottom: 40px;
                font-size: 26px;
                line-height: 30px;
                font-weight: 800;
            }

        .responder-panel {
            margin-top: 10px;
        }

        .mar-top {
            margin-top: 10px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <%-- Este bloque vacío es porque hay un error en VisualStudio al tener una expresión condicional parece
        https://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context
    --%>
    <%="" %>

    <% if (!indice)
        { %>

    <%-- Boton reseña/denuncia --%>
    <% if (webform.Seguridad.UsuarioActual != null && !webform.Seguridad.creoCurso(curso.Id))
        { %>
    <div class="container text-end py-3 px-4">
        <a href="DetallesCurso.aspx?id=<%: curso.Id %>" class="btn btn-primary">Agregar Reseña o Denuncia</a>
    </div>
    <%}%>

    <div class="row m-4 shadow-lg p-4">
        <div class="col">
            <div class="row justify-content-between ">
                <div class="col-4 py-3 text-muted">
                    <a class="text-muted text-decoration-none" href="VerCurso.aspx?curso=<%: curso.Id %>"><span>📜</span> Indice</a>
                </div>
                <div class="col-4 py-3 text-muted">
                    <%: capitulo.Orden %>. <%: capitulo.Nombre %> - <a class="text-muted" href="DetallesCurso.aspx?id=<%: curso.Id %>"><%: curso.Nombre %></a>
                </div>
            </div>




            <div class="row p-4">
                <h2 class="my-3"><%: capitulo.Orden %>.<%: contenido.Orden %> <%: contenido.Nombre %></h2>
                <p class="text-muted px-4"><%: contenido.Tipo != null ? contenido.Tipo.Nombre : "" %></p>
                <p class="px-5 py-3" style="white-space: pre-line;"><%: contenido.Texto %></p>
            </div>

            <% if (!string.IsNullOrEmpty(contenido.UrlVideo))
                { %>
            <%-- VIDEO (si hay Url) --%>
            <div class="row p-4" style="height: 70vh;">

                <iframe src="<%: contenido.UrlVideo %>"></iframe>

            </div>
            <% }%>

            <% if (contenido.Archivo != null)

                { %>
            <%-- PDF (si hay archivo) --%>
            <div class="row justify-content-center">
                <div class="col-4 d-flex justify-content-center">
                    <asp:Button Text="Descargar Pdf" ID="btnPdf" runat="server" OnClick="btnPdf_Click" CssClass="btn btn-dark"></asp:Button>

                </div>


            </div>
            <% } %>

            <div class="row p-5">
                <div class="col">
                    <% if (!string.IsNullOrEmpty(urlAnterior))
                        { %>

                    <a href="<%: urlAnterior %>" class="btn btn-secondary"><- <%: contenidoAnterior.Nombre %></a>
                    <% } %>
                </div>
                <div class="col d-flex justify-content-end">
                    <% if (!string.IsNullOrEmpty(urlSiguiente))
                        { %>
                    <a href="<%: urlSiguiente %>" class="btn btn-secondary"><%: contenidoSiguiente.Nombre %> -></a>
                    <% } %>
                </div>
            </div>

        </div>
    </div>
    <%}
        else
        { %>

    <%-- INDICE --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>






    <div class="row justify-content-center">
        <div class="col-8">
            <h3 class="my-5 text-center">Contenidos de <a class="link-dark" href="DetallesCurso.aspx?id=<%= curso.Id %>">
                <%= curso.Nombre %>
            </a>
            </h3>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <%-- Progreso --%>
                    <% if (webform.Seguridad.UsuarioActual != null && !webform.Seguridad.creoCurso(curso.Id))
                        {%>
                    <div class="row justify-content-center">
                        <div class="col-5">
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped bg-success"
                                    role="progressbar" style="<%= "width:"+ procentajeCompletado.ToString() + "%" %>">
                                    <%= Math.Round(procentajeCompletado, 0).ToString() + "%" %>
                                </div>
                            </div>
                            <h6 class="text-center my-2">Realizado: <%: Math.Round(procentajeCompletado, 0).ToString() %> %</h6>

                        </div>
                    </div>
                    <div class="container text-end my-4">
                        <a href="DetallesCurso.aspx?id=<%: curso.Id %>" class="btn btn-primary">Agregar Reseña o Denuncia</a>
                    </div>
                    <% }%>

                    <ul class="list-group p-2 m-5">
                        <asp:Repeater ID="repCapitulos" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item p-3">
                                    <h6>
                                        <%# Eval("Orden") + ". " + Eval("Nombre") %>
                                    </h6>
                                    <ul class="list-group-flush">
                                        <asp:Repeater ID="repContenidos" runat="server" DataSource='<%# Eval("Contenidos")%>'>
                                            <ItemTemplate>
                                                <li class="list-group-item py-3">
                                                    <span data-bs-toggle="tooltip" data-bs-title="Visto" data-bs-placement="left">

                                                        <%-- Mostrar checkbox solo si hay usuario --%>

                                                        <% if (Session["usuario"] != null)
                                                            { %>
                                                        <asp:CheckBox ID="cbxCompletado" runat="server" AutoPostBack="true"
                                                            Checked='<%# Eval("Completado") %>'
                                                            data-id-contenido='<%# Eval("Id")%>'
                                                            CssClass="form-check-input me-1 border-0"
                                                            OnCheckedChanged="cbxCompletado_CheckedChanged" />
                                                        <%} %>
                                                        <asp:Label Text='<%# DataBinder.Eval(Container,"Parent.Parent.DataItem.Orden") + "." + 
                                                        Eval("Orden") + "."%>'
                                                            runat="server" CssClass="form-check-label" />
                                                        <asp:LinkButton ID="btnAContenido" Text='<%# Eval("Nombre")%>' runat="server" CssClass="text-muted text-decoration-none"
                                                            href='<%# "VerCurso.aspx?curso=" + curso.Id + "&capitulo=" + DataBinder.Eval(Container,"Parent.Parent.DataItem.Orden")
                                                        + "&contenido=" + Eval("Orden")%>' />

                                                    </span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <%} %>

    <div class="container bootstrap snippets bootdey">
        <div class="row">
            <div class="col-md-12">
                <div class="blog-comment">
                    <h3 class="text-primary">Comentarios</h3>
                    <hr />

                    <div class="panel">
                        <div class="panel-body">
                            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" placeholder="Deja tu comentario"></asp:TextBox>
                            <div class="text-end" style="margin-top: 5px">
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-sm btn-primary" OnClick="btnEnviar_Click" />
                            </div>
                        </div>
                    </div>
                    <ul class="comments" style="margin-top: 10px;">

                        <asp:Repeater ID="rptComentarios" runat="server" OnItemDataBound="rptComentarios_ItemDataBound">
                            <ItemTemplate>
                                <li class="clearfix">
                                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp" class="avatar" alt="">
                                    <div class="post-comments">
                                        <p class="meta"><%# Eval("FechaCreacion") %> <a href="#"><%# Eval("NombreUsuario") %></a> dijo: </p>
                                        <p><%# Eval("Mensaje") %></p>
                                        <i class="pull-right">
                                            <a class="text-muted" data-toggle="collapse" href="#collapseResponder<%# Eval("Id") %>" aria-expanded="false" aria-controls="collapseResponder<%# Eval("Id") %>">
                                                <small>Responder</small>
                                            </a>
                                        </i>
                                        <div class="collapse" id="collapseResponder<%# Eval("Id") %>">
                                            <asp:TextBox ID="txtResponder" runat="server" CssClass="form-control" Rows="2" placeholder="Deja tu respuesta"></asp:TextBox>
                                            <asp:Button ID="btnResponderEnviar" runat="server" Text="Enviar" CssClass="btn btn-sm btn-primary mar-top" OnClick="btnResponderEnviar_Click" CommandArgument='<%# Eval("Id") %>' />
                                        </div>
                                        <ul class="comments">

                                            <asp:Repeater ID="rptRespuestas" runat="server">
                                                <ItemTemplate>
                                                    <li class="clearfix">
                                                        <img src="https://bootdey.com/img/Content/user_3.jpg" class="avatar" alt="">
                                                        <div class="post-comments">
                                                            <p class="meta"><%# Eval("FechaCreacion") %> <a href="#"><%# Eval("NombreUsuario") %></a> dijo: </p>
                                                            <p>
                                                                <%# Eval("Mensaje") %>
                                                            </p>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

