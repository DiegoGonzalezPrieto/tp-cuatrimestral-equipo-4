<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DatosUsuario.aspx.cs" Inherits="webform.DatosUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }

        .a1 {
            display: flex;
            justify-content: space-around;
            align-items: center;
            border: 1px solid grey;
            color: darkgray;
            width: 50%;
            margin-top: 20px;
            margin-bottom: 50px;
            padding: 10px;
            border-radius: 5px;
        }

        .inter {
            margin-bottom: 20px;
        }
    </style>
    <script>
        function abrirModal(nombreCurso) {
            $('#ModalPublicacion').modal('show');

            console.log("Curso seleccionado: " + nombreCurso);
        }
    </script>
    <!--SCRIPT PARA 3 DIAS -->
    <!-- 
    <script>
        var temporizadores = {};

        function iniciarTemporizador(filaId, duracion) {
            var tiempo = duracion;
            var pantalla = document.getElementById('tiempo_' + filaId);
            clearInterval(temporizadores[filaId]);

            temporizadores[filaId] = setInterval(function () {
                var dias = parseInt(tiempo / (60 * 60 * 24), 10);
                var horas = parseInt((tiempo % (60 * 60 * 24)) / (60 * 60), 10);
                var minutos = parseInt((tiempo % (60 * 60)) / 60, 10);
                var segundos = parseInt(tiempo % 60, 10);

                dias = dias < 10 ? "0" + dias : dias;
                horas = horas < 10 ? "0" + horas : horas;
                minutos = minutos < 10 ? "0" + minutos : minutos;
                segundos = segundos < 10 ? "0" + segundos : segundos;

                pantalla.textContent = dias + "d " + horas + ":" + minutos + ":" + segundos;

                if (--tiempo < 0) {
                    clearInterval(temporizadores[filaId]);
                    pantalla.textContent = "00d 00:00:00";
                    localStorage.removeItem("temporizador_" + filaId);
                    manejarTemporizadorFinalizado(filaId);
                } else {
                    localStorage.setItem("temporizador_" + filaId, tiempo);
                }
            }, 1000);
        }

        function manejarAccion(filaId, accion) {
            var claveInicioTiempo = "horaInicio_" + filaId;
            var horaActual = Math.floor(Date.now() / 1000);
            var duracion = 60 * 60 * 72; 

            if (accion === 'suspender') {
                reiniciarTemporizador(filaId);
            } else if (accion === 'activar') {
                localStorage.setItem(claveInicioTiempo, horaActual);
                iniciarTemporizador(filaId, duracion);
            }
        }

        function reiniciarTemporizador(filaId) {
            clearInterval(temporizadores[filaId]);
            var pantalla = document.getElementById('tiempo_' + filaId);
            pantalla.textContent = "03D 00:00:00"; 
            localStorage.removeItem("temporizador_" + filaId);
            localStorage.removeItem("horaInicio_" + filaId);
        }

        function restaurarTemporizadores() {
            var filas = document.querySelectorAll('[id^="tiempo_"]');
            filas.forEach(function (fila) {
                var filaId = fila.id.split('_')[1];
                var tiempoAlmacenado = parseInt(localStorage.getItem("temporizador_" + filaId), 10);
                if (tiempoAlmacenado && tiempoAlmacenado > 0) {
                    var horaInicio = parseInt(localStorage.getItem("horaInicio_" + filaId), 10);
                    var horaActual = Math.floor(Date.now() / 1000);
                    var transcurrido = horaActual - horaInicio;
                    if (transcurrido < 60 * 60 * 72) { 
                        iniciarTemporizador(filaId, tiempoAlmacenado - transcurrido);
                    } else {
                        reiniciarTemporizador(filaId);
                    }
                }
            });
        }

        function manejarTemporizadorFinalizado(filaId) {
            __doPostBack('tiempoFinalizado', filaId); 
        }

        window.onload = function () {
            restaurarTemporizadores();
        };
    </script>
        -->
    <!-- SCRIPT PARA 5 MINUITOS-->
    <script>
        var temporizadores = {};

        function iniciarTemporizador(filaId, duracion) {
            var tiempo = duracion;
            var pantalla = document.getElementById('tiempo_' + filaId);
            clearInterval(temporizadores[filaId]);

            temporizadores[filaId] = setInterval(function () {
                var minutos = parseInt(tiempo / 60, 10);
                var segundos = parseInt(tiempo % 60, 10);

                minutos = minutos < 10 ? "0" + minutos : minutos;
                segundos = segundos < 10 ? "0" + segundos : segundos;

                pantalla.textContent = minutos + ":" + segundos;

                if (--tiempo < 0) {
                    clearInterval(temporizadores[filaId]);
                    pantalla.textContent = "00:00";
                    localStorage.removeItem("temporizador_" + filaId);
                    manejarTemporizadorFinalizado(filaId);
                } else {
                    localStorage.setItem("temporizador_" + filaId, tiempo);
                }
            }, 1000);
        }

        function manejarAccion(filaId, accion) {
            var claveInicioTiempo = "horaInicio_" + filaId;
            var horaActual = Math.floor(Date.now() / 1000);
            var duracion = 60 * 1;

            if (accion === 'suspender') {
                reiniciarTemporizador(filaId);
            } else if (accion === 'activar') {
                localStorage.setItem(claveInicioTiempo, horaActual);
                iniciarTemporizador(filaId, duracion);
            }
        }

        function reiniciarTemporizador(filaId) {
            clearInterval(temporizadores[filaId]);
            var pantalla = document.getElementById('tiempo_' + filaId);
            pantalla.textContent = "01:00";
            localStorage.removeItem("temporizador_" + filaId);
            localStorage.removeItem("horaInicio_" + filaId);
        }

        function restaurarTemporizadores() {
            var filas = document.querySelectorAll('[id^="tiempo_"]');
            filas.forEach(function (fila) {
                var filaId = fila.id.split('_')[1];
                var tiempoAlmacenado = parseInt(localStorage.getItem("temporizador_" + filaId), 10);
                if (tiempoAlmacenado && tiempoAlmacenado > 0) {
                    var horaInicio = parseInt(localStorage.getItem("horaInicio_" + filaId), 10);
                    var horaActual = Math.floor(Date.now() / 1000);
                    var transcurrido = horaActual - horaInicio;
                    if (transcurrido < 300) {
                        iniciarTemporizador(filaId, tiempoAlmacenado - transcurrido);
                    } else {
                        reiniciarTemporizador(filaId);
                    }
                }
            });
        }

        function manejarTemporizadorFinalizado(filaId) {
            __doPostBack('tiempoFinalizado', filaId);
        }

        window.onload = function () {
            restaurarTemporizadores();
        };
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="display: flex; justify-content: center; margin: 50px;">
            <h1>Datos Personales</h1>
        </div>

        <div class="a1">
            <asp:Image ID="ImgFotoPerfil" runat="server" CssClass="profile-circle" />
            <asp:Label ID="lblUserName" Text="UserName" runat="server" />
            <asp:Button ID="btnSuspenderUsuario" OnClick="btnSuspenderUsuario_Click" runat="server" />



        </div>
        <div style="display: flex; justify-content: space-around; align-items: center; color: darkgray; margin: 20px 20px 40px 20px; width: 50%;">
            <asp:Label ID="lblNombreUsuario" Text="Nombre" runat="server" />
            <asp:Label ID="lblApellidoUsuario" Text="Apellido" runat="server" />
        </div>
        <div style="display: grid; color: darkgray; margin: 20px">
            <asp:Label ID="lblFechaNacimiento" Text="FechaNacimiento" runat="server" CssClass="inter" />
            <asp:Label ID="lblProfesion" Text="Profesion" runat="server" CssClass="inter" />
            <asp:Label ID="lblBibliografia" Text="Bibliografia" runat="server" CssClass="inter" />
        </div>

    </div>
    <div>
        <h3 style="display: flex; justify-content: center; margin: 25px;">Datos de Cursos</h3>
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
            <asp:GridView ID="gvCursosUsuario" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" ShowHeader="true">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Capitulos.Count" HeaderText="Capitulos" />
                    <asp:BoundField DataField="NombresCategorias" HeaderText="Categorías" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# (bool)Eval("Disponible") ? "Disponible" : "No Disponible" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accion">
                        <ItemTemplate>
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnSuspender" Text='<%# (bool)Eval("suspencionCurso") ? "Activar" : "Suspender" %>' CssClass='<%# (bool)Eval("suspencionCurso") ? "btn btn-sm btn-outline-success" : "btn btn-sm btn-outline-secondary" %>'
                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnSuspender_Click" data-bs-toggle="modal" data-bs-target="#ModalPublicacion" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiempo">
                        <ItemTemplate>
                            <!--
                                <span id="tiempo_<%# Eval("Id") %>">03D 00:00:00</span>
                             -->
                            <span id="tiempo_<%# Eval("Id") %>">01:00</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="display: flex; justify-content: center; align-items: center; margin: 25px;">
            <asp:Button ID="Volver" Text="Volver" CssClass="btn btn-secondary" OnClick="Volver_Click" runat="server" />
        </div>
        <!--Modal Aviso para Activar/Desactivar -->
        <div class="modal fade" id="ModalPublicacion" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalPublicacion">Opciones de Publicacion</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="txtActivarDesactivar" Text="“Está a punto de realizar una acción que modificará la visibilidad del contenido del curso en la plataforma.
                    ¿Confirma que desea proceder con la activación/suspencion de la publicación del curso?”"
                            runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-danger" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnSuspenderActivar" Text="Aceptar" CssClass="btn btn-sm btn-success" OnClick="btnSuspenderActivar_Click" runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
