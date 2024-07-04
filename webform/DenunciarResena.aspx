<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DenunciarResena.aspx.cs" Inherits="webform.DenunciarResena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function showSpinner() {
            var spinner = document.getElementById('spinner');
            spinner.classList.remove('d-none');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% if (!mostrarDenuncia)
        { %>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-success tituloPagina" Visible="false"></asp:Label>
    <div class="tituloPagina">
        <h1>
            <asp:Label ID="tituloDenuncia" Text="Formulario de denuncia" runat="server" />
        </h1>
    </div>

    <div class="fDenuncia">
        <div class="mb-3">
            <label class="form-label">Nombre del curso en que está la reseña:</label>
            <asp:TextBox ID="nombreCurso" CssClass="form-control form-control-sm" type="text" ReadOnly="true" runat="server" />
        </div>
        <div class="mb-3">
            <label class="form-label">Email de contacto:</label>
            <asp:TextBox ID="emailUsuario" CssClass="form-control form-control-sm" type="text" ReadOnly="true" runat="server" />
        </div>
        <div class="mb-3">
            <label class="form-label">Fecha de denuncia:</label>
            <asp:TextBox ID="fechaDenuncia" CssClass="form-control form-control-sm" type="text" ReadOnly="true" runat="server" />
        </div>
        <div class="mb-3">
            <label class="form-label me-2">Texto de la reseña</label>
            <asp:TextBox ID="txtResena" CssClass="form-control form-control-sm" type="text" TextMode="MultiLine"
                Rows="4" runat="server" ReadOnly="true" />
        </div>
        <div class="mb-3">
            <label class="form-label me-2">Describa el motivo de su denuncia:</label>
            <asp:TextBox ID="motivoDenuncia" CssClass="form-control form-control-sm" type="text" TextMode="MultiLine" Rows="4" MaxLength="499" runat="server" />
            <asp:RequiredFieldValidator ErrorMessage="Completar motivo." ControlToValidate="motivoDenuncia"
                CssClass="text-danger small" runat="server" />
        </div>

    </div>
    <div class="d-flex align-items-center mb-3">
        <asp:Button ID="btnEnviarDenuncia" Text="Enviar denuncia" CssClass="btn btn-success" OnClick="btnEnviarDenuncia_Click" OnClientClick="showSpinner();" runat="server" />
        <div id="spinner" class="spinner-border text-success d-none" role="status" style="margin-left: 12px;"></div>

    </div>

    <% }
        else
        {  %>
    <div class="card p-5 mt-5 shadow-lg">
        <div class="card-body">
            <h5 class="card-title">Denuncia Realizada</h5>
            <h6 id="fechaDenunciaRealizada" class="card-subtitle mb-4 text-muted" runat="server"></h6>
            <asp:Label ID="lblDenunciaRealizada" CssClass="card-text" Text="" runat="server" />
        </div>
        <div class="text-center">
            <asp:LinkButton ID="btnVolver" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click" runat="server" />
        </div>
    </div>

    <% } %>
</asp:Content>
