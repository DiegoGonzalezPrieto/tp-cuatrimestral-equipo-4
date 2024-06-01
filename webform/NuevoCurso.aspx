<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NuevoCurso.aspx.cs" Inherits="webform.NuevoCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formularioNuevoCurso">
        <div class="tituloPagina">
            <h1>Nuevo Curso</h1>
        </div>
        <div class="fNuevoCurso">
            <div class="mb-3">
                <label for="Nombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="nombreCurso" CssClass="form-control form-control-sm" type="text" placeholder="Nombre Curso" runat="server" />
            </div>
            <div class="mb-3">
                <label for="Descripcion" class="form-label">Descripcion:</label>
                <asp:TextBox ID="descripcionCurso" CssClass="form-control form-control-sm" type="text" Rows="3" placeholder="Descripcion del curso" runat="server" />

            </div>
            <div class="mb-3">
                <label for="Costo" class="form-label">Costo:</label>
                <asp:TextBox ID="costoCurso" CssClass="form-control form-control-sm" type="text" placeholder="$50.000" runat="server" />
            </div>
            <div class="mb-3">
                <label for="Etiquetas" class="form-label">Etiquetas:</label>
                <asp:TextBox ID="etiquetasCurso" CssClass="form-control form-control-sm" type="text" placeholder="Palabras clave" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ImagenPortada" class="form-label">Portada del Curso</label>
                <asp:FileUpload ID="ImagenCategoria" CssClass="form-control form-control-sml" type="file" placeholder="Buscar Archivo" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnGuardarNuevoCurso" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarNuevoCurso_Click" runat="server" />
            </div>
        </div>


    </div>

</asp:Content>
