// Forms/CarPartManagementForm.cs - Complete Implementation
using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class CarPartManagementForm : Form
    {
        private CarPartRepository carPartRepository;
        private CategoryRepository categoryRepository;

        public CarPartManagementForm()
        {
            carPartRepository = new CarPartRepository();
            categoryRepository = new CategoryRepository();
            InitializeComponent();
            LoadCarParts();
        }

        private void InitializeComponent()
        {
            this.dgvCarParts = new System.Windows.Forms.DataGridView();
            this.btnAddPart = new System.Windows.Forms.Button();
            this.btnEditPart = new System.Windows.Forms.Button();
            this.btnDeletePart = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.lblStockInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarParts)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Car Parts Management";
            
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(20, 70);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search:";
            
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(80, 67);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(290, 65);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(380, 70);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category:";
            
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(440, 67);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(150, 21);
            this.cmbCategory.TabIndex = 5;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            
            // 
            // btnAddPart
            // 
            this.btnAddPart.BackColor = System.Drawing.Color.Green;
            this.btnAddPart.ForeColor = System.Drawing.Color.White;
            this.btnAddPart.Location = new System.Drawing.Point(20, 110);
            this.btnAddPart.Name = "btnAddPart";
            this.btnAddPart.Size = new System.Drawing.Size(100, 30);
            this.btnAddPart.TabIndex = 6;
            this.btnAddPart.Text = "Add Part";
            this.btnAddPart.UseVisualStyleBackColor = false;
            this.btnAddPart.Click += new System.EventHandler(this.btnAddPart_Click);
            
            // 
            // btnEditPart
            // 
            this.btnEditPart.BackColor = System.Drawing.Color.Orange;
            this.btnEditPart.ForeColor = System.Drawing.Color.White;
            this.btnEditPart.Location = new System.Drawing.Point(130, 110);
            this.btnEditPart.Name = "btnEditPart";
            this.btnEditPart.Size = new System.Drawing.Size(100, 30);
            this.btnEditPart.TabIndex = 7;
            this.btnEditPart.Text = "Edit Part";
            this.btnEditPart.UseVisualStyleBackColor = false;
            this.btnEditPart.Click += new System.EventHandler(this.btnEditPart_Click);
            
            // 
            // btnDeletePart
            // 
            this.btnDeletePart.BackColor = System.Drawing.Color.Red;
            this.btnDeletePart.ForeColor = System.Drawing.Color.White;
            this.btnDeletePart.Location = new System.Drawing.Point(240, 110);
            this.btnDeletePart.Name = "btnDeletePart";
            this.btnDeletePart.Size = new System.Drawing.Size(100, 30);
            this.btnDeletePart.TabIndex = 8;
            this.btnDeletePart.Text = "Delete Part";
            this.btnDeletePart.UseVisualStyleBackColor = false;
            this.btnDeletePart.Click += new System.EventHandler(this.btnDeletePart_Click);
            
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.BackColor = System.Drawing.Color.Blue;
            this.btnUpdateStock.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStock.Location = new System.Drawing.Point(350, 110);
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateStock.TabIndex = 9;
            this.btnUpdateStock.Text = "Update Stock";
            this.btnUpdateStock.UseVisualStyleBackColor = false;
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);
            
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(460, 110);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // 
            // lblStockInfo
            // 
            this.lblStockInfo.AutoSize = true;
            this.lblStockInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblStockInfo.Location = new System.Drawing.Point(620, 120);
            this.lblStockInfo.Name = "lblStockInfo";
            this.lblStockInfo.Size = new System.Drawing.Size(200, 13);
            this.lblStockInfo.TabIndex = 11;
            this.lblStockInfo.Text = "Red = Out of Stock, Yellow = Low Stock";
            
            // 
            // dgvCarParts
            // 
            this.dgvCarParts.AllowUserToAddRows = false;
            this.dgvCarParts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarParts.Location = new System.Drawing.Point(20, 160);
            this.dgvCarParts.MultiSelect = false;
            this.dgvCarParts.Name = "dgvCarParts";
            this.dgvCarParts.ReadOnly = true;
            this.dgvCarParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarParts.Size = new System.Drawing.Size(1150, 480);
            this.dgvCarParts.TabIndex = 12;
            this.dgvCarParts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarParts_CellDoubleClick);
            
            // 
            // CarPartManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvCarParts);
            this.Controls.Add(this.lblStockInfo);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdateStock);
            this.Controls.Add(this.btnDeletePart);
            this.Controls.Add(this.btnEditPart);
            this.Controls.Add(this.btnAddPart);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "CarPartManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Parts Management - ABC Car Traders";
            this.Load += new System.EventHandler(this.CarPartManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.DataGridView dgvCarParts;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.Button btnEditPart;
        private System.Windows.Forms.Button btnDeletePart;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnUpdateStock;
        private System.Windows.Forms.Label lblStockInfo;
        #endregion

        #region Form Events
        private void CarPartManagementForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadCarParts();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            ShowAddPartDialog();
        }

        private void btnEditPart_Click(object sender, EventArgs e)
        {
            ShowEditPartDialog();
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            DeleteSelectedPart();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            ShowUpdateStockDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCarParts();
        }

        private void dgvCarParts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowEditPartDialog();
            }
        }
        #endregion

        #region Data Loading Methods
        private void LoadCategories()
        {
            try
            {
                var categories = categoryRepository.GetPartCategories();
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add(new { CategoryID = 0, CategoryName = "All Categories" });
                
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(new { CategoryID = category.CategoryID, CategoryName = category.CategoryName });
                }
                
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCarParts()
        {
            try
            {
                var parts = carPartRepository.GetAllCarParts();
                DisplayCarParts(parts);
                UpdateTitle(parts.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading car parts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCarParts(List<CarPart> parts)
        {
            var partData = parts.Select(p => new
            {
                p.PartID,
                p.PartName,
                p.PartNumber,
                p.Brand,
                Price = p.Price.ToString("C"),
                Category = p.CategoryName,
                Stock = p.StockQuantity,
                Available = p.IsAvailable ? "Yes" : "No",
                p.Description,
                Created = p.CreatedDate.ToString("MM/dd/yyyy")
            }).ToList();

            dgvCarParts.DataSource = partData;

            // Hide PartID column
            if (dgvCarParts.Columns["PartID"] != null)
            {
                dgvCarParts.Columns["PartID"].Visible = false;
            }

            // Set column widths
            if (dgvCarParts.Columns["PartName"] != null)
                dgvCarParts.Columns["PartName"].FillWeight = 150;
            if (dgvCarParts.Columns["PartNumber"] != null)
                dgvCarParts.Columns["PartNumber"].FillWeight = 100;
            if (dgvCarParts.Columns["Brand"] != null)
                dgvCarParts.Columns["Brand"].FillWeight = 100;
            if (dgvCarParts.Columns["Price"] != null)
                dgvCarParts.Columns["Price"].FillWeight = 80;
            if (dgvCarParts.Columns["Category"] != null)
                dgvCarParts.Columns["Category"].FillWeight = 120;
            if (dgvCarParts.Columns["Stock"] != null)
                dgvCarParts.Columns["Stock"].FillWeight = 60;
            if (dgvCarParts.Columns["Available"] != null)
                dgvCarParts.Columns["Available"].FillWeight = 70;
            if (dgvCarParts.Columns["Description"] != null)
                dgvCarParts.Columns["Description"].FillWeight = 200;

            // Color code rows based on stock levels
            ApplyStockColorCoding();
        }

        private void ApplyStockColorCoding()
        {
            foreach (DataGridViewRow row in dgvCarParts.Rows)
            {
                if (row.Cells["Stock"].Value != null)
                {
                    int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                    
                    if (stock == 0)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (stock <= 5)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        private void UpdateTitle(int count)
        {
            this.Text = $"Car Parts Management - ABC Car Traders ({count} parts)";
        }
        #endregion

        #region Search and Filter Methods
        private void PerformSearch()
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                int categoryId = 0;
                
                if (cmbCategory.SelectedItem != null)
                {
                    var selectedCategory = (dynamic)cmbCategory.SelectedItem;
                    categoryId = selectedCategory.CategoryID;
                }

                var parts = carPartRepository.SearchCarParts(searchTerm, categoryId);
                DisplayCarParts(parts);
                UpdateTitle(parts.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching car parts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CRUD Operations
        private void ShowAddPartDialog()
        {
            using (var dialog = new CarPartEditDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadCarParts();
                    MessageBox.Show("Car part added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowEditPartDialog()
        {
            if (dgvCarParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car part to edit.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int partId = Convert.ToInt32(dgvCarParts.SelectedRows[0].Cells["PartID"].Value);
                var parts = carPartRepository.GetAllCarParts();
                var selectedPart = parts.FirstOrDefault(p => p.PartID == partId);

                if (selectedPart != null)
                {
                    using (var dialog = new CarPartEditDialog(selectedPart))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            LoadCarParts();
                            MessageBox.Show("Car part updated successfully!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing car part: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedPart()
        {
            if (dgvCarParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car part to delete.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string partName = dgvCarParts.SelectedRows[0].Cells["PartName"].Value.ToString();
            
            if (MessageBox.Show($"Are you sure you want to delete '{partName}'?\n\nThis action cannot be undone.", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int partId = Convert.ToInt32(dgvCarParts.SelectedRows[0].Cells["PartID"].Value);
                    
                    if (carPartRepository.DeleteCarPart(partId))
                    {
                        MessageBox.Show("Car part deleted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCarParts();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete car part.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting car part: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowUpdateStockDialog()
        {
            if (dgvCarParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car part to update stock.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int partId = Convert.ToInt32(dgvCarParts.SelectedRows[0].Cells["PartID"].Value);
                string partName = dgvCarParts.SelectedRows[0].Cells["PartName"].Value.ToString();
                int currentStock = Convert.ToInt32(dgvCarParts.SelectedRows[0].Cells["Stock"].Value);

                using (var dialog = new StockUpdateDialog(partId, partName, currentStock))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadCarParts();
                        MessageBox.Show("Stock updated successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }

    // Car Part Edit Dialog
    public partial class CarPartEditDialog : Form
    {
        private CarPart part;
        private bool isEditMode;
        private CarPartRepository carPartRepository;
        private CategoryRepository categoryRepository;

        public CarPartEditDialog(CarPart partToEdit = null)
        {
            part = partToEdit ?? new CarPart();
            isEditMode = partToEdit != null;
            carPartRepository = new CarPartRepository();
            categoryRepository = new CategoryRepository();
            InitializeComponent();
            
            if (isEditMode)
            {
                PopulateFields();
            }
        }

        private void InitializeComponent()
        {
            this.lblPartName = new System.Windows.Forms.Label();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkAvailable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblPartName
            // 
            this.lblPartName.AutoSize = true;
            this.lblPartName.Location = new System.Drawing.Point(20, 30);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(62, 13);
            this.lblPartName.TabIndex = 0;
            this.lblPartName.Text = "Part Name:";
            
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(120, 27);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(250, 20);
            this.txtPartName.TabIndex = 1;
            
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.AutoSize = true;
            this.lblPartNumber.Location = new System.Drawing.Point(20, 70);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(72, 13);
            this.lblPartNumber.TabIndex = 2;
            this.lblPartNumber.Text = "Part Number:";
            
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(120, 67);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(250, 20);
            this.txtPartNumber.TabIndex = 3;
            
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(20, 110);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(38, 13);
            this.lblBrand.TabIndex = 4;
            this.lblBrand.Text = "Brand:";
            
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(120, 107);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(250, 20);
            this.txtBrand.TabIndex = 5;
            
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(20, 150);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 13);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Price:";
            
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Location = new System.Drawing.Point(120, 148);
            this.nudPrice.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(120, 20);
            this.nudPrice.TabIndex = 7;
            
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 190);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "Category:";
            
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(120, 187);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(250, 21);
            this.cmbCategory.TabIndex = 9;
            
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(20, 230);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(38, 13);
            this.lblStock.TabIndex = 10;
            this.lblStock.Text = "Stock:";
            
            // 
            // nudStock
            // 
            this.nudStock.Location = new System.Drawing.Point(120, 228);
            this.nudStock.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.nudStock.Name = "nudStock";
            this.nudStock.Size = new System.Drawing.Size(120, 20);
            this.nudStock.TabIndex = 11;
            
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 270);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 12;
            this.lblDescription.Text = "Description:";
            
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 267);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(250, 80);
            this.txtDescription.TabIndex = 13;
            
            // 
            // chkAvailable
            // 
            this.chkAvailable.AutoSize = true;
            this.chkAvailable.Checked = true;
            this.chkAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvailable.Location = new System.Drawing.Point(120, 360);
            this.chkAvailable.Name = "chkAvailable";
            this.chkAvailable.Size = new System.Drawing.Size(95, 17);
            this.chkAvailable.TabIndex = 14;
            this.chkAvailable.Text = "Available for Sale";
            this.chkAvailable.UseVisualStyleBackColor = true;
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(120, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // CarPartEditDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 470);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkAvailable);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.nudStock);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.nudPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.lblPartNumber);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.lblPartName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CarPartEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = isEditMode ? "Edit Car Part" : "Add Car Part";
            this.Load += new System.EventHandler(this.CarPartEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblPartName;
        private System.Windows.Forms.TextBox txtPartName;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkAvailable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void CarPartEditDialog_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = categoryRepository.GetPartCategories();
                cmbCategory.Items.Clear();
                
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(new { CategoryID = category.CategoryID, CategoryName = category.CategoryName });
                }
                
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                
                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            txtPartName.Text = part.PartName;
            txtPartNumber.Text = part.PartNumber;
            txtBrand.Text = part.Brand;
            nudPrice.Value = part.Price;
            nudStock.Value = part.StockQuantity;
            txtDescription.Text = part.Description;
            chkAvailable.Checked = part.IsAvailable;
            
            // Set category
            for (int i = 0; i < cmbCategory.Items.Count; i++)
            {
                var item = (dynamic)cmbCategory.Items[i];
                if (item.CategoryID == part.CategoryID)
                {
                    cmbCategory.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SavePart();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPartName.Text))
            {
                MessageBox.Show("Please enter a part name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPartName.Focus();
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }

            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            return true;
        }

        private void SavePart()
        {
            try
            {
                part.PartName = txtPartName.Text.Trim();
                part.PartNumber = txtPartNumber.Text.Trim();
                part.Brand = txtBrand.Text.Trim();
                part.Price = nudPrice.Value;
                part.StockQuantity = (int)nudStock.Value;
                part.Description = txtDescription.Text.Trim();
                part.IsAvailable = chkAvailable.Checked;
                
                var selectedCategory = (dynamic)cmbCategory.SelectedItem;
                part.CategoryID = selectedCategory.CategoryID;

                bool success;
                if (isEditMode)
                {
                    success = carPartRepository.UpdateCarPart(part);
                }
                else
                {
                    success = carPartRepository.AddCarPart(part);
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to {(isEditMode ? "update" : "add")} car part.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving car part: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Stock Update Dialog
    public partial class StockUpdateDialog : Form
    {
        private int partId;
        private string partName;
        private int currentStock;
        private CarPartRepository carPartRepository;

        public StockUpdateDialog(int partId, string partName, int currentStock)
        {
            this.partId = partId;
            this.partName = partName;
            this.currentStock = currentStock;
            this.carPartRepository = new CarPartRepository();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblPartInfo = new System.Windows.Forms.Label();
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.lblNewStock = new System.Windows.Forms.Label();
            this.nudNewStock = new System.Windows.Forms.NumericUpDown();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewStock)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblPartInfo
            // 
            this.lblPartInfo.Location = new System.Drawing.Point(20, 20);
            this.lblPartInfo.Name = "lblPartInfo";
            this.lblPartInfo.Size = new System.Drawing.Size(350, 20);
            this.lblPartInfo.TabIndex = 0;
            this.lblPartInfo.Text = $"Part: {partName}";
            
            // 
            // lblCurrentStock
            // 
            this.lblCurrentStock.Location = new System.Drawing.Point(20, 60);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(350, 20);
            this.lblCurrentStock.TabIndex = 1;
            this.lblCurrentStock.Text = $"Current Stock: {currentStock}";
            
            // 
            // lblNewStock
            // 
            this.lblNewStock.AutoSize = true;
            this.lblNewStock.Location = new System.Drawing.Point(20, 100);
            this.lblNewStock.Name = "lblNewStock";
            this.lblNewStock.Size = new System.Drawing.Size(65, 13);
            this.lblNewStock.TabIndex = 2;
            this.lblNewStock.Text = "New Stock:";
            
            // 
            // nudNewStock
            // 
            this.nudNewStock.Location = new System.Drawing.Point(120, 98);
            this.nudNewStock.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.nudNewStock.Name = "nudNewStock";
            this.nudNewStock.Size = new System.Drawing.Size(120, 20);
            this.nudNewStock.TabIndex = 3;
            this.nudNewStock.Value = new decimal(new int[] { currentStock, 0, 0, 0 });
            
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Blue;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(120, 140);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update Stock";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // StockUpdateDialog
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 200);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.nudNewStock);
            this.Controls.Add(this.lblNewStock);
            this.Controls.Add(this.lblCurrentStock);
            this.Controls.Add(this.lblPartInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockUpdateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Stock";
            ((System.ComponentModel.ISupportInitialize)(this.nudNewStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblPartInfo;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.Label lblNewStock;
        private System.Windows.Forms.NumericUpDown nudNewStock;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int newStock = (int)nudNewStock.Value;
                
                if (carPartRepository.UpdateStock(partId, newStock))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update stock.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}