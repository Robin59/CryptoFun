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

        private abstract class Cypher_Algorithm
        {
          
            protected abstract bool Key_Conform(String Key);
            protected abstract String Encrypt_Specific_Algo(String Source_Text, String Key);
            protected abstract String Decrypt_Specific_Algo(String Cypher_Text, String Key);

            public String Encrypt(String Source_Text, String Key)
            {
                if (Key_Conform(Key)) 
                    return Encrypt_Specific_Algo(Source_Text, Key);                
                    else return "The key is incompatible with the chosen algorithm";
            }
            public String Decrypt(String Cypher_Text, String Key)
            {
                if (Key_Conform(Key))
                    return Decrypt_Specific_Algo(Cypher_Text, Key);
                    else return "The key is incompatible with the chosen algorithm";
            }
        }

        private class Ceasar_Cypher : Cypher_Algorithm
        {
            protected override bool Key_Conform(String Key)
            {
                Int32 Mook_Int;
                return Int32.TryParse(Key, out Mook_Int) && (Mook_Int>=0);
            }

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

            public override String ToString() {return "Ceasar cypher";}
           
        }
               

        public Main()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            comboBox_Algo_Choice.Items.Add(new Ceasar_Cypher());            
        }
               

        private void button_Encrypt_Click(object sender, EventArgs e)
        {
            if (comboBox_Algo_Choice.SelectedItem != null) {
                textBox_Out.Text = ((Cypher_Algorithm)(comboBox_Algo_Choice.SelectedItem)).Encrypt(textBox_In.Text, textBox_Key.Text);
            }else {
                textBox_Out.Text = "Choose a cypher algorithm";
            }
        }

        private void button_Decrypt_Click(object sender, EventArgs e)
        {
            if (comboBox_Algo_Choice.SelectedItem != null)
            {
                textBox_In.Text = ((Cypher_Algorithm)(comboBox_Algo_Choice.SelectedItem)).Decrypt(textBox_Out.Text, textBox_Key.Text);
            }
            else
            {
                textBox_In.Text = "Choose a cypher algorithm";
            }
        }
    }
}
