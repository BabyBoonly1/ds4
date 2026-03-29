<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="BodeX.Productos" %>

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

        .filter-section {
            background: #f7fafc;
            padding: 24px;
            border-radius: 8px;
            margin-bottom: 32px;
            border: 1px solid #e2e8f0;
        }

        .filter-group {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 16px;
            align-items: end;
        }

        .filter-control {
            display: flex;
            flex-direction: column;
        }

        .filter-control label {
            font-weight: 500;
            color: #4a5568;
            margin-bottom: 8px;
            font-size: 0.9rem;
        }

        .form-control {
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
            font-size: 0.95rem;
        }

        .btn-primary {
            background: #2d3748;
            color: white;
        }

        .btn-primary:hover {
            background: #1a202c;
        }

        .grid-container {
            background: white;
            border-radius: 8px;
            overflow-x: auto;
            border: 1px solid #e2e8f0;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            min-width: 800px;
        }

        .gridview th {
            background: #f7fafc;
            color: #4a5568;
            padding: 14px 16px;
            text-align: left;
            font-weight: 600;
            position: sticky;
            top: 0;
            z-index: 10;
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

        .badge {
            padding: 4px 10px;
            border-radius: 4px;
            font-size: 0.85rem;
            font-weight: 500;
        }

        .badge-primary {
            background: #dbeafe;
            color: #1e40af;
        }

        .badge-success {
            background: #dcfce7;
            color: #166534;
        }

        .badge-info {
            background: #e0f2fe;
            color: #075985;
        }

        .no-location {
            color: #9ca3af;
            font-style: italic;
        }

        .summary-cards {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 16px;
            margin-bottom: 32px;
        }

        .summary-card {
            background: white;
            border: 1px solid #e2e8f0;
            padding: 24px;
            border-radius: 8px;
            text-align: center;
        }

        .summary-card h3 {
            font-size: 2rem;
            margin: 0 0 8px 0;
            color: #2d3748;
            font-weight: 600;
        }

        .summary-card p {
            margin: 0;
            color: #718096;
            font-size: 0.9rem;
        }

        .info-note {
            margin-top: 24px;
            padding: 16px;
            background: #f7fafc;
            border-radius: 8px;
            border: 1px solid #e2e8f0;
        }

        .info-note p {
            margin: 0;
            color: #4a5568;
            font-size: 0.9rem;
        }

        .info-note strong {
            color: #2d3748;
        }

        .action-btn-small {
            padding: 6px 14px;
            font-size: 0.85rem;
        }
    </style>

    <div class="page-header">
        <h2>Inventario de Productos</h2>
    </div>

    <!-- Tarjetas de Resumen -->
    <div class="summary-cards">
        <div class="summary-card">
            <h3><asp:Label ID="lblTotalProductos" runat="server" Text="0"></asp:Label></h3>
            <p>Total Productos</p>
        </div>
        <div class="summary-card">
            <h3><asp:Label ID="lblProductosConUbicacion" runat="server" Text="0"></asp:Label></h3>
            <p>Con Ubicación</p>
        </div>
        <div class="summary-card">
            <h3><asp:Label ID="lblUnidadesTotales" runat="server" Text="0"></asp:Label></h3>
            <p>Unidades Totales</p>
        </div>
    </div>

    <!-- Sección de Filtros -->
    <div class="filter-section">
        <div class="filter-group">
            <div class="filter-control">
                <label>Buscar por Nombre/Código</label>
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" 
                    placeholder="Ingrese nombre o código"></asp:TextBox>
            </div>
            
            <div class="filter-control">
                <label>Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                    <asp:ListItem Value="0">Todas las categorías</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="filter-control">
                <label>Bodega</label>
                <asp:DropDownList ID="ddlBodega" runat="server" CssClass="form-control">
                    <asp:ListItem Value="0">Todas las bodegas</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="filter-control">
                <label>&nbsp;</label>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                    CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
            </div>
        </div>
    </div>

    <!-- GridView de Productos -->
    <div class="grid-container">
        <asp:GridView ID="gvProductos" runat="server" 
            CssClass="gridview"
            AutoGenerateColumns="False"
            DataKeyNames="InventarioID"
            OnRowCommand="gvProductos_RowCommand"
            EmptyDataText="No se encontraron productos">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                
                <asp:TemplateField HeaderText="Producto">
                    <ItemTemplate>
                        <strong><%# Eval("NombreProducto") %></strong><br />
                        <small style="color: #718096;"><%# Eval("Descripcion") %></small>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Categoría">
                    <ItemTemplate>
                        <span class="badge badge-primary">
                            <%# string.IsNullOrEmpty(Eval("Categoria").ToString()) ? "Sin categoría" : Eval("Categoria") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ubicación">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(Eval("CodigoUbicacion").ToString()) 
                            ? "<span class='no-location'>Sin ubicación</span>" 
                            : "<span class='badge badge-success'>" + Eval("CodigoUbicacion") + "</span>" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bodega">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(Eval("NombreBodega").ToString()) 
                            ? "<span class='no-location'>-</span>" 
                            : Eval("NombreBodega") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <strong>
                            <%# string.IsNullOrEmpty(Eval("Cantidad").ToString()) || Eval("Cantidad").ToString() == "0" 
                                ? "-" 
                                : Eval("Cantidad") %>
                        </strong>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Precio">
                    <ItemTemplate>
                        <%# Eval("PrecioUnitario") != DBNull.Value 
                            ? String.Format("${0:N2}", Eval("PrecioUnitario")) 
                            : "-" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnEliminarAsignacion" 
                            Text="Quitar" 
                            CssClass="btn btn-secondary action-btn-small" 
                            CommandName="EliminarAsignacion" 
                            CommandArgument='<%# Eval("InventarioID") %>' 
                            OnClientClick="return confirm('¿Está seguro de eliminar esta asignación?');" 
                            CausesValidation="false"
                            Visible='<%# !string.IsNullOrEmpty(Eval("InventarioID").ToString()) && Eval("InventarioID").ToString() != "0" %>' />
                        <span class='no-location' 
                            style='<%# string.IsNullOrEmpty(Eval("InventarioID").ToString()) || Eval("InventarioID").ToString() == "0" ? "" : "display:none;" %>'>
                            -
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Información adicional -->
    <div class="info-note">
        <p>
            <strong>Nota:</strong> Esta vista muestra todos los productos registrados en el sistema junto con sus ubicaciones actuales en las bodegas.
            Los productos sin ubicación asignada se muestran como "Sin ubicación".
        </p>
    </div>

</asp:Content>