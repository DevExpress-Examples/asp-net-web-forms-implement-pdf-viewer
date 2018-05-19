<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="PdfViewer.ascx.vb" Inherits="E5095.PdfViewer" %>
<dx:ASPxLabel ID="lbErrorMessage" runat="server" ForeColor="Red">
</dx:ASPxLabel>
<dx:ASPxDataView ID="dvDocument" runat="server">
    <SettingsTableLayout ColumnCount="1" RowsPerPage="1" />
    <PagerSettings ShowNumericButtons="True">
        <AllButton Visible="True">
        </AllButton>
    </PagerSettings>
    <ItemTemplate>
        <dx:ASPxBinaryImage ID="bimPdfPage" runat="server" OnDataBinding="bimPdfPage_DataBinding">
        </dx:ASPxBinaryImage>
    </ItemTemplate>
    <ItemStyle>
        <Paddings Padding="0px" />
    </ItemStyle>
</dx:ASPxDataView>