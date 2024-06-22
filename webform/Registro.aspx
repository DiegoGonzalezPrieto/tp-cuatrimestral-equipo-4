<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="webform.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="vh-100">
        <div class="container-fluid h-custom">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-md-9 col-lg-6 col-xl-5">
                    <img src="Media/registro.svg"
                        class="img-fluid" alt="Sample image">
                </div>
                <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">

                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" ClientIDMode="Static" />
                        <label class="form-label" for="txtNombre">Nombre de usuario</label>
                    </div>

                    <!-- Email input -->
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ClientIDMode="Static" />
                        <label class="form-label" for="txtEmail">Email</label>
                    </div>

                    <!-- Password input -->
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" ClientIDMode="Static" />
                        <label class="form-label" for="txtPassword">Contraseña</label>
                    </div>

                    <div class="d-flex justify-content-around align-items-center mb-4">

                        <a href="#!">Restablecer contraseña</a>
                    </div>

                    <% if (errorEmail)
                        { %>

                    <div class="alert alert-warning" role="alert">
                        Debe ingresar un correo válido.
                    </div>
                    <%}%>
                    
                    <% if (errorEmailExistente)
                        { %>

                    <div class="alert alert-warning" role="alert">
                        El correo ya se encuentra registrado en la plataforma.
                    </div>
                    <%}%>

                    <% if (errorNombre)
                        { %>

                    <div class="alert alert-warning" role="alert">
                        Debe ingresar un nombre de usuario válido.
                    </div>
                    <%}%>

                    <% if (errorPassword)
                        { %>

                    <div class="alert alert-warning" role="alert">
                        Debe ingresar una contraseña de más de 4 caracteres.
                    </div>
                    <%}%>

                    <!-- Submit button -->
                    <asp:Button ID="btnIngresar" runat="server" Text="Registrarme" CssClass="btn btn-primary btn-lg" OnClick="btnIngresar_Click" />

                </div>
            </div>
        </div>
    </section>
</asp:Content>
