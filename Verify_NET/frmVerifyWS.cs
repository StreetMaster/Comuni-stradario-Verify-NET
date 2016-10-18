using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Verify_NET
{
    public partial class frmVerifyWS : Form
    {
        int currCand = 0;
        VerifyWS.totOutVerify totOutVerifyWS;
        public frmVerifyWS()
        {
            InitializeComponent();
        }

        private void btnCallVerify_Click(object sender, EventArgs e)
        {
            
            if (txtKey.Text==String.Empty)
            {
                MessageBox.Show("E' necessario specificare una chiave valida per il servizio Verify");
                txtKey.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var verifyObj = new VerifyWS.VerifyClient();

            var inVerify = new VerifyWS.inputCommon();

            inVerify.localita = txtInComune.Text;
            inVerify.cap = txtInCap.Text;
            inVerify.provincia = txtInProvincia.Text;

            inVerify.indirizzo = txtInIndirizzo.Text;

            totOutVerifyWS = verifyObj.Verify(inVerify, txtKey.Text);

            txtOutEsito.Text = totOutVerifyWS.norm.ToString();
            txtOutCodErr.Text = totOutVerifyWS.codErr.ToString();
            txtOutNumCand.Text = totOutVerifyWS.numCand.ToString();

            currCand = 0;
            if (totOutVerifyWS.numCand > 0)
            {
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;
            }
            Cursor = Cursors.Default;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            if (currCand > 0)
            {
                currCand-=1;
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            if (currCand< totOutVerifyWS.numCand-1)
            {
                currCand += 1;
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;

            }
        }
    }
}
