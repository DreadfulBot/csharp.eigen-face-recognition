using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using faceRecognitionHelper;

namespace MultiFaceRec
{
    public partial class TechcodeForm : Form
    {
        private FileWorker _fw;
        private RecognitionWorker _rw;
        public TechcodeForm()
        {
            InitializeComponent();
            txtb_TestDbPath.Text = MainSettings.Default.testDbPath;
            txtb_FormattedDbPath.Text = MainSettings.Default.formattedDbPath;
            txtb_SourceDbPath.Text = MainSettings.Default.sourceDbPath;
        }

        private void btn_ChooseFormattedDbPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbd_FormattedDbPath.ShowDialog() == DialogResult.OK)
                {
                    var path = fbd_FormattedDbPath.SelectedPath;

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    txtb_FormattedDbPath.Text = path;
                }
                else
                {
                    txtb_FormattedDbPath.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                rtb_SystemLogs.AppendText(ex.Message + "\n");
            }
        }

        private void btn_TrainSystem_Click(object sender, EventArgs e)
        {
            try
            {
                var sourceDbPath = txtb_SourceDbPath.Text;
                var formattedDbPath = txtb_FormattedDbPath.Text;
                var testDbPath = txtb_TestDbPath.Text;
                

                if (!Directory.Exists(testDbPath) || 
                    !Directory.Exists(formattedDbPath) ||
                    !Directory.Exists(sourceDbPath))
                    throw new DirectoryNotFoundException();

                _rw = new RecognitionWorker(txtb_SourceDbPath.Text, txtb_FormattedDbPath.Text, txtb_TestDbPath.Text,
                    (string s) => { rtb_TrainSystem.AppendText(s); });

                IDatabaseFiles df = new LocalDatabaseFiles(sourceDbPath);
                IFormattedFiles ff = new LocalFormattedFiles(testDbPath, (string s) => { rtb_SystemLogs.AppendText(s); });

                _fw = new FileWorker(df, ff, (string s) => { rtb_TrainSystem.AppendText(s);}, _rw);
                
            }
            catch (Exception ex)
            {
                rtb_SystemLogs.AppendText(ex.Message);
            }
        }

        private void btn_SourceDbPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbd_SourceDbPath.ShowDialog() == DialogResult.OK)
                {
                    var path = fbd_SourceDbPath.SelectedPath;

                    if (!Directory.Exists(path))
                        throw new DirectoryNotFoundException();

                    txtb_SourceDbPath.Text = path;
                }
            }
            catch (Exception ex)
            {
                rtb_SystemLogs.AppendText(ex.Message);
            }
        }

        private void btn_ChooseTestDbPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbd_TestDbPath.ShowDialog() == DialogResult.OK)
                {
                    var path = fbd_TestDbPath.SelectedPath;
                    if (!Directory.Exists(path))
                        throw new DirectoryNotFoundException();

                    if (Directory.GetFiles(path).Length == 0)
                        throw new Exception("Empty directory");

                    txtb_TestDbPath.Text = path;
                }
                else
                {
                    txtb_TestDbPath.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                rtb_SystemLogs.AppendText(ex.Message + "\n");
            }
        }

        private void btn_SourceDbPath_Explorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtb_SourceDbPath.Text);
        }

        private void btn_ChooseFormattedDbPath_Explorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtb_FormattedDbPath.Text);
        }

        private void btn_ChooseTestDbPath_Explorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtb_TestDbPath.Text);
        }

        private void btn_StartRecognition_Click(object sender, EventArgs e)
        {
            try
            {
                _rw.RecognizeAll();
            }
            catch (Exception ex)
            {
                rtb_SystemLogs.AppendText(ex.Message);
            }
        }

        private void btn_PrepareTest_Click(object sender, EventArgs e)
        {
            if (_fw == null)
            {
                rtb_SystemLogs.AppendText("Train system first!\n");
                return;
            }

            _fw.ProcessGroups();
        }
    }
}
