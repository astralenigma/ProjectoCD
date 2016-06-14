using BibliotecaDeClasses;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace ProjectoARC
{
    public partial class Form1 : Form
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ProcessosComunicacao oPC;
        const int PORTASCV = 8888;
        public Form1()
        {
            InitializeComponent();
            oPC = new ProcessosComunicacao(clientSocket);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Código onde recebe o voto escolhido.
            if (comboBox1.Text.CompareTo("") == 0)
            {
                return;
            }
            //Aqui ele envia a informação do utilizador.
            try
            {
                if (comboBox1.Text.CompareTo("Aprovado") == 0)
                {
                    oPC.enviarMensagem("1");
                }
                else
                {
                    oPC.enviarMensagem("0");
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054)
                {
                    toggleVisibilidade();
                    MessageBox.Show("Servidor não encontrado, tente outra vez.", "Mensagem de erro.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }
        /*Código onde os partidos são adicionados à interface.*/
        //private void inicializacaoDosPartidos(int nmrPartidos)
        //{
        //    comboBox1.Sorted = false;
        //    for (int i = 0; i <= nmrPartidos; i++)
        //    {
        //        comboBox1.Items.Add(i);
        //    }
        //}

        /// <summary>
        ///Método do botão da mensagem. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (!clientSocket.Connected)
                {
                    clientSocket.Connect(textBox3.Text, PORTASCV);
                }
                oPC.enviarMensagem(textBox1.Text + " " + textBox2.Text);
                //Aqui ele recebe a resposta não quer dizer que ele goste dela.
                mensagens(oPC.receberMensagem());
                //Apagar o input e mostrar o resultado.
                //inicializacaoDosPartidos(nmrPartidos);
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    MessageBox.Show("Servidor não encontrado, tente outra vez.", "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //toggleVisibilidade();
                    //button2.Visible = false;
                }
            }
            //Não gosto do facto de estar a usar uma string para meter o IP em vez de algo 
            //mais automático com menos necessidade de um programador a fuçar no código

        }

        //Método que troca a visiblidade das mensagens.
        private void toggleVisibilidade()
        {
            label3.Visible = !label3.Visible;
            button1.Visible = !button1.Visible;
            button2.Visible = !button2.Visible;
            comboBox1.Visible = !comboBox1.Visible;
            textBox1.Visible = !textBox1.Visible;
            label1.Visible = !label1.Visible;
            label2.Visible = !label2.Visible;
            label4.Visible = !label4.Visible;
            textBox2.Visible = !textBox2.Visible;
            nvL.Visible = !nvL.Visible;
            aprovadosL.Visible = !aprovadosL.Visible;
            reprovadosL.Visible = !reprovadosL.Visible;
            label5.Visible = !label5.Visible;
            textBox3.Visible = !textBox3.Visible;
        }

        //Método que limpa os campos de input
        private void limparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void mensagens(String mensagemRecebida)
        {
            switch (mensagemRecebida)
            {
                case "1":
                    MessageBox.Show("A combinação Utilizador/Pass está errada.", "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "2":
                    MessageBox.Show("Já votou.", "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "3":
                    toggleVisibilidade();
                    label3.Text = oPC.receberMensagem();
                    oPC.enviarMensagem("Ok");
                    Thread arThread = new Thread(updateVar);
                    arThread.Start();
                    MessageBox.Show("Login bem sucedido.", "Mensagem de Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "4":
                    MessageBox.Show("Perdeu-se a ligação com o SRE por favor contacte o administrador.", "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                //case 5:
                //    label3.Text = returndata;
                //    break;
                default:
                    MessageBox.Show("Erro Estranho impossível de perceber FUJAM.", "Mensagem de muito erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            oPC.getSocket().Close();
        }

        private void updateVar()
        {
            try
            {
                do
                {
                    actualizarResultados(oPC.receberMensagem());
                } while (clientSocket.Connected);
            }
            catch (SocketException)
            {

            }
        }

        private void actualizarResultados(String mensagem)
        {
            String[] resultados = mensagem.Split(' ');
            SetAprovados(resultados[0]);
            SetReprovados(resultados[1]);
            SetNVL(resultados[2]);
        }

        private void SetAprovados(string mensagem)
        {
            if (aprovadosL.InvokeRequired)
            {
                aprovadosL.Invoke(new MethodInvoker(delegate { aprovadosL.Text = "Aprovar=" + mensagem; }));
            }
            else
            {
                aprovadosL.Text = "Aprovar=" + mensagem;
            }
        }
        private void SetReprovados(string mensagem)
        {
            if (reprovadosL.InvokeRequired)
            {
                reprovadosL.Invoke(new MethodInvoker(delegate { reprovadosL.Text = "Reprovar=" + mensagem; }));
            }
            else
            {
                reprovadosL.Text = "Reprovar=" + mensagem;
            }
        }
        private void SetNVL(string mensagem)
        {
            if (nvL.InvokeRequired)
            {
                nvL.Invoke(new MethodInvoker(delegate { nvL.Text = "Não Votaram=" + mensagem; }));
            }
            else
            {
                nvL.Text = "Não Votaram=" + mensagem;
            }
        }

    }
}
