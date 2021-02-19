using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toyota_WPP_Sender_W
{

    class EtapaTimer : System.Windows.Forms.Timer
    {
        private Acao action;
        private Etapa proxima_etapa;

        public Etapa ProximaEtapa
        {
            get { return proxima_etapa; }
            set { proxima_etapa = value; } 
        }
        public Acao Acao
        { 
            get { return action; }
            set { action= value; } 
        }
        



    }
}
