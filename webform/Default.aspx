<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webform.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="Inicio">

        <div class="container">
            <div class="row">
                <div class="col-md-8">
                    <img src="Media/Portada.svg" class="img-fluid" alt="Imagen" style="width: 100%;">
                </div>
                <div class="col-md-4 d-flex align-items-center">
                    <div class="text-center">
                        <asp:Label ID="lblInicio" runat="server" Text="" Style="font-size: 2em; font-weight: bold; display: block;"></asp:Label>
                        <asp:Button ID="BtnCrearCuenta" runat="server" CssClass="btn btn-primary btn-lg mt-3" Text="" OnClick="BtnCrearCuenta_Click" />
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager" runat="server" />


        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <div class="container my-4">
                    <h1 class="text-center">- Bienvenido a nuestra web de cursos - </h1>
                    <div class="container my-5">
                        <h2 class="mb-4">Categorías principales</h2>
                        <div class="row row-cols-1 row-cols-md-3 g-4">

                            <asp:Repeater ID="RepeaterCategorias" runat="server">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card h-100">
                                            <asp:LinkButton ID="CardCategoria" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="CardCategoria_Click" Style="text-decoration: none; color: inherit;">
                                        <div class="text-center">
                                            <img src='<%# Eval("ImagenDataUrl") %>' class="card-img-top img-fluid" style="width: 80%;" alt='<%# Eval("Nombre") %>' />
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



                        <div class="container text-end my-4">

                            <asp:Button ID="btnMasCategorias" Text="Más categorías" runat="server" CssClass="btn btn-secondary" OnClick="btnMasCategorias_Click" />
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </main>
</asp:Content>
