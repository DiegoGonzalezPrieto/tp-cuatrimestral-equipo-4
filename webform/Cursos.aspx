<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="webform.Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container my-4">
        <div class="container my-5">
            <h2 class="mb-4">Cursos</h2>

            <div class="row">
                <div class="col">
                </div>
                <div class="col-4">
                    <asp:Label Text="Categoría" AssociatedControlID="ddlCategorias" runat="server" CssClass="form-label" />
                    <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select my-2">
                    </asp:DropDownList>
                    <asp:Button ID="btnFiltroCategorias" Text="Filtrar" runat="server" CssClass="btn btn-secondary" OnClick="btnFiltroCategorias_Click" />
                </div>
            </div>

            <div class="my-3">
                <asp:Label ID="lblMensaje" runat="server" Font-Size="X-Large"></asp:Label>

            </div>
            <div class="row row-cols-1 row-cols-md-3 g-4">


                <asp:Repeater ID="repCursos" runat="server">
                    <ItemTemplate>

                        <div class="col">
                            <div class="card h-100">
                                <asp:LinkButton ID="BtnCurso" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="BtnCurso_Click" Style="text-decoration: none; color: inherit;">
                                    <div class="text-center">
                                        <asp:Image ID="imgCurso" runat="server" ImageUrl='<%# Eval("ImagenDataUrl") %>'
                                            onerror="this.src = 'Media/noImage.svg';" CssClass="card-img-top img-fluid" Style="width: 80%;" />
                                    </div>
                                    <div class="card-body">
                                        <h5 class="card-title text-center"><%# Eval("Nombre") %></h5>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>

</asp:Content>
