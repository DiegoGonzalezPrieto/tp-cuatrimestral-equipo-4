﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NuevoCurso.aspx.cs" Inherits="webform.NuevoCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

    <div class="formularioNuevoCurso">
        <div class="tituloPagina">
            <h1>
                <asp:Label ID="tituloNuevoCurso" Text="Nuevo Curso" runat="server" />
            </h1>

        </div>
        <div class="fNuevoCurso">
            <div style="display: flex; justify-content: center; align-items: center; color: forestgreen;">
                <h4>
                    <asp:Label ID="lblAvisoDeGuardado" Text="Curso guardado exitosamente!" runat="server" />
                </h4>
            </div>
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
                <asp:FileUpload ID="ImagenCurso" CssClass="form-control form-control-sml" type="file" placeholder="Buscar Archivo" runat="server" />
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3 d-flex align-items-center">
                        <label for="Categorias" class="form-label me-2">Categoria:</label>

                        <asp:DropDownList ID="DDLCategorias1" CssClass="form-select me-2" Style="margin-bottom: 5px;"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLCategorias1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DDLCategorias2" CssClass="form-select me-2" Style="margin-bottom: 5px;"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLCategorias2_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DDLCategorias3" CssClass="form-select" Style="margin-bottom: 5px;"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLCategorias3_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
            <% if (categoriaRepetida)
                { %>
            <div class="alert alert-warning" role="alert" id="alertCategorias" visible="false">
                No se pueden seleccionar categorías repetidas.
            </div>

            <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="mb-3">
                <div class="form-check">
                    <asp:CheckBox ID="chkHabilitarComentario" type="checkbox" runat="server" />

                    <label class="form-check-label" for="flexCheckDefault">
                        Habilitar Comentarios
                    </label>
                </div>
                <div class="form-check">
                    <asp:CheckBox ID="chkDisponible" type="checkbox" Checked="true" runat="server" />

                    <label class="form-check-label" for="flexCheckChecked">
                        Disponible
                    </label>
                </div>
            </div>
            <div style="display: flex; justify-content: space-around; align-items: center;">
                <asp:Button ID="btnGuardarNuevoCurso" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarNuevoCurso_Click" runat="server" />
                <asp:Button ID="btnVolver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" runat="server" Text="Volver" />
            </div>
        </div>
</asp:Content>
