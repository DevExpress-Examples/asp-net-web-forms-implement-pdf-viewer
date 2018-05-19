Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Pdf
Imports System.IO
Imports DevExpress.Web
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace E5095
    Partial Public Class PdfViewer
        Inherits System.Web.UI.UserControl

        Private _pdfData() As Byte
        Private _pdfFilePath As String
        Private _documentProcessor As PdfDocumentProcessor

        Public Sub New()
            _pdfData = Nothing
            _pdfFilePath = ""
            _documentProcessor = New PdfDocumentProcessor()
        End Sub

        Protected ReadOnly Property DocumentProcessor() As PdfDocumentProcessor
            Get
                Return _documentProcessor
            End Get
        End Property

        Public Property Width() As Unit
            Get
                Return dvDocument.Width
            End Get
            Set(ByVal value As Unit)
                dvDocument.Width = value
            End Set
        End Property

        Public Property Height() As Unit
            Get
                Return dvDocument.Height
            End Get
            Set(ByVal value As Unit)
                dvDocument.Height = value
            End Set
        End Property

        Public Property PdfFilePath() As String
            Get
                Return _pdfFilePath
            End Get
            Set(ByVal value As String)
                Try
                    _pdfFilePath = value
                    If Not String.IsNullOrEmpty(value) Then
                        DocumentProcessor.LoadDocument(Server.MapPath(value), True)
                        BindDataView()
                    End If
                Catch ex As Exception
                    ShowError(String.Format("File Loading Failed: {0}", ex.Message))
                End Try
            End Set
        End Property

        Public Property PdfData() As Byte()
            Get
                Return _pdfData
            End Get
            Set(ByVal value As Byte())
                Try
                    _pdfData = value
                    If value IsNot Nothing Then
                        Using stream As New MemoryStream(value)
                            DocumentProcessor.LoadDocument(stream, True)
                            BindDataView()
                        End Using
                    End If
                Catch ex As Exception
                    ShowError(String.Format("File Loading Failed: {0}", ex.Message))
                End Try
            End Set
        End Property

        Protected Sub BindDataView()
            If DocumentProcessor.Document IsNot Nothing Then
                Dim data As New List(Of PdfPageItem)()
                For pageNumber As Integer = 1 To DocumentProcessor.Document.Pages.Count
                    data.Add(New PdfPageItem() With {.PageNumber = pageNumber})
                Next pageNumber
                dvDocument.DataSource = data
                dvDocument.DataBind()
            End If
            lbErrorMessage.Text = String.Empty
        End Sub

        Protected Sub bimPdfPage_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
            Dim image As ASPxBinaryImage = TryCast(sender, ASPxBinaryImage)
            Dim container As DataViewItemTemplateContainer = TryCast(image.NamingContainer, DataViewItemTemplateContainer)
            Dim pageNumber As Integer = CInt((container.EvalDataItem("PageNumber")))

            Using bitmap As Bitmap = DocumentProcessor.CreateBitmap(pageNumber, 900)
                Using stream As New MemoryStream()
                    bitmap.Save(stream, ImageFormat.Png)
                    image.ContentBytes = stream.ToArray()
                End Using
            End Using

        End Sub

        Protected Class PdfPageItem
            Public Property PageNumber() As Integer
        End Class

        Protected Sub ShowError(ByVal message As String)
            dvDocument.DataSource = Nothing
            dvDocument.DataBind()
            lbErrorMessage.Text = message
        End Sub
    End Class
End Namespace