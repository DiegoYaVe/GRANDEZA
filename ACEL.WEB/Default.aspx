<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ACEL.WEB.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Grandeza Veracruz</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="assets/vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="assets/vendors/font-awesome/css/font-awesome.min.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="assets/vendors/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css" />
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <!-- endinject -->
    <!-- Layout styles -->
    <link rel="stylesheet" href="assets/css/vertical-light/style.css" />
    <!-- End layout styles -->
    <link rel="shortcut icon" href="assets/images/logo_mini2.png" />

</head>
<body class="sidebar-fixed">
    <form runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-scroller">
            <!-- partial:partials/_navbar.html -->
            <nav class="navbar default-layout-navbar col-lg-12 col-12 p-0 fixed-top d-flex flex-row">
                <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-start">
                    <a style="margin-left: 10%;" href="index.html">
                        <img width="130" height="70" src="assets/images/logo2.png" alt="logo" /></a>
                    <a class="navbar-brand brand-logo-mini" href="index.html">
                        <img src="assets/images/logo_mini2.png" alt="logo" /></a>
                </div>
                <div class="navbar-menu-wrapper d-flex align-items-stretch">
                    <button class="navbar-toggler navbar-toggler align-self-center" type="button" data-toggle="minimize">
                        <span class="mdi mdi-menu"></span>
                    </button>
                    <div class="search-field d-none d-md-block">
                        <form class="d-flex align-items-center h-100" action="#">
                            <div class="input-group">
                                <div class="input-group-prepend bg-transparent">
                                    <i class="input-group-text border-0 mdi mdi-magnify"></i>
                                </div>
                                <input type="text" class="form-control bg-transparent border-0" placeholder="Search projects">
                            </div>
                        </form>
                    </div>
                    <ul class="navbar-nav navbar-nav-right">
                        <li class="nav-item nav-profile dropdown">
                            <a class="nav-link dropdown-toggle" id="profileDropdown" href="#" data-bs-toggle="dropdown" aria-expanded="false">
                                <div class="nav-profile-img">
                                    <img src="assets/images/faces/face1.jpg" alt="image">
                                    <span class="availability-status online"></span>
                                </div>
                                <div class="nav-profile-text">
                                    <p class="mb-1 text-black">
                                        <asp:Literal ID="ltrNombre1" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </a>
                            <div class="dropdown-menu navbar-dropdown" aria-labelledby="profileDropdown">
                                <a class="dropdown-item" href="#">
                                    <i class="mdi mdi-cached me-2 text-success"></i>Activity Log </a>
                                <div class="dropdown-divider"></div>
                                <asp:LinkButton runat="server" ID="lnkSalir" OnClick="lnkSalir_Click" CssClass="dropdown-item">
                  <i class="mdi mdi-logout me-2 text-primary"></i> Salir </asp:LinkButton>
                            </div>
                        </li>
                        <li class="nav-item d-none d-lg-block full-screen-link">
                            <a class="nav-link">
                                <i class="mdi mdi-fullscreen" id="fullscreen-button"></i>
                            </a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link count-indicator dropdown-toggle" id="messageDropdown" href="#" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="mdi mdi-email-outline"></i>
                                <span class="count-symbol bg-warning"></span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-end navbar-dropdown preview-list" aria-labelledby="messageDropdown">
                                <h6 class="p-3 mb-0">Messages</h6>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <img src="assets/images/faces/face4.jpg" alt="image" class="profile-pic">
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject ellipsis mb-1 font-weight-normal">Mark send you a message</h6>
                                        <p class="text-gray mb-0">1 Minutes ago </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <img src="assets/images/faces/face2.jpg" alt="image" class="profile-pic">
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject ellipsis mb-1 font-weight-normal">Cregh send you a message</h6>
                                        <p class="text-gray mb-0">15 Minutes ago </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <img src="assets/images/faces/face3.jpg" alt="image" class="profile-pic">
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject ellipsis mb-1 font-weight-normal">Profile picture updated</h6>
                                        <p class="text-gray mb-0">18 Minutes ago </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <h6 class="p-3 mb-0 text-center">4 new messages</h6>
                            </div>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link count-indicator dropdown-toggle" id="notificationDropdown" href="#" data-bs-toggle="dropdown">
                                <i class="mdi mdi-bell-outline"></i>
                                <span class="count-symbol bg-danger"></span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-end navbar-dropdown preview-list" aria-labelledby="notificationDropdown">
                                <h6 class="p-3 mb-0">Notifications</h6>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <div class="preview-icon bg-success">
                                            <i class="mdi mdi-calendar"></i>
                                        </div>
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject font-weight-normal mb-1">Event today</h6>
                                        <p class="text-gray ellipsis mb-0">Just a reminder that you have an event today </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <div class="preview-icon bg-warning">
                                            <i class="mdi mdi-cog"></i>
                                        </div>
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject font-weight-normal mb-1">Settings</h6>
                                        <p class="text-gray ellipsis mb-0">Update dashboard </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item preview-item">
                                    <div class="preview-thumbnail">
                                        <div class="preview-icon bg-info">
                                            <i class="mdi mdi-link-variant"></i>
                                        </div>
                                    </div>
                                    <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                                        <h6 class="preview-subject font-weight-normal mb-1">Launch Admin</h6>
                                        <p class="text-gray ellipsis mb-0">New admin wow! </p>
                                    </div>
                                </a>
                                <div class="dropdown-divider"></div>
                                <h6 class="p-3 mb-0 text-center">See all notifications</h6>
                            </div>
                        </li>
                        <li class="nav-item nav-logout d-none d-lg-block">
                            <a class="nav-link" href="#">
                                <i class="mdi mdi-power"></i>
                            </a>
                        </li>
                        <li class="nav-item nav-settings d-none d-lg-block">
                            <a class="nav-link" href="#">
                                <i class="mdi mdi-format-line-spacing"></i>
                            </a>
                        </li>
                    </ul>
                    <button class="navbar-toggler navbar-toggler-right d-lg-none align-self-center" type="button" data-toggle="offcanvas">
                        <span class="mdi mdi-menu"></span>
                    </button>
                </div>
            </nav>
            <!-- partial -->
            <div class="container-fluid page-body-wrapper">
                <!-- partial:partials/_settings-panel.html -->
                <div class="right-sidebar-toggler-wrapper">
                    <div class="sidebar-toggler" id="settings-trigger"><i class="mdi mdi-palette"></i></div>
                    <div class="sidebar-toggler" id="chat-toggler"><i class="mdi mdi-chat-processing"></i></div>
                    <div class="sidebar-toggler"><a href="https://www.bootstrapdash.com/demo/majestic-admin-pro/docs/documentation.html" target="_blank"><i class="mdi mdi-file-document-outline"></i></a></div>
                    <div class="sidebar-toggler"><a href="https://www.bootstrapdash.com/product/majestic-admin-pro/" target="_blank"><i class="mdi mdi-cart"></i></a></div>
                </div>
                <div class="theme-setting-wrapper">
                    <div id="theme-settings" class="settings-panel">
                        <i class="settings-close mdi mdi-close"></i>
                        <p class="settings-heading">SIDEBAR SKINS</p>
                        <div class="sidebar-bg-options selected" id="sidebar-light-theme">
                            <div class="img-ss rounded-circle bg-light border me-3"></div>
                            Light
           
                        </div>
                        <div class="sidebar-bg-options" id="sidebar-dark-theme">
                            <div class="img-ss rounded-circle bg-dark border me-3"></div>
                            Dark
           
                        </div>
                        <p class="settings-heading mt-2">HEADER SKINS</p>
                        <div class="color-tiles mx-0 px-4">
                            <div class="tiles success"></div>
                            <div class="tiles warning"></div>
                            <div class="tiles danger"></div>
                            <div class="tiles info"></div>
                            <div class="tiles dark"></div>
                            <div class="tiles default"></div>
                        </div>
                    </div>
                    <div id="layout-settings" class="settings-panel">
                        <i class="settings-close mdi mdi-close"></i>
                        <div class="d-flex align-items-center justify-content-between border-bottom">
                            <p class="settings-heading font-weight-bold border-top-0 mb-3 ps-3 pt-0 border-bottom-0 pb-0">Template Demos </p>
                        </div>
                        <div class="demo-screen-wrapper">
                            <a href="https://demo.bootstrapdash.com/purple/jquery/template/demo_1/" target="_blank" class="demo-thumb-image" id="theme-light-switch">
                                <img src="assets/images/screenshots/vertical-light.jpg" alt="demo image">
                            </a>
                            <a href="https://demo.bootstrapdash.com/purple/jquery/template/demo_2/" target="_blank" class="demo-thumb-image">
                                <img src="assets/images/screenshots/vertical-dark.jpg" alt="demo image">
                            </a>
                            <a href="https://demo.bootstrapdash.com/purple/jquery/template/demo_3/" target="_blank" class="demo-thumb-image" id="theme-dark-switch">
                                <img src="assets/images/screenshots/horizontal-light.jpg" alt="demo image">
                            </a>
                            <a href="https://demo.bootstrapdash.com/purple/jquery/template/demo_4/" target="_blank" class="demo-thumb-image">
                                <img src="assets/images/screenshots/horizontal-dark.jpg" alt="demo image">
                            </a>
                        </div>
                    </div>
                </div>
                <div id="right-sidebar" class="settings-panel">
                    <i class="settings-close mdi mdi-close"></i>
                    <ul class="nav nav-tabs" id="setting-panel" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="todo-tab" data-bs-toggle="tab" href="#todo-section" role="tab" aria-controls="todo-section" aria-expanded="true">TO DO LIST</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="chats-tab" data-bs-toggle="tab" href="#chats-section" role="tab" aria-controls="chats-section">CHATS</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="setting-content">
                        <div class="tab-pane fade show active scroll-wrapper" id="todo-section" role="tabpanel" aria-labelledby="todo-section">
                            <div class="add-items d-flex px-3 mb-0">
                                <form class="form w-100">
                                    <div class="form-group d-flex">
                                        <input type="text" class="form-control todo-list-input" placeholder="Add To-do">
                                        <button type="submit" class="add btn btn-primary todo-list-add-btn" id="add-task">Add</button>
                                    </div>
                                </form>
                            </div>
                            <div class="list-wrapper px-3">
                                <ul class="d-flex flex-column-reverse todo-list">
                                    <li>
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="checkbox" type="checkbox">
                                                Team review meeting at 3.00 PM
                                            </label>
                                        </div>
                                        <i class="remove mdi mdi-close-circle-outline"></i>
                                    </li>
                                    <li>
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="checkbox" type="checkbox">
                                                Prepare for presentation
                                            </label>
                                        </div>
                                        <i class="remove mdi mdi-close-circle-outline"></i>
                                    </li>
                                    <li>
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="checkbox" type="checkbox">
                                                Resolve all the low priority tickets due today
                                            </label>
                                        </div>
                                        <i class="remove mdi mdi-close-circle-outline"></i>
                                    </li>
                                    <li class="completed">
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="checkbox" type="checkbox" checked>
                                                Schedule meeting for next week
                                            </label>
                                        </div>
                                        <i class="remove mdi mdi-close-circle-outline"></i>
                                    </li>
                                    <li class="completed">
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="checkbox" type="checkbox" checked>
                                                Project review
                                            </label>
                                        </div>
                                        <i class="remove mdi mdi-close-circle-outline"></i>
                                    </li>
                                </ul>
                            </div>
                            <div class="events py-4 border-bottom px-3">
                                <div class="wrapper d-flex mb-2">
                                    <i class="mdi mdi-circle-outline text-primary me-2"></i>
                                    <span>Feb 11 2018</span>
                                </div>
                                <p class="mb-0 font-weight-thin text-gray">Creating component page</p>
                                <p class="text-gray mb-0">build a js based app</p>
                            </div>
                            <div class="events pt-4 px-3">
                                <div class="wrapper d-flex mb-2">
                                    <i class="mdi mdi-circle-outline text-primary me-2"></i>
                                    <span>Feb 7 2018</span>
                                </div>
                                <p class="mb-0 font-weight-thin text-gray">Meeting with Alisa</p>
                                <p class="text-gray mb-0 ">Call Sarah Graves</p>
                            </div>
                        </div>
                        <!-- To do section tab ends -->
                        <div class="tab-pane fade" id="chats-section" role="tabpanel" aria-labelledby="chats-section">
                            <div class="d-flex align-items-center justify-content-between border-bottom">
                                <p class="settings-heading border-top-0 mb-3 ps-3 pt-0 border-bottom-0 pb-0">Friends</p>
                                <small class="settings-heading border-top-0 mb-3 pt-0 border-bottom-0 pb-0 pe-3 font-weight-normal">See All</small>
                            </div>
                            <ul class="chat-list">
                                <li class="list active">
                                    <div class="profile">
                                        <img src="assets/images/faces/face1.jpg" alt="image"><span class="online"></span>
                                    </div>
                                    <div class="info">
                                        <p>Thomas Douglas</p>
                                        <p>Available</p>
                                    </div>
                                    <small class="text-muted my-auto">19 min</small>
                                </li>
                                <li class="list">
                                    <div class="profile">
                                        <img src="assets/images/faces/face2.jpg" alt="image"><span class="offline"></span>
                                    </div>
                                    <div class="info">
                                        <div class="wrapper d-flex">
                                            <p>Catherine</p>
                                        </div>
                                        <p>Away</p>
                                    </div>
                                    <div class="badge badge-success badge-pill my-auto mx-2">4</div>
                                    <small class="text-muted my-auto">23 min</small>
                                </li>
                                <li class="list">
                                    <div class="profile">
                                        <img src="assets/images/faces/face3.jpg" alt="image"><span class="online"></span>
                                    </div>
                                    <div class="info">
                                        <p>Daniel Russell</p>
                                        <p>Available</p>
                                    </div>
                                    <small class="text-muted my-auto">14 min</small>
                                </li>
                                <li class="list">
                                    <div class="profile">
                                        <img src="assets/images/faces/face4.jpg" alt="image"><span class="offline"></span>
                                    </div>
                                    <div class="info">
                                        <p>James Richardson</p>
                                        <p>Away</p>
                                    </div>
                                    <small class="text-muted my-auto">2 min</small>
                                </li>
                                <li class="list">
                                    <div class="profile">
                                        <img src="assets/images/faces/face5.jpg" alt="image"><span class="online"></span>
                                    </div>
                                    <div class="info">
                                        <p>Madeline Kennedy</p>
                                        <p>Available</p>
                                    </div>
                                    <small class="text-muted my-auto">5 min</small>
                                </li>
                                <li class="list">
                                    <div class="profile">
                                        <img src="assets/images/faces/face6.jpg" alt="image"><span class="online"></span>
                                    </div>
                                    <div class="info">
                                        <p>Sarah Graves</p>
                                        <p>Available</p>
                                    </div>
                                    <small class="text-muted my-auto">47 min</small>
                                </li>
                            </ul>
                        </div>
                        <!-- chat tab ends -->
                    </div>
                </div>
                <!-- partial -->
                <!-- partial:partials/_sidebar.html -->
                <nav class="sidebar sidebar-offcanvas" id="sidebar">
                    <ul class="nav">
                        <li class="nav-item nav-profile">
                            <a href="#" class="nav-link">
                                <div class="nav-profile-image">
                                    <img src="assets/images/faces/face1.jpg" alt="profile" />
                                    <span class="login-status online"></span>
                                    <!--change to offline or busy as needed-->
                                </div>
                                <div class="nav-profile-text d-flex flex-column">
                                    <span class="font-weight-bold mb-2">
                                        <asp:Literal runat="server" ID="ltrNomUsuario" Text="NomUsuario"></asp:Literal>
                                    </span>
                                    <span class="text-secondary text-small">
                                        <asp:Literal runat="server" ID="ltrPuestoUsuario" Text="Puesto"></asp:Literal>
                                    </span>
                                </div>
                                <i class="mdi mdi-bookmark-check text-success nav-profile-badge"></i>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Default.aspx">
                                <span class="menu-title">Panel Principal</span>
                                <i class="mdi mdi-home menu-icon"></i>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-bs-toggle="collapse" href="#oper" aria-expanded="false" aria-controls="oper">
                                <span class="menu-title">Operación</span>
                                <i class="menu-arrow"></i>
                                <i class="mdi mdi-lead-pencil menu-icon"></i>
                            </a>
                            <div class="collapse" id="oper">
                                <ul class="nav flex-column sub-menu">
                                    <li class="nav-item">
                                        <a class="nav-link" href="pages/operacion/configura_evento.aspx">Configura Eventos</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="pages/operacion/clientes.aspx">Inversionistas </a>
                                    </li>
                                </ul>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-bs-toggle="collapse" href="#forms" aria-expanded="false" aria-controls="forms">
                                <span class="menu-title">Tesorería</span>
                                <i class="menu-arrow"></i>
                                <i class="mdi mdi-account-credit-card menu-icon"></i>
                            </a>
                            <div class="collapse" id="forms">
                                <ul class="nav flex-column sub-menu">
                                    <li class="nav-item">
                                        <a class="nav-link" href="pages/tesoreria/cap_ingresos.aspx">Captura de Ingresos</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#">Captura de Egresos</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#">Recibos</a>
                                    </li>
                                </ul>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-bs-toggle="collapse" href="#cobranza" aria-expanded="false" aria-controls="cobranza">
                                <span class="menu-title">Cobranza</span>
                                <i class="menu-arrow"></i>
                                <i class="mdi mdi-account-alert menu-icon"></i>
                            </a>
                            <div class="collapse" id="cobranza">
                                <ul class="nav flex-column sub-menu">
                                    <li class="nav-item">
                                        <a class="nav-link" href="#">Cobranza Preventiva</a>
                                    </li>
                                </ul>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-bs-toggle="collapse" href="#auth" aria-expanded="false" aria-controls="auth">
                                <span class="menu-title">Configuración</span>
                                <i class="menu-arrow"></i>
                                <i class="mdi mdi-tools menu-icon"></i>
                            </a>
                            <div class="collapse" id="auth">
                                <ul class="nav flex-column sub-menu">
                                    <li class="nav-item">
                                        <a class="nav-link" href="#">Usuarios </a>
                                    </li>

                                </ul>
                            </div>
                        </li>

                    </ul>
                </nav>
                <!-- partial -->
                <div class="main-panel">
                    <div class="content-wrapper">
                        <div class="row">
                            <div class="col-md-6 stretch-card grid-margin">
                                <div class="card bg-gradient-success card-img-holder text-white">
                                    <div class="card-body">
                                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" style="z-index: 1;" />
                                        <h4 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center" style="z-index: 2;">Buscar Pagos STRIPE
                                            <asp:ImageButton ImageUrl="~/assets/images/actualizar1.png" ID="btnRealizarPagosStripe" Width="50" Height="50" runat="server"  OnClick="btnRealizarPagosStripe_Click"  Style="z-index: 3; position: relative;" />
                                        </h4>
                                        <h2 class="mb-5">$
                                            <asp:Literal ID="ltrMonto" runat="server"></asp:Literal></h2>
                                        <h6 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center">
                                            <asp:Literal Text="Pendientes de aplicar: " runat="server" ID="ltrTotalPagos"></asp:Literal>
                                            <asp:Button Visible="false" ID="btnAplicarSTRIPE" runat="server" Text="Aplicar Pagos STRIPE" OnClick="btnAplicarSTRIPE_Click" CssClass="btn btn-gradient-info" Style="z-index: 3; position: relative;" />
                                        </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 stretch-card grid-margin">
                                <div class="card bg-gradient-danger card-img-holder text-white">
                                    <div class="card-body">
                                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" style="z-index: 1;" />
                                        <h4 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center" style="z-index: 2;">Subir Pagos BAJIO
                                            <asp:ImageButton ID="btnProcesarExcel" runat="server" ImageUrl="~/assets/images/subir.png" OnClick="btnProcessExcel_Click" Width="50" Height="50" Style="z-index: 3; position: relative;" />
                                         </h4>
                                        <!-- Control para cargar el archivo -->
                                        <ajaxToolkit:AsyncFileUpload OnUploadedComplete="afuExcelFile_UploadedComplete" ID="afuExcelFile" runat="server" PersistFile="True" />
                                        <!-- Información -->
                                        <h2 class="mb-5"><asp:Literal ID="ltrMontoBajio" runat="server"></asp:Literal></h2>
                                        <h6 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center">
                                            <asp:Literal Text="Pendientes de aplicar" runat="server" ID="ltrBajio"></asp:Literal>
                                            <asp:Button Visible="false" ID="btnAplicarBAJIO" runat="server" Text="Aplicar Pagos BAJIO" OnClick="btnAplicarBAJIO_Click" CssClass="btn btn-gradient-info" Style="z-index: 3; position: relative;" />
                                        </h6>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-12 grid-margin">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Pagos sin aplicar de STRIPE</h4>
                                        <div class="table-responsive">
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Nombre</th>
                                                        <th>Correo</th>
                                                        <th>Amount</th>
                                                        <th>Fecha</th>
                                                        <th>CARD ID</th>
                                                        <th>ID</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptPagosSTRIPE">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="max-width: 200px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("Referencia") %></td>
                                                                <td style="max-width: 200px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("Correo") %></td>
                                                                <td>
                                                                    <label class="badge badge-gradient-success">
                                                                        <%# decimal.Parse(Eval("Amount").ToString()).ToString("N2") %></label>
                                                                </td>
                                                                <td><%# Eval("Created") %></td>
                                                                <td style="max-width: 120px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("CardID") %></td>
                                                                <td style="max-width: 120px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("ID") %></td>
                                                                <td>
                                                                    <label class="badge badge-gradient-success">
                                                                        <%# Eval("StatusGrandeza") %></label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 grid-margin">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Pagos sin aplicar de BAJIO</h4>
                                        <div class="table-responsive">
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Nombre</th>
                                                        <th>Referencia</th>
                                                        <th>Amount</th>
                                                        <th>Fecha</th>
                                                        <th>CARD ID</th>
                                                        <th>ID</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptBajio">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="max-width: 200px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("ReferenciaBajio") %></td>
                                                                <td style="max-width: 200px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("CorreoBajio") %></td>
                                                                <td>
                                                                    <label class="badge badge-gradient-success">
                                                                        <%# decimal.Parse(Eval("Amount").ToString()).ToString("N2") %></label>
                                                                </td>
                                                                <td><%# Eval("CreatedBajio") %></td>
                                                                <td style="max-width: 120px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("CardIDBajio") %></td>
                                                                <td style="max-width: 120px; word-wrap: break-word; white-space: normal;">
                                                                    <%# Eval("IDBajio") %></td>
                                                                <td>
                                                                    <label class="badge badge-gradient-success">
                                                                        <%# Eval("StatusBajio") %></label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 grid-margin">
                                <div class="card">
                                    <div class="card-body">
                                        <h1>¿Cómo va GRANDEZA?</h1>
                                        <!-- Sección de estadísticas (6 columnas) -->
                                        <div class="row">
                                            <!-- Sección de estadísticas (6 columnas) -->
                                            <div class="col-md-6 grid-margin d-flex flex-column justify-content-between">
                                                <!-- Elemento 1 -->
                                                <div class="col-12 mb-3 h-50">
                                                    <div class="card h-100">
                                                        <div class="card-body d-flex align-items-center justify-content-center text-center">
                                                            <div>
                                                                <h4 class="card-title font-weight-medium mb-3">Certificados</h4>
                                                                <h1 class="font-weight-medium mb-0">19.187</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Elemento 2 -->
                                                <div class="col-12 h-50">
                                                    <div class="card h-100">
                                                        <div class="card-body d-flex align-items-center justify-content-center text-center">
                                                            <div>
                                                                <h4 class="card-title font-weight-medium mb-3">Monto Cerrado Bruto</h4>
                                                                <h1 class="font-weight-medium mb-0">$107.164.320</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Sección de gráfica (6 columnas) -->
                                            <div class="col-md-6 grid-margin d-flex flex-column justify-content-center text-center">
                                                <div class="card-body">
                                                    <h4 class="card-title">Porcentaje de avance a la meta</h4>
                                                    <div class="mb-4" id="g1">
                                                        <svg height="100%" version="1.1" width="100%" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="overflow: hidden; position: relative; top: -0.6875px;">
                                                            <desc style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Created with Raphaël 2.1.4</desc><defs style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><filter id="inner-shadow-g1"><feOffset dx="0" dy="3"></feOffset>
                                                                <feGaussianBlur result="offset-blur" stdDeviation="5"></feGaussianBlur>
                                                                <feComposite operator="out" in="SourceGraphic" in2="offset-blur" result="inverse"></feComposite>
                                                                <feFlood flood-color="black" flood-opacity="0.2" result="color"></feFlood>
                                                                <feComposite operator="in" in="color" in2="inverse" result="shadow"></feComposite>
                                                                <feComposite operator="over" in="shadow" in2="SourceGraphic"></feComposite>
                                                            </filter>
                                                            </defs><path fill="#edebeb" stroke="none" d="M157.375,120L129.25,120A75,75,0,0,1,279.25,120L251.125,120A46.875,46.875,0,0,0,157.375,120Z" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);" filter="url(#inner-shadow-g1)"></path><path fill="#fd4600" stroke="none" d="M157.375,120L129.25,120A75,75,0,0,1,268.0723787002962,80.60832604425177L244.13898668768513,95.38020377765736A46.875,46.875,0,0,0,157.375,120Z" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);" filter="url(#inner-shadow-g1)"></path><text x="204.25" y="23.4375" text-anchor="middle" font-family="sans-serif" font-size="15px" stroke="none" fill="#999999" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: sans-serif; font-size: 15px; font-weight: bold; fill-opacity: 1;" font-weight="bold" fill-opacity="1"><tspan dy="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Meta</tspan>
                                                            </text><text x="204.25" y="117.64705882352942" text-anchor="middle" font-family="Arial" font-size="23px" stroke="none" fill="#010101" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: Arial; font-size: 23px; font-weight: bold; fill-opacity: 1;" font-weight="bold" fill-opacity="1"><tspan dy="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">58.50</tspan>
                                                            </text><text x="204.25" y="134.18552036199097" text-anchor="middle" font-family="Arial" font-size="10px" stroke="none" fill="#b3b3b3" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: Arial; font-size: 10px; font-weight: normal; fill-opacity: 1;" font-weight="normal" fill-opacity="1"><tspan dy="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">%</tspan>
                                                            </text><text x="143.3125" y="134.18552036199097" text-anchor="middle" font-family="Arial" font-size="10px" stroke="none" fill="#b3b3b3" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: Arial; font-size: 10px; font-weight: normal; fill-opacity: 1;" font-weight="normal" fill-opacity="1"><tspan dy="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">0</tspan>
                                                            </text><text x="265.1875" y="134.18552036199097" text-anchor="middle" font-family="Arial" font-size="10px" stroke="none" fill="#b3b3b3" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: Arial; font-size: 10px; font-weight: normal; fill-opacity: 1;" font-weight="normal" fill-opacity="1"><tspan dy="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">200 M</tspan>
                                                            </text></svg>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Indicadores Comerciales Acumulado</h4>
                                        <div class="d-flex">
                                            <div class="d-flex align-items-center text-muted font-weight-light">
                                                <i class="mdi mdi-clock icon-sm me-2"></i>
                                                <span>October 3rd, 2018</span>
                                            </div>
                                        </div>
                                        <div class="container mt-3">
                                            <div class="row">
                                                <div class="col-md-6 mb-3">
                                                    <div class="card aligner-wrapper h-100">
                                                        <div class="card-body">
                                                            <div class="absolute left top bottom h-100 v-strock-2 bg-primary"></div>
                                                            <p class="text-muted mb-2">CASH COLLECTED (MONTO TOTAL DE APARTADOS Y ENGANCHES)</p>
                                                            <div class="d-flex align-items-center">
                                                                <h1 class="font-weight-medium mb-2">$43.798.668,94</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <div class="card aligner-wrapper h-100">
                                                        <div class="card-body">
                                                            <div class="absolute left top bottom h-100 v-strock-2 bg-danger"></div>
                                                            <p class="text-muted mb-2">MONTO CERRADO BRUTO</p>
                                                            <div class="d-flex align-items-center">
                                                                <h1 class="font-weight-medium mb-2">$107.164.320</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <div class="card aligner-wrapper h-100">
                                                        <div class="card-body">
                                                            <div class="absolute left top bottom h-100 v-strock-2 bg-primary"></div>
                                                            <p class="text-muted mb-2">CERTIFICADOS CERRADOS</p>
                                                            <div class="d-flex align-items-center">
                                                                <h1 class="font-weight-medium mb-2">19.187</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <div class="card aligner-wrapper h-100">
                                                        <div class="card-body">
                                                            <div class="absolute left top bottom h-100 v-strock-2 bg-primary"></div>
                                                            <p class="text-muted mb-2">CERTIFICADOS CERRADOS</p>
                                                            <div class="d-flex align-items-center">
                                                                <h1 class="font-weight-medium mb-2">19.187</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-4 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card">
                                <div class="card card-statistics">
                                    <div class="card-body pb-0">
                                        <p class="text-muted">Total LEADS</p>
                                        <div class="d-flex align-items-center">
                                            <h4 class="font-weight-semibold">0</h4>
                                        </div>
                                        <small class="text-muted">Importados desde PIPEDRIVE.</small>
                                    </div>
                                    <canvas class="mt-2" height="40" id="statistics-graph-1"></canvas>
                                </div>
                            </div>
                            <div class="col-xl-4 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card">
                                <div class="card card-statistics">
                                    <div class="card-body pb-0">
                                        <p class="text-muted">Total Apartados</p>
                                        <div class="d-flex align-items-center">
                                            <h4 class="font-weight-semibold">1,173</h4>
                                        </div>
                                        <small class="text-muted">Conversión de Apartados</small>
                                    </div>
                                    <canvas class="mt-2" height="40" id="statistics-graph-3"></canvas>
                                </div>
                            </div>
                            <div class="col-xl-4 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card">
                                <div class="card card-statistics">
                                    <div class="card-body pb-0">
                                        <p class="text-muted">Enganches</p>
                                        <div class="d-flex align-items-center">
                                            <h4 class="font-weight-semibold">1,088</h4>
                                        </div>
                                        <small class="text-muted">Conversión de Enganches 93.47%</small>
                                    </div>
                                    <canvas class="mt-2" height="40" id="statistics-graph-2"></canvas>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-3 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="d-flex justify-content-center">
                                            <i class="mdi mdi-clock icon-lg text-primary d-flex align-items-center"></i>
                                            <div class="d-flex flex-column ms-4">
                                                <div class="d-flex flex-column">
                                                    <p class="mb-0">Venta prom. X Evento</p>
                                                    <h4 class="font-weight-bold">$9.953.827,00</h4>
                                                </div>
                                                <small class="text-muted">Unicamente carpetas cerradas</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="d-flex justify-content-center">
                                            <i class="mdi mdi-cart-outline icon-lg text-success d-flex align-items-center"></i>
                                            <div class="d-flex flex-column ms-4">
                                                <div class="d-flex flex-column">
                                                    <p class="mb-0">Ticket Promedio</p>
                                                    <h4 class="font-weight-bold">$103.041,69</h4>
                                                </div>
                                                <small class="text-muted">%</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="d-flex justify-content-center">
                                            <i class="mdi mdi-laptop icon-lg text-warning d-flex align-items-center"></i>
                                            <div class="d-flex flex-column ms-4">
                                                <div class="d-flex flex-column">
                                                    <p class="mb-0">Clientes por Evento</p>
                                                    <h4 class="font-weight-bold">97</h4>
                                                </div>
                                                <small class="text-muted">Unicamente carpetas cerradas</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="d-flex justify-content-center">
                                            <i class="mdi mdi-earth icon-lg text-danger d-flex align-items-center"></i>
                                            <div class="d-flex flex-column ms-4">
                                                <div class="d-flex flex-column">
                                                    <p class="mb-0">% Conversíon X Evento</p>
                                                    <h4 class="font-weight-bold">32,62 %</h4>
                                                </div>
                                                <small class="text-muted">Únicamente carpetas cerradas</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- content-wrapper ends -->
                    <!-- partial:partials/_footer.html -->
                    <footer class="footer">
                        <div class="d-sm-flex justify-content-center justify-content-sm-between">
                            <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">Copyright © 2023 <a href="https://www.bootstrapdash.com/" target="_blank">BootstrapDash</a>. All rights reserved.</span>
                            <span class="float-none float-sm-right d-block mt-1 mt-sm-0 text-center">Hand-crafted & made with <i class="mdi mdi-heart text-danger"></i></span>
                        </div>
                    </footer>
                    <!-- partial -->
                </div>
                <!-- main-panel ends -->
            </div>
            <!-- page-body-wrapper ends -->
            
        </div>
    </form>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <script src="assets/vendors/chart.js/chart.umd.js"></script>
    <script src="assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
    <script src="assets/vendors/jquery-bar-rating/jquery.barrating.min.js"></script>
    <script src="assets/vendors/justgage/raphael-2.1.4.min.js"></script>
    <script src="assets/vendors/justgage/justgage.js"></script>
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="assets/js/off-canvas.js"></script>
    <script src="assets/js/hoverable-collapse.js"></script>
    <script src="assets/js/misc.js"></script>
    <script src="assets/js/settings.js"></script>
    <script src="assets/js/todolist.js"></script>
    <script src="assets/js/jquery.cookie.js"></script>
    <!-- endinject -->
    <!-- Custom js for this page -->
    <script src="assets/js/dashboard.js"></script>
    <script src="assets/js/widgets.js"></script>
    <!-- End custom js for this page -->
</body>
</html>
