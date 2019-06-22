using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cave.DynDns.Client
{
    public partial class CaveDynDnsSettingsForm : Form
    {
        CaveDynDnsSettings m_Settings;

        public CaveDynDnsSettingsForm(CaveDynDnsSettings p_Settings)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Properties.Resources.dns_update.GetHicon());
            m_Settings = p_Settings;
            textBox1.Text = m_Settings.DomainName;
            textBox2.Text = m_Settings.Password;
            trackBar1.Value = Math.Min(Math.Max(trackBar1.Minimum, m_Settings.IntervalMinutes), trackBar1.Maximum);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            m_Settings.DomainName = textBox1.Text;
            m_Settings.Password= textBox2.Text;
            m_Settings.IntervalMinutes = trackBar1.Value;
        }
    }
}
