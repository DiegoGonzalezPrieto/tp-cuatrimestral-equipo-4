<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="webform.Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblMensaje" runat="server" Font-Size="X-Large"></asp:Label>
    <nav class="navbar bg-body-tertiary">
        <div class="container-fluid">
            <a class="navbar-brand">Categoria: </a>
            <div class="d-flex" role="search">
                <input class="form-control me-2" type="search" placeholder="Buscar" aria-label="Search">
                <button class="btn btn-outline-success" type="submit">
                    <img src="Media/ico/buscar.png" alt="OK!" />
                </button>
            </div>
        </div>
    </nav>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <div class="col">
            <div class="card h-100">
                <asp:LinkButton ID="BtnMarketing" runat="server" Style="text-decoration: none; color: inherit;">
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
                <asp:LinkButton ID="BtnSoftware" runat="server" Style="text-decoration: none; color: inherit;">
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
                <asp:LinkButton ID="BtnDesarrolloPersonal" runat="server" Style="text-decoration: none; color: inherit;">
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
                <asp:LinkButton ID="BtnIdiomas" runat="server" Style="text-decoration: none; color: inherit;">
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
                <asp:LinkButton ID="BtnArte" runat="server" Style="text-decoration: none; color: inherit;">
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
                <asp:LinkButton ID="BtnCiencia" runat="server" Style="text-decoration: none; color: inherit;">
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

</asp:Content>
