using BibliotecaDeClasses;
using System;
using System.Net.Sockets;
using System.Windows.Forms;
namespace ProjectoARC
{
    public partial class Form1 : Form
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ProcessosComunicacao oPC;
        const int PORTASCV = 8888;
        String ficheiro = "";
        public Form1()
        {
            InitializeComponent();
            oPC = new ProcessosComunicacao(clientSocket);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Código onde recebe o voto escolhido.

            //Aqui ele envia a informação do utilizador.
            try
            {
                oPC.enviarMensagem(textBox1.Text + " " + textBox2.Text);
                //Aqui ele recebe a resposta não quer dizer que ele goste dela.
                mensagens(oPC.receberMensagem());
                //Apagar o input e mostrar o resultado.
                limparCampos();
                toggleVisibilidade();
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054)
                {
                    toggleVisibilidade();
                    MessageBox.Show("Conecção ao SCV perdida por favor reinicie o terminal.","Mensagem de erro.",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
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
                clientSocket.Connect(textBox3.Text, PORTASCV);
                ficheiro = oPC.receberMensagem();
                //inicializacaoDosPartidos(nmrPartidos);
                toggleVisibilidade();
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    MessageBox.Show("Servidor não encontrado, tente outra vez", "Mensagem de erro", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
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
            chart1.Visible = !chart1.Visible;
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
                    MessageBox.Show("A combinação Utilizador/Pass está errada.", "Mensagem de erro", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    break;
                case "2":
                    MessageBox.Show("Já votou.", "Mensagem de erro", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    break;
                case "3":
                    MessageBox.Show("Votação bem sucedida.", "Mensagem de Sucesso", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    break;
                case "4":
                    MessageBox.Show("Perdeu-se a ligação com o SRE por favor contacte o administrador.", "Mensagem de erro", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    break;
                //case 5:
                //    label3.Text = returndata;
                //    break;
                default:
                    MessageBox.Show("Erro Estranho impossível de perceber FUJAM.","Mensagem de muito erro",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
