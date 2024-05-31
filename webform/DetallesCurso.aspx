<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetallesCurso.aspx.cs" Inherits="webform.DetallesCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; align-items: center; margin: 20px; background: black; color: aliceblue;">
        <h1 style="">Detalles del curso</h1>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="col">
                    <div class="card h-100">
                        <asp:LinkButton ID="BtnMarketing" runat="server" Style="text-decoration: none; color: inherit;">
                            <div class="text-center">
                                <img src="Media/marketing.svg" class="card-img-top img-fluid" style="width: 80%;" alt="Marketing y negocios" />
                            </div>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <ul class="list-group">
                    <li class="list-group-item">
                        <span class="fw-bold">Nombre:</span> HTML
                    </li>
                    <li class="list-group-item" style="height: ;">
                        <span class="fw-bold">Descripción:</span> HTML es el lenguaje con el que se define el contenido de las páginas web. 
                        Básicamente se trata de un conjunto de etiquetas que sirven para definir el texto y otros elementos que compondrán una página web,
                        como imágenes, listas, vídeos, etc.
                        El HTML se creó en un principio con objetivos divulgativos de información con texto y algunas imágenes. 
                        No se pensó que llegara a ser utilizado para crear área de ocio y consulta con carácter multimedia (lo que es actualmente la web),
                        de modo que, el HTML se creó sin dar respuesta a todos los posibles usos que se le iba a dar y a 
                        todos los colectivos de gente que lo utilizarían en un futuro. Sin embargo, pese a esta deficiente planificación, 
                        si que se han ido incorporando modificaciones con el tiempo, estos son los estándares del HTML. Numerosos estándares
                        se han presentado ya. El HTML 4.01 es el último estándar a febrero de 2001.
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Fecha de Publicacion:</span> 25/05/2024
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Categoria:</span> Programacion Web
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Cantidad de capitulos:</span> 10
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Estado:</span> Activo
                    </li>
                    <li class="list-group-item">
                        <span class="fw-bold">Costo:</span> $15.000
                    </li>
                </ul>
                <div class="container p-2 .me">
                    <button type="button" class="btn btn-success"> Inscribirse! </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
