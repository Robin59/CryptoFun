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
                return Int32.TryParse(Key, out Mook_Int) && (Mook_Int >= 0);
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
                return Encrypt_Specific_Algo(Cypher_Text, "-" + Key);
            }

            public override String ToString() { return "Ceasar cypher"; }

        }
        // End of the Ceasar cypher\\

        private class DES_Cypher : Cypher_Algorithm
        {
            const int length_Key_in_Bits = 64;
            private byte[] IP = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
            private byte[] FP = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
            private byte[] E = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
            private byte[] P = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
            //The S-boxes S[0] represent S1, S[1] is S2 etc. 
            private byte[,,] S = { { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 }, { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 }, { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 }, { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } }, { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 }, { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 }, { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 }, { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } }, { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 }, { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 }, { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 }, { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } }, { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 }, { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 }, { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 }, { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } }, { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 }, { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 }, { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 }, { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } }, { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 }, { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 }, { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 }, { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } }, { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 }, { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 }, { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 }, { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } }, { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 }, { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 }, { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 }, { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } } };

            /// <summary>
            /// inverse the position of the most and least significant bit in the bit array
            /// ex : 0100000 become 00000010
            /// </summary>
            /// <param name="aBitArray"></param>
            private void change_Least_Significant_Bit_position(BitArray aBitArray)
            {
                BitArray tempBitArray = new BitArray(aBitArray);
                for (int i = 0; i < aBitArray.Length; i++)
                {
                    int x = (i / 8) * 8 + (7 - i % 8);
                    aBitArray[i] = tempBitArray[x];
                }
            }

            protected override bool Key_Conform(String Key)
            {
                byte[] Key_in_Bytes = System.Text.UTF8Encoding.Default.GetBytes(Key);
                BitArray Key_in_Bits = new System.Collections.BitArray(Key_in_Bytes);
                return Key_in_Bits.Length == length_Key_in_Bits;
            }

            ///<param name="L">left parts of the message, 32 bits long</param> 
            ///<param name="R">Rigth parts of the message, 32 bits long</param> 
            ///<param name="Ki">The partial keys, need 16 keys of 48 bits long</param>                        
            private void Feistel_Algo(BitArray L, BitArray R, BitArray[] Ki)
            {
                for (int i=0; i<Ki.Length; i++)
                {

                    BitArray ExpendedR = new BitArray(48);
                    for (int j = 0; j < 48; j++) ExpendedR[j] = R[(E[j] - 1)]; // Expension fonction, R goes from 32 bits to 48

                    ExpendedR.Xor(Ki[i]);// Add(Xor operator) the partial key ki to the rigth part

                    // the transformed left part is cut in 8 parts of 6 bits 
                    byte[] SBoxesResult = new byte[4]; //Since the 8 results of the S-Boxes are 4 bits long, we're putting two results in one byte
                    for (int j=0; j<8; j++)
                    {
                        // for each of the 8 parts, the first bit plus the last one decide the ordinate, the middle bits define the abscissa
                        int x = ExpendedR[j*6] ? 2 : 0; 
                        x = ExpendedR[j*6+5] ? x+1 : x;
                        int y = ExpendedR[j*6+1] ? 8 : 0;
                        y = ExpendedR[j*6+2] ? y+4 : y;
                        y = ExpendedR[j*6+3] ? y+2 : y;
                        y = ExpendedR[j*6+4] ? y+1 : y;

                        //Since the 8 results of the S-Boxes are 4 bits long, we're putting two results in one byte
                        SBoxesResult[j/2] = (j % 2==0)? (byte)(S[j, x, y]*16): (byte)(SBoxesResult[j/2]+S[j,x,y]);                       
                    }
                         
                    BitArray SBoxesResult_Bits = new BitArray(SBoxesResult);
                    change_Least_Significant_Bit_position(SBoxesResult_Bits);

                    //final permutation to have the result of the fonction f
                    BitArray f = new BitArray (SBoxesResult_Bits.Length);
                    for (int j = 0; j < SBoxesResult_Bits.Length; j++)
                        f[j] = SBoxesResult_Bits[(P[j]-1)];

                    L.Xor(f);

                    //left part and rigth part change positions
                    BitArray temp_left = L;//new BitArray(L);
                    L = R;
                    R = temp_left;
                }
                
            }        

            ///<param name="Key">The key must be 64 bits long, even if only 56 bits of it are use</param>                        
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
                return Crypt_Decrypt_Specific_Algo(Source_Text,Key, false);
            }

            protected override String Decrypt_Specific_Algo(String Cypher_Text, String Key)
            {
                return Crypt_Decrypt_Specific_Algo(Cypher_Text, Key, true);
            }

            // Crypt_Decrypt_Specific_Algo is a factorisation of the code use by Encrypt_Specific_Algo and Decrypt_Specific_Algo
            // The encryption and decryption algo are the same, the only change is the order of the keys
            protected String Crypt_Decrypt_Specific_Algo(String Text, String Key, Boolean encrypt)
            {
                const String _NULL_VALUE_ = "<NULL>";// this constante is use to replace the '\0' char (since we won't be able to read the \0 char again for decoding) 
                const int Message_Blocks_Lenght = 64;
                String Coded_text_String = "";
                byte[] TempoBytes = System.Text.UTF8Encoding.Default.GetBytes(Key);
                BitArray Key_in_Bits = new System.Collections.BitArray(TempoBytes);
                change_Least_Significant_Bit_position(Key_in_Bits);// change MSB and LSB positions to make it more conveniant for the rest of the algo
                BitArray[] Ki = Partial_Keys_Creation(Key_in_Bits);                
                if (encrypt) Array.Reverse(Ki);//the order of the keys are change if we want to decrypt

                Text = Text.Replace(_NULL_VALUE_, "\0");
                while (Text.Length % 8 != 0) // Add bytes to the message (if necessary)
                    Text += " ";                
                TempoBytes = System.Text.UTF8Encoding.Default.GetBytes(Text);
                BitArray Source_Text_in_Bits = new System.Collections.BitArray(TempoBytes);
                change_Least_Significant_Bit_position(Source_Text_in_Bits);
                // divid message in part of 64 bits
                for (int i = 0; i < Source_Text_in_Bits.Length / Message_Blocks_Lenght; i++)
                {
                    // Initial permutation PI on the message + split it in two parts, the left part and the rigth part
                    BitArray L = new BitArray(Message_Blocks_Lenght / 2);
                    BitArray R = new BitArray(Message_Blocks_Lenght / 2);
                    for (int j = 0; j < (Message_Blocks_Lenght / 2); j++)
                        L[j] = Source_Text_in_Bits[IP[j] - 1 + i * 64];
                    for (int j = (Message_Blocks_Lenght / 2); j < (Message_Blocks_Lenght); j++)
                        R[j - (Message_Blocks_Lenght / 2)] = Source_Text_in_Bits[IP[j] - 1 + i * 64];

                    Feistel_Algo(L, R, Ki);

                    // switch and aggregate the left and rigth parts + final permutation IP^(-1) on the coded message
                    BitArray Coded_text_In_Bits = new BitArray(64);
                    for (int j = 0; j < FP.Length; j++)
                        Coded_text_In_Bits[j] = (FP[j] - 1) < R.Length ? R[FP[j] - 1] : L[FP[j] - 1 - R.Length];

                    // convert bits to String  
                    byte[] Coded_text_In_Bytes = new byte[Coded_text_In_Bits.Length / 8];
                    change_Least_Significant_Bit_position(Coded_text_In_Bits);//Put back the MSB and LSB in the order .Net is use to
                    Coded_text_In_Bits.CopyTo(Coded_text_In_Bytes, 0);                    
                    Coded_text_String = Coded_text_String + System.Text.UTF8Encoding.Default.GetString(Coded_text_In_Bytes);
                }

                return Coded_text_String.Replace("\0",_NULL_VALUE_);
                //return Coded_text_String;
            }


            public override String ToString() { return "DES"; }

        }
        //End of the DES cypher\\
        
        private class TDES_Cypher : DES_Cypher {

            const int length_Key_in_Bits = 128;          

            protected override bool Key_Conform(String Key)
            {
                byte[] Key_in_Bytes = System.Text.UTF8Encoding.Default.GetBytes(Key);
                BitArray Key_in_Bits = new System.Collections.BitArray(Key_in_Bytes);
                return Key_in_Bits.Length == length_Key_in_Bits;
            }

            protected override String Encrypt_Specific_Algo(String Source_Text, String Key)
            {
                String Key1 = Key.Substring(0,8);
                String Key2 = Key.Substring(8, 8);

                String tempString=Crypt_Decrypt_Specific_Algo(Source_Text, Key1, false);
                tempString = Crypt_Decrypt_Specific_Algo(tempString, Key2, true);
                return Crypt_Decrypt_Specific_Algo(tempString, Key1, false);
            }

            protected override String Decrypt_Specific_Algo(String Cypher_Text, String Key)
            {
                String Key1 = Key.Substring(0, 8);
                String Key2 = Key.Substring(8, 8);

                String tempString = Crypt_Decrypt_Specific_Algo(Cypher_Text, Key1, true);
                tempString = Crypt_Decrypt_Specific_Algo(tempString, Key2, false);
                return Crypt_Decrypt_Specific_Algo(tempString, Key1, true);
            }

            public override String ToString() { return "TDES"; }
        }//End of the TDES cypher\\


        public Main()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            comboBox_Algo_Choice.Items.Add(new Ceasar_Cypher());
            comboBox_Algo_Choice.Items.Add(new DES_Cypher());
            comboBox_Algo_Choice.Items.Add(new TDES_Cypher());
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
