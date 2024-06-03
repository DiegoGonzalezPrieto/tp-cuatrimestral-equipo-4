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
            <div class="mb-3 d-flex align-items-center">
                <label for="Categorias" class="form-label me-2">Categoria:</label>
                <asp:DropDownList ID="DDLCategorias1" CssClass="form-select me-2" Style="margin-bottom: 5px;" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="DDLCategorias2" CssClass="form-select me-2" Style="margin-bottom: 5px;" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="DDLCategorias3" CssClass="form-select" Style="margin-bottom: 5px;" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <div class="form-check">
                    <asp:CheckBox ID="chkHabilitarComentario"  type="checkbox" runat="server" />
                    
                    <label class="form-check-label" for="flexCheckDefault">
                        Habilitar Comentarios
                    </label>
                </div>
                <div class="form-check">
                    <asp:CheckBox ID="chkDisponible"  type="checkbox" Checked="true" runat="server" />
                   
                    <label class="form-check-label" for="flexCheckChecked">
                        Disponible
                    </label>
                </div>
            </div>
            <div>
                <asp:Button ID="btnGuardarNuevoCurso" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarNuevoCurso_Click" data-bs-toggle="modal" data-bs-target="#exampleModal" runat="server" />
            </div>


            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">Curso Agregado</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <label id="lblNombreCursoAgregado" runat="server"></label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Aceptar" CssClass="btn btn-primary" ID="btnModalAceptar" OnClick="btnModalAceptar_Click" runat="server" />

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
