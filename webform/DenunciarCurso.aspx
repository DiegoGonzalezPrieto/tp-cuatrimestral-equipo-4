<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DenunciarCurso.aspx.cs" Inherits="webform.DenunciarCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    function showSpinner() {
        var spinner = document.getElementById('spinner');
        spinner.classList.remove('d-none');
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-success tituloPagina" Visible="false"></asp:Label>
     <div class="tituloPagina">
         <h1>
             <asp:Label ID="tituloDenuncia" Text="Formulario de denuncia" runat="server" />
         </h1>
     </div>

     <div class="fDenuncia">
         <div class="mb-3">
             <label class="form-label">Nombre del curso:</label>
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
             <label class="form-label">Motivo de denuncia</label>
             <asp:DropDownList ID="DDLTipoDenuncia" CssClass="form-control form-control-sml" type="file" placeholder="Buscar Archivo" runat="server" />
         </div>
         <div class="mb-3">
             <label class="form-label me-2">Describa el motivo de su denuncia:</label>
                <asp:TextBox ID="motivoDenuncia" CssClass="form-control form-control-sm" type="text" TextMode="MultiLine" Rows="4" runat="server" />
             <asp:RequiredFieldValidator ErrorMessage="Completar motivo." ControlToValidate="motivoDenuncia" 
                 CssClass="text-danger small" runat="server" />
         </div>

     </div>
    <div class="d-flex align-items-center mb-3">
<asp:Button ID="btnEnviarDenuncia" Text="Enviar denuncia" CssClass="btn btn-success" OnClick="btnEnviarDenuncia_Click" OnClientClick="showSpinner();" runat="server" />
<div id="spinner" class="spinner-border text-success d-none" role="status" style="margin-left: 12px;"></div>

    </div>

    


</asp:Content>
