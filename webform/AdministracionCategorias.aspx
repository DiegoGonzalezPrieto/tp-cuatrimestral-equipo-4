<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministracionCategorias.aspx.cs" Inherits="webform.AdministracionCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-custom">
        <div class="mb-3">
            <label>Ingresar Nombre:</label>
            <asp:TextBox ID="NombreCategoria" CssClass="form-control" type="text" runat="server" />
        </div>
        <div class="mb-3">
            <label for="formFile" class="form-label">Carga de Imagen</label>

            <asp:FileUpload ID="ImagenCategoria" CssClass="form-control" type="file" runat="server" />
            <asp:Label ID="lblId" Text="" runat="server" Visible="false" />
        </div>

        <div>
            <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" />
        </div>
    </div>

    <div class="container mb-3">
        <div class="row">
            <asp:Repeater ID="repCategorias" runat="server">
                <ItemTemplate>
                    <div class="col-4 mb-3">
                        <div class="card h-100 py-4">
                            <asp:LinkButton ID="BtnSoftware" runat="server" Style="text-decoration: none; color: inherit;">
                                <div class="text-center">
                                    <img src="<%#Eval("ImagenDataUrl") %>" class="card-img-top img-fluid" style="width: 30%;" />
                                </div>
                            </asp:LinkButton>
                            <div class="card-body">
                                    <span class="position-absolute top-0 start-50 translate-middle badge rounded-pill bg-<%# (bool)Eval("Activo") ? "primary" : "secondary" %>">
                                        <%# (bool)Eval("Activo") ? "Activo" : "Inactiva" %>
                                    </span>
                                <h5 class="card-title text-center"><%# Eval("Nombre") %>
                                </h5>

                                <div class="justify-content-center d-flex py-4">
                                    <asp:Button ID="btnEditar" Text="Editar" runat="server" CssClass="btn btn-warning mx-1"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnEditar_Click" />
                                    <asp:Button ID="btnDesactivar" Text='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>' 
                                        runat="server" CssClass='<%# (bool)Eval("Activo") ? "btn btn-secondary mx-1" : "btn btn-primary mx-1" %>'
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnDesactivar_Click" />
                                    <asp:Button ID="btnEliminar" Text="Eliminar" runat="server" CssClass="btn btn-danger mx-1"
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnEliminar_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>


</asp:Content>
