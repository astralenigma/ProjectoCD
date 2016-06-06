using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCV
{
    public partial class JanelaServidor : Form
    {
        SCV oSCV;
        public JanelaServidor()
        {
            InitializeComponent();
        }

        private void carregar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //System.IO.StreamReader sr = new
                //   System.IO.StreamReader(openFileDialog1.FileName);
                //MessageBox.Show(sr.ReadToEnd());
                //sr.Close();
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null || textBox1.Text != "")
            {
                String fileName = textBox1.Text.Split('\\').Last();
                //Ligações dos TRVs
                //O iniciado o servidor para ouvir os TRVs.
                toggleVisibility();
                oSCV = new SCV(fileName,chart1);
                oSCV.esperandoPorUmTRV();
                //Os TRVs são aceites.
                Thread aceitar = new Thread(oSCV.aceitarTRVs);
                aceitar.Start();
            }
        }

        private void toggleVisibility()
        {
            chart1.Visible = !chart1.Visible;
            textBox1.Visible = !textBox1.Visible;
            carregar.Visible = !carregar.Visible;
            label1.Visible = !label1.Visible;
        }
    }
}
