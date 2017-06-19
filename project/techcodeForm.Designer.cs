namespace MultiFaceRec
{
    partial class TechcodeForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_TrainSystem = new System.Windows.Forms.Button();
            this.btn_ChooseTestDbPath = new System.Windows.Forms.Button();
            this.txtb_TestDbPath = new System.Windows.Forms.TextBox();
            this.btn_StartRecognition = new System.Windows.Forms.Button();
            this.txtb_FormattedDbPath = new System.Windows.Forms.TextBox();
            this.btn_ChooseFormattedDbPath = new System.Windows.Forms.Button();
            this.fbd_FormattedDbPath = new System.Windows.Forms.FolderBrowserDialog();
            this.fbd_TestDbPath = new System.Windows.Forms.FolderBrowserDialog();
            this.rtb_SystemLogs = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtb_TrainSystem = new System.Windows.Forms.RichTextBox();
            this.txtb_SourceDbPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SourceDbPath = new System.Windows.Forms.Button();
            this.fbd_SourceDbPath = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SourceDbPath_Explorer = new System.Windows.Forms.Button();
            this.btn_ChooseFormattedDbPath_Explorer = new System.Windows.Forms.Button();
            this.btn_ChooseTestDbPath_Explorer = new System.Windows.Forms.Button();
            this.btn_PrepareTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_TrainSystem
            // 
            this.btn_TrainSystem.Location = new System.Drawing.Point(12, 129);
            this.btn_TrainSystem.Name = "btn_TrainSystem";
            this.btn_TrainSystem.Size = new System.Drawing.Size(251, 25);
            this.btn_TrainSystem.TabIndex = 0;
            this.btn_TrainSystem.Text = "Обучить систему";
            this.btn_TrainSystem.UseVisualStyleBackColor = true;
            this.btn_TrainSystem.Click += new System.EventHandler(this.btn_TrainSystem_Click);
            // 
            // btn_ChooseTestDbPath
            // 
            this.btn_ChooseTestDbPath.Location = new System.Drawing.Point(171, 103);
            this.btn_ChooseTestDbPath.Name = "btn_ChooseTestDbPath";
            this.btn_ChooseTestDbPath.Size = new System.Drawing.Size(43, 20);
            this.btn_ChooseTestDbPath.TabIndex = 5;
            this.btn_ChooseTestDbPath.Text = "...";
            this.btn_ChooseTestDbPath.UseVisualStyleBackColor = true;
            this.btn_ChooseTestDbPath.Click += new System.EventHandler(this.btn_ChooseTestDbPath_Click);
            // 
            // txtb_TestDbPath
            // 
            this.txtb_TestDbPath.Location = new System.Drawing.Point(12, 103);
            this.txtb_TestDbPath.Name = "txtb_TestDbPath";
            this.txtb_TestDbPath.Size = new System.Drawing.Size(153, 20);
            this.txtb_TestDbPath.TabIndex = 4;
            // 
            // btn_StartRecognition
            // 
            this.btn_StartRecognition.Location = new System.Drawing.Point(12, 191);
            this.btn_StartRecognition.Name = "btn_StartRecognition";
            this.btn_StartRecognition.Size = new System.Drawing.Size(251, 25);
            this.btn_StartRecognition.TabIndex = 3;
            this.btn_StartRecognition.Text = "Старт распознавания";
            this.btn_StartRecognition.UseVisualStyleBackColor = true;
            this.btn_StartRecognition.Click += new System.EventHandler(this.btn_StartRecognition_Click);
            // 
            // txtb_FormattedDbPath
            // 
            this.txtb_FormattedDbPath.Location = new System.Drawing.Point(12, 64);
            this.txtb_FormattedDbPath.Name = "txtb_FormattedDbPath";
            this.txtb_FormattedDbPath.Size = new System.Drawing.Size(153, 20);
            this.txtb_FormattedDbPath.TabIndex = 6;
            // 
            // btn_ChooseFormattedDbPath
            // 
            this.btn_ChooseFormattedDbPath.Location = new System.Drawing.Point(171, 64);
            this.btn_ChooseFormattedDbPath.Name = "btn_ChooseFormattedDbPath";
            this.btn_ChooseFormattedDbPath.Size = new System.Drawing.Size(43, 20);
            this.btn_ChooseFormattedDbPath.TabIndex = 7;
            this.btn_ChooseFormattedDbPath.Text = "...";
            this.btn_ChooseFormattedDbPath.UseVisualStyleBackColor = true;
            this.btn_ChooseFormattedDbPath.Click += new System.EventHandler(this.btn_ChooseFormattedDbPath_Click);
            // 
            // rtb_SystemLogs
            // 
            this.rtb_SystemLogs.Location = new System.Drawing.Point(442, 12);
            this.rtb_SystemLogs.Name = "rtb_SystemLogs";
            this.rtb_SystemLogs.Size = new System.Drawing.Size(151, 417);
            this.rtb_SystemLogs.TabIndex = 8;
            this.rtb_SystemLogs.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Отформатированная база:";
            // 
            // rtb_TrainSystem
            // 
            this.rtb_TrainSystem.Location = new System.Drawing.Point(285, 12);
            this.rtb_TrainSystem.Name = "rtb_TrainSystem";
            this.rtb_TrainSystem.Size = new System.Drawing.Size(151, 417);
            this.rtb_TrainSystem.TabIndex = 11;
            this.rtb_TrainSystem.Text = "";
            // 
            // txtb_SourceDbPath
            // 
            this.txtb_SourceDbPath.Location = new System.Drawing.Point(12, 25);
            this.txtb_SourceDbPath.Name = "txtb_SourceDbPath";
            this.txtb_SourceDbPath.Size = new System.Drawing.Size(153, 20);
            this.txtb_SourceDbPath.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Исходная база:";
            // 
            // btn_SourceDbPath
            // 
            this.btn_SourceDbPath.Location = new System.Drawing.Point(171, 25);
            this.btn_SourceDbPath.Name = "btn_SourceDbPath";
            this.btn_SourceDbPath.Size = new System.Drawing.Size(43, 20);
            this.btn_SourceDbPath.TabIndex = 14;
            this.btn_SourceDbPath.Text = "...";
            this.btn_SourceDbPath.UseVisualStyleBackColor = true;
            this.btn_SourceDbPath.Click += new System.EventHandler(this.btn_SourceDbPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Тестовая база:";
            // 
            // btn_SourceDbPath_Explorer
            // 
            this.btn_SourceDbPath_Explorer.Location = new System.Drawing.Point(220, 25);
            this.btn_SourceDbPath_Explorer.Name = "btn_SourceDbPath_Explorer";
            this.btn_SourceDbPath_Explorer.Size = new System.Drawing.Size(43, 20);
            this.btn_SourceDbPath_Explorer.TabIndex = 16;
            this.btn_SourceDbPath_Explorer.Text = "...";
            this.btn_SourceDbPath_Explorer.UseVisualStyleBackColor = true;
            this.btn_SourceDbPath_Explorer.Click += new System.EventHandler(this.btn_SourceDbPath_Explorer_Click);
            // 
            // btn_ChooseFormattedDbPath_Explorer
            // 
            this.btn_ChooseFormattedDbPath_Explorer.Location = new System.Drawing.Point(220, 64);
            this.btn_ChooseFormattedDbPath_Explorer.Name = "btn_ChooseFormattedDbPath_Explorer";
            this.btn_ChooseFormattedDbPath_Explorer.Size = new System.Drawing.Size(43, 20);
            this.btn_ChooseFormattedDbPath_Explorer.TabIndex = 17;
            this.btn_ChooseFormattedDbPath_Explorer.Text = "...";
            this.btn_ChooseFormattedDbPath_Explorer.UseVisualStyleBackColor = true;
            this.btn_ChooseFormattedDbPath_Explorer.Click += new System.EventHandler(this.btn_ChooseFormattedDbPath_Explorer_Click);
            // 
            // btn_ChooseTestDbPath_Explorer
            // 
            this.btn_ChooseTestDbPath_Explorer.Location = new System.Drawing.Point(220, 103);
            this.btn_ChooseTestDbPath_Explorer.Name = "btn_ChooseTestDbPath_Explorer";
            this.btn_ChooseTestDbPath_Explorer.Size = new System.Drawing.Size(43, 20);
            this.btn_ChooseTestDbPath_Explorer.TabIndex = 18;
            this.btn_ChooseTestDbPath_Explorer.Text = "...";
            this.btn_ChooseTestDbPath_Explorer.UseVisualStyleBackColor = true;
            this.btn_ChooseTestDbPath_Explorer.Click += new System.EventHandler(this.btn_ChooseTestDbPath_Explorer_Click);
            // 
            // btn_PrepareTest
            // 
            this.btn_PrepareTest.Location = new System.Drawing.Point(12, 160);
            this.btn_PrepareTest.Name = "btn_PrepareTest";
            this.btn_PrepareTest.Size = new System.Drawing.Size(251, 25);
            this.btn_PrepareTest.TabIndex = 19;
            this.btn_PrepareTest.Text = "Сформировать тестовые данные";
            this.btn_PrepareTest.UseVisualStyleBackColor = true;
            this.btn_PrepareTest.Click += new System.EventHandler(this.btn_PrepareTest_Click);
            // 
            // TechcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 441);
            this.Controls.Add(this.btn_PrepareTest);
            this.Controls.Add(this.btn_ChooseTestDbPath_Explorer);
            this.Controls.Add(this.btn_ChooseFormattedDbPath_Explorer);
            this.Controls.Add(this.btn_SourceDbPath_Explorer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SourceDbPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtb_SourceDbPath);
            this.Controls.Add(this.rtb_TrainSystem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtb_SystemLogs);
            this.Controls.Add(this.btn_ChooseFormattedDbPath);
            this.Controls.Add(this.txtb_FormattedDbPath);
            this.Controls.Add(this.btn_ChooseTestDbPath);
            this.Controls.Add(this.txtb_TestDbPath);
            this.Controls.Add(this.btn_StartRecognition);
            this.Controls.Add(this.btn_TrainSystem);
            this.Name = "TechcodeForm";
            this.Text = "techcodeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_TrainSystem;
        private System.Windows.Forms.Button btn_ChooseTestDbPath;
        private System.Windows.Forms.TextBox txtb_TestDbPath;
        private System.Windows.Forms.Button btn_StartRecognition;
        private System.Windows.Forms.TextBox txtb_FormattedDbPath;
        private System.Windows.Forms.Button btn_ChooseFormattedDbPath;
        private System.Windows.Forms.FolderBrowserDialog fbd_FormattedDbPath;
        private System.Windows.Forms.FolderBrowserDialog fbd_TestDbPath;
        private System.Windows.Forms.RichTextBox rtb_SystemLogs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtb_TrainSystem;
        private System.Windows.Forms.TextBox txtb_SourceDbPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SourceDbPath;
        private System.Windows.Forms.FolderBrowserDialog fbd_SourceDbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SourceDbPath_Explorer;
        private System.Windows.Forms.Button btn_ChooseFormattedDbPath_Explorer;
        private System.Windows.Forms.Button btn_ChooseTestDbPath_Explorer;
        private System.Windows.Forms.Button btn_PrepareTest;
    }
}