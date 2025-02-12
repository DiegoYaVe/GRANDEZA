

<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="recibo_pago.aspx.cs" Inherits="ACEL.WEB.pages.tesoreria.recibos.impresion.recibo_pago" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title><%= Page.Title %></title>
    <%--<link rel="stylesheet" href="recibo.css">--%>

    <style>
        @page {
            size: letter; /* auto es el valor inicial */
            margin: 0 5px 0 5px; /* afecta el margen en la configuración de impresión */
            
        }

        body {
            background-color: #DEDEDE;
            font-family: "Arial";
        }

        #page1 {
            width: 200mm; /*210-40*/
            height: 100%; /*297.4-60*/
            background-color: white;
            padding: 0 5px 0 5px;
            margin-left: auto;
            margin-right: auto;
            box-shadow: 0 0 50px #888888;
        }

        .div1 {
            padding-bottom: 20mm;
            padding-top: 22mm;
        }

        header {
            height: 25mm;
            border-bottom: 0px solid #000000;
            position: absolute;
            background: white;
            width: 200mm;
            top:0;
            z-index:1000;
        }

            header > div {
                float: left;
                padding-top: 30px;
            }

            header div.left, header div.right {
                width: 50%;
            }

            header div.center {
                width: calc(20% - 2px);
                border-left: 0px solid #000000;
                border-right: 0px solid #000000;
            }

        section {
            padding-top: 5px;
            padding-bottom: 5px;
        }

            section div.left, section div.right {
                width: 50%;
            }

        section, pre {
            margin: 0px;
        }

        .bordeRecibo section {
            padding: 5px;
        }

        footer {
            position: static;
            bottom: 0;
            width: 200mm;
            height: 20mm;
            text-align: center;
            border-top: 1px solid #D3D3D3;
            background: white;
            vertical-align: middle;
        }

        img {
            margin-bottom: 0px;
            height: 35px;
            max-width: 130px;
        }

        .column {
            height: calc(100% - 2px);
        }

        .row {
            left: 0;
            right: 0;
            position: relative;
        }

        .text-center {
            text-align: center;
        }

        .text-left {
            text-align: left;
        }

        .text-right {
            text-align: right;
        }

        .negrita {
            font-weight: bold;
        }

        .preimpreso {
            color: #999999;
            text-transform: uppercase;
        }

        .h1 {
            font-size: 20px;
        }

        .h2 {
            font-size: 12px;
        }

        .h3 {
            font-size: 8px;
        }

        .container {
            margin: 0 5px 0 5px;
        }
        /*.bordeRecibo{border:1px solid #000000;margin:0;padding:0;height:100%;position:relative;}*/ /*Para que el footer llegue hasta abajo*/
        .pull-right {
            position: absolute;
            right: 0;
        }

        #tipoComprobante {
            font-size: 50px;
        }

        #leyendaTipoComprobante {
            font-size: 10px;
        }

        #tipoComprobante, #lblComprobante {
            line-height: 70%;
        }

        #lblNroCmp {
            line-height: 200%;
        }

        #hr {
            width: 30%;
            border-top: 1px solid #000000;
            height: 1px;
            position: absolute;
            right: 20px;
        }

        #firma {
            padding-top: 30mm;
            position: relative;
        }

        .importeEnPesos:before {
            content: '$';
        }

        @media screen {
            body {
                background-color: #DEDEDE;
            }

            #page1 {
                box-shadow: 0 0 50px #888888;
                padding: 0;
            }

            .footer {
                page-break-inside: auto;
            }

            header {
                page-break-before: always;
            }

        }

        @media print {

            body {
                background-color: #FFFFFF; /*font-family:"Verdana";*/
            }

            #page1 {
                box-shadow: none;
                padding: 0;
            }

            button, #botonera {
                display: none;
            }

            .footer {
                page-break-before: always;
            }
        }
        /*BOTONERA*/
        .button {
            color: #FFFFFF;
            background-color: #428BCA;
            border: 1px solid #357EBD;
            padding: 6px 12px;
            margin-bottom: 5px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }

        #botonera {
            z-index: 999;
            position: absolute;
            left: 10px;
            top: 10px;
        }

        .auto-style1 {
            width: 100%;
        }

        .auto-style4 {
            width: 80%;
            height: 14px;
        }

        .auto-style5 {
            width: 82px;
            height: 43px;
        }

        </style>
</head>

