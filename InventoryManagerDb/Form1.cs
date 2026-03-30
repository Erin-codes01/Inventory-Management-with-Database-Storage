using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagerDb
{
    public partial class Form1 : Form
    {
        // Controls
        private DataGridView dgvProducts = new DataGridView();
        private TextBox txtName = new TextBox();
        private TextBox txtCategory = new TextBox();
        private TextBox txtQuantity = new TextBox();
        private TextBox txtPrice = new TextBox();
        private TextBox txtSearchName = new TextBox();
        private TextBox txtSearchCategory = new TextBox();
        private Button btnAdd = new Button();
        private Button btnUpdate = new Button();
        private Button btnDelete = new Button();
        private Button btnSearch = new Button();

        private long selectedId = 0;

        public Form1()
        {
            Text = "Inventory Manager";
            Size = new Size(900, 750);

            InitializeControls();
            LoadProducts();
        }

        private void InitializeControls()
        {
            // Labels with AutoSize = true and proper vertical spacing
            var lblName = new System.Windows.Forms.Label { Text = "Name:", Location = new Point(10, 10), AutoSize = true };
            var lblCategory = new System.Windows.Forms.Label { Text = "Category:", Location = new Point(10, 50), AutoSize = true };
            var lblQuantity = new System.Windows.Forms.Label { Text = "Quantity:", Location = new Point(10, 90), AutoSize = true };
            var lblPrice = new System.Windows.Forms.Label { Text = "Price:", Location = new Point(10, 130), AutoSize = true };

            var lblSearchName = new System.Windows.Forms.Label { Text = "Search Name:", Location = new Point(420, 10), AutoSize = true };
            var lblSearchCategory = new System.Windows.Forms.Label { Text = "Search Category:", Location = new Point(420, 50), AutoSize = true };

            // TextBoxes with wider width and aligned next to labels
            txtName.Location = new Point(140, 7);
            txtName.Size = new Size(220, 25);

            txtCategory.Location = new Point(140, 47);
            txtCategory.Size = new Size(220, 25);

            txtQuantity.Location = new Point(140, 87);
            txtQuantity.Size = new Size(220, 25);

            txtPrice.Location = new Point(140, 127);
            txtPrice.Size = new Size(220, 25);

            txtSearchName.Location = new Point(600, 7);
            txtSearchName.Size = new Size(220, 25);

            txtSearchCategory.Location = new Point(600, 47);
            txtSearchCategory.Size = new Size(220, 25);

            // Buttons spaced horizontally with comfortable size below inputs
            btnAdd.Location = new Point(100, 170);
            btnAdd.Size = new Size(90, 40);
            btnAdd.Text = "Add";

            btnUpdate.Location = new Point(190, 170);
            btnUpdate.Size = new Size(100, 40);
            btnUpdate.Text = "Update";

            btnDelete.Location = new Point(280, 170);
            btnDelete.Size = new Size(100, 40);
            btnDelete.Text = "Delete";

            btnSearch.Location = new Point(530, 85);
            btnSearch.Size = new Size(110, 40);
            btnSearch.Text = "Search";

            // DataGridView below all controls, filling width and height nicely
            dgvProducts.Location = new Point(10, 260);
            dgvProducts.Size = new Size(760, 330);
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Add all controls to the form
            Controls.AddRange(new Control[]
            {
                lblName, lblCategory, lblQuantity, lblPrice,
                txtName, txtCategory, txtQuantity, txtPrice,
                lblSearchName, lblSearchCategory,
                txtSearchName, txtSearchCategory,
                btnAdd, btnUpdate, btnDelete, btnSearch,
                dgvProducts
            });

            // Wire up event handlers
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        private void LoadProducts()
        {
            dgvProducts.DataSource = Db.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var p = new Product(
                txtName.Text,
                txtCategory.Text,
                int.TryParse(txtQuantity.Text, out var q) ? q : 0,
                double.TryParse(txtPrice.Text, out var pr) ? pr : 0
            );

            Db.Insert(p);
            LoadProducts();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId <= 0) return;

            var p = new Product(
                txtName.Text,
                txtCategory.Text,
                int.TryParse(txtQuantity.Text, out var q) ? q : 0,
                double.TryParse(txtPrice.Text, out var pr) ? pr : 0,
                selectedId
            );

            Db.Update(p);
            LoadProducts();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId <= 0) return;

            Db.Delete(selectedId);
            LoadProducts();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProducts.DataSource = Db.Search(txtSearchName.Text, txtSearchCategory.Text);
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.Cells["Id"].Value != null)
            {
                selectedId = Convert.ToInt64(dgvProducts.CurrentRow.Cells["Id"].Value);
                txtName.Text = dgvProducts.CurrentRow.Cells["Name"].Value?.ToString() ?? "";
                txtCategory.Text = dgvProducts.CurrentRow.Cells["Category"].Value?.ToString() ?? "";
                txtQuantity.Text = dgvProducts.CurrentRow.Cells["Quantity"].Value?.ToString() ?? "";
                txtPrice.Text = dgvProducts.CurrentRow.Cells["Price"].Value?.ToString() ?? "";
            }
        }

        private void ClearInputs()
        {
            selectedId = 0;
            txtName.Clear();
            txtCategory.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtSearchName.Clear();
            txtSearchCategory.Clear();
        }
    }
}