<%@ Page Title="Asignar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarProducto.aspx.cs" Inherits="BodeX.AsignarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .page-header {
            border-bottom: 1px solid #e2e8f0;
            padding-bottom: 20px;
            margin-bottom: 32px;
        }

        .page-header h2 {
            color: #2d3748;
            margin: 0;
            font-size: 1.75rem;
            font-weight: 600;
            letter-spacing: -0.5px;
        }

        .form-section {
            background: #f7fafc;
            padding: 32px;
            border-radius: 8px;
            margin-bottom: 32px;
            border: 1px solid #e2e8f0;
        }

        .form-group {
            margin-bottom: 24px;
        }

        .form-group label {
            display: block;
            font-weight: 500;
            color: #4a5568;
            margin-bottom: 8px;
            font-size: 0.9rem;
        }

        .form-control {
            width: 100%;
            padding: 10px 14px;
            border: 1px solid #cbd5e0;
            border-radius: 6px;
            font-size: 0.95rem;
            color: #2d3748;
            background: #ffffff;
        }

        .form-control:focus {
            outline: none;
            border-color: #4a5568;
        }

        .btn {
            padding: 10px 24px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-weight: 500;
            transition: all 0.2s ease;
            margin-right: 12px;
            font-size: 0.95rem;
        }

        .btn-primary {
            background: #2d3748;
            color: white;
        }

        .btn-primary:hover {
            background: #1a202c;
        }

        .btn-secondary {
            background: #e2e8f0;
            color: #4a5568;
        }

        .btn-secondary:hover {
            background: #cbd5e0;
        }

        .info-card {
            background: white;
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            padding: 24px;
            margin-bottom: 24px;
        }

        .info-card h4 {
            color: #2d3748;
            font-size: 1.1rem;
            margin-bottom: 16px;
            font-weight: 600;
        }

        .info-row {
            display: flex;
            justify-content: space-between;
            padding: 10px 0;
            border-bottom: 1px solid #f7fafc;
        }

        .info-row:last-child {
            border-bottom: none;
        }

        .info-label {
            color: #718096;
            font-size: 0.9rem;
        }

        .info-value {
            color: #2d3748;
            font-weight: 500;
            font-size: 0.9rem;
        }

        .capacity-bar {
            height: 24px;
            background: #f7fafc;
            border-radius: 6px;
            overflow: hidden;
            margin-top: 8px;
            border: 1px solid #e2e8f0;
        }

        .capacity-fill {
            height: 100%;
            background: linear-gradient(135deg, #28a745 0%, #218838 100%);
            transition: width 0.3s ease;
        }

        .alert {
            padding: 14px 18px;
            border-radius: 6px;
            margin-bottom: 24px;
            font-size: 0.95rem;
        }

        .alert-success {
            background: #f0fdf4;
            color: #166534;
            border: 1px solid #bbf7d0;
        }

        .alert-danger {
            background: #fef2f2;
            color: #991b1b;
            border: 1px solid #fecaca;
        }
    </style>

    <div class="page-header">
        <h2>Asignar Producto a Ubicación</h2>
    </div>

    <!-- Mensajes de Alerta -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
        <div class="alert" id="divMessage" runat="server">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <!-- Formulario de Asignación -->
    <div class="form-section">
        <div class="form-group">
            <label>Seleccione el Producto</label>
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvProducto" runat="server" 
                ControlToValidate="ddlProducto" 
                InitialValue="0"
                ErrorMessage="Seleccione un producto" 
                ForeColor="#991b1b" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <!-- Información del Producto -->
        <asp:Panel ID="pnlProductoInfo" runat="server" Visible="false">
            <div class="info-card">
                <h4>Información del Producto</h4>
                <div class="info-row">
                    <span class="info-label">Código:</span>
                    <span class="info-value"><asp:Label ID="lblProductoCodigo" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Nombre:</span>
                    <span class="info-value"><asp:Label ID="lblProductoNombre" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Categoría:</span>
                    <span class="info-value"><asp:Label ID="lblProductoCategoria" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Precio:</span>
                    <span class="info-value"><asp:Label ID="lblProductoPrecio" runat="server"></asp:Label></span>
                </div>
            </div>
        </asp:Panel>

        <div class="form-group">
            <label>Seleccione la Ubicación</label>
            <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="form-control" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUbicacion" runat="server" 
                ControlToValidate="ddlUbicacion" 
                InitialValue="0"
                ErrorMessage="Seleccione una ubicación" 
                ForeColor="#991b1b" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <!-- Información de la Ubicación -->
        <asp:Panel ID="pnlUbicacionInfo" runat="server" Visible="false">
            <div class="info-card">
                <h4>Información de la Ubicación</h4>
                <div class="info-row">
                    <span class="info-label">Código:</span>
                    <span class="info-value"><asp:Label ID="lblUbicacionCodigo" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Bodega:</span>
                    <span class="info-value"><asp:Label ID="lblUbicacionBodega" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Capacidad Total:</span>
                    <span class="info-value"><asp:Label ID="lblUbicacionCapacidad" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Ocupado:</span>
                    <span class="info-value"><asp:Label ID="lblUbicacionOcupado" runat="server"></asp:Label></span>
                </div>
                <div class="info-row">
                    <span class="info-label">Disponible:</span>
                    <span class="info-value"><asp:Label ID="lblUbicacionDisponible" runat="server"></asp:Label></span>
                </div>
                <div style="margin-top: 16px;">
                    <span class="info-label">Ocupación: <asp:Label ID="lblPorcentajeOcupacion" runat="server"></asp:Label></span>
                    <div class="capacity-bar">
                        <div id="capacityFill" runat="server" class="capacity-fill"></div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="form-group">
            <label>Cantidad a Asignar</label>
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" 
                TextMode="Number" Text="1" min="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                ControlToValidate="txtCantidad" 
                ErrorMessage="Ingrese la cantidad" 
                ForeColor="#991b1b" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvCantidad" runat="server" 
                ControlToValidate="txtCantidad" 
                MinimumValue="1" 
                MaximumValue="10000" 
                Type="Integer" 
                ErrorMessage="Cantidad entre 1 y 10000" 
                ForeColor="#991b1b" 
                Display="Dynamic">
            </asp:RangeValidator>
        </div>

        <div style="margin-top: 32px;">
            <asp:Button ID="btnAsignar" runat="server" Text="Asignar Producto" 
                CssClass="btn btn-primary" OnClick="btnAsignar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" CausesValidation="false" />
        </div>
    </div>

    <div class="info-card" style="margin-top: 32px;">
        <h4>Instrucciones</h4>
        <ol style="margin: 0; padding-left: 20px; color: #4a5568; line-height: 1.8;">
            <li>Seleccione el producto que desea asignar</li>
            <li>Seleccione la ubicación destino en la bodega</li>
            <li>Ingrese la cantidad de unidades a asignar</li>
            <li>Verifique que la ubicación tenga capacidad disponible</li>
            <li>Haga clic en "Asignar Producto" para completar la operación</li>
        </ol>
    </div>

</asp:Content>