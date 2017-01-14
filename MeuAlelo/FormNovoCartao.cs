using MeuAlelo.Source.Alelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeuAlelo
{
    public partial class FormNovoCartao : Form
    {
        public FormNovoCartao()
        {
            InitializeComponent();
            if (Alelo.Current == null)
                new Alelo();

        }
        public AleloDados Cartao { get;private set; }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.DialogResult = DialogResult.None;
                Cartao = await Alelo.Current.LoadCartao(textBox_cartao.Text, textBox_captcha.Text);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                button1.DialogResult = DialogResult.None;
            }
        }

        private async void FormNovoCartao_Load(object sender, EventArgs e)
        {
            pictureBox_captcha.Image =await Alelo.Current.NewCaptcha();
        }

        private async void pictureBox_captcha_Click(object sender, EventArgs e)
        {
            pictureBox_captcha.Image =await Alelo.Current.NewCaptcha();
        }
    }
}