<body>
    <form runat="server" id="form1">
        <div id="botonera">
            <a href="../../cap_ingresos.aspx" style="text-decoration:none; font-size:small;" class="button">Regresar</a>
            <br />
            <br />
            <button class="button" onclick="javascript:window.print();"">Descargar o Imprimir</button>
        </div>

        <div id="page1">

            <div class="bordeRecibo">
                <header>

                    <!-- Lado Izquierdo -->
                    <div class="column left">
                        <div class="container">
                            <div class="row text-left">
                                <img src="image/logo2.png" alt="logo" class="auto-style5">
                                <div class="text-left h3">GRANDEZA VERACRUZANA</div>
                                <div class="text-left h3">RFC: XXX00000000A</div>
                            </div>
                        </div>
                    </div>

                    <!-- Lado Central -->
                    <%--<div class="column center text-center"> <span id="tipoComprobante">X</span> 
					<br>
					<span id="leyendaTipoComprobante" class="preimpreso">DOCUMENTO<br>NO VALIDO<br>COMO<br>FACTURA</span> 
				</div>
                    --%>
                    <!-- Lado Derecho -->
                    <div class="column right">
                        <div class="container">
                            <div class="row text-right">
                                <br />
                                <br />
                                <div class="text-right h3">Datos generales de la operación</div>
                                <br />
                            </div>
                        </div>
                    </div>
                </header>
                <div class="div1">
                    <table>
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <div class="text-left negrita h2">
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                </div>
                            </td>
                            <td style="width: 50%; justify-content: right;">
                                <div class="text-right negrita h3">
                                    Fecha:
                                    <asp:Label ID="lblFecha" CssClass="text-right h3" Font-Size="10px" Font-Bold="false"
                                        runat="server" Text="dd/mm/aaaa"></asp:Label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: left;" class="text-left">
                                <div class="text-left negrita h3">
                                    Certificados: 
                                    <asp:Label ID="lblCertificados" CssClass="text-left h3" Font-Size="10px" Font-Bold="false"
                                        runat="server" Text="0"></asp:Label>
                                    <br />
                                </div>
                            </td>
                            <td style="width: 50%; justify-content: right;">
                                <div class="text-right negrita h3">
                                    Recibo:
                                    <asp:Label ID="lblCotizacion" CssClass="text-right h3" Font-Size="10px" Font-Bold="true"
                                        runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 100%;" colspan="2">
                                <div class="text-center negrita" style="font-size: 15px; border-bottom: solid;">
                                    COMPROBANTE DE APORTACIÓN
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="2" >
                                <div class="text-left h3">
                                    <p style="text-align: justify; margin:0; padding:0;">
                                        RECIBIMOS LA CANTIDAD Y POR EL CONCEPTO MENCIONADO A CONTINUACIÓN, CORRESPONDIENTE A LA 
                                        ADQUISICIÓN DE LOS CERTIFICADOS DE INVERSIÓN EN EL DESARROLLO INMOBILIARIO DENOMINADO 
                                        "GRANDEZA RIVIERA VERACRUZANA BY SERES DE RIQUEZA", VALOR RECIBIDO A NUESTRA ENTERA 
                                        SATISFACCIÓN
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table class="auto-style1">
                        <tr>
                            <td style="width: 200mm;">
                                <table style="width:100%;">
                                    <tr style="background-color:#236785; color:#fff;">
                                        <th style="width:33%;">Cantidad</th>
                                        <th>Descripción</th>
                                        <th>Monto</th>
                                    </tr>
                                    <tr>
                                        <td style="text-align:center;">1</td>
                                        <td>
                                            <asp:Literal ID="ltrTipoPago" Text="SIN DATOS" runat="server"></asp:Literal></td>
                                        <td style="text-align:right;">$
                                            <asp:Literal ID="ltrMontoPago" Text="0.00" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class="auto-style1" style="border: solid; border-width: 1px">
                        
                        <tr>
                            <td style="width: 81%; padding-left: 2%;">
                                <div class="text-left negrita h2">
                                    <asp:Label ID="lblNumeroEnLetras" runat="server" Text="IMPORTE EN LETRA"></asp:Label>
                                    <asp:Label ID="lblCentavos" runat="server" Text=" 00/100"></asp:Label>
                                </div>
                            </td>
                            <td style="text-align: right; width: 10%;" class="auto-style4">
                                <div class="text-right negrita h2">Total: $</div>
                            </td>
                            <td>
                                <div class="text-right h2">
                                    <asp:Label Font-Size="20px" Font-Bold="true" ID="lblTotal" runat="server" Text="0.00"></asp:Label></div>
                            </td>
                        </tr>

                    </table>

                    <%--<table style="width: 100%; padding-top:35mm; page-break-inside: auto;">
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                <p>A T E N T A M E N T E</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                <asp:Image ImageUrl="" Width="100px" Height="60px" ID="imgFirma" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: center;">--------------------------------------</td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: center;">
                                <p>
                                    <strong>
                                        <asp:Label ID="lblVendedor2" runat="server" Text="Nom Vendedor"></asp:Label></strong>
                                </p>
                            </td>
                        </tr>
                    </table>--%>

                    <div style="width: 100%; ">
                        <div style="width: 100%; text-align: center;">
                            <br />
                            <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="A T E N T A M E N T E"></asp:Label><br />
                            --------------------------------------<br />
                            <strong>
                                <asp:Label ID="lblVendedor2"  Font-Size="12px" runat="server" Text="GRANDEZA VERACRUZANA"></asp:Label></strong><br />
                            <asp:Label ID="Label3" Font-Size="XX-Small" runat="server" Text="Filadelfia 182, int. 502, Col Nápoles, 
                                Benito Juarez, CP. 03810, CDMX"></asp:Label>
                        </div>
                    </div>
                </div>
                
            </div>
            <!-- bordeRecibo -->
        </div>
        <!-- Page1 -->
    </form>

