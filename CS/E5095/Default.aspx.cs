using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E5095 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Init(object sender, EventArgs e) {
            if (Session["PdfFile"] != null) {
                viewer.PdfData = (byte[]) Session["PdfFile"];
            }
        }

        protected void ucUploadPdf_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e) {
            if (e.IsValid) {
                Session["PdfFile"] = e.UploadedFile.FileBytes;
            }
        }
    }
}