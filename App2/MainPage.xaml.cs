using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using XAMLtoPDF;

namespace App2
{
   
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        

        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            //Add page to the PDF document
            PdfPage page = document.Pages.Add();
            //Create graphics instance
            PdfGraphics graphics = page.Graphics;
            Stream imageStream = null;

            //Captures the XAML page as image and returns the image in memory stream
            imageStream = new MemoryStream(DependencyService.Get<ISave>().CaptureAsync().Result);

            //Load the image in PdfBitmap
            PdfBitmap image = new PdfBitmap(imageStream);

            //Draw the image to the page
            graphics.DrawImage(image, 0, 0, page.GetClientSize().Width, page.GetClientSize().Height);


            //Save the document into memory stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            stream.Position = 0;
            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().Save("XAMLtoPDF.pdf", "application/pdf", stream);
        }

    }
}
