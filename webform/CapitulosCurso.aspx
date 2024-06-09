<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CapitulosCurso.aspx.cs" Inherits="webform.CapitulosCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-title" style="display:flex; justify-content:center; align-items:center; margin:20px; font-size:30px; color:dimgrey;">
        <asp:Label ID="lblTituloCurso" Text="Sin titulo" runat="server" />
    </div>
    
    <div class="col-md-12 mb-2">
        <!--<asp:Button ID="btnNuevoCapitulo" Text="Nuevo Capitulo" OnClick="btnNuevoCapitulo_Click" CssClass="btn btn-success" runat="server" />
             <br />-->
        <button id="btnNuevoCapitul" type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#ModalNuevoCapitulo" runat="server">Nuevo Capitulo</button>
    </div>

    <asp:Label ID="lblCapitulo" Text="No hay capitulos. Debe agregar uno." Style="color: red; font-size: small; margin-bottom: 1px;" runat="server" />

    <div class="col-md-12 table-responsive">

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Orden</th>
                    <th>Nombre</th>
                    <th>Cantidad</th>
                    <th>Contenidos</th>
                    <th>Liberado</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCapitulos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Orden") %></td>
                            <td><%#Eval("Nombre") %></td>
                            <td>5 </td>
                            <td>
                                <asp:Button ID="btnVer" Text="Ver Contenido" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnVer_Click" runat="server" />
                            </td>
                            <td><%# (bool)Eval("Liberado") ? "Disponible" : "No Disponible" %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>



    <div class="modal fade" id="ModalNuevoCapitulo" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Curso Agregado</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <label>Nombre Nuevo Capitulo</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control form-control-sm" runat="server" />

                </div>
                <div class="modal-footer">
                    <asp:Button Text="Agregar Capitulo" CssClass="btn btn-sm btn-success" ID="btnModalAceptar" OnClick="btnNuevoCapitulo_Click" runat="server" />

                </div>
            </div>
        </div>
    </div>

</asp:Content>
