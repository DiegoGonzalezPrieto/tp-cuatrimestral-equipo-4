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
        #ContentPlaceHolder1_txtAreaTexto{
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
                <asp:TextBox ID="txtNombreContenido" CssClass="form-control" type="text" placeholder="Nombre Contenido" runat="server" />
            </div>
            <div class="m-4 d-flex d-flex-nowrap">
                <asp:Label ID="lblTipoContenido" class="form-label me-2" runat="server" Text="Tipo Contenido"></asp:Label>
                <asp:DropDownList ID="DDLTipoContenido" CssClass="form-select form-select-small" OnSelectedIndexChanged="DDLTipoContenido_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>
            <div class="m-4">
                <asp:Label ID="lblTextoContenido" runat="server" Text="Texto"></asp:Label>
                <div class="form-floating">
                    <textarea class="form-control" placeholder="Escribe tu contenido aquí" id="txtAreaTexto" runat="server"></textarea>
                    <label for="txtAreaTexto">Texto de Contenido</label>
                </div>
            </div>
            <div class="m-4">
                <asp:Label ID="lblArchivo" runat="server" Text="Archivo PDF o Imagen(JPG o SVG)"></asp:Label>
                <asp:FileUpload ID="FileUpload1" CssClass="form-control form-control-sml" type="file" placeholder="Buscar Archivo" runat="server" />
            </div>
            <div class="m-4">
                <asp:Label ID="lblUrlVideo" runat="server" Text="Url de Youtube"></asp:Label>
                <asp:TextBox ID="txtUrlVideo" CssClass="form-control" type="text" placeholder="Ejemplo: https://www.youtube.com/watch?v=kNZQFbCeWcQ&ab_channel=DanaVicci" runat="server"></asp:TextBox>
            </div>
            <div style="display: flex; justify-content: center; align-items: center;">
                <asp:Button ID="btnGuardarContenido" CssClass="btn btn-success" OnClick="btnGuardarContenido_Click" runat="server" Text="Guardar Contenido" />
            </div>
        </div>
    </div>
</asp:Content>
