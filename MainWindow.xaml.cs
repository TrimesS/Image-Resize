using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace ImageResize
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
    public partial class MainWindow : Window
    {
		public bool ratio = false;
		public bool selected = false;
		public string fileName;
        public MainWindow()
        {
            InitializeComponent();
        }


		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			CommonFileDialogResult result = dialog.ShowDialog();
			if (result == CommonFileDialogResult.Ok)
			{
				fileName = dialog.FileName;
			}
			image_output.Text = fileName;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try

			{
				int count1 = int.Parse(iwidth.Text); 
				int count2 = int.Parse(iheight.Text); 
			}
			catch (Exception ec)
			{
				MessageBox.Show(ec.Message);
			}
			using (Image originalImage = Image.FromFile(image_input.Text))
			{
				// 定义新的尺寸
				int newWidth = int.Parse(iwidth.Text);
				int newHeight = int.Parse(iheight.Text);

				// 创建一个新的Bitmap对象，并设置其尺寸
				using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
				{
					// 使用Graphics对象来绘制调整尺寸后的图片
					using (Graphics graphics = Graphics.FromImage(resizedImage))
					{
						// 设置高质量插值模式
						graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
						// 设置高质量像素偏移模式
						graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
						// 设置高质量平滑模式
						graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

						// 绘制图片
						graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
					}
					
					// 保存调整尺寸后的图片
					resizedImage.Save(fileName+"/output.jpg");
					
				}
			}

			Console.WriteLine("图片尺寸已调整并保存为 output.jpg");

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image File (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*"; // 设置文件过滤器
			openFileDialog.Multiselect = false; // 是否允许选择多个文件

			if (openFileDialog.ShowDialog() == true)
			{
				string selectedFilePath = openFileDialog.FileName;
				image_input.Text = selectedFilePath; // 将选择的文件路径显示在TextBox中
			}
			using (Image image = Image.FromFile(image_input.Text)) //更新尺寸
			{
				iwidth.Text = image.Width.ToString();
				iheight.Text = image.Height.ToString();
			}
			selected = true;

		}

		private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{

		}

		public void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			ratio = true;
		}
		public void CheckBox_UnChecked(object sender, RoutedEventArgs e)
		{
			ratio = false;
		}

		private void iwidth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (selected == true && ratio==true)
			{
				using (Image image = Image.FromFile(image_input.Text)) //更新尺寸
				{
					double aspectRatio = (double)image.Width / image.Height;
					int newWidth, newHeight;
					if (image.Width > image.Height) // 图片更宽
					{
						newWidth = int.Parse(iwidth.Text);
						newHeight = (int)(newWidth / aspectRatio);
						iheight.Text = newHeight.ToString();
					}
					else // 图片更高或正方形
					{
						newHeight = int.Parse(iheight.Text);
						newWidth = (int)(newHeight * aspectRatio);
						iwidth.Text = newWidth.ToString();
					}
				}
			}
		}

		private void iheight_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (selected == true && ratio == true)
			{
				using (Image image = Image.FromFile(image_input.Text)) //更新尺寸
				{
					double aspectRatio = (double)image.Width / image.Height;
					int newWidth, newHeight;
					if (image.Width > image.Height) // 图片更宽
					{
						newWidth = int.Parse(iwidth.Text);
						newHeight = (int)(newWidth / aspectRatio);
						iheight.Text = newHeight.ToString();
					}
					else // 图片更高或正方形
					{
						newHeight = int.Parse(iheight.Text);
						newWidth = (int)(newHeight * aspectRatio);
						iwidth.Text = newWidth.ToString();
					}
				}
			}
		}
	}
}



		