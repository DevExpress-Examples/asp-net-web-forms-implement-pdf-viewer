Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace E5095

    Public Partial Class [Default]
        Inherits Page

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            If MyBase.Session("PdfFile") IsNot Nothing Then
                viewer.PdfData = CType(MyBase.Session("PdfFile"), Byte())
            End If
        End Sub

        Protected Sub ucUploadPdf_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs)
            If e.IsValid Then
                MyBase.Session("PdfFile") = e.UploadedFile.FileBytes
            End If
        End Sub
    End Class
End Namespace
