using System;
using System.Windows.Forms;

namespace SCV
{
    class Program
    {


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new JanelaServidor());
            //Ligação ao SRE
            //estabelecerLigacaoSRE();
        }
    }
}
