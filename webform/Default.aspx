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
                        <h2>Creá tu cuenta gratis y empezá a aprender hoy mismo</h2>
                        <asp:Button ID="BtnCrearCuenta" runat="server" CssClass="btn btn-primary btn-lg mt-3" Text="Crear cuenta gratis" OnClick="BtnCrearCuenta_Click" />
                    </div>
                </div>
            </div>
        </div>


        <div class="container my-4">
            <h1 class="text-center">- Bienvenido a nuestra web de cursos - </h1>
            <div class="container my-5">
                <h2 class="mb-4">Categorías principales</h2>
                <div class="row row-cols-1 row-cols-md-3 g-4">

                    <% foreach (dominio.Categoria cat in Categorias)
                        { %>


                    <div class="col">

                        <div class="card h-100 py-4">
                            <a href="Cursos.aspx?cat=<%: cat.Id %>" style="text-decoration: none; color: inherit;">
                                <div class="text-center">
                                    <img src="<%: cat.ImagenDataUrl %>" class="card-img-top img-fluid" style="width: 80%;" alt="<%: cat.Nombre %>" />
                                </div>
                                <div class="card-body">
                                    <h5 class="card-title text-center"><%: cat.Nombre %></h5>
                                </div>
                            </a>
                        </div>
                    </div>

                    <% } %>


                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnMarketing" runat="server" OnClick="BtnMarketing_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/marketing.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Marketing y negocios" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Marketing y negocios</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnSoftware" runat="server" OnClick="BtnSoftware_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/software.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Informática y software" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Informática y software</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnDesarrolloPersonal" runat="server" OnClick="BtnDesarrolloPersonal_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/desarrolloPersonal.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Desarrollo personal" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Desarrollo personal</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnIdiomas" runat="server" OnClick="BtnIdiomas_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/idiomas.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Idiomas y lenguas" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Idiomas y lenguas</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnArte" runat="server" OnClick="BtnArte_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/arte.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Arte" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Arte</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col">
                        <div class="card h-100">
                            <asp:LinkButton ID="BtnCiencia" runat="server" OnClick="BtnCiencia_Click" Style="text-decoration: none; color: inherit;">
                        <div class="text-center">
                            <img src="Media/ciencia.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Ciencia y tecnología" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title text-center">Ciencia y tecnología</h5>
                        </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="container text-end my-4">
                    <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Descubrí más categorías
                        </button>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:Button ID="BtnCat1" runat="server" CssClass="dropdown-item" Text="Categoria extra 1" OnClick="BtnCat1_Click" /></li>
                            <li>
                                <asp:Button ID="BtnCat2" runat="server" CssClass="dropdown-item" Text="Categoria extra 2" OnClick="BtnCat2_Click" /></li>
                            <li>
                                <asp:Button ID="BtnCat3" runat="server" CssClass="dropdown-item" Text="Categoria extra 3" OnClick="BtnCat3_Click" /></li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
    </main>
</asp:Content>
