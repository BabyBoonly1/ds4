<%@ Page Title="Ubicaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ubicaciones.aspx.cs" Inherits="BodeX.Ubicaciones" %>

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

        .form-section h3 {
            color: #2d3748;
            margin-bottom: 24px;
            font-size: 1.25rem;
            font-weight: 600;
        }

        .form-group {
            margin-bottom: 20px;
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
            transition: all 0.2s ease;
        }

        .form-control:focus {
            outline: none;
            border-color: #4a5568;
            box-shadow: 0 0 0 3px rgba(74, 85, 104, 0.1);
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

        .grid-container {
            background: white;
            border-radius: 8px;
            overflow: hidden;
            border: 1px solid #e2e8f0;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
        }

        .gridview th {
            background: #f7fafc;
            color: #4a5568;
            padding: 14px 16px;
            text-align: left;
            font-weight: 600;
            font-size: 0.9rem;
            border-bottom: 1px solid #e2e8f0;
        }

        .gridview td {
            padding: 14px 16px;
            border-bottom: 1px solid #f7fafc;
            color: #2d3748;
            font-size: 0.9rem;
        }

        .gridview tr:hover {
            background: #f7fafc;
        }

        .action-buttons {
            white-space: nowrap;
        }

        .action-buttons .btn {
            padding: 6px 14px;
            margin-right: 6px;
            font-size: 0.85rem;
        }

        .badge {
            padding: 4px 10px;
            border-radius: 4px;
            font-size: 0.85rem;
            font-weight: 500;
        }

        .badge-success {
            background: #dcfce7;
            color: #166534;
        }

        .badge-warning {
            background: #fef3c7;
            color: #854d0e;
        }

        .badge-danger {
            background: #fee2e2;
            color: #991b1b;
        }

        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
        }
    </style>

    <div class="page-header">
        <h2>Gestión de Ubicaciones</h2>
    </div>

    <!-- Mensajes de Alerta -->
    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
        <div class="alert" id="divMessage" runat="server">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <!-- Formulario de Ubicación -->
    <div class="form-section">
        <h3>
            <asp:Label ID="lblFormTitle" runat="server" Text="Nueva Ubicación"></asp:Label>
        </h3>
        
        <asp:HiddenField ID="hfUbicacionID" runat="server" Value="0" />
        
        <div class="form-grid">
            <div class="form-group">
                <label>Bodega</label>
                <asp:DropDownList ID="ddlBodega" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvBodega" runat="server" 
                    ControlToValidate="ddlBodega" 
                    InitialValue="0"
                    ErrorMessage="Seleccione una bodega" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label>Pasillo</label>
                <asp:TextBox ID="txtPasillo" runat="server" CssClass="form-control" 
                    placeholder="Ej: A, B, C" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPasillo" runat="server" 
                    ControlToValidate="txtPasillo" 
                    ErrorMessage="Ingrese el pasillo" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label>Estante</label>
                <asp:TextBox ID="txtEstante" runat="server" CssClass="form-control" 
                    placeholder="Ej: 01, 02, 03" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEstante" runat="server" 
                    ControlToValidate="txtEstante" 
                    ErrorMessage="Ingrese el estante" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label>Nivel</label>
                <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control" 
                    placeholder="Ej: 1, 2, 3" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNivel" runat="server" 
                    ControlToValidate="txtNivel" 
                    ErrorMessage="Ingrese el nivel" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label>Capacidad</label>
                <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control" 
                    placeholder="Ej: 100" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCapacidad" runat="server" 
                    ControlToValidate="txtCapacidad" 
                    ErrorMessage="Ingrese la capacidad" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvCapacidad" runat="server" 
                    ControlToValidate="txtCapacidad" 
                    MinimumValue="1" 
                    MaximumValue="10000" 
                    Type="Integer" 
                    ErrorMessage="Capacidad entre 1 y 10000" 
                    ForeColor="#991b1b" 
                    Display="Dynamic">
                </asp:RangeValidator>
            </div>
        </div>

        <div style="margin-top: 24px;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
        </div>
    </div>

    <!-- GridView de Ubicaciones -->
    <div class="grid-container">
        <asp:GridView ID="gvUbicaciones" runat="server" 
            CssClass="gridview"
            AutoGenerateColumns="False" 
            DataKeyNames="UbicacionID"
            OnRowCommand="gvUbicaciones_RowCommand"
            EmptyDataText="No hay ubicaciones registradas">
            <Columns>
                <asp:BoundField DataField="NombreBodega" HeaderText="Bodega" />
                <asp:BoundField DataField="CodigoUbicacion" HeaderText="Código Ubicación" />
                <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" />
                <asp:BoundField DataField="Ocupado" HeaderText="Ocupado" />
                <asp:BoundField DataField="Disponible" HeaderText="Disponible" />
                
                <asp:TemplateField HeaderText="Ocupación">
                    <ItemTemplate>
                        <span class='badge <%# Convert.ToDouble(Eval("PorcentajeOcupado")) >= 80 ? "badge-danger" : 
                                             Convert.ToDouble(Eval("PorcentajeOcupado")) >= 50 ? "badge-warning" : "badge-success" %>'>
                            <%# String.Format("{0:F1}%", Eval("PorcentajeOcupado")) %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <div class="action-buttons">
                            <asp:Button ID="btnEditar" runat="server" 
                                Text="Editar" 
                                CssClass="btn btn-primary" 
                                CommandName="Editar" 
                                CommandArgument='<%# Eval("UbicacionID") %>' 
                                CausesValidation="false" />
                            <asp:Button ID="btnEliminar" runat="server" 
                                Text="Eliminar" 
                                CssClass="btn btn-secondary" 
                                CommandName="Eliminar" 
                                CommandArgument='<%# Eval("UbicacionID") %>' 
                                OnClientClick="return confirm('¿Está seguro de eliminar esta ubicación?');" 
                                CausesValidation="false" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>