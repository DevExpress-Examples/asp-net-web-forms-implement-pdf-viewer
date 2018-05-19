using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Pdf;
using System.IO;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxDataView;
using System.Drawing;
using System.Drawing.Imaging;

namespace E5095 {
    public partial class PdfViewer : System.Web.UI.UserControl {
        byte[] _pdfData;
        string _pdfFilePath;
        PdfDocumentProcessor _documentProcessor;

        public PdfViewer() {
            _pdfData = null;
            _pdfFilePath = "";
            _documentProcessor = new PdfDocumentProcessor();
        }

        protected PdfDocumentProcessor DocumentProcessor {
            get {
                return _documentProcessor;
            }
        }

        public Unit Width {
            get {
                return dvDocument.Width;
            }
            set {
                dvDocument.Width = value;
            }
        }

        public Unit Height {
            get {
                return dvDocument.Height;
            }
            set {
                dvDocument.Height = value;
            }
        }

        public string PdfFilePath {
            get {
                return _pdfFilePath;
            }
            set {
                _pdfFilePath = value;
                if (!String.IsNullOrEmpty(value)) {
                    DocumentProcessor.LoadDocument(Server.MapPath(value));
                    BindDataView();
                }
            }
        }

        public byte[] PdfData {
            get {
                return _pdfData;
            }
            set {
                _pdfData = value;
                if (value != null) {
                    using (MemoryStream stream = new MemoryStream(value)) {
                        DocumentProcessor.LoadDocument(stream);
                        BindDataView();
                    }
                }
            }
        }

        protected void BindDataView() {
            if (DocumentProcessor.Document != null) {
                List<PdfPageItem> data = new List<PdfPageItem>();
                for (int pageNumber = 1; pageNumber <= DocumentProcessor.Document.Pages.Count; pageNumber++) {
                    data.Add(new PdfPageItem() {
                        PageNumber = pageNumber
                    });
                }
                dvDocument.DataSource = data;
                dvDocument.DataBind();
            }
        }

        protected void bimPdfPage_DataBinding(object sender, EventArgs e) {
            ASPxBinaryImage image = sender as ASPxBinaryImage;
            DataViewItemTemplateContainer container = image.NamingContainer as DataViewItemTemplateContainer;
            int pageNumber = (int) container.EvalDataItem("PageNumber");

            using (Bitmap bitmap = DocumentProcessor.CreateBitmap(pageNumber, 900)) {
                using (MemoryStream stream = new MemoryStream()) {
                    bitmap.Save(stream, ImageFormat.Png);
                    image.ContentBytes = stream.ToArray();
                }
            }

        }

        protected class PdfPageItem {
            public int PageNumber {
                get;
                set;
            }
        }
    }
}