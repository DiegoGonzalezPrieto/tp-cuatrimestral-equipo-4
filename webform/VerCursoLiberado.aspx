<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerCursoLiberado.aspx.cs" Inherits="webform.VerCursoLiberado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center">
        <div class="col-8">
            <h3 class="my-5 text-center">Contenidos liberados de <a class="link-dark" href="DetallesCurso.aspx?id=<%= curso.Id %>">
                <%= curso.Nombre %>
            </a>
            </h3>
            <div class="container text-end my-4">
                <a href="DetallesCurso.aspx?id=<%: curso.Id %>" class="btn btn-primary">Inscribirse</a>
            </div>

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
                                            
                                                <asp:LinkButton ID="btnAContenido" 
                                                    Text='<%# Eval("Nombre")%>' runat="server" CssClass="text-muted text-decoration-none"
                                                    href='<%# "VerContenidoLiberado.aspx?id=" + Eval("Id") %>' />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
