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
        enum CryptoAlgorithm { CA_Ceasar };

        private abstract class Cypher_Algorithm
        {
          
            protected abstract bool Key_Conform(String Key);
            protected abstract String Encrypt_Specific_Algo(String Source_Text, String Key);
            protected abstract String Decrypt_Specific_Algo(String Cypher_Text, String Key);

            public String Encrypt(String Source_Text, String Key)
            {
                if (Key_Conform(Key)) 
                    return Encrypt_Specific_Algo(Source_Text, Key);                
                    else return "";
            }
            public String Decrypt(String Cypher_Text, String Key)
            {
                if (Key_Conform(Key))
                    return Decrypt_Specific_Algo(Cypher_Text, Key);
                    else return "";
            }
        }

        private class Ceasar_Cypher : Cypher_Algorithm
        {
            protected override bool Key_Conform(String Key) { return true; }

            protected override String Encrypt_Specific_Algo(String Source_Text, String Key)
            {
                int Encoded_Int;
                String Result = "";
                foreach (char Character in Source_Text)
                {
                    Encoded_Int = ((int)Character) + int.Parse(Key);
                    Result = Result + (char)((Encoded_Int) % (System.Char.MaxValue));
                }

                return Result;
            }

            protected override String Decrypt_Specific_Algo(String Cypher_Text, String Key)
            {
                return Encrypt_Specific_Algo(Cypher_Text, "-"+Key);
            }
           
        }
       
        private Cypher_Algorithm Chosen_CryptoAlgorithm;

        public Main()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            Chosen_CryptoAlgorithm = new Ceasar_Cypher();
        }
               

        private void button_Encrypt_Click(object sender, EventArgs e)
        {
            textBox_Out.Text = Chosen_CryptoAlgorithm.Encrypt(textBox_In.Text, textBox_Key.Text);
        }

        private void button_Decrypt_Click(object sender, EventArgs e)
        {            
            textBox_In.Text = Chosen_CryptoAlgorithm.Decrypt(textBox_Out.Text, textBox_Key.Text);
        }
    }
}
