using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toyota_WPP_Sender_W
{

    public class Acao 
    {
        public virtual void Executar() { }
        private int esperaFinal;
        private int esperaInicial;
        private string whatsapp;

        public string WhatsAPP
        {
            get
            {
                return whatsapp;
            }
            set
            {
                whatsapp = value;
            }
        }

        public int EsperaFinal
        {
            get
            {
                return esperaFinal;
            }
            set
            {
                esperaFinal = value;
            }
        }
        public int EsperaInicial
        {
            get
            {
                return esperaInicial;
            }
            set
            {
                esperaInicial = value;
            }
        }
    }



        

}
