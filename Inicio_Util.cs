using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toyota_WPP_Sender_W
{
    public partial class Form_Inicio 
    {

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("User32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static Point Position { get; set; }

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        private EtapaTimer timerAcao   = new EtapaTimer();
        private EtapaTimer timerEspera = new EtapaTimer();

        private bool timerAcao_first = true;
        private bool timerEspera_first = true;


        static public void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);

        }

        private void ExecutarAcao(Etapa proxima,  Acao acao)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            timerAcao.Interval = 250;
            timerAcao.Acao = acao;
            timerAcao.ProximaEtapa = proxima;
            progressBar1.Maximum = acao.EsperaInicial * 4;
            if (timerAcao_first)
                timerAcao.Tick += delegate(object sender, EventArgs e)
                {
                    EtapaTimer timer =(EtapaTimer) sender;
                    timerAcao_first = false;
                    progressBar1.Increment(1);
                    if (progressBar1.Value == progressBar1.Maximum)
                    {
                        timerAcao.Stop();
                        timer.Acao.Executar();
                        Esperar(timer.Acao, timer.ProximaEtapa);
                    }
                };
            timerAcao.Start();

        }

        private void Esperar(Acao acao, Etapa etapa)
        {
            progressBar2.MarqueeAnimationSpeed = 1000;
            timerEspera.Interval = 250;
            timerEspera.Acao = acao;
            progressBar2.Maximum = acao.EsperaFinal *4;
            timerEspera.ProximaEtapa = etapa;
            if(timerEspera_first)
            timerEspera.Tick += delegate(object sender, EventArgs e)
            {
                timerEspera_first = false;
                progressBar2.Increment(1);
                if (progressBar2.Value == progressBar2.Maximum)
                {
                    timerEspera.Stop();
                    ProximaEtapa(((EtapaTimer)sender).ProximaEtapa, ((EtapaTimer)sender).Acao.WhatsAPP);
                }
            };
            timerEspera.Start();
        }

        public class Click : Acao
        {
            private Point point;
            private int Y;

            public Click(Point point, int tempoInicio, int tempoEspera, string whatsapp)
            {
                this.point      = point;
                this.WhatsAPP   = whatsapp;
                EsperaFinal     = tempoEspera;
                EsperaInicial   = tempoInicio;
            }

            override public void Executar()
            {
                LeftMouseClick(point.X, point.Y);
            }
        }

        public class Digitar : Acao
        {
            private string texto;
            private bool pressEnter;
            public Digitar(string texto, int esperaInicio, int esperaFinal, string whatsapp, bool enter = false)
            {
                this.texto      = texto;
                this.WhatsAPP   = whatsapp;
                EsperaFinal     = esperaFinal;
                EsperaInicial   = esperaInicio;
                this.pressEnter = enter;
            }

            override public void Executar()
            {
                SendKeys.Send(texto.Replace("+","{+}"));
                if(pressEnter)
                    SendKeys.Send("{ENTER}");
            }
        }

        public int CheckContato()
        {
            HashSet<Color> colors = new HashSet<Color>();
            int count = 0;


            Rectangle rect = new Rectangle(11, 246, 271 - 11, 305 - 246);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);

            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Size.Height; y++)
                {
                    for (int x = 0; x < bmp.Size.Width; x++)
                    {
                        colors.Add(bmp.GetPixel(x, y));
                    }
                }
                count = colors.Count;
            }
            return count;
        }
    }
}
