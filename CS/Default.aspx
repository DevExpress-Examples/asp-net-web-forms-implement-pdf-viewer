<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="E5095.Default" %>

<%@ Register TagPrefix="uc" TagName="PdfViewer" Src="~/PdfViewer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Upload your PDF file (Max file size: 4Mb):
        <dx:ASPxUploadControl ID="ucUploadPdf" runat="server" UploadMode="Auto" Width="280px"
            ShowUploadButton="true" OnFileUploadComplete="ucUploadPdf_FileUploadComplete">
            <ValidationSettings AllowedFileExtensions=".pdf" MaxFileSize="4194304">
            </ValidationSettings>
            <ClientSideEvents FileUploadComplete="function(s, e) { if (e.isValid) { callbackPanel.PerformCallback(); } }" />
        </dx:ASPxUploadControl>

        <dx:ASPxCallbackPanel ID="cbViewer" runat="server" ClientInstanceName="callbackPanel">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">

                    <uc:PdfViewer ID="viewer" runat="server" PdfFilePath="FallCatalog.pdf" />

                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
    </form>
</body>
</html>
