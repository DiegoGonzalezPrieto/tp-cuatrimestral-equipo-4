<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NuevoContenido.aspx.cs" Inherits="webform.NuevoContenido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-select-small {
            width: 150px;
        }

        .d-flex-nowrap {
            white-space: nowrap;
            align-items: center;
        }

        .m-3 {
            margin: 30px 0px 30px 0px !important;
        }

        #ContentPlaceHolder1_txtAreaTexto {
            min-height: 6em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="formularioNuevoContenido" style="width: 75%; margin: auto;">
        <div class="tituloPagina">
            <h1>
                <asp:Label ID="lblTituloNuevoContenido" Text="Nuevo Contenido" runat="server" />
            </h1>
        </div>
        <div class="fNuevoContenido" style="width: 50%; margin: auto;">
            <div style="display: flex; justify-content: center; align-items: center; color: forestgreen;">
                <h4>
                    <asp:Label ID="lblAvisoDeGuardado" Text="Contenido guardado exitosamente!" runat="server" />
                </h4>

            </div>
            <div class="m-4">
                <asp:Label Text="Nombre" CssClass="form-label" runat="server" />
                <asp:TextBox ID="txtNombreContenido" CssClass="form-control" type="text" placeholder="Nombre del Contenido" runat="server" />
                <asp:RequiredFieldValidator ErrorMessage="Nombre requerido" ControlToValidate="txtNombreContenido"
                    CssClass="text-danger small" runat="server" />
            </div>
            <div class="m-4 d-flex d-flex-nowrap">
                <asp:Label ID="lblTipoContenido" class="form-label me-2" runat="server" Text="Tipo Contenido"></asp:Label>
                <asp:DropDownList ID="DDLTipoContenido" CssClass="form-select form-select-small"
                    OnSelectedIndexChanged="DDLTipoContenido_SelectedIndexChanged" AutoPostBack="true" runat="server">
                </asp:DropDownList>
            </div>
            <% if (errorTipoContenido)
                { %>
            <div class="text-danger small px-5">
                Debe seleccionar un tipo de contenido.
            </div>

            <%}%>

            <div class="m-4">
                <asp:Label ID="lblTextoContenido" runat="server" Text="Texto"></asp:Label>
                <div class="form-floating">
                    <textarea class="form-control" placeholder="Escribe tu contenido aquí" id="txtAreaTexto" runat="server"
                        required></textarea>
                    <label for="txtAreaTexto">Texto de Contenido</label>
                </div>
            </div>
            <% if (tipoElegido == "PDF")
                { %>
            <div class="m-4">
                <asp:Label ID="lblArchivo" runat="server" Text="Archivo PDF"></asp:Label>
                <asp:FileUpload ID="FileUpload1" CssClass="form-control form-control-sml" accept=".pdf"
                    type="file" placeholder="Buscar Archivo" runat="server" />
                <% if (errorPdf)
                    { %>
                <div class="text-danger small">Debe ingresar un archivo PDF.</div>
                <%}%>
            </div>
            <%}%>

            <% if (tipoElegido == "VIDEO")
                { %>
            <div class="m-4">
                <asp:Label ID="lblUrlVideo" runat="server" Text="Url de Youtube (embebible)"></asp:Label>
                <asp:TextBox ID="txtUrlVideo" CssClass="form-control" type="text"
                    placeholder="Ejemplo: https://www.youtube.com/embed/Y2B6yJIahZ0" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexVideo" runat="server" CssClass="text-danger small"
                    ErrorMessage="URL debe ser: https://www.youtube.com/embed/[id_del_video]"
                    ValidationExpression="^https://www\.youtube\.com/embed/[a-zA-Z0-9_\-]{11}$"
                    ControlToValidate="txtUrlVideo"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ErrorMessage="Url requerida" ControlToValidate="regexVideo" runat="server" />--%>
                <% if (errorVideo)
                    { %>
                <div class="text-danger small">Debe ingresar una URL de video.</div>
                <%}%>
            </div>

            <%}%>
            <div style="display: flex; justify-content: space-around; align-items: center;">
                <% if (guardado)
                    { %>
                <a href="ContenidoCurso.aspx" class="btn btn-success">Aceptar</a>
                <%}
                    else
                    { %>
                <asp:Button ID="btnGuardarContenido" CssClass="btn btn-success" OnClick="btnGuardarContenido_Click"
                    runat="server" Text="Guardar Contenido" />
                <a href="ContenidoCurso.aspx" class="btn btn-secondary">Volver</a>
                <%} %>
            </div>
        </div>
    </div>
</asp:Content>
