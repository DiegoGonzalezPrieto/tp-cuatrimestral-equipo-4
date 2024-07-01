<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="webform.Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex vh-100 justify-content-center mt-5">
        <div class="w-50 w-md-33 mt-5">

            <asp:Panel ID="PanelCambiarPassword" runat="server">

                <!-- Contraseña -->
                <div class="form-outline mb-3">
                    <label class="form-label" for="txtPassActual">Contraseña actual</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPassActual" TextMode="Password" />
                    <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtPassActual"
                        CssClass="text-danger small" runat="server" />
                </div>

                <div class="form-outline mb-3">
                    <label class="form-label" for="txtPassNueva">Nueva contraseña</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPassNueva" TextMode="Password" />
                    <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtPassNueva"
                        CssClass="text-danger small" runat="server" />
                </div>

                <div class="form-outline mb-3">
                    <label class="form-label" for="txtPassNueva2">Repita la contraseña</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPassNueva2" TextMode="Password" />
                    <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtPassNueva2"
                        CssClass="text-danger small" runat="server" />
                    <asp:CompareValidator
                        ID="cvPasswords"
                        runat="server"
                        ControlToCompare="txtPassNueva"
                        ControlToValidate="txtPassNueva2"
                        ErrorMessage="Las contraseñas no coinciden"
                        CssClass="text-danger small"
                        Operator="Equal"
                        Type="String" />
                </div>
                <% if (errorPassword)
                    { %>

                <div class="alert alert-warning" role="alert">
                    Debe ingresar una contraseña de más de 4 caracteres.
                </div>
                <%}%>
                <asp:Label runat="server" ID="lblCambiar"></asp:Label>
                <div class="d-flex justify-content-between align-items-center">
                    <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" CssClass="btn btn-primary btn-lg" OnClick="btnCambiar_Click" />
                </div>
                <div class="d-flex justify-content-between align-items-center">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary btn-lg" OnClick="btnVolver_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelRecuperarPassword" runat="server">
                <!-- Email -->
                <div class="form-outline mb-4">
                    <label class="form-label" for="form1Example13">Ingrese su email, le enviaremos un correo para recuperar su contraseña</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailRecupero" />
                    <asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtEmailRecupero"
                        CssClass="text-danger small" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="Debe ser un correo."
                        ControlToValidate="txtEmailRecupero" runat="server" CssClass="text-danger small"
                        ValidationExpression="^[\d\w_\.]+@[\d\w_\.]+$" />
                </div>
                <asp:Label runat="server" ID="lblMensajeRecupero"></asp:Label>
                <div class="d-flex justify-content-between align-items-center">
                    <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar contraseña" CssClass="btn btn-primary btn-lg" OnClick="btnRecuperar_Click" />
                </div>

            </asp:Panel>

        </div>
    </div>



</asp:Content>
