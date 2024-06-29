<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webform.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% if (Session["mensaje-login"] != null)
        { %>

    <div class="alert alert-primary m-5" role="alert">
        <%: Session["mensaje-login"] %>
    </div>
    <%}
        Session.Remove("mensaje-login");
    %>

    <section class="vh-100">
        <div class="container-fluid h-custom">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-md-9 col-lg-6 col-xl-5">
                    <img src="Media/login.svg"
                        class="img-fluid" alt="Sample image">
                </div>
                <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">

                    <!-- Email -->
                    <div class="form-outline mb-4">
                        <label class="form-label" for="form1Example13">Email</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                        <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtEmail"
                            CssClass="text-danger small" runat="server" />
                        <asp:RegularExpressionValidator ErrorMessage="Debe ser un correo." 
                            ControlToValidate="txtEmail" runat="server" CssClass="text-danger small"
                            ValidationExpression="^[\d\w_\.]+@[\d\w_\.]+$"/>
                    </div>

                    <!-- Contraseña -->
                    <div class="form-outline mb-3">
                        <label class="form-label" for="form1Example13">Contraseña</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPass" TextMode="Password" />
                        <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtPass"
                            CssClass="text-danger small" runat="server" />
                    </div>

                    <div class="d-flex justify-content-between align-items-center">

                        <a href="Password.aspx" class="text-body">Olvidé mi contraseña</a>
                    </div>

                    <div class="text-center text-lg-start mt-4 pt-2">
                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-lg" OnClick="btnIngresar_Click" />
                        <p class="small fw-bold mt-2 pt-1 mb-0">
                            ¿No tienes una cuenta? <a href="Registro.aspx"
                                class="link-danger">Registrarse</a>
                        </p>
                    </div>

                </div>
            </div>
        </div>


    </section>
</asp:Content>
