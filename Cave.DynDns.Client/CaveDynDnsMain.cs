using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cave.DynDns.Client
{
    class CaveDynDnsMain : Control
    {
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private Timer timer1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaveDynDnsMain));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Dynamic DNS";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem1.Text = "Settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        CaveDynDnsSettings m_Settings = new CaveDynDnsSettings();
        DateTime m_NextCheck = DateTime.Now.AddMinutes(1);
        string m_LastResult = null;
        DateTime m_LastCheck;

        public CaveDynDnsMain()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (new CaveDynDnsSettingsForm(m_Settings).ShowDialog() == DialogResult.OK)
            {
               m_Settings.Save();
               m_NextCheck = DateTime.Now;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Dispose();
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_Settings.IntervalMinutes <= 0) return;
            if (DateTime.Now < m_NextCheck) return;

            m_NextCheck = DateTime.Now.AddMinutes(m_Settings.IntervalMinutes);
            try
            {
                string l_Result = CaveDynDnsClient.Update(m_Settings.DomainName, m_Settings.Password, null);
                m_LastCheck = DateTime.Now;

                if (m_LastResult != l_Result)
                {
                    m_LastResult = l_Result;
                    if (l_Result.StartsWith("OK"))
                    {
                        notifyIcon1.ShowBalloonTip(10000, "Cave DynDns Update", m_LastCheck.ToLongTimeString() + " " + m_LastResult, ToolTipIcon.Info);
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(10000, "Cave DynDns Update", m_LastCheck.ToLongTimeString() + " " + m_LastResult, ToolTipIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                m_LastResult = ex.Message;
                notifyIcon1.Text = ex.Message.Substring(0, Math.Min(63, ex.Message.Length));
                notifyIcon1.ShowBalloonTip(10000, "Cave DynDns Update Error", m_LastCheck.ToLongTimeString() + " " + m_LastResult, ToolTipIcon.Error);
            }
            notifyIcon1.Text = m_LastResult.Substring(0, Math.Min(63, m_LastResult.Length));
        }
    }
}
