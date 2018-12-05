using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IPAInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            if (txtpackagename.Text.Length == 0)
            {
                MessageBox.Show("لطفا پکیج نیم را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txttitle.Text.Length == 0)
            {
                MessageBox.Show("لطفا عنوان اپلیکیشن را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtversion.Text.Length == 0)
            {
                MessageBox.Show("لطفا نسخه اپلیکیشن را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Uri uriResult;
            bool result = Uri.TryCreate(txturl.Text, UriKind.Absolute, out uriResult)
    && (uriResult.Scheme == Uri.UriSchemeHttps);
            if (result == false)
            {
                MessageBox.Show("لطفا آدرس دانلود فایل اپلیکیشن را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtfilename.Text.Length == 0)
            {
                MessageBox.Show("لطفا نام فایل آپلود شده را با پسوند آن وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string Html = "<html><head><meta charset=\"UTF-8\"></head><body><a href=\"itms-services://?action=download-manifest&amp;url=" + txturl.Text + "/app.plist\"><h1>جهت نصب اینجا کلیک کنید</h1></a></body></html>";

            string plist = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\"><plist version=\"1.0\"><dict><key>items</key><array><dict><key>assets</key><array><dict><key>kind</key><string>software-package</string><key>url</key><string>{url}</string></dict></array><key>metadata</key><dict><key>bundle-identifier</key><string>com.apple.xcode.dsym.{pk}</string><key>bundle-version</key><string>{version}</string><key>kind</key><string>software</string><key>title</key><string>{title}</string></dict></dict></array></dict></plist>";

            plist = plist.Replace("{url}", txturl.Text + "/" + txtfilename.Text).Replace("{pk}", txtpackagename.Text);
            plist = plist.Replace("{title}", txttitle.Text).Replace("{version}", txtversion.Text);

            FolderBrowserDialog f1 = new FolderBrowserDialog();
            f1.ShowNewFolderButton = true;
            f1.Description = "محل ذخیره فایل های ایجاد شده";
            if (f1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(f1.SelectedPath + "/index.html", Html);
                File.WriteAllText(f1.SelectedPath + "/app.plist", plist);
                pnlresult.Visible = true;
            }

        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtversion.Text = txturl.Text = txtfilename.Text = txtpackagename.Text = txttitle.Text = "";
            pnlresult.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://iranapp.org");
        }

    }
}
