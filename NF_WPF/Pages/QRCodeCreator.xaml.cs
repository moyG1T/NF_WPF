using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using NF_WPF.Data;

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRCodeCreator.xaml
    /// </summary>
    public partial class QRCodeCreator : Page
    {
        private Student student;
        public QRCodeCreator(Student student)
        {
            InitializeComponent();
            this.student = student;
        }

        private void CreateQRButton_Click(object sender, RoutedEventArgs e)
        {
            string source = student.Surname;

            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = qr.CreateQrCode(source, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode code = new QRCoder.QRCode(data);

            Bitmap bitmap = code.GetGraphic(100);

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                QRCodeImage.Source = bitmapimage;
            }
        }

    }
}
