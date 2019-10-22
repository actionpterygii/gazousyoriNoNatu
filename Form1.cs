using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace natu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // gazou wo syutoku
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        // gazou wo hyouji
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.ImageLocation = textBox1.Text;
        }


        // miginiyatushidariniyaruyatu
        private void button10_Click(object sender, System.EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = pictureBox2.Image;
        }


        // zenntai no RGB no heikin
        private void button3_Click(object sender, EventArgs e)
        {
            float cR = 0;
            float cG = 0;
            float cB = 0;
            float cH = 0;
            float cS = 0;
            float cV = 0;



             Bitmap Gen = new Bitmap(textBox1.Text);

           // irowototteiku

             

             for (int i = pictureBox1.Image.Width - pictureBox1.Image.Width; i < pictureBox1.Image.Width; i++)
           {
               for (int j = pictureBox1.Image.Height - pictureBox1.Image.Height; j < pictureBox1.Image.Height; j++)
               {
                   System.Drawing.Color ARGB = Gen.GetPixel( i, j );

                   float R = ARGB.R;
                   float G = ARGB.G;
                   float B = ARGB.B;

                   float max = Math.Max(R, Math.Max(G, B));
                   float min = Math.Min(R, Math.Min(G, B));
                   float value = max;
                   float hue, saturation;

                   // RGB → HSV

                   if (max == min)
                   {
                       hue = 0f;
                       saturation = 0f;
                   }
                   else
                   {
                       float c = max - min;

                       if(max == R)
                       {
                           hue = (G - B) / c;
                       }
                       else if(max == G)
                       {
                           hue = (B - R) / c + 2f;
                       }
                       else
                       {
                           hue = (R - G) / c + 4f;
                       }

                       hue *= 60f;
                       if(hue < 0f)
                       {
                           hue += 360f;
                       }
                       saturation = c / max;
                   }
                   cR += R;
                   cG += G;
                   cB += B;
                   cH += hue;
                   cS += saturation;
                   cV += value;
               }
           }
           
           // heikin motomeru
           float average_R = cR / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_G = cG / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_B = cB / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_H = cH / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_S = cS / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_V = cV / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);

           // textbox ni ireru
           textBox2.Text = average_R.ToString();
           textBox3.Text = average_G.ToString();
           textBox4.Text = average_B.ToString();
           textBox5.Text = average_H.ToString();
           textBox6.Text = average_S.ToString();
           textBox7.Text = average_V.ToString();

           
           
        
        }

        // migi ni irokaetano dasu
        private void button4_Click(object sender, EventArgs e)
        {




            float aR = Convert.ToSingle(textBox2.Text);
            float aG = Convert.ToSingle(textBox3.Text);
            float aB = Convert.ToSingle(textBox4.Text);
            float aH = Convert.ToSingle(textBox5.Text);
            float aS = Convert.ToSingle(textBox6.Text);
            float aV = Convert.ToSingle(textBox7.Text);




            Bitmap Gen = new Bitmap(textBox1.Text);
            // iroduke
            for (int i = pictureBox1.Image.Width - pictureBox1.Image.Width;  i < pictureBox1.Image.Width; i++)
            {
                for (int j = pictureBox1.Image.Height - pictureBox1.Image.Height; j < pictureBox1.Image.Height; j++)
                {
                    System.Drawing.Color ARGB = Gen.GetPixel(i, j);

                    float R = ARGB.R;
                    float G = ARGB.G;
                    float B = ARGB.B;

                    float max = Math.Max(R, Math.Max(G, B));
                    float min = Math.Min(R, Math.Min(G, B));
                    float value = max;
                    float hue, saturation;

                    // RGB → HSV
                    if (max == min)
                    {
                        hue = 0f;
                        saturation = 0f;
                    }
                    else
                    {
                        float c = max - min;

                        if (max == R)
                        {
                            hue = (G - B) / c;
                        }
                        else if (max == G)
                        {
                            hue = (B - R) / c + 2f;
                        }
                        else
                        {
                            hue = (R - G) / c + 4f;
                        }

                        hue *= 60f;
                        if (hue < 0f)
                        {
                            hue += 360f;
                        }
                        saturation = c / max;
                    }
                   
                 
                    

                    //sagyo
                    if (R < aR && G < aG && B < aB || value < aV)
                    {
                        Gen.SetPixel(i, j, Color.Red);
                    }

                    else if (R >= aR && G >= aG && B >= aB || value > aV)
                    {
                        Gen.SetPixel(i, j, Color.Blue);
                    }

                    else
                    {
                        Gen.SetPixel(i, j, Color.White);
                    }

                    
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = Gen;
            
            
            
        }





        // hozon
        // ttp://stackoverflow.com/questions/11055258/how-to-use-savefiledialog-for-saving-images-in-c
        private void button5_Click(object sender, EventArgs e)
        {
           // Image myImage = pictureBox2.Image;
           //
           // myImage.Save("C:\\Users\\shark\\Desktop\\a.jpg");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Bmp;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                pictureBox2.Image.Save(sfd.FileName, format);
            }
        }




        // hendou shikiiti    mada totyuu
        private void button6_Click(object sender, EventArgs e)
        {
             float cR = 0;
            float cG = 0;
            float cB = 0;
            float cH = 0;
            float cS = 0;
            float cV = 0;



             Bitmap Gen = new Bitmap(textBox1.Text);

               // irowototteiku

             

             for (int i = pictureBox1.Image.Width - pictureBox1.Image.Width; i < pictureBox1.Image.Width; i++)
           {
               for (int j = pictureBox1.Image.Height - pictureBox1.Image.Height; j < pictureBox1.Image.Height; j++)
               {
                   System.Drawing.Color ARGB = Gen.GetPixel( i, j );

                   float R = ARGB.R;
                   float G = ARGB.G;
                   float B = ARGB.B;

                   float max = Math.Max(R, Math.Max(G, B));
                   float min = Math.Max(R, Math.Min(G, B));
                   float value = max;
                   float hue, saturation;

                   // RGB → HSV

                   if (max == min)
                   {
                       hue = 0f;
                       saturation = 0f;
                   }
                   else
                   {
                       float c = max - min;

                       if(max == R)
                       {
                           hue = (G - B) / c;
                       }
                       else if(max == G)
                       {
                           hue = (B - R) / c + 2f;
                       }
                       else
                       {
                           hue = (R - G) / c + 4f;
                       }

                       hue *= 60f;
                       if(hue < 0f)
                       {
                           hue += 360f;
                       }
                       saturation = c / max;
                   }
                   cR += R;
                   cG += G;
                   cB += B;
                   cH += hue;
                   cS += saturation;
                   cV += value;
               }
           }
           
           // heikin motomeru
           float average_R = cR / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_G = cG / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_B = cB / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_H = cH / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_S = cS / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);
           float average_V = cV / (float)(pictureBox1.Image.Height * pictureBox1.Image.Width);

           // textbox ni ireru
           textBox2.Text = average_R.ToString();
           textBox3.Text = average_G.ToString();
           textBox4.Text = average_B.ToString();
           textBox5.Text = average_H.ToString();
           textBox6.Text = average_S.ToString();
           textBox7.Text = average_V.ToString();

           
           
        
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            int i = e.Location.X;
            int j = e.Location.Y;


            Bitmap Gen = new Bitmap(textBox1.Text);

            

            label10.Text = i.ToString();
            label11.Text = j.ToString();

            label10.Text = pictureBox1.Width.ToString();
            label11.Text = pictureBox1.Height.ToString();
            

            System.Drawing.Color ARGB = Gen.GetPixel( i, j );

            float MR = ARGB.R;
            float MG = ARGB.G;
            float MB = ARGB.B;


            textBox8.Text = MR.ToString();
            textBox9.Text = MG.ToString();
            textBox10.Text = MB.ToString();

            
        }


        // yama toru yatu hanbun RGB
        private void button7_Click(object sender, EventArgs e)
        {
            int i = pictureBox1.Image.Width / 2;

            float aR = Convert.ToSingle(textBox2.Text);
            float aG = Convert.ToSingle(textBox3.Text);
            float aB = Convert.ToSingle(textBox4.Text);
            float aH = Convert.ToSingle(textBox5.Text);
            float aS = Convert.ToSingle(textBox6.Text);
            float aV = Convert.ToSingle(textBox7.Text);

            Bitmap Gen = new Bitmap(textBox1.Text);
            
            for (int j = pictureBox1.Image.Height - pictureBox1.Image.Height; j < pictureBox1.Image.Height; j++)
            {
                

                System.Drawing.Color ARGB = Gen.GetPixel(i, j);

                    float R = ARGB.R;
                    float G = ARGB.G;
                    float B = ARGB.B;

                    float max = Math.Max(R, Math.Max(G, B));
                    float min = Math.Max(R, Math.Min(G, B));
                    float value = max;
                    float hue, saturation;

                    // RGB → HSV
                    if (max == min)
                    {
                        hue = 0f;
                        saturation = 0f;
                    }
                    else
                    {
                        float c = max - min;

                        if (max == R)
                        {
                            hue = (G - B) / c;
                        }
                        else if (max == G)
                        {
                            hue = (B - R) / c + 2f;
                        }
                        else
                        {
                            hue = (R - G) / c + 4f;
                        }

                        hue *= 60f;
                        if (hue < 0f)
                        {
                            hue += 360f;
                        }
                        saturation = c / max;
                    }


                    // irotuke sagyo
                    if (R < aR && G < aG && B < aB || value < aV)
                    {
                        Gen.SetPixel(i, j, Color.Red);
                    }

                    else if (R >= aR && G >= aG && B >= aB || value > aV)
                    {
                        Gen.SetPixel(i, j, Color.Blue);
                    }

                    else
                    {
                        Gen.SetPixel(i, j, Color.White);
                    }

                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = Gen;
            }
           
        }








        // NET HIKIPURO no yatuni hituyou na yatu PiXelManipulator toiu
        class PixelManipulator
        {
            // 画像の幅
            public int width;

            // 画像の高さ
            public int height;

            // スキャンラインの幅
            public int stride;

            // ピクセルのバイトサイズ (24 bit RGB の場合は 3)
            public int pixelSize;

            // 画像データを展開したバイト配列
            public byte[] bytes;

            // ピクセルフォーマット
            public PixelFormat pixelFormat;

            // コンストラクタ
            public PixelManipulator()
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }
            public void SetPixel(int x, int y, byte r, byte g, byte b)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return;
                }
                int i = _GetIndex(x, y);
                bytes[i + 2] = r;
                bytes[i + 1] = g;
                bytes[i + 0] = b;
            }

            // R の値を取得する
            public byte R(int x, int y, byte defaultValue = 0)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return defaultValue;
                }
                int i = _GetIndex(x, y);
                return bytes[i + 2];
            }

            // R の値を設定する
            public void SetR(int x, int y, byte value)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return;
                }
                int i = _GetIndex(x, y);
                bytes[i + 2] = value;
            }

            // G の値を取得する
            public byte G(int x, int y, byte defaultValue = 0)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return defaultValue;
                }
                int i = _GetIndex(x, y);
                return bytes[i + 1];
            }

            // G の値を設定する
            public void SetG(int x, int y, byte value)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return;
                }
                int i = _GetIndex(x, y);
                bytes[i + 1] = value;
            }

            // B の値を取得する
            public byte B(int x, int y, byte defaultValue = 0)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return defaultValue;
                }
                int i = _GetIndex(x, y);
                return bytes[i];
            }

            // B の値を設定する
            public void SetB(int x, int y, byte value)
            {
                if (_IsValidPosition(x, y) == false)
                {
                    return;
                }
                int i = _GetIndex(x, y);
                bytes[i] = value;
            }

            // 全てのピクセルを巡回する
            public void EachPixel(Action<int, int> action)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        action(x, y);
                    }
                }
            }

            // (x, y) を中心に、周囲 size ピクセル分の R 値の配列を取得する
            public byte[,] RangeR(int x, int y, int size)
            {
                return _Range(R, x, y, size);
            }

            // (x, y) を中心に、周囲 size ピクセル分の G 値の配列を取得する
            public byte[,] RangeG(int x, int y, int size)
            {
                return _Range(G, x, y, size);
            }

            // (x, y) を中心に、周囲 size ピクセル分の B 値の配列を取得する
            public byte[,] RangeB(int x, int y, int size)
            {
                return _Range(B, x, y, size);
            }

            // PixelManipulator をコピーする
            public PixelManipulator Clone(bool copyBytes = false)
            {
                PixelManipulator result = new PixelManipulator();
                result.width = width;
                result.height = height;
                result.stride = stride;
                result.pixelSize = pixelSize;
                result.bytes = new byte[bytes.Length];
                if (copyBytes)
                {
                    Array.Copy(bytes, result.bytes, bytes.Length);
                }
                return result;
            }

            // ビットマップ画像を作成する
            public Bitmap CreateBitmap()
            {
                Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, pixelFormat);
                Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
                bitmap.UnlockBits(data);
                return bitmap;
            }

            // 指定された座標が正常な範囲に収まっているかチェックする
            private bool _IsValidPosition(int x, int y)
            {
                if (x < 0 || x >= width || y < 0 || y >= height)
                {
                    return false;
                }
                return true;
            }

            // bytes 変数の中の、指定された座標のインデックス値を取得する
            private int _GetIndex(int x, int y)
            {
                return (x + y * stride) * pixelSize;
            }

            // (x, y) を中心に、周囲 size ピクセル分のピクセルを取得する
            private byte[,] _Range(Func<int, int, byte, byte> func, int x, int y, int size)
            {
                int count = size * 2 + 1;
                byte[,] pixels = new byte[count, count];

                byte center = func(x, y, 0);
                for (int y2 = -size; y2 <= size; y2++)
                {
                    for (int x2 = -size; x2 <= size; x2++)
                    {
                        pixels[x2 + size, y2 + size] = func(x + x2, y + y2, center);
                    }
                }
                return pixels;
            }

            // Bitmap オブジェクトから PixelManipulator を作成する
            public static PixelManipulator LoadBitmap(Bitmap bitmap)
            {
                if (bitmap == null)
                {
                    return null;
                }

                PixelManipulator result = new PixelManipulator();
                result.width = bitmap.Width;
                result.height = bitmap.Height;
                result.stride = _GetScanLineSize(bitmap, result.pixelFormat);
                result.pixelSize = Image.GetPixelFormatSize(result.pixelFormat) / 8;
                result.bytes = _GetPixels(bitmap, result.pixelFormat);
                return result;
            }

            // 横の長さを計る (width よりも大きいサイズになることがある)
            private static int _GetScanLineSize(Bitmap bitmap, PixelFormat pixelFormat)
            {
                int pixelSize = Image.GetPixelFormatSize(pixelFormat) / 8;
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, pixelFormat);
                int stride = data.Stride;
                bitmap.UnlockBits(data);
                return stride / pixelSize;
            }

            // ビットマップから全てのピクセルをコピーする
            private static byte[] _GetPixels(Bitmap bitmap, PixelFormat pixelFormat)
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, pixelFormat);
                byte[] bytes = new byte[data.Stride * bitmap.Height];
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                bitmap.UnlockBits(data);
                return bytes;
            }
        }



        // NET HIKIPURO medexiann 
        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            bitmap = MedianFilter.Apply(bitmap);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = bitmap;
        }

        // NET HIKIPURO medexiann suruyatu
        class MedianFilter
        {
                // 引数で渡されたビットマップ画像にメディアンフィルタを適用します
                public static Bitmap Apply(Bitmap source, int size = 3)
                {
                    // ビットマップ画像から全てのピクセルを抜き出す
                    PixelManipulator s = PixelManipulator.LoadBitmap(source);
                    PixelManipulator d = s.Clone();

                    // 範囲チェック
                    if (size < 3)
                    {
                        size = 3;
                    }
                    if (size > 9)
                    {
                        size = 9;
                    }
                    size--;
                    size /= 2;

                    // 全てのピクセルを巡回する
                    s.EachPixel((x, y) =>
                    {
                        byte r = _Median(s.RangeR(x, y, size));
                        byte g = _Median(s.RangeG(x, y, size));
                        byte b = _Median(s.RangeB(x, y, size));
                        d.SetPixel(x, y, r, g, b);
                    });

                    // 新しいビットマップ画像を作成して、ピクセルをセットする
                    return d.CreateBitmap();
                }

                // ピクセル列の中央値を出す
                private static byte _Median(byte[,] pixels)
                {
                    List<byte> colors = new List<byte>();
                    int size = pixels.GetLength(0);
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < size; x++)
                        {
                            colors.Add(pixels[x, y]);
                        }
                    }
                    colors.Sort();
                    int index = colors.Count / 2;
                    return colors[index];
                }
            }
      


        // NET HIKIPURO gaushiann
        private void button11_Click(object sender, System.EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            bitmap = GaussianBlurFilter.Apply(bitmap);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = bitmap;
        }

        // NET HIKIPURO gaushiann suruyatu

        class GaussianBlurFilter
        {
            // 引数で渡されたビットマップ画像にガウシアンフィルタを適用します
            public static Bitmap Apply(Bitmap source, int size = 3)
            {
                // ビットマップ画像から全てのピクセルを抜き出す
                PixelManipulator s = PixelManipulator.LoadBitmap(source);
                PixelManipulator d = s.Clone();

                // 範囲チェック
                if (size < 3)
                {
                    size = 3;
                }
                if (size > 15)
                {
                    size = 15;
                }
                size--;
                size /= 2;

                // カーネルを作成する
                float[] kernel = _CreateGaussianKernel(size);

                // 全てのピクセルを巡回する
                s.EachPixel((x, y) =>
                {
                    byte r = _GaussianBlur(kernel, s.RangeR(x, y, size));
                    byte g = _GaussianBlur(kernel, s.RangeG(x, y, size));
                    byte b = _GaussianBlur(kernel, s.RangeB(x, y, size));
                    d.SetPixel(x, y, r, g, b);
                });

                // 新しいビットマップ画像を作成して、ピクセルをセットする
                return d.CreateBitmap();
            }

            // カーネルを作成する
            private static float[] _CreateGaussianKernel(int size)
            {
                float sigma = 0.215f * ((size - 1f) * 0.5f - 1f) + 0.81f;
                float[] kernel = new float[(size * 2 + 1) * (size * 2 + 1)];
                float total = 0;
                int count = 0;
                for (int y = -size; y <= size; y++)
                {
                    for (int x = -size; x <= size; x++)
                    {
                        kernel[count] = _GaussianF(x, y, sigma);
                        total += kernel[count];
                        count++;
                    }
                }
                return kernel;
            }

            // ガウス分布のメソッド
            private static float _GaussianF(float x, float y, float sigma)
            {
                float pi = (float)Math.PI;
                float sigma2 = sigma * sigma;
                return (1f / (2f * pi * sigma2)) * (float)Math.Exp(-(x * x + y * y) / (2f * sigma2));
            }

            // ガウシアンフィルタを適用する
            private static byte _GaussianBlur(float[] kernel, byte[,] pixels)
            {
                float color = 0;
                int size = pixels.GetLength(0);
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        float c = pixels[x, y];
                        c *= kernel[x + y * size];
                        color += c;
                    }
                }
                return (byte)color;
            }
        }



        // NET HIKIPURO gure-suke-ru
        private void button12_Click(object sender, System.EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            bitmap = GrayScaleFilter.Apply(bitmap);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = bitmap;
        }

        //NET HIKIPURO gure-suke-ru suruyatu
        class GrayScaleFilter
        {
            // 引数で渡されたビットマップ画像を、グレースケールに変換します
            public static Bitmap Apply(Bitmap source)
            {
                // ピクセルフォーマットは今のところ 24 bit RGB で固定
                PixelFormat pixelFormat = PixelFormat.Format24bppRgb;

                // ビットマップ画像から全てのピクセルを抜き出して、バイト配列形式にする
                byte[] bytes = _GetPixels(source, pixelFormat);

                // 全てのピクセルをグレースケールに変換する
                // (ピクセルフォーマットが変わると、ここの処理内容を変更する必要がある)
                for (int i = 0; i < bytes.Length; )
                {
                    byte color = (byte)(
                        0.29891f * bytes[i + 0] +
                        0.58661f * bytes[i + 1] +
                        0.11448f * bytes[i + 2]
                    );
                    bytes[i++] = color;
                    bytes[i++] = color;
                    bytes[i++] = color;
                }

                // 新しいビットマップ画像を作成して、ピクセルをセットする
                Bitmap bitmap = new Bitmap(source);
                _SetPixels(bitmap, pixelFormat, bytes);

                return bitmap;
            }

            // ビットマップから全てのピクセルをコピーする
            private static byte[] _GetPixels(Bitmap bitmap, PixelFormat pixelFormat)
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, pixelFormat);
                byte[] bytes = new byte[data.Stride * bitmap.Height];
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                bitmap.UnlockBits(data);

                return bytes;
            }

            // ビットマップへピクセルデータを適用する
            private static void _SetPixels(Bitmap bitmap, PixelFormat pixelFormat, byte[] bytes)
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, pixelFormat);
                Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
                bitmap.UnlockBits(data);
            }
        }





       
        

       

        
    }
}

      