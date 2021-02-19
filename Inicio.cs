using System;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using PhoneNumbers;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;


namespace Toyota_WPP_Sender_W
{
    public partial class Form_Inicio : Form
    {
        static string path =  @"toyota\";
        Queue<Numero> numeros = new Queue<Numero>();
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        Acao acao;
        Point point;
        Timer queueTimer = new Timer();

        public Form_Inicio()
        {
            InitializeComponent();

            queueTimer.Interval = 3000;
            queueTimer.Tick += queueTimer_Tick;
            queueTimer.Start();
            dataGridNumbers.DataSource = numeros.ToList();
            dataGridNumbers.Columns[0].Width = 300;
            Run();

            if (Directory.Exists(path))
            {
                string[] fileEntries = Directory.GetFiles(path + @"\novos\");
                string name, ext;
                string[] files;

                foreach (string fileName in fileEntries)
                {
                    files   = Path.GetFileName(fileName).Split('.');
                    name    = files[0];
                    ext     = files[1];
                    if (ext == "mp4")
                    {
                        Console.WriteLine("Arquivo {0} Encontrado.", name);
                        FFMPEG(name);
                        
                    }
                }
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ProximoNumero();
        }

        private void ProximaEtapa(Etapa etapa, string whatsapp = "")
        {
            Console.WriteLine("Etapa\n\t{0}", etapa);
            switch (etapa)
            {
                case Etapa.Atualizar:
                    queueTimer.Stop();
                    point   = new Point(84, 64);
                    acao = new Click(point, 4, 15, whatsapp);
                    ExecutarAcao(Etapa.Click_NovaConversa,  acao);
                    break;

                case Etapa.Click_NovaConversa:
                    point   = new Point(186, 113);
                    acao    = new Click(point, 4, 5, whatsapp);
                    ExecutarAcao(Etapa.Click_Campo_Pesquisar, acao);
                    break;

                case Etapa.Click_Campo_Pesquisar:
                    point   = new Point(171, 212);
                    acao    = new Click(point, 5, 5, whatsapp);
                    ExecutarAcao(Etapa.Digitar_Campo_Pesquisar, acao);
                    break;
                case Etapa.Digitar_Campo_Pesquisar:
                    acao = new Digitar(whatsapp.Remove(0,1), 2, 3, whatsapp);
                    ExecutarAcao(Etapa.OCR_Verificar_Contato, acao);
                    break;
                case Etapa.OCR_Verificar_Contato:
                    if (CheckContato() > 50)
                    {
                        Console.WriteLine("Contato Encontrado.");
                        ProximaEtapa(Etapa.Click_Resultado_Pesquisa, whatsapp);
                    }
                    else
                    {
                       // ExecutarAcao(Etapa.Click_Resultado_Pesquisa, acao);
                        Console.WriteLine("Contato Não Encontrado.");

                        SyncNumber sync = new SyncNumber();
                        sync.sync(whatsapp);
                        ProximoNumero();
                    }                    
                    break;
                case Etapa.Click_Resultado_Pesquisa:
                    point   = new Point(137, 278);
                    acao    = new Click(point, 2, 2, whatsapp);
                    ExecutarAcao(Etapa.Click_Menu, acao);
                    break;

                case Etapa.Click_Menu:
                    point   = new Point(587, 113);
                    acao    = new Click(point, 2, 2, whatsapp);
                    ExecutarAcao(Etapa.Click_Menu_Anexo, acao);
                    break;

                case Etapa.Click_Menu_Anexo:
                    point   = new Point(589, 173);
                    acao    = new Click(point, 1, 1, whatsapp);
                    ExecutarAcao(Etapa.Digitar_CaminhoVideo,acao);
                    break;

                case Etapa.Digitar_CaminhoVideo:
                    acao = new Digitar(@"C:\Users\UAU3\Documents\whatsup\toyota\completado\" + whatsapp + ".mp4", 1, 2, whatsapp, true);
                    ExecutarAcao(Etapa.Click_Envio_Legenda, acao);
                    break;
                case Etapa.Digitar_Legenda:
                    acao = new Digitar("{ENTER}", 2, 4, whatsapp);
                    ExecutarAcao(Etapa.Click_Envio_Legenda, acao);
                    break;
                case Etapa.Click_Envio_Legenda:
                    point = new Point(599, 596);
                    acao = new Click(point, 2, 40, whatsapp);
                    ExecutarAcao(Etapa.Proximo_Numero, acao);
                    break;

                case Etapa.Proximo_Numero:
                    //acao = new Digitar("{ENTER}", 2, 4, whatsapp);
                    //ExecutarAcao(Etapa.Atualizar, acao);
                    ProximoNumero();
                    break;

                default:
                    break;
            }
        }


        private void FFMPEG(string name)
        {
            try
            {
                PhoneNumber nCheck = phoneUtil.Parse(name, "BR");

                if (phoneUtil.IsValidNumber(nCheck))
                {
                    Console.WriteLine("Processando {0}", name);

                    Process ffmpeg = new Process();
                    ProcessStartInfo oInfo = new ProcessStartInfo(
                        @".\ffmpeg.exe",
                        @"-i " + path 
                        + @"novos\" + name 
                        + @".mp4  -acodec copy -vcodec libx264 -b:v 1000k -an -f mp4 -y " + path 
                        + @"compactado\" + name 
                        + ".mp4");
                     
                    oInfo.CreateNoWindow = true;
                    oInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    ffmpeg.StartInfo = oInfo;
                    ffmpeg.EnableRaisingEvents = true;

                    ffmpeg.Exited += (s, o) =>
                    {
                        string numberParsed = phoneUtil.Format(nCheck, PhoneNumberFormat.INTERNATIONAL);
                        try
                        {
                            File.Move(path + @"compactado\" + name + @".mp4", path + @"completado\" + numberParsed + @".mp4");
                            File.Delete(path + @"novos\" + name + @".mp4");
                        }
                        catch (IOException ex)
                        {
                            File.Delete(path + @"compactado\" + name + @".mp4");
                            File.Move(path + @"novos\" + name + @".mp4", path + @"errors\" + name + @"_" + new Random().Next() + @".mp4");
                            Console.WriteLine("Esse número ja teve uma mensagem enviada.\n  {0}", ex.Message);
                        }
                    };
                    ffmpeg.Start();
                }
            }
            catch (NumberParseException ex)
            {
                Console.WriteLine("Nome do arquivo não pode ser transformado em numero de telefone brasileiro!.");
            }
        }


        void queueTimer_Tick(object sender, EventArgs e)
        {
            ProximoNumero();
        }

        private void ProximoNumero()
        {
            queueTimer.Start();
            try
            {
                ProximaEtapa(Etapa.Atualizar, numeros.Dequeue().Celular);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("\tNão é possivel Forçar o inicio do programa.\n\tEspere que novos videos sejam enviados.");
            }

        }

        void timerNextNumber_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // O arquivo aqui esta sendo mantido bloqueado 
                //por algum satanismo conhecido ou desconhecido
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //Arquivo fora desbloqueado ou nunca esteve bloqueado.
            return false;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            FileSystemWatcher watcherCompletado = new FileSystemWatcher();
            watcherCompletado.Path = path + @"completado\";
            watcherCompletado.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcherCompletado.Filter = "*.mp4";
            watcherCompletado.Created += new FileSystemEventHandler(OnCreatedCompletado);
            watcherCompletado.EnableRaisingEvents = true;

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path  +@"\novos\";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.mp4";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;


        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(e.FullPath);
            while (IsFileLocked(fileInfo))
            {
                Console.WriteLine("Arquivo sendo COPIADO.");
            }
            Console.WriteLine("Copia terminada: " + e.Name.Split('.')[0] + " " + e.ChangeType);
            FFMPEG(e.Name.Split('.')[0]);
        }

        private  void OnCreatedCompletado(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("OnCreated: " + e.Name.Split('.')[0] + " " + e.ChangeType);
            SyncNumber sync = new SyncNumber();
            sync.sync(e.Name.Split('.')[0]);
            

        }

        private void contactUpdater_Tick(object sender, EventArgs e)
        {
            WebClient myService = new WebClient();

            myService.Headers["Accept"] = "application/json";

            myService.DownloadDataCompleted += (s, o) =>
            {

                if (o.Error == null)
                {
                    Newtonsoft.Json.Linq.JObject results = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(
                        System.Text.Encoding.UTF8.GetString(o.Result)
                        );

                    if (null != results)
                    {
                        var numero = results["numero"].ToString();
                        if (numero != null)
                            numeros.Enqueue(new Numero(numero));

                        Console.WriteLine("Serviço de atualização\n   Encontrado {0}", numero);
                    }

                }
            };
            myService.DownloadDataAsync((new SyncNumber()).GetUpdaterURI());

        }

        private void timerAtualizarNumeros_Tick(object sender, EventArgs e)
        {
            dataGridNumbers.DataSource = typeof(List<Numero>); ;
            dataGridNumbers.DataSource = numeros.ToList();
            dataGridNumbers.Columns[0].Width = 300;
            dataGridNumbers.Refresh();
            
        }

    }
}
