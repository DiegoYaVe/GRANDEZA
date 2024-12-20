<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ACEL.WEB.Default" %>

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
    <form runat="server">
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
                                        <h4 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center" style="z-index: 2;">Pagos STRIPE
                        <asp:Button ID="btnRealizarPagosStripe" runat="server" Text="Realizar Pagos" OnClick="btnRealizarPagosStripe_Click" CssClass="btn btn-primary" Style="z-index: 3; position: relative;" />
                                        </h4>
                                        <h2 class="mb-5">$ 15,0000</h2>
                                        <h6 class="card-text">
                                            <asp:Literal Text="Total de pagos" runat="server" ID="ltrTotalPagos"></asp:Literal></h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 stretch-card grid-margin">
                                <div class="card bg-gradient-danger card-img-holder text-white">
                                    <div class="card-body">
                                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" style="z-index: 1;" />
                                        <h4 class="font-weight-normal mb-3 d-flex justify-content-between align-items-center" style="z-index: 2;">Pagos Bajio y Otros
                                          <button class="btn btn-primary"
                                              style="z-index: 3; position: relative; background-color: transparent; border: none; cursor: pointer; padding: 0; display: inline-flex; align-items: center; justify-content: center;"
                                              onclick="btnRealizarPagosBajio_Click();">
                                              <i class="mdi mdi-upload mdi-24px"></i>
                                          </button>
                                        </h4>
                                        <h2 class="mb-5">45,6334</h2>
                                        <h6 class="card-text">
                                            <asp:Literal Text="Total de pagos" runat="server" ID="ltrBajio"></asp:Literal></h6>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 grid-margin">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Pagos aplicados</h4>
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
                                        <h4 class="card-title">Pagos no localizados</h4>
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
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="Repeater1">
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
                                            <div class="col-md-6 grid-margin">
                                                <div class="card h-100">
                                                    <div class="card-body align-items-center justify-content-center text-center">
                                                        <h4 class="card-title">Porcentaje de Avance a la Meta</h4>
                                                        <div class="doughnutjs-wrapper d-flex justify-content-center">
                                                            <canvas id="traffic-chart"></canvas>
                                                        </div>
                                                        <div id="traffic-chart-legend" class="rounded-legend legend-vertical legend-bottom-left pt-4"></div>
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
    <!-- End custom js for this page -->
</body>
</html>
