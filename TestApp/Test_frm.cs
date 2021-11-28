using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//using zatca.einvoicing;
using ZXing;
using ZXing.Common;

namespace TestApp
{
    public partial class Test_frm : Form
    {
        public Test_frm()
        {
            InitializeComponent();
        }

        private void DigitCtrl(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void NumberCrtl(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void Gent_but_Click(object sender, EventArgs e)
        {
            String Seller = Seller_txt.Text;
            String VatNo = Vat_txt.Text;
            DateTime dTime = Time_val.Value;
            Double Total = Convert.ToDouble(Invoice_txt.Text);
            Double Tax = Convert.ToDouble(Tax_txt.Text);
            string getTLVFormat =
              $"{Convert.ToChar(1)}{Convert.ToChar(UnicodeEncoding.UTF8.GetByteCount(Seller))}{Seller}"
            + $"{Convert.ToChar(2)}{Convert.ToChar(VatNo.Length)}{VatNo}"
            + $"{Convert.ToChar(3)}{Convert.ToChar(dTime.ToString("yyyy-MM-dd'T'HH:mm:ssZ").Length)}{dTime.ToString("yyyy-MM-dd'T'HH:mm:ssZ")}"
            + $"{Convert.ToChar(4)}{Convert.ToChar(Total.ToString().Length)}{Total}"
            + $"{Convert.ToChar(5)}{Convert.ToChar(Tax.ToString().Length)}{Tax}"; 
            string QRcodeFormat = Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(getTLVFormat)); 
            Hex_txt.Text =BitConverter.ToString( Convert.FromBase64String( QRcodeFormat)).Replace ('-',' ');
            Qr_Box.Image = toQrCode(Base64txt.Text=QRcodeFormat); 
        }
        Bitmap toQrCode(string base64,int width = 250, int height = 250)
        {

            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };
            Bitmap QrCode = barcodeWriter.Write(base64);

            return QrCode;
        }
    }
}
