<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webform.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            <input type="email" id="form3Example3" class="form-control form-control-lg"/>
             <label class="form-label" for="form1Example13">Email</label>
          </div>

          <!-- Contraseña -->
          <div class="form-outline mb-3">
            <input type="password" id="form3Example4" class="form-control form-control-lg"/>
             <label class="form-label" for="form1Example13">Contraseña</label>
          </div>

          <div class="d-flex justify-content-between align-items-center">

            <!-- Checkbox -->
            <div class="form-check mb-0">
              <input class="form-check-input me-2" type="checkbox" value="" id="form2Example3" />
              <label class="form-check-label" for="form2Example3">
                Mantenerse logueado
              </label>
            </div>
            <a href="#!" class="text-body">Olvidé mi contraseña</a>
          </div>

          <div class="text-center text-lg-start mt-4 pt-2">
          <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-lg" OnClick="btnIngresar_Click"/>
            <p class="small fw-bold mt-2 pt-1 mb-0">¿No tienes una cuenta? <a href="Registro.aspx"
                class="link-danger">Registrarse</a></p>
          </div>

      </div>
    </div>
  </div>


</section>
</asp:Content>
