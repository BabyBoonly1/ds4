<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BodeX._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .dashboard-header {
            text-align: center;
            margin-bottom: 40px;
        }

        .dashboard-header h1 {
            color: #667eea;
            font-size: 2.5rem;
            margin-bottom: 10px;
        }

        .dashboard-header p {
            color: #666;
            font-size: 1.1rem;
        }

        .stats-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-bottom: 40px;
        }

        .stat-card {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            transition: transform 0.3s ease;
        }

        .stat-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.15);
        }

        .stat-card h3 {
            font-size: 2.5rem;
            margin-bottom: 10px;
        }

        .stat-card p {
            font-size: 1.1rem;
            opacity: 0.9;
        }

        .quick-actions {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 15px;
            margin-top: 30px;
        }

        .action-btn {
            display: block;
            padding: 20px;
            background: white;
            border: 2px solid #667eea;
            color: #667eea;
            text-decoration: none;
            border-radius: 10px;
            text-align: center;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .action-btn:hover {
            background: #667eea;
            color: white;
            transform: scale(1.05);
        }

        .connection-status {
            margin-top: 20px;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
        }

        .connection-success {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .connection-error {
            background: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
    </style>

    <div class="dashboard-header">
        <h1>🏢 Sistema Bode_X</h1>
        <p>Sistema de Gestión de Ubicaciones en Bodega</p>
    </div>

    <!-- Estado de Conexión -->
    <asp:Panel ID="pnlConnectionStatus" runat="server" CssClass="connection-status" Visible="false">
        <asp:Label ID="lblConnectionStatus" runat="server"></asp:Label>
    </asp:Panel>

    <!-- Estadísticas -->
    <div class="stats-container">
        <div class="stat-card">
            <h3><asp:Label ID="lblTotalUbicaciones" runat="server" Text="0"></asp:Label></h3>
            <p>📍 Ubicaciones Activas</p>
        </div>
        
        <div class="stat-card">
            <h3><asp:Label ID="lblTotalProductos" runat="server" Text="0"></asp:Label></h3>
            <p>📦 Productos Registrados</p>
        </div>
        
        <div class="stat-card">
            <h3><asp:Label ID="lblCapacidadTotal" runat="server" Text="0"></asp:Label></h3>
            <p>📊 Capacidad Total</p>
        </div>
        
        <div class="stat-card">
            <h3><asp:Label ID="lblOcupacionPromedio" runat="server" Text="0%"></asp:Label></h3>
            <p>📈 Ocupación Promedio</p>
        </div>
    </div>

    <!-- Acciones Rápidas -->
    <h2 style="color: #667eea; margin-bottom: 20px;">⚡ Acciones Rápidas</h2>
    <div class="quick-actions">
        <a href="Ubicaciones.aspx" class="action-btn">
            ➕ Nueva Ubicación
        </a>
        <a href="AsignarProducto.aspx" class="action-btn">
            📦 Asignar Producto
        </a>
        <a href="Productos.aspx" class="action-btn">
            📊 Ver Inventario
        </a>
    </div>

    <!-- Información Adicional -->
    <div style="margin-top: 40px; padding: 20px; background: #f8f9fa; border-radius: 10px;">
        <h3 style="color: #667eea;">ℹ️ Información del Sistema</h3>
        <p><strong>Versión:</strong> 1.0</p>
        <p><strong>Base de Datos:</strong> BodeX</p>
        <p><strong>Última Actualización:</strong> <asp:Label ID="lblFechaActual" runat="server"></asp:Label></p>
    </div>

</asp:Content>