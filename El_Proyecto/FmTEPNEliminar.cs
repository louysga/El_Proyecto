using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capas.NetFramwork.ADO.Net.Data;
using Capas.NetFramwork.ADO.Net.Business;
using System.Data.SqlClient;

namespace El_Proyecto
{
    public partial class FmTEPNEliminar : Form
    {
        public FmTEPNEliminar()
        {
            InitializeComponent();
        }

        private void BTBuscar_Click(object sender, EventArgs e)
        {
            string id = TBId.Text;
            if (TBId.Text != "")
            {
                SqlDataReader datos = ClassBusinessTE.BTBuscarLogica(id);
                if (datos.Read())
                {
                    TBComentario.Text = datos["Comentario"].ToString();
                    TBDescripcion.Text = datos["Descripcion"].ToString();

                    string idGrupo = datos["IdGrupoEntidad"].ToString();
                    SqlDataReader datoGrupo = ClassBusinessTE.CargarDatosGrupo(idGrupo);
                    datoGrupo.Read();
                    LBGrupoEntidad.Text = datoGrupo["Comentario"].ToString();

                    LBStatus.Text = datos["Status"].ToString();
                    if (datos["NoEliminable"].ToString() == "0")
                    {
                        LBEditable.Text = "Editable";
                    }
                    else
                    {
                        LBEditable.Text = "No editable";
                    }

                    PNBuscar.Visible = false;
                    PNLista.Visible = false;
                    PNDatos.Visible = true;

                }
                else
                {
                    InvAvisoNoExiste();
                }
            }
            else
            {
                InvAvisoVacio();
            }
        }

        public void InvAvisoNoExiste()
        {
            AvisoInstanciaNoExiste aviso = new AvisoInstanciaNoExiste();
            aviso.Show();
        }

        public void InvAvisoVacio()
        {
            AvisoVacio aviso = new AvisoVacio();
            aviso.Show();
        }

        public void InvAvisoEliminado()
        {
            AvisoInstanciaEliminada aviso = new AvisoInstanciaEliminada();
            aviso.Show();
        }

        private void BTRefrescar_Click(object sender, EventArgs e)
        {
            dvgLista.DataSource = ClassDataTE.MostrarLista();
        }

        private void FmTEPNEliminar_Load(object sender, EventArgs e)
        {
            dvgLista.DataSource = ClassDataTE.MostrarLista();
        }

        private void BTAtras_Click(object sender, EventArgs e)
        {
            TBId.Text = "";

            PNBuscar.Visible = true;
            PNLista.Visible = true;
            PNDatos.Visible = false;
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            string id = TBId.Text;
            string comando = ClassBusinessTE.BTEliminarComando(id);
            ClassBusinessTE.BTEliminarLogica(comando, CommandType.Text);
            InvAvisoEliminado();
            PNBuscar.Visible = true;
            PNLista.Visible = true;
            PNDatos.Visible = false;
            TBId.Text = "";
            TBComentario.Text = "";
            TBDescripcion.Text = "";
            LBStatus.Text = "";
            LBEditable.Text = "";
            LBGrupoEntidad.Text = "";
        }
    }
}
