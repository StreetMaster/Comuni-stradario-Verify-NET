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

    /// <summary>
    /// Esempio di utilizzo del servizio WS VERIFY per la verifica e la normalizzazione degli indirizzi italiani 
    /// realizzato da StreetMaster Italia
    /// 
    /// L'end point del servizio è 
    ///     http://ec2-46-137-97-173.eu-west-1.compute.amazonaws.com/smws/verify?wsdl
    ///     
    /// Per l'utilizzo registrarsi sul sito http://streetmaster.it e richiedere la chiave per il servizio VERIFY 
    /// 
    ///  2016 - Software by StreetMaster (c)
    /// </summary>
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
                MessageBox.Show("E' necessario specificare una chiave valida per il servizio VERIFY");
                txtKey.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            // oggetto client per l'utilizzo del ws FILL
            var verifyObj = new VerifyWS.VerifyClient();

            // classe di input
            var inVerify = new VerifyWS.inputCommon();

            // valorizzazione input
            inVerify.localita = txtInComune.Text;
            inVerify.cap = txtInCap.Text;
            inVerify.provincia = txtInProvincia.Text;
            inVerify.indirizzo = txtInIndirizzo.Text;

            // chiamata al servizio
            totOutVerifyWS = verifyObj.Verify(inVerify, txtKey.Text);

            // lettura campi generali del risultato
            txtOutEsito.Text = totOutVerifyWS.norm.ToString();
            txtOutCodErr.Text = totOutVerifyWS.codErr.ToString();
            txtOutNumCand.Text = totOutVerifyWS.numCand.ToString();

            currCand = 0;
            // dettaglio del primo candidato se esiste
            // nella form di output e' riportato solo un sottoinsieme di tutti i valori restituiti
            if (totOutVerifyWS.numCand > 0)
            {
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyWS.outItem[currCand].scoreComune.ToString();
                txtOutScoreStrada.Text = totOutVerifyWS.outItem[currCand].scoreStrada.ToString();
            }
            Cursor = Cursors.Default;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            // dettaglio del successivo candidato se esiste
            if (currCand > 0)
            {
                currCand-=1;
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyWS.outItem[currCand].scoreComune.ToString();
                txtOutScoreStrada.Text = totOutVerifyWS.outItem[currCand].scoreStrada.ToString();
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            // dettagli del precedente candidato se esiste
            if (currCand< totOutVerifyWS.numCand-1)
            {
                currCand += 1;
                txtOutCap.Text = totOutVerifyWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyWS.outItem[currCand].frazione;
                txtOutIndirizzo.Text = totOutVerifyWS.outItem[currCand].indirizzo;
                txtOutProvincia.Text = totOutVerifyWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyWS.outItem[currCand].scoreComune.ToString();
                txtOutScoreStrada.Text = totOutVerifyWS.outItem[currCand].scoreStrada.ToString();
            }
        }
    }
}
