using LinqCRUD.BusinessLayer;
using LinqCRUD.DataAccessLayer;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqCRUD
{
    public partial class Form1 : MaterialForm
    {
        EmpleadoBL empRules;

        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan800, Primary.Cyan900, Primary.Cyan500, Accent.Cyan200, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbSexo1.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void GetMessageError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GetMessageInfo(string mensaje)
        {
            MessageBox.Show(mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            empRules = new EmpleadoBL();

            if (cbOptiones.Text == "Departamento" && cbDepartamentos.Text != string.Empty)
            {
                var empleados = empRules.GetByDepartment(cbDepartamentos.Text);
                dglist.DataSource = empleados;
            }
            if (cbOptiones.Text == "Nombre" && txtBuscar.Text != string.Empty)
            {
                var empleados = empRules.GetByName(txtBuscar.Text);
                dglist.DataSource = empleados;
            }
            else if(cbOptiones.Text == string.Empty)
            {
                GetMessageInfo("Debe Seleccionar una opcion de busqueda");
            }
        }

        private void cbOptiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbOptiones.Text == "Departamento")
            {
                cbDepartamentos.Enabled = true;
                txtBuscar.Enabled = false;
            }
            if(cbOptiones.Text == "Nombre")
            {
                cbDepartamentos.Enabled = false;
                txtBuscar.Enabled = true;
            }
        }

        private void ClearTextBoxes(Control.ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Text = "";
                else
                    ClearTextBoxes(ctrl.Controls);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtDireccion.Text == string.Empty || txtTelefono.Text == string.Empty || txtEmail.Text == string.Empty || cbDepartamento.Text == string.Empty || cbTipoDocu.Text == string.Empty)
            {
                GetMessageError("Debe ingresar datos válidos");
            }
            else
            {
                var result = true;
                string sexo = "";
                if (rbSexo1.Checked)
                {
                    sexo = rbSexo1.Text;
                }
                else if (rbSexo2.Checked)
                {
                    sexo = rbSexo2.Text;
                }

                if(txtId.Text == string.Empty)
                {
                    empRules = new EmpleadoBL();
                       result = empRules.Create(
                        txtNombre.Text
                        , txtApellido.Text
                        , dtFechaNac.Value
                        , sexo
                        , txtDireccion.Text
                        , txtTelefono.Text
                        , cbTipoDocu.Text
                        , txtNoDocu.Text
                        , txtEmail.Text
                        , cbDepartamento.Text
                        );
                }
                else
                {
                    int id = Convert.ToInt32(txtId.Text);
                    empRules = new EmpleadoBL();
                    result = empRules.Update(
                     id
                     , txtNombre.Text
                     , txtApellido.Text
                     , dtFechaNac.Value
                     , sexo
                     , txtDireccion.Text
                     , txtTelefono.Text
                     , cbTipoDocu.Text
                     , txtNoDocu.Text
                     , txtEmail.Text
                     , cbDepartamento.Text
                     );

                }

                if (result)
                {
                    GetMessageInfo("Guardado con exito");
                }

                ClearTextBoxes(this.Controls);
                dglist.DataSource = null;
            }

        }

        int currentSelected = -1;
        private void dglist_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var gv = ((DataGridView)sender);
                if (gv.CurrentRow.Index != currentSelected)
                {
                    currentSelected = gv.CurrentRow.Index;
                    var currentCl = (empleado)gv.CurrentRow.DataBoundItem;
                    populate(currentCl);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Debe buscar por nombre del documento o empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void populate(empleado cl)
        {
            if (cl.sexo == "Masculino")
            {
                rbSexo1.Checked = true;
            }
            else if (cl.sexo == "Femenino")
            {
                rbSexo2.Checked = true;
            }

            txtId.Text = (cl.idempleado).ToString();
            txtNombre.Text = cl.nombre;
            txtApellido.Text = cl.apellido;
            dtFechaNac.Value = Convert.ToDateTime(cl.fecha_nacimiento);
            txtNoDocu.Text = cl.numero_documento;
            txtDireccion.Text = cl.direccion;
            txtTelefono.Text = cl.telefono;
            txtEmail.Text = cl.email;
            cbDepartamento.Text = cl.departamento;
            cbTipoDocu.Text = cl.tipo_documento;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("Realmente desea borrar empleado?", "sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcion == DialogResult.OK)
            {
                var result = true;
                empRules = new EmpleadoBL();
                result = empRules.Delete(txtId.Text);

                if (result)
                {
                    GetMessageInfo("Eliminado con exito");
                }
                else
                {
                    GetMessageError("Error");
                }
            }

            ClearTextBoxes(this.Controls);
            dglist.DataSource = null;
        }

    }
}
