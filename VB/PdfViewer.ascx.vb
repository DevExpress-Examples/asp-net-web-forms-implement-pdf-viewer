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

    Public Partial Class PdfViewer
        Inherits System.Web.UI.UserControl

        Private _pdfData As Byte()

        Private _pdfFilePath As String

        Private _documentProcessor As DevExpress.Pdf.PdfDocumentProcessor

        Public Sub New()
            Me._pdfData = Nothing
            Me._pdfFilePath = ""
            Me._documentProcessor = New DevExpress.Pdf.PdfDocumentProcessor()
        End Sub

        Protected ReadOnly Property DocumentProcessor As PdfDocumentProcessor
            Get
                Return Me._documentProcessor
            End Get
        End Property

        Public Property Width As Unit
            Get
                Return Me.dvDocument.Width
            End Get

            Set(ByVal value As Unit)
                Me.dvDocument.Width = value
            End Set
        End Property

        Public Property Height As Unit
            Get
                Return Me.dvDocument.Height
            End Get

            Set(ByVal value As Unit)
                Me.dvDocument.Height = value
            End Set
        End Property

        Public Property PdfFilePath As String
            Get
                Return Me._pdfFilePath
            End Get

            Set(ByVal value As String)
                Try
                    Me._pdfFilePath = value
                    If Not System.[String].IsNullOrEmpty(value) Then
                        Me.DocumentProcessor.LoadDocument(MyBase.Server.MapPath(value), True)
                        Me.BindDataView()
                    End If
                Catch ex As System.Exception
                    Me.ShowError(System.[String].Format("File Loading Failed: {0}", ex.Message))
                End Try
            End Set
        End Property

        Public Property PdfData As Byte()
            Get
                Return Me._pdfData
            End Get

            Set(ByVal value As Byte())
                Try
                    Me._pdfData = value
                    If value IsNot Nothing Then
                        Using stream As System.IO.MemoryStream = New System.IO.MemoryStream(value)
                            Me.DocumentProcessor.LoadDocument(stream, True)
                            Me.BindDataView()
                        End Using
                    End If
                Catch ex As System.Exception
                    Me.ShowError(System.[String].Format("File Loading Failed: {0}", ex.Message))
                End Try
            End Set
        End Property

        Protected Sub BindDataView()
            If Me.DocumentProcessor.Document IsNot Nothing Then
                Dim data As System.Collections.Generic.List(Of E5095.PdfViewer.PdfPageItem) = New System.Collections.Generic.List(Of E5095.PdfViewer.PdfPageItem)()
                For pageNumber As Integer = 1 To Me.DocumentProcessor.Document.Pages.Count
                    data.Add(New E5095.PdfViewer.PdfPageItem() With {.PageNumber = pageNumber})
                Next

                Me.dvDocument.DataSource = data
                Me.dvDocument.DataBind()
            End If

            Me.lbErrorMessage.Text = System.[String].Empty
        End Sub

        Protected Sub bimPdfPage_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim image As DevExpress.Web.ASPxBinaryImage = TryCast(sender, DevExpress.Web.ASPxBinaryImage)
            Dim container As DevExpress.Web.DataViewItemTemplateContainer = TryCast(image.NamingContainer, DevExpress.Web.DataViewItemTemplateContainer)
            Dim pageNumber As Integer = CInt(container.EvalDataItem("PageNumber"))
            Using bitmap As System.Drawing.Bitmap = Me.DocumentProcessor.CreateBitmap(pageNumber, 900)
                Using stream As System.IO.MemoryStream = New System.IO.MemoryStream()
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
                    image.ContentBytes = stream.ToArray()
                End Using
            End Using
        End Sub

        Protected Class PdfPageItem

            Public Property PageNumber As Integer
        End Class

        Protected Sub ShowError(ByVal message As String)
            Me.dvDocument.DataSource = Nothing
            Me.dvDocument.DataBind()
            Me.lbErrorMessage.Text = message
        End Sub
    End Class
End Namespace