</body>
<script>


    function Unidades(num) {

        switch (num) {
            case 1: return "UN";
            case 2: return "DOS";
            case 3: return "TRES";
            case 4: return "CUATRO";
            case 5: return "CINCO";
            case 6: return "SEIS";
            case 7: return "SIETE";
            case 8: return "OCHO";
            case 9: return "NUEVE";
        }

        return "";
    }//Unidades()

    function Decenas(num) {

        decena = Math.floor(num / 10);
        unidad = num - (decena * 10);

        switch (decena) {
            case 1:
                switch (unidad) {
                    case 0: return "DIEZ";
                    case 1: return "ONCE";
                    case 2: return "DOCE";
                    case 3: return "TRECE";
                    case 4: return "CATORCE";
                    case 5: return "QUINCE";
                    default: return "DIECI" + Unidades(unidad);
                }
            case 2:
                switch (unidad) {
                    case 0: return "VEINTE";
                    default: return "VEINTI" + Unidades(unidad);
                }
            case 3: return DecenasY("TREINTA", unidad);
            case 4: return DecenasY("CUARENTA", unidad);
            case 5: return DecenasY("CINCUENTA", unidad);
            case 6: return DecenasY("SESENTA", unidad);
            case 7: return DecenasY("SETENTA", unidad);
            case 8: return DecenasY("OCHENTA", unidad);
            case 9: return DecenasY("NOVENTA", unidad);
            case 0: return Unidades(unidad);
        }
    }//Unidades()

    function DecenasY(strSin, numUnidades) {
        if (numUnidades > 0)
            return strSin + " Y " + Unidades(numUnidades)

        return strSin;
    }//DecenasY()

    function Centenas(num) {
        centenas = Math.floor(num / 100);
        decenas = num - (centenas * 100);

        switch (centenas) {
            case 1:
                if (decenas > 0)
                    return "CIENTO " + Decenas(decenas);
                return "CIEN";
            case 2: return "DOSCIENTOS " + Decenas(decenas);
            case 3: return "TRESCIENTOS " + Decenas(decenas);
            case 4: return "CUATROCIENTOS " + Decenas(decenas);
            case 5: return "QUINIENTOS " + Decenas(decenas);
            case 6: return "SEISCIENTOS " + Decenas(decenas);
            case 7: return "SETECIENTOS " + Decenas(decenas);
            case 8: return "OCHOCIENTOS " + Decenas(decenas);
            case 9: return "NOVECIENTOS " + Decenas(decenas);
        }

        return Decenas(decenas);
    }//Centenas()

    function Seccion(num, divisor, strSingular, strPlural) {
        cientos = Math.floor(num / divisor)
        resto = num - (cientos * divisor)

        letras = "";

        if (cientos > 0)
            if (cientos > 1)
                letras = Centenas(cientos) + " " + strPlural;
            else
                letras = strSingular;

        if (resto > 0)
            letras += "";

        return letras;
    }//Seccion()

    function Miles(num) {
        divisor = 1000;
        cientos = Math.floor(num / divisor)
        resto = num - (cientos * divisor)

        strMiles = Seccion(num, divisor, "UN MIL", "MIL");
        strCentenas = Centenas(resto);

        if (strMiles == "")
            return strCentenas;

        return strMiles + " " + strCentenas;
    }//Miles()

    function Millones(num) {
        divisor = 1000000;
        cientos = Math.floor(num / divisor)
        resto = num - (cientos * divisor)

        strMillones = Seccion(num, divisor, "UN MILLON DE", "MILLONES DE");
        strMiles = Miles(resto);

        if (strMillones == "")
            return strMiles;

        return strMillones + " " + strMiles;
    }//Millones()

    function NumeroALetras(num) {
        var data = {
            numero: num,
            enteros: Math.floor(num),
            centavos: Math.round((num - Math.floor(num)) * 100), // Convertir centavos a número entero
            letrasCentavos: "",
            letrasMonedaPlural: 'PESOS', //"PESOS", 'Dólares', 'Bolívares', 'etcs'
            letrasMonedaSingular: 'PESO', //"PESO", 'Dólar', 'Bolivar', 'etc'

            letrasMonedaCentavoPlural: "CENTAVOS",
            letrasMonedaCentavoSingular: "CENTAVO"
        };

        if (data.centavos > 0) {
            data.letrasCentavos = "CON " + (function () {
                if (data.centavos == 1)
                    return Millones(data.centavos) + " " + data.letrasMonedaCentavoSingular;
                else
                    return Millones(data.centavos) + " " + data.letrasMonedaCentavoPlural;
            })();
        };

        if (data.enteros == 0)
            return "CERO " + data.letrasMonedaPlural + " " + data.letrasCentavos;
        else if (data.enteros == 1)
            return Millones(data.enteros) + " " + data.letrasMonedaSingular + " " + data.letrasCentavos;
        else
            return Millones(data.enteros) + " " + data.letrasMonedaPlural + " " + data.letrasCentavos;
    }//NumeroALetras()

</script>
</html>
