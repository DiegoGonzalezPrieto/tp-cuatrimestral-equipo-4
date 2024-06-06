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
              <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"/>
            <label class="form-label" for="form1Example13">Nombre de usuario</label>
          </div>

          <!-- Email input -->
          <div class="form-outline mb-4">
              <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"/>
            <label class="form-label" for="form1Example13">Email</label>
          </div>

          <!-- Password input -->
          <div class="form-outline mb-4">
              <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password"/>
            <label class="form-label" for="form1Example23">Contraseña</label>
          </div>

          <div class="d-flex justify-content-around align-items-center mb-4">

            <a href="#!">Restablecer contraseña</a>
          </div>

          <!-- Submit button -->
          <asp:Button ID="btnIngresar" runat="server" Text="Registrarme" CssClass="btn btn-primary btn-lg" OnClick="btnIngresar_Click"/>

      </div>
    </div>
  </div>
</section>
</asp:Content>
