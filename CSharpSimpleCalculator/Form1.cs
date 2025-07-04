﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpSimpleCalculator
{
    
    public partial class Form1 : Form
    {
        stringNumber value = new stringNumber(); 
        List<string> operation = new List<string>();
        
        

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            value.AddDigit("1");
            UpdateLabel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            value.AddDigit("2");
            UpdateLabel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            value.AddDigit("3");
            UpdateLabel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            value.AddDigit("4");
            UpdateLabel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            value.AddDigit("5");
            UpdateLabel();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            value.AddDigit("6");
            UpdateLabel();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            value.AddDigit("7");
            UpdateLabel();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            value.AddDigit("8");
            UpdateLabel();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            value.AddDigit("9");
            UpdateLabel();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            value.AddDigit("0");
            UpdateLabel();
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            value.AddDigit(".");
            UpdateLabel();
        }

        

        private void buttonCE_Click(object sender, EventArgs e)
        {
            value.RemoveDigit();
            UpdateLabel();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            value = new stringNumber(); // Reset the stringNumber instance
            operation.Clear(); // Clear the operation list 
            UpdateLabel(); // Update the label to show the reset state
        }




        private void buttonEqual_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count == 0)
            {
                if (value.StringValue == "")
                {
                    MessageBox.Show("No numbers to calculate.");
                    return;
                }
                UpdateLabel();
                value = new stringNumber(); // Reset the stringNumber instance after displaying the value
                return;
            }
            
            calculation();
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("/");
            }

        }

        private void buttonMulti_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("*");
                
            }

        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            if (value.StringValue == "" && operation.Count!=1)
            {
                value.AddDigit("-"); 
                UpdateLabel();
                return;
            }
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("-");
                
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("+");
                
            }
        }

        private void buttonRoot_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("√");
            }
           

        }

        private void buttonPow_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("^");
            }
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            endOfNumber();
            if (operation.Count > 0 && "+-*/^√log".IndexOf(operation[operation.Count - 1]) == -1)
            {
                operation.Add("log");
            }
        }


        private void UpdateLabel()
        {
            
            label1.Text = value.StringValue;
            string stringLabel = string.Join(" ", operation);
            label2.Text = stringLabel + " " + value.StringValue;

        }

        private void endOfNumber()
        {
            if (!string.IsNullOrEmpty(value.StringValue))
            {
                operation.Add(value.StringValue);
                value = new stringNumber();
                
            }
        }

        

       

        private void calculation()
        {
            if (operation.Count < 3) return;

            double result;
            if (!double.TryParse(operation[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result)) return;


            for (int i = 1; i < operation.Count - 1; i += 2)
            {
                string op = operation[i];
                if (!double.TryParse(operation[i + 1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double nextNumber)) continue;

                switch (op)
                {
                    case "+": result += nextNumber; break;
                    case "-": result -= nextNumber; break;
                    case "*": result *= nextNumber; break;
                    case "/":
                        if (nextNumber != 0) result /= nextNumber;
                        else MessageBox.Show("Nie można dzielić przez zero.");
                        break;
                    case "^": result = Math.Pow(result, nextNumber); break;
                    case "√":
                        if (nextNumber > 0) 
                        {
                            if (nextNumber%2 == 0 && result < 0)
                            {
                                MessageBox.Show("Nie można obliczyć pierwiastka parzystego z liczby ujemnej.");
                                return;
                            }
                            else
                            {
                                result = Math.Pow(result, (1.0 / nextNumber));
                            }



                        }
                        else MessageBox.Show("Stopień pierwiastka nie może być ujemny");
                        break;
                    case "log":
                        if (nextNumber > 0 && result > 0)
                        {
                            result = Math.Log(result, nextNumber);
                        }
                        else
                        {
                            MessageBox.Show("Podstawa logarytmu i liczba muszą być większe od zera.");
                            return;
                        }
                        break;

                }
            }
            label1.Text = result.ToString();
            value = new stringNumber();
            operation.Clear();
        }

        
    }
    public class stringNumber
    {
        private string stringValue = "";

        public string StringValue => stringValue;



        public void AddDigit(string digit)
        {
            if (digit == ".")
            {
                if (stringValue.Contains("."))
                    return; // już jest kropka, nie dodawaj kolejnej
                if (stringValue == "" || stringValue == "-")
                    stringValue += "0."; // zaczynamy od 0.
                else
                    stringValue += ".";
            }
            else
            {
                stringValue += digit;
            }
        }
        public void RemoveDigit()
        {
            if (this.stringValue.Length > 0)
            {
                this.stringValue = this.stringValue.Substring(0, this.stringValue.Length - 1);
            }

        }
    }
}
