<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCurso.aspx.cs" Inherits="webform.VerCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%-- Este bloque vacío es porque hay un error en VisualStudio al tener una expresión condicional parece
        https://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context
    --%>
    <%="" %>

    <% if (!indice)
        { %>

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
                    <% if (Session["usuario"] != null)
                        {%>
                    <div class="row justify-content-center">
                        <div class="col-5">
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped bg-success"
                                    role="progressbar" style="<%= "width:"+ procentajeCompletado.ToString() + "%" %>">
                                    <%= Math.Round(procentajeCompletado, 0).ToString() + "%" %>
                                </div>
                            </div>

                        </div>
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
</asp:Content>
