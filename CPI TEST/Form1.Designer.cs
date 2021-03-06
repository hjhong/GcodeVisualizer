﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
namespace GcodeVisualizer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, ref Point lParam);

        private const int WM_VSCROLL = 0x115;  //tells the control to scroll

        private const int WM_GETDLGCODE = 0x87;   //sent when the caret is going out of the 'visible area' (so scroll is needed)

        private const int WM_MOUSEFIRST = 0x200;  //scrolls if the mouse leaves the 'visible area' (example when you select text)

        private const int EM_GETSCROLLPOS = 0x4DD;  //you send this message and the control returns it's scroll position

        private const int EM_SETSCROLLPOS = 0x4DE;//this is used to set the control's scroll position
        public class ExRichTextBox : System.Windows.Forms.RichTextBox
        {

            private double _Yfactor = 1.0d;


            public Point ScrollPos
            {
                get
                {
                    Point scrollPoint = new Point();

                    SendMessage(this.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref scrollPoint);
                    return scrollPoint;
                }
                set
                {
                    Point original = value;
                    if (original.Y < 0)
                        original.Y = 0;
                    if (original.X < 0)
                        original.X = 0;

                    Point factored = value;
                    factored.Y = (int)((double)original.Y * _Yfactor);

                    Point result = value;

                    SendMessage(this.Handle, EM_SETSCROLLPOS, IntPtr.Zero, ref factored);
                    SendMessage(this.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref result);

                    int loopcount = 0;
                    int maxloop = 100;
                    while (result.Y != original.Y)
                    {
                        // Adjust the input.
                        if (result.Y > original.Y)
                            factored.Y -= (result.Y - original.Y) / 2 - 1;
                        else if (result.Y < original.Y)
                            factored.Y += (original.Y - result.Y) / 2 + 1;

                        // test the new input.
                        SendMessage(this.Handle, EM_SETSCROLLPOS, IntPtr.Zero, ref factored);
                        SendMessage(this.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref result);

                        // save new factor, test for exit.
                        loopcount++;
                        if (loopcount >= maxloop || result.Y == original.Y)
                        {
                            _Yfactor = (double)factored.Y / (double)original.Y;
                            break;
                        }
                    }
                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loadButton = new System.Windows.Forms.Button();
            this.Draw = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.FasterButton = new System.Windows.Forms.Button();
            this.SlowerButton = new System.Windows.Forms.Button();
            this.BackUp = new System.Windows.Forms.Button();
            this.ResetView = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.InvertX = new System.Windows.Forms.CheckBox();
            this.InvertY = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Arena = new OpenTK.GLControl();
            this.debugText = new GcodeVisualizer.MainForm.ExRichTextBox();
            this.LoadGCODEFile = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 471);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.loadButton);
            this.flowLayoutPanel1.Controls.Add(this.Draw);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.TestButton);
            this.flowLayoutPanel1.Controls.Add(this.EditButton);
            this.flowLayoutPanel1.Controls.Add(this.InvertX);
            this.flowLayoutPanel1.Controls.Add(this.InvertY);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 366);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(810, 102);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(84, 3);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(75, 23);
            this.Draw.TabIndex = 5;
            this.Draw.Text = "Draw";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.FasterButton);
            this.flowLayoutPanel3.Controls.Add(this.SlowerButton);
            this.flowLayoutPanel3.Controls.Add(this.BackUp);
            this.flowLayoutPanel3.Controls.Add(this.ResetView);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(165, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // FasterButton
            // 
            this.FasterButton.Location = new System.Drawing.Point(3, 3);
            this.FasterButton.Name = "FasterButton";
            this.FasterButton.Size = new System.Drawing.Size(75, 23);
            this.FasterButton.TabIndex = 0;
            this.FasterButton.Text = "Faster";
            this.FasterButton.UseVisualStyleBackColor = true;
            this.FasterButton.Click += new System.EventHandler(this.FasterButton_Click);
            // 
            // SlowerButton
            // 
            this.SlowerButton.Location = new System.Drawing.Point(3, 32);
            this.SlowerButton.Name = "SlowerButton";
            this.SlowerButton.Size = new System.Drawing.Size(75, 23);
            this.SlowerButton.TabIndex = 1;
            this.SlowerButton.Text = "Slower";
            this.SlowerButton.UseVisualStyleBackColor = true;
            this.SlowerButton.Click += new System.EventHandler(this.SlowerButton_Click);
            // 
            // BackUp
            // 
            this.BackUp.Location = new System.Drawing.Point(3, 61);
            this.BackUp.Name = "BackUp";
            this.BackUp.Size = new System.Drawing.Size(75, 23);
            this.BackUp.TabIndex = 2;
            this.BackUp.Text = "Back Up";
            this.BackUp.UseVisualStyleBackColor = true;
            this.BackUp.Click += new System.EventHandler(this.BackUp_Click);
            // 
            // ResetView
            // 
            this.ResetView.Location = new System.Drawing.Point(84, 3);
            this.ResetView.Name = "ResetView";
            this.ResetView.Size = new System.Drawing.Size(75, 23);
            this.ResetView.TabIndex = 3;
            this.ResetView.Text = "Reset View";
            this.ResetView.UseVisualStyleBackColor = true;
            this.ResetView.Click += new System.EventHandler(this.ResetView_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(371, 3);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 6;
            this.TestButton.Text = "TEST";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Location = new System.Drawing.Point(452, 3);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(75, 23);
            this.EditButton.TabIndex = 7;
            this.EditButton.Text = "EDIT";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // InvertX
            // 
            this.InvertX.AutoSize = true;
            this.InvertX.Location = new System.Drawing.Point(533, 3);
            this.InvertX.Name = "InvertX";
            this.InvertX.Size = new System.Drawing.Size(60, 17);
            this.InvertX.TabIndex = 8;
            this.InvertX.Text = "InvertX";
            this.InvertX.UseVisualStyleBackColor = true;
            this.InvertX.CheckedChanged += new System.EventHandler(this.InvertX_CheckedChanged);
            // 
            // InvertY
            // 
            this.InvertY.AutoSize = true;
            this.InvertY.Location = new System.Drawing.Point(599, 3);
            this.InvertY.Name = "InvertY";
            this.InvertY.Size = new System.Drawing.Size(63, 17);
            this.InvertY.TabIndex = 9;
            this.InvertY.Text = "Invert Y";
            this.InvertY.UseVisualStyleBackColor = true;
            this.InvertY.CheckedChanged += new System.EventHandler(this.InvertY_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.Arena);
            this.flowLayoutPanel2.Controls.Add(this.debugText);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(810, 357);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // Arena
            // 
            this.Arena.BackColor = System.Drawing.Color.Black;
            this.Arena.Location = new System.Drawing.Point(3, 3);
            this.Arena.Name = "Arena";
            this.Arena.Size = new System.Drawing.Size(417, 348);
            this.Arena.TabIndex = 0;
            this.Arena.VSync = false;
            this.Arena.Load += new System.EventHandler(this.Arena_Load);
            this.Arena.Paint += new System.Windows.Forms.PaintEventHandler(this.Arena_Paint);
            this.Arena.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Arena_MouseClick);
            this.Arena.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Arena_MouseDown);
            this.Arena.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Arena_MouseMove);
            this.Arena.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Arena_MouseUp);
            this.Arena.Resize += new System.EventHandler(this.Arena_Resize);
            // 
            // debugText
            // 
            this.debugText.Location = new System.Drawing.Point(426, 3);
            this.debugText.Name = "debugText";
            this.debugText.ScrollPos = new System.Drawing.Point(0, 0);
            this.debugText.Size = new System.Drawing.Size(375, 348);
            this.debugText.TabIndex = 1;
            this.debugText.Text = "";
            this.debugText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.debugText_MouseDown);
            // 
            // LoadGCODEFile
            // 
            this.LoadGCODEFile.FileName = "smile.gcode";
            this.LoadGCODEFile.Filter = "GCODE files (*.NC)|*.NC|All files (*.*)|*.*";
            this.LoadGCODEFile.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadGCODEFile_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 471);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "GCode Visualizer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.OpenFileDialog LoadGCODEFile;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private OpenTK.GLControl Arena;
        public ExRichTextBox debugText;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button FasterButton;
        private System.Windows.Forms.Button SlowerButton;
        private System.Windows.Forms.Button BackUp;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.Button ResetView;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.CheckBox InvertX;
        private System.Windows.Forms.CheckBox InvertY;
    }

}

