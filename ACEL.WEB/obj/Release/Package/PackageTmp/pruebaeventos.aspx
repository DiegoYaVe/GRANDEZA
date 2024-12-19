<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebaeventos.aspx.cs" Inherits="ACEL.WEB.pruebaeventos" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Sortable Table - ACEL_CUENTA_EVENTOS</title>
    <link rel="stylesheet" href="assets/vendors/mdi/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="assets/vendors/ti-icons/css/themify-icons.css" />
    <link rel="stylesheet" href="assets/vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="assets/css/vertical-light/style.css" />
    <style>
        #formSection {
            display: none;
        }
        .page-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        #AltaButton {
            margin-left: auto;
        }
        #formSection .card-title {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        #RegresarButton {
            align-self: flex-end;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper">
                    <div class="page-header d-flex justify-content-between align-items-center">
                        <h3 id="tableTitle" class="page-title">Sortable Table - ACEL_CUENTA_EVENTOS</h3>
                        <asp:Button ID="AltaButton" runat="server" CssClass="btn btn-gradient-primary btn-fw" Text="Alta" OnClick="AltaButton_Click" />
                    </div>
                    <div class="row mt-4" id="tableSection" runat="server">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="sort-panel d-flex align-items-center mb-4">
                                        <p class="my-2">Sorting Field:</p>
                                        <label class="d-flex justify-content-start mb-0">
                                            <asp:DropDownList ID="SortingField" runat="server" CssClass="form-select form-select-sm me-2 ms-2">
                                                <asp:ListItem Text="Nombre Evento" Value="NombreEvento"></asp:ListItem>
                                                <asp:ListItem Text="Descripción" Value="Descripcion"></asp:ListItem>
                                                <asp:ListItem Text="Estado del Evento" Value="StatusEvento"></asp:ListItem>
                                                <asp:ListItem Text="Fecha de Alta" Value="FechaAlta"></asp:ListItem>
                                            </asp:DropDownList>
                                        </label>
                                        <asp:Button ID="SortButton" runat="server" CssClass="btn btn-info btn-sm" Text="Sort" OnClick="SortButton_Click" />
                                    </div>
                                    <div id="js-grid-sortable">
                                        <asp:GridView ID="EventosGrid" runat="server" AutoGenerateColumns="False" CssClass="table sortable-table" OnRowCommand="EventosGrid_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:Button ID="EditButton" runat="server" CssClass="btn btn-info btn-sm" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("idEvento") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idEvento" HeaderText="ID Evento" />
                                                <asp:BoundField DataField="NombreEvento" HeaderText="Nombre Evento" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:BoundField DataField="StatusEvento" HeaderText="Estado del Evento" />
                                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" />
                                                <asp:BoundField DataField="FechaMod" HeaderText="Última Modificación" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Formulario para modificar o dar de alta datos -->
                    <div class="row" id="formSection" runat="server">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-title">
                                        <h4 id="formTitle">Modificar Evento</h4>
                                        <asp:Button ID="RegresarButton" runat="server" CssClass="btn btn-gradient-primary btn-fw" Text="Regresar" OnClick="RegresarButton_Click" />
                                    </div>
                                    <div class="form-sample">
                                        <p class="card-description"> Detalles del evento </p>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">ID Evento</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="idEvento" runat="server" CssClass="form-control" ReadOnly="True" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Nombre Evento</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="nombreEvento" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Descripción</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="descripcion" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Estado del Evento</label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="statusEvento" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Activo" Value="Activo"></asp:ListItem>
                                                            <asp:ListItem Text="Inactivo" Value="Inactivo"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Fecha de Alta</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="fechaAlta" runat="server" CssClass="form-control" TextMode="Date" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Última Modificación</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="fechaMod" runat="server" CssClass="form-control" TextMode="Date" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="GuardarButton" runat="server" CssClass="btn btn-gradient-primary btn-fw" Text="Guardar" OnClick="GuardarButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- content-wrapper ends -->
            </div>
            <!-- page-body-wrapper ends -->
        </div>
    </form>
    <!-- plugins:js -->
    <script runat="server" src="assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- inject:js -->
    <script src="assets/js/off-canvas.js"></script>
    <script src="assets/js/hoverable-collapse.js"></script>
    <script src="assets/js/misc.js"></script>
    <script src="assets/js/settings.js"></script>
    <script src="assets/js/todolist.js"></script>
    <!-- endinject -->
</body>
</html>



