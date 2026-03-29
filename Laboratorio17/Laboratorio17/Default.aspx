<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Laboratorio17._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="row">
          <asp:GridView ID="MyGridView" DataSourceID="MyDataSource1"
              AllowSorting="true" AllowPaging="true"
              DataKeyNames="ProductID"
              AutoGenerateButton="true"
              runat="server"/>
            
            <asp:SqlDataSource ID="MyDataSource1" runat="server"
                ConnectionString="data source=BOO\SQLEXPRESS;initial catalog=Northwind;persist security info=True;Integrated Security=SSPI;"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT ProductId, ProductName, UnitPrice From Products"
                UpdateCommand="Update Products Set [ProductName]=@ProductName, [UnitPrice]=@UnitPrice Where [ProductId]=@ProductId">
                </asp:SqlDataSource>
        </div>
    </main>

</asp:Content>
