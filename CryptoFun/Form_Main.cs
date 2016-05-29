using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoFun
{
    public partial class Main : Form
    {
        enum CryptoAlgorithme {CA_Ceasar};
        private CryptoAlgorithme Chosen_CryptoAlgorithme ;
        public Main()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            Chosen_CryptoAlgorithme = CryptoAlgorithme.CA_Ceasar;
        }

       private String Ceasar_Encrypt(String Source_Text, int Key)
        {
            int Encoded_Int;
            String Result = "";   
            //UnicodeEncoding       
            foreach (char Character in Source_Text)
            {
                Encoded_Int=((int)Character) + Key;
                Result = Result + (((char)Encoded_Int)%(System.Char.MaxValue));               
            }

            return Result;
        }

        private void button_Encrypt_Click(object sender, EventArgs e)
        {
            String Encoded_Text="";

            switch (Chosen_CryptoAlgorithme)
            {
                case CryptoAlgorithme.CA_Ceasar:     
                    //if Ceasar_Key_Conform then
                    Encoded_Text = Ceasar_Encrypt(textBox_In.Text, Int32.Parse(textBox_Key.Text));
                    break;                    
            }

            textBox_Out.Text = Encoded_Text;
        }

        private void button_Decrypt_Click(object sender, EventArgs e)
        {
            String Decoded_Text = "";

            switch (Chosen_CryptoAlgorithme)
            {
                case CryptoAlgorithme.CA_Ceasar:
                    //if Ceasar_Key_Conform then
                    Decoded_Text = Ceasar_Encrypt(textBox_Out.Text, -Int32.Parse(textBox_Key.Text));
                    break;
            }

            textBox_In.Text = Decoded_Text;
        }
    }
}
