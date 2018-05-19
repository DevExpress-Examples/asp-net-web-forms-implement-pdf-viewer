Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace E5095
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            If Session("PdfFile") IsNot Nothing Then
                viewer.PdfData = DirectCast(Session("PdfFile"), Byte())
            End If
        End Sub

        Protected Sub ucUploadPdf_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs)
            If e.IsValid Then
                Session("PdfFile") = e.UploadedFile.FileBytes
            End If
        End Sub
    End Class
End Namespace