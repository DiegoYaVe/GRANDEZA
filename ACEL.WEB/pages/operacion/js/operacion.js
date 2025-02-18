
window.filterClientes = function () {
    var criterio = document.getElementById("sortingField").value;
    var orden = "asc";

    console.log("Citerio: ", criterio);

    $.ajax({
        type: "POST",
        url: "clientes.aspx/FiltrarInversionistas",
        data: JSON.stringify({ criterio: criterio, orden: orden }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var inversionistas = JSON.parse(response.d);
            actualizarTabla(inversionistas);
            console.log("Response inversionistas: ", inversionistas);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", xhr.responseText);
            alert("Error en la solicitud: " + xhr.responseText);
        }
    });
}

window.GuardarInversionista = function () {
    var orden = "asc";

    var pidRegistro = document.getElementById("hfidRegister").value;
    var pNom = document.getElementById("txtNom").value;
    var pCliente = document.getElementById("cmbCliente").value;
    var pCorreo = document.getElementById("txtCorreo").value;
    var pTelefono = document.getElementById("txtTelefono").value;
    var pCertificado = document.getElementById("txtCertificado").value;

    var cmbInversionista = document.getElementById("cmbInversionista");
    var pidInversionista = cmbInversionista.value;
    var pInversionista = cmbInversionista.options[cmbInversionista.selectedIndex].text;

    //var pInversionista = document.getElementById("cmbInversionista").value;

    var cmbEventos = document.getElementById("cmbEventos");
    var pidEventos = cmbEventos.value; // Obtiene el valor (id del evento)
    var pEventos = cmbEventos.options[cmbEventos.selectedIndex].text; // Obtiene el texto visible

    var cmbTipoPago = document.getElementById("cmbTipoPago");
    var pTipoPago = cmbTipoPago.options[cmbTipoPago.selectedIndex].text; // Obtiene el texto visible

    var pCveUsuario = "22";

    $.ajax({
        type: "POST",
        url: "clientes.aspx/GuardarInversionista",
        data: JSON.stringify({
            pidRegistro: pidRegistro,
            pNom: pNom,
            pCliente: pCliente,
            pCorreo: pCorreo,
            pTelefono: pTelefono,
            pCertificado: pCertificado,
            pidInversionista: pidInversionista,
            pInversionista: pInversionista, 
            pidEventos: pidEventos,
            pEventos: pEventos,
            pTipoPago: pTipoPago,
            pCveUsuario: pCveUsuario
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ocultarDetallesConsulta();
            filterClientes();
            setTimeout(() => {
                if (response.d.Exito) {
                    // Si es true, hacer algo
                    if (pidRegistro != "0") {
                        alert("Se actualizó correctamente el inversionista");
                    } else {
                        alert("Se registró correctamente el inversionista");
                    }
                } else {
                    alert("No existe una escala registrada asociada");
                }
            }, 1500);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", xhr.responseText);
            alert("Error en la solicitud: " + xhr.responseText);
        }
    });
}

$("#btnFiltrar").click(function () {
    filterClientes();
});

window.backStep = function () {
    $("#txtCertificado").val("");
    ocultarDetallesConsulta();
}

function formatPrecioBundle(fieldId) {
    var txtField = document.getElementById(fieldId);
    var value = parseFloat(txtField.value.replace(/,/g, ''));

    if (isNaN(value) || value === 0) {
        txtField.value = '0.00';
    } else {
        txtField.value = value.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }
}

window.editarInversionista = function (idInversionista) {
console.log("Cargando inversionista con ID:", idInversionista);

    //mostrarDetallesConsulta();
            
    $.ajax({
        type: "POST",
        url: "clientes.aspx/ObtenerInversionista",
        data: JSON.stringify({ idInversionista: idInversionista }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var inversionista = JSON.parse(response.d);
            console.log("inversionista DD: ", inversionista);
            document.getElementById("hfidRegister").value = inversionista.id;
            mostrarDetallesInversionista(inversionista); // ✅ Cambia al panel de edición
            llenarClientes(inversionista.cliente);
            llenarEvento(inversionista.evento);
            llenarInversionistas(inversionista.cliente, inversionista.tipo);
            llenarDataEstadoDeCuenta(inversionista.evento, inversionista.tipoTexto, inversionista.tipoPago, Number(inversionista.certificados), Number(inversionista.id));

        },
        error: function (xhr, status, error) {
            console.error("Error al cargar inversionista:", xhr.responseText);
        }
    });
            
}

function actualizarTabla(data) {
    $("#tablaInversionistas").empty();

    $.each(data, function (index, item) {
        var fila = "<tr data-id='" + item.id + "'>" +  // ✅ Agrega un atributo data-id
            "<td><div class='badge badge-outline-success'><button onClick=editarInversionista(" + item.id + ") class='editar' style='outline: none; border: none; background: none;'>Editar</button></div></td>" +
            "<td>" + item.NomComercial + "</td>" +
            "<td>" + item.TipoCliente + "</td>" +
            "<td>" + item.Correo + "</td>" +
            "<td>" + item.Evento + "</td>" +
            "<td>" + item.Certificados + "</td>" +
            "<td><div class='badge badge-outline-success'>" + item.Status + "</div></td>" +
            "</tr>";
        $("#tablaInversionistas").append(fila);
    });
}

window.mostrarDetallesConsulta = function () {
    // ✅ Oculta la tabla y muestra el panel de edición (simulando TipoAmbiente("DATOS"))
    $("#tablaInversionistas").hide();
    $("#panConsulta").hide();
    $("#btnAlta").hide();
    $("#panDatos").show();
    $("#btnConsulta").show();
}

window.ocultarDetallesConsulta = function () {
    // ✅ Muestra la tabla y oculta el panel de edición (simulando TipoAmbiente("CONSULTA"))
    $("#panDatos").hide();
    $("#btnConsulta").hide();
    $("#panConsulta").show();
    $("#tablaInversionistas").show();
    $("#btnAlta").show();
}

function mostrarDetallesInversionista(inversionista) {
        if (inversionista.error) {
            alert("Error: " + inversionista.error);
            return;
        }

        mostrarDetallesConsulta();

        // ✅ Simula el método `LimpiaDatos()`
    limpiarDatos();
    console.log("inversionista: ", inversionista);
        // ✅ Llena los campos con los datos del inversionista
        $("#txtNom").val(inversionista.nombre);
        $("#txtCorreo").val(inversionista.correo);
        $("#txtTelefono").val(inversionista.telefono);
        $("#cmbEventos").val(inversionista.evento);
        $("#txtCertificado").val(inversionista.certificados);
        //$("#cmbInversionista").val(inversionista.tipo);
}

function limpiarDatos() {
    $("#txtNom").val("");
    $("#txtCorreo").val("");
    $("#txtTelefono").val("");
    $("#cmbEventos").val("");
    $("#txtCertificado").val("");
    $("#cmbInversionista").val("");
}

window.darAlta = function () {
    mostrarDetallesConsulta();
    limpiarDatos();
    llenarClientes(0, 0);
    llenarEvento(0);
    llenarInversionistas(0, 0)
}

function mostrarDatos(idInversionista) {
    $.ajax({
        type: "POST",
        url: "clientes.aspx/ObtenerInversionista",
        data: JSON.stringify({ idInversionista: idInversionista }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let data = JSON.parse(response.d);

            if (data.error) {
                alert(data.error);
                return;
            }

            console.log("Datos recibidos:", data); // <-- IMPRIME LOS DATOS

            // ✅ Actualizar Labels en ASP.NET (revisar ClientID)
            document.getElementById("<%= lblTotalCertificados.ClientID %>").innerText = "$" + data.total;
            document.getElementById("<%= lblAnticipo.ClientID %>").innerText = "$" + data.anticipo;
            document.getElementById("<%= lblEnganche.ClientID %>").innerText = "$" + data.enganche;
            document.getElementById("<%= lblPagos.ClientID %>").innerText = data.pagos.length + " pagos registrados";

            // ✅ Llenar tabla de pagos
            let pagosHtml = "";
            data.pagos.forEach(pago => {
                pagosHtml += `
                <tr>
                    <td>${pago.Descripcion}</td>
                    <td>${pago.Fecha}</td>
                    <td class="text-danger">$${pago.Monto}<i class="mdi mdi-arrow-down"></i></td>
                    <td><label class="badge badge-danger">${pago.Status}</label></td>
                </tr>`;
            });

            $("#tablaPagos tbody").html(pagosHtml);
        },
        error: function () {
            alert("Error al obtener los datos del inversionista.");
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    filterClientes();
    ocultarDetallesConsulta();
    llenarEventoFilter();
});

function llenarInversionistas(pCliente, pInversionista) {
    $.ajax({
        type: "POST",
        url: "clientes.aspx/llenarInversionistas", // Cambia por el nombre de tu página
        data: JSON.stringify({ pCliente: pCliente }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var dropdown = document.getElementById("cmbInversionista");
            dropdown.innerHTML = "";

            response.d.forEach(function (item) {
                var option = document.createElement("option");
                option.value = item.Value; 
                option.text = item.Text;  // Asigna el texto de la opción

                if (pInversionista != 0 && item.Value == pInversionista) {
                    option.selected = true;
                    matchFound = true;
                }

                dropdown.appendChild(option);

            });

        },
        error: function (xhr, status, error) {
            console.error("Error llenando el dropdown:", error);
        }
    });
}

function llenarClientes(pCliente, pInversionista) {
    $.ajax({
        type: "POST",
        url: "clientes.aspx/llenarClientes", // Cambia por el nombre de tu página
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var dropdown = document.getElementById("cmbCliente");
            dropdown.innerHTML = ""; // Limpia el DropDownList
            let matchFound = false;

            // Agrega las opciones retornadas por el backend
            response.d.forEach(function (item, index) {
                var option = document.createElement("option");
                option.value = item.Value; // Asigna el valor de la opción
                option.text = item.Text;  // Asigna el texto de la opción

                // Si pCliente es diferente de 0 y coincide con item.Value, lo preselecciona
                if (pCliente != 0 && item.Value == pCliente) {
                    option.selected = true;
                    matchFound = true;
                }

                dropdown.appendChild(option);
            });

        },
        error: function (xhr, status, error) {
            console.error("Error llenando el dropdown:", error);
        }
    });
}


function llenarEvento(pEvento) {
    $.ajax({
        type: "POST",
        url: "clientes.aspx/llenarEvento",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var dropdown = document.getElementById("cmbEventos");
            dropdown.innerHTML = "";
            let matchFound = false;

            response.d.forEach(function (item) {
                var option = document.createElement("option");
                option.value = item.Value;
                option.text = item.Text;

                if (pEvento != 0 && item.Value == pEvento) {
                    option.selected = true;
                    matchFound = true;
                }

                dropdown.appendChild(option);
            });
        },
        error: function (xhr, status, error) {
            console.error("Error llenando el dropdown:", error);
        }
    });
}

function llenarEventoFilter() {
    $.ajax({
        type: "POST",
        url: "clientes.aspx/llenarEvento",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var dropdown = document.getElementById("sortingField");
            dropdown.innerHTML = "";
            let matchFound = false;

            var optionTodos = document.createElement("option");
            optionTodos.value = "";  // Cadena vacía
            optionTodos.text = "TODOS";
            dropdown.appendChild(optionTodos);

            response.d.forEach(function (item) {
                var option = document.createElement("option");
                option.value = item.Text;
                option.text = item.Text;

                dropdown.appendChild(option);
            });
        },
        error: function (xhr, status, error) {
            console.error("Error llenando el dropdown:", error);
        }
    });
}

function llenarDataEstadoDeCuenta(idEvento, cmbInversionista, CondicionesPago, pCertificados, pidInversionista) {
    //Los parametros son escalas y numero de certificados
    $.ajax({
        type: "POST",
        url: "clientes.aspx/CargaTotalPagos",
        data: JSON.stringify({ idEvento: idEvento, cmbInversionista: cmbInversionista, CondicionesPago: CondicionesPago, pCertificados: pCertificados, pidInversionista: pidInversionista }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = JSON.parse(response.d);

            $("#lblTotalCertificados").text("$"+resultado.Total);
            $("#lblAnticipo").text("$"+resultado.Anticipo);
            $("#lblEnganche").text("$"+resultado.Enganche);
            $("#lblPagos").text(resultado.Pagos);

            let tbody = document.getElementById('tbodyPagos');
            tbody.insertAdjacentHTML('beforeend', resultado.tableHTML);
        },
        error: function (xhr, status, error) {
            console.error("Error llenando el dropdown:", error);
        }
    });
}