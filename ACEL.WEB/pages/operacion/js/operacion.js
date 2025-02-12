
function formatPrecioBundle(fieldId) {
    var txtField = document.getElementById(fieldId);
    var value = parseFloat(txtField.value.replace(/,/g, ''));

    if (isNaN(value) || value === 0) {
        txtField.value = '0.00';
    } else {
        txtField.value = value.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }
}

$(document).ready(function () {
    $("#btnFiltrar").click(function () {
        var criterio = $("#sortingField").val();
        var orden = "asc";
        var dataJson = document.getElementById('HiddenFieldData').value;

        $.ajax({
            type: "POST",
            url: "clientes.aspx/FiltrarInversionistas",
            data: JSON.stringify({ dataJson: dataJson, criterio: criterio, orden: orden }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var inversionistas = JSON.parse(response.d);
                actualizarTabla(inversionistas);
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error:", xhr.responseText);
                alert("Error en la solicitud: " + xhr.responseText);
            }
        });
    });

    function actualizarTabla(data) {
        var tabla = $("#tablaInversionistas");
        tabla.empty();

        $.each(data, function (index, item) {
            var fila = "<tr data-id='" + item.id + "'>" +  // ✅ Agrega un atributo data-id
                "<td><div class='badge badge-outline-success'><a href='#' class='editar'>Editar</a></div></td>" +
                "<td>" + item.NomComercial + "</td>" +
                "<td>" + item.TipoCliente + "</td>" +
                "<td>" + item.Correo + "</td>" +
                "<td>" + item.Evento + "</td>" +
                "<td>" + item.Certificados + "</td>" +
                "<td><div class='badge badge-outline-success'>" + item.Status + "</div></td>" +
                "</tr>";
            tabla.append(fila);
        });
    }

    $("#tablaInversionistas").on("click", ".editar", function (e) {
        e.preventDefault();

        var idInversionista = $(this).closest("tr").data("id"); // ✅ Obtiene el ID del inversionista
        console.log("Cargando inversionista con ID:", idInversionista);

        $.ajax({
            type: "POST",
            url: "clientes.aspx/ObtenerInversionista",
            data: JSON.stringify({ idInversionista: idInversionista }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var inversionista = JSON.parse(response.d);
                mostrarDetallesInversionista(inversionista); // ✅ Cambia al panel de edición
            },
            error: function (xhr, status, error) {
                console.error("Error al cargar inversionista:", xhr.responseText);
            }
        });
    });

    function mostrarDetallesInversionista(inversionista) {
        if (inversionista.error) {
            alert("Error: " + inversionista.error);
            return;
        }

        // ✅ Simula el método `LimpiaDatos()`
        $("#txtNom").val("");
        $("#txtCorreo").val("");
        $("#txtTelefono").val("");
        $("#cmbEventos").val("");
        $("#txtCertificados").val("");
        $("#cmbInversionista").val("");

        // ✅ Llena los campos con los datos del inversionista
        $("#txtNom").val(inversionista.nombre);
        $("#txtCorreo").val(inversionista.correo);
        $("#txtTelefono").val(inversionista.telefono);
        $("#cmbEventos").val(inversionista.evento);
        $("#txtCertificados").val(inversionista.certificados);
        $("#cmbInversionista").val(inversionista.tipo);

        // ✅ Oculta la tabla y muestra el panel de edición (simulando TipoAmbiente("DATOS"))
        $("#panConsulta").hide();
        $("#panDatos").show();
        $("#btnConsulta").show();
        $("#btnAlta").hide();
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


});

