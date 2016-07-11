using System;
using System.Collections;
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
        // End of the Ceasar cypher\\

        private class DES_Cypher : Cypher_Algorithm
        {
            const int length_Key_in_Bits=64;

            protected override bool Key_Conform(String Key)
            {   
                byte[] Key_in_Bytes = System.Text.UTF32Encoding.Default.GetBytes(Key);
                BitArray Key_in_Bits = new System.Collections.BitArray(Key_in_Bytes);
                return Key_in_Bits.Length==length_Key_in_Bits;
            }
            
            ///<param name="Message">Message in clear, must be 64 bits long</param> 
            ///<param name="Ki">The partial keys, need 16 keys of 48 bits long</param>                        
            private void DES_Algo(BitArray Message, BitArray[] Ki )
            {
                // Initial permutation on the message IP

                //Main part : Feistel algo 

                // inverse permutation on the message IP^(-1)     
            }

            ///<param name="Key">The key must be 56 bits long</param>                        
            private BitArray[] Partial_Keys_Creation(BitArray Key)
            {
                const int Nb_partial_Key = 16;
                const int Length_of_partial_Key = 48;
                int[] PC1 = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
                int[] PC2 = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };

                BitArray[] Partial_Keys = new BitArray[Nb_partial_Key];

                // first permutation and splitting in 2 tabs of bytes U and D
                Boolean[] U = new Boolean[28];
                Boolean[] D = new Boolean[28];

                for (int i = 0; i < 28; i++)                
                    U[i] = (Key[(PC1[i] - 1)]); 
                for (int i = 0; i < 28; i++)
                    D[i] = (Key[PC1[i+28] - 1]);

                //creation of the partial keys 
                for (int i=0; i<Nb_partial_Key; i++)
                {
                    //Shift on left
                    if (i==0 || i==1 || i==8 || i==15) // shift from one position
                    {
                        Boolean temp = U[0];
                        for (int j=0; j<27; j++)  U[j] = U[j + 1];
                        U[27] = temp;
                        temp = D[0];
                        for (int j = 0; j < 27; j++) D[j] = D[j + 1];
                        D[27] = temp;
                    }
                    else // shift from two positions
                    {
                        Boolean temp1 = U[0];
                        Boolean temp2 = U[1];
                        for (int j = 0; j < 26; j++) U[j] = U[j + 2];
                        U[26] = temp1;
                        U[27] = temp2;
                        temp1 = D[0];
                        temp2 = D[1];
                        for (int j = 0; j < 26; j++) D[j] = D[j + 2];
                        D[26] = temp1;
                        D[27] = temp2;
                    }
                    //Paste the 2 tabs in one and do the PC2 permutation 
                    Partial_Keys[i] = new BitArray(Length_of_partial_Key);
                    for (int j=0; j< Length_of_partial_Key; j++)          
                        Partial_Keys[i][j] = (PC2[j]<=U.Length) ? U[PC2[j]-1] : D[PC2[j]-1-U.Length];          
                }

                return Partial_Keys;
            }

            protected override String Encrypt_Specific_Algo(String Source_Text, String Key)
            {                
                byte[] TempoBytes = System.Text.UTF32Encoding.Default.GetBytes(Key);
                BitArray Key_in_Bits = new System.Collections.BitArray(TempoBytes);
                TempoBytes = System.Text.UTF32Encoding.Default.GetBytes(Source_Text);
                BitArray Source_Text_in_Bits = new System.Collections.BitArray(TempoBytes);
                BitArray[] Ki = Partial_Keys_Creation(Key_in_Bits);

                //Add bytes to the message (if necessary), dived it in part of 64 bits

                //DES_Algo(Source_Text_in_Bits, Ki);

                // convert bits to String

                return "Not working for now";
            }

            protected override String Decrypt_Specific_Algo(String Cypher_Text, String Key)
            {
                return "Not working for now";
            }

            public override String ToString() { return "DES"; }

        }
        //End of the DES cypher\\

        public Main()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            comboBox_Algo_Choice.Items.Add(new Ceasar_Cypher());
            comboBox_Algo_Choice.Items.Add(new DES_Cypher());
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
