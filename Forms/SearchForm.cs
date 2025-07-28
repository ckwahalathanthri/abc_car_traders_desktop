using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using ABCCarTraders.Business;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class SearchForm : Form
    {
        private CarRepository carRepository;
        private CarPartRepository carPartRepository;
        private CategoryRepository categoryRepository;

        public SearchForm()
        {
            carRepository = new CarRepository();
            carPartRepository = new CarPartRepository();
            categoryRepository = new CategoryRepository();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCars = new System.Windows.Forms.TabPage();
            this.dgvCars = new System.Windows.Forms.DataGridView();
            this.cmbCarCategory = new System.Windows.Forms.ComboBox();
            this.lblCarCategory = new System.Windows.Forms.Label();
            this.btnOrderCar = new System.Windows.Forms.Button();
            this.btnSearchCars = new System.Windows.Forms.Button();
            this.txtCarSearch = new System.Windows.Forms.TextBox();
            this.lblSearchCars = new System.Windows.Forms.Label();
            this.tabPageParts = new System.Windows.Forms.TabPage();
            this.dgvParts = new System.Windows.Forms.DataGridView();
            this.cmbPartCategory = new System.Windows.Forms.ComboBox();
            this.lblPartCategory = new System.Windows.Forms.Label();
            this.btnOrderPart = new System.Windows.Forms.Button();
            this.btnSearchParts = new System.Windows.Forms.Button();
            this.txtPartSearch = new System.Windows.Forms.TextBox();
            this.lblSearchParts = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageCars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).BeginInit();
            this.tabPageParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).BeginInit();
            this.SuspendLayout();
            
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCars);
            this.tabControl1.Controls.Add(this.tabPageParts);
            this.tabControl1.Location = new System.Drawing.Point(20, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(950, 620);
            this.tabControl1.TabIndex = 0;
            
            // 
            // tabPageCars
            // 
            this.tabPageCars.Controls.Add(this.dgvCars);
            this.tabPageCars.Controls.Add(this.cmbCarCategory);
            this.tabPageCars.Controls.Add(this.lblCarCategory);
            this.tabPageCars.Controls.Add(this.btnOrderCar);
            this.tabPageCars.Controls.Add(this.btnSearchCars);
            this.tabPageCars.Controls.Add(this.txtCarSearch);
            this.tabPageCars.Controls.Add(this.lblSearchCars);
            this.tabPageCars.Location = new System.Drawing.Point(4, 22);
            this.tabPageCars.Name = "tabPageCars";
            this.tabPageCars.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCars.Size = new System.Drawing.Size(942, 594);
            this.tabPageCars.TabIndex = 0;
            this.tabPageCars.Text = "Cars";
            this.tabPageCars.UseVisualStyleBackColor = true;
            
            // 
            // dgvCars
            // 
            this.dgvCars.AllowUserToAddRows = false;
            this.dgvCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCars.Location = new System.Drawing.Point(20, 90);
            this.dgvCars.Name = "dgvCars";
            this.dgvCars.ReadOnly = true;
            this.dgvCars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCars.Size = new System.Drawing.Size(900, 470);
            this.dgvCars.TabIndex = 6;
            
            // 
            // cmbCarCategory
            // 
            this.cmbCarCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarCategory.FormattingEnabled = true;
            this.cmbCarCategory.Location = new System.Drawing.Point(90, 48);
            this.cmbCarCategory.Name = "cmbCarCategory";
            this.cmbCarCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbCarCategory.TabIndex = 5;
            
            // 
            // lblCarCategory
            // 
            this.lblCarCategory.AutoSize = true;
            this.lblCarCategory.Location = new System.Drawing.Point(20, 50);
            this.lblCarCategory.Name = "lblCarCategory";
            this.lblCarCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCarCategory.TabIndex = 4;
            this.lblCarCategory.Text = "Category:";
            
            // 
            // btnOrderCar
            // 
            this.btnOrderCar.Location = new System.Drawing.Point(570, 16);
            this.btnOrderCar.Name = "btnOrderCar";
            this.btnOrderCar.Size = new System.Drawing.Size(150, 25);
            this.btnOrderCar.TabIndex = 3;
            this.btnOrderCar.Text = "Order Selected Car";
            this.btnOrderCar.UseVisualStyleBackColor = true;
            this.btnOrderCar.Click += new System.EventHandler(this.btnOrderCar_Click);
            
            // 
            // btnSearchCars
            // 
            this.btnSearchCars.Location = new System.Drawing.Point(310, 16);
            this.btnSearchCars.Name = "btnSearchCars";
            this.btnSearchCars.Size = new System.Drawing.Size(80, 25);
            this.btnSearchCars.TabIndex = 2;
            this.btnSearchCars.Text = "Search";
            this.btnSearchCars.UseVisualStyleBackColor = true;
            this.btnSearchCars.Click += new System.EventHandler(this.btnSearchCars_Click);
            
            // 
            // txtCarSearch
            // 
            this.txtCarSearch.Location = new System.Drawing.Point(90, 18);
            this.txtCarSearch.Name = "txtCarSearch";
            this.txtCarSearch.Size = new System.Drawing.Size(200, 20);
            this.txtCarSearch.TabIndex = 1;
            this.txtCarSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarSearch_KeyPress);
            
            // 
            // lblSearchCars
            // 
            this.lblSearchCars.AutoSize = true;
            this.lblSearchCars.Location = new System.Drawing.Point(20, 20);
            this.lblSearchCars.Name = "lblSearchCars";
            this.lblSearchCars.Size = new System.Drawing.Size(44, 13);
            this.lblSearchCars.TabIndex = 0;
            this.lblSearchCars.Text = "Search:";
            
            // 
            // tabPageParts
            // 
            this.tabPageParts.Controls.Add(this.dgvParts);
            this.tabPageParts.Controls.Add(this.cmbPartCategory);
            this.tabPageParts.Controls.Add(this.lblPartCategory);
            this.tabPageParts.Controls.Add(this.btnOrderPart);
            this.tabPageParts.Controls.Add(this.btnSearchParts);
            this.tabPageParts.Controls.Add(this.txtPartSearch);
            this.tabPageParts.Controls.Add(this.lblSearchParts);
            this.tabPageParts.Location = new System.Drawing.Point(4, 22);
            this.tabPageParts.Name = "tabPageParts";
            this.tabPageParts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParts.Size = new System.Drawing.Size(942, 594);
            this.tabPageParts.TabIndex = 1;
            this.tabPageParts.Text = "Car Parts";
            this.tabPageParts.UseVisualStyleBackColor = true;
            
            // 
            // dgvParts
            // 
            this.dgvParts.AllowUserToAddRows = false;
            this.dgvParts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParts.Location = new System.Drawing.Point(20, 90);
            this.dgvParts.Name = "dgvParts";
            this.dgvParts.ReadOnly = true;
            this.dgvParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParts.Size = new System.Drawing.Size(900, 470);
            this.dgvParts.TabIndex = 6;
            
            // 
            // cmbPartCategory
            // 
            this.cmbPartCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartCategory.FormattingEnabled = true;
            this.cmbPartCategory.Location = new System.Drawing.Point(90, 48);
            this.cmbPartCategory.Name = "cmbPartCategory";
            this.cmbPartCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbPartCategory.TabIndex = 5;
            
            // 
            // lblPartCategory
            // 
            this.lblPartCategory.AutoSize = true;
            this.lblPartCategory.Location = new System.Drawing.Point(20, 50);
            this.lblPartCategory.Name = "lblPartCategory";
            this.lblPartCategory.Size = new System.Drawing.Size(52, 13);
            this.lblPartCategory.TabIndex = 4;
            this.lblPartCategory.Text = "Category:";
            
            // 
            // btnOrderPart
            // 
            this.btnOrderPart.Location = new System.Drawing.Point(570, 16);
            this.btnOrderPart.Name = "btnOrderPart";
            this.btnOrderPart.Size = new System.Drawing.Size(150, 25);
            this.btnOrderPart.TabIndex = 3;
            this.btnOrderPart.Text = "Order Selected Part";
            this.btnOrderPart.UseVisualStyleBackColor = true;
            this.btnOrderPart.Click += new System.EventHandler(this.btnOrderPart_Click);
            
            // 
            // btnSearchParts
            // 
            this.btnSearchParts.Location = new System.Drawing.Point(310, 16);
            this.btnSearchParts.Name = "btnSearchParts";
            this.btnSearchParts.Size = new System.Drawing.Size(80, 25);
            this.btnSearchParts.TabIndex = 2;
            this.btnSearchParts.Text = "Search";
            this.btnSearchParts.UseVisualStyleBackColor = true;
            this.btnSearchParts.Click += new System.EventHandler(this.btnSearchParts_Click);
            
            // 
            // txtPartSearch
            // 
            this.txtPartSearch.Location = new System.Drawing.Point(90, 18);
            this.txtPartSearch.Name = "txtPartSearch";
            this.txtPartSearch.Size = new System.Drawing.Size(200, 20);
            this.txtPartSearch.TabIndex = 1;
            this.txtPartSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPartSearch_KeyPress);
            
            // 
            // lblSearchParts
            // 
            this.lblSearchParts.AutoSize = true;
            this.lblSearchParts.Location = new System.Drawing.Point(20, 20);
            this.lblSearchParts.Name = "lblSearchParts";
            this.lblSearchParts.Size = new System.Drawing.Size(44, 13);
            this.lblSearchParts.TabIndex = 0;
            this.lblSearchParts.Text = "Search:";
            
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControl1);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Cars & Parts";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCars.ResumeLayout(false);
            this.tabPageCars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).EndInit();
            this.tabPageParts.ResumeLayout(false);
            this.tabPageParts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).EndInit();
            this.ResumeLayout(false);
        }

        #region Designer Components
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCars;
        private System.Windows.Forms.DataGridView dgvCars;
        private System.Windows.Forms.ComboBox cmbCarCategory;
        private System.Windows.Forms.Label lblCarCategory;
        private System.Windows.Forms.Button btnOrderCar;
        private System.Windows.Forms.Button btnSearchCars;
        private System.Windows.Forms.TextBox txtCarSearch;
        private System.Windows.Forms.Label lblSearchCars;
        private System.Windows.Forms.TabPage tabPageParts;
        private System.Windows.Forms.DataGridView dgvParts;
        private System.Windows.Forms.ComboBox cmbPartCategory;
        private System.Windows.Forms.Label lblPartCategory;
        private System.Windows.Forms.Button btnOrderPart;
        private System.Windows.Forms.Button btnSearchParts;
        private System.Windows.Forms.TextBox txtPartSearch;
        private System.Windows.Forms.Label lblSearchParts;
        #endregion

        private void SearchForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            SearchCars("");
            SearchParts("");
        }

        private void LoadCategories()
        {
            try
            {
                // Load car categories
                var carCategories = categoryRepository.GetCarCategories();
                cmbCarCategory.Items.Clear();
                cmbCarCategory.Items.Add(new { CategoryID = 0, CategoryName = "All Categories" });
                foreach (var category in carCategories)
                {
                    cmbCarCategory.Items.Add(new { CategoryID = category.CategoryID, CategoryName = category.CategoryName });
                }
                cmbCarCategory.DisplayMember = "CategoryName";
                cmbCarCategory.ValueMember = "CategoryID";
                cmbCarCategory.SelectedIndex = 0;

                // Load part categories
                var partCategories = categoryRepository.GetPartCategories();
                cmbPartCategory.Items.Clear();
                cmbPartCategory.Items.Add(new { CategoryID = 0, CategoryName = "All Categories" });
                foreach (var category in partCategories)
                {
                    cmbPartCategory.Items.Add(new { CategoryID = category.CategoryID, CategoryName = category.CategoryName });
                }
                cmbPartCategory.DisplayMember = "CategoryName";
                cmbPartCategory.ValueMember = "CategoryID";
                cmbPartCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Car Search Events
        private void btnSearchCars_Click(object sender, EventArgs e)
        {
            SearchCars(txtCarSearch.Text);
        }

        private void txtCarSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchCars(txtCarSearch.Text);
                e.Handled = true;
            }
        }

        private void btnOrderCar_Click(object sender, EventArgs e)
        {
            OrderSelectedCar();
        }
        #endregion

        #region Part Search Events
        private void btnSearchParts_Click(object sender, EventArgs e)
        {
            SearchParts(txtPartSearch.Text);
        }

        private void txtPartSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchParts(txtPartSearch.Text);
                e.Handled = true;
            }
        }

        private void btnOrderPart_Click(object sender, EventArgs e)
        {
            OrderSelectedPart();
        }
        #endregion

        #region Search Methods
        private void SearchCars(string searchTerm)
        {
            try
            {
                int categoryId = 0;
                if (cmbCarCategory.SelectedItem != null)
                {
                    var selectedCategory = (dynamic)cmbCarCategory.SelectedItem;
                    categoryId = selectedCategory.CategoryID;
                }

                var cars = carRepository.SearchCars(searchTerm, categoryId);
                var carData = cars.Select(c => new
                {
                    c.CarID,
                    c.Brand,
                    c.Model,
                    c.Year,
                    Price = c.Price.ToString("C"),
                    c.Color,
                    c.Mileage,
                    c.FuelType,
                    c.Transmission,
                    Category = c.CategoryName,
                    c.Description
                }).ToList();

                dgvCars.DataSource = carData;
                
                // Hide CarID column if it exists
                if (dgvCars.Columns["CarID"] != null)
                {
                    dgvCars.Columns["CarID"].Visible = false;
                }

                // Update status
                this.Text = $"Search Cars & Parts - Found {cars.Count} cars";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching cars: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchParts(string searchTerm)
        {
            try
            {
                int categoryId = 0;
                if (cmbPartCategory.SelectedItem != null)
                {
                    var selectedCategory = (dynamic)cmbPartCategory.SelectedItem;
                    categoryId = selectedCategory.CategoryID;
                }

                var parts = carPartRepository.SearchCarParts(searchTerm, categoryId);
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
                    p.Description
                }).ToList();

                dgvParts.DataSource = partData;
                
                // Hide PartID column if it exists
                if (dgvParts.Columns["PartID"] != null)
                {
                    dgvParts.Columns["PartID"].Visible = false;
                }

                // Color code rows based on stock
                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                    if (stock == 0)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                    }
                    else if (stock <= 5)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                // Update tab text with count
                if (tabControl1.SelectedTab == tabPageParts)
                {
                    this.Text = $"Search Cars & Parts - Found {parts.Count} parts";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching parts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Order Methods
        private void OrderSelectedCar()
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to order.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvCars.SelectedRows[0];
                int carId = Convert.ToInt32(selectedRow.Cells["CarID"].Value);
                string carInfo = $"{selectedRow.Cells["Brand"].Value} {selectedRow.Cells["Model"].Value} ({selectedRow.Cells["Year"].Value})";
                decimal price = 0;
                
                // Extract price from formatted string
                string priceStr = selectedRow.Cells["Price"].Value.ToString().Replace("$", "").Replace(",", "");
                decimal.TryParse(priceStr, out price);
                
                using (var orderDialog = new QuickOrderDialog(carId, carInfo, ItemType.Car, price))
                {
                    if (orderDialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Car order placed successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error placing car order: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OrderSelectedPart()
        {
            if (dgvParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a part to order.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvParts.SelectedRows[0];
                int partId = Convert.ToInt32(selectedRow.Cells["PartID"].Value);
                string partInfo = $"{selectedRow.Cells["PartName"].Value} - {selectedRow.Cells["Brand"].Value}";
                int stock = Convert.ToInt32(selectedRow.Cells["Stock"].Value);
                decimal price = 0;
                
                // Extract price from formatted string
                string priceStr = selectedRow.Cells["Price"].Value.ToString().Replace("$", "").Replace(",", "");
                decimal.TryParse(priceStr, out price);

                if (stock == 0)
                {
                    MessageBox.Show("This part is out of stock.", "Warning", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                using (var orderDialog = new QuickOrderDialog(partId, partInfo, ItemType.Part, price, stock))
                {
                    if (orderDialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Part order placed successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchParts(txtPartSearch.Text); // Refresh to update stock
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error placing part order: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }

    // Enhanced Quick Order Dialog
    public partial class QuickOrderDialog : Form
    {
        private int itemId;
        private string itemInfo;
        private ItemType itemType;
        private decimal itemPrice;
        private int maxQuantity;

        public QuickOrderDialog(int itemId, string itemInfo, ItemType itemType, decimal price, int maxQty = 10)
        {
            this.itemId = itemId;
            this.itemInfo = itemInfo;
            this.itemType = itemType;
            this.itemPrice = price;
            this.maxQuantity = maxQty;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblItem = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblItem
            // 
            this.lblItem.Location = new System.Drawing.Point(20, 30);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(350, 20);
            this.lblItem.TabIndex = 0;
            this.lblItem.Text = $"Item: {itemInfo}";
            
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(20, 60);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(350, 20);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = $"Unit Price: {itemPrice:C}";
            
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(20, 100);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity:";
            
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(120, 98);
            this.nudQuantity.Maximum = new decimal(new int[] { maxQuantity, 0, 0, 0 });
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(100, 20);
            this.nudQuantity.TabIndex = 3;
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(20, 140);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(69, 13);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total Cost:";
            
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Green;
            this.lblTotalAmount.Location = new System.Drawing.Point(120, 140);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(32, 13);
            this.lblTotalAmount.TabIndex = 5;
            this.lblTotalAmount.Text = "$0.00";
            
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(120, 180);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(100, 30);
            this.btnOrder.TabIndex = 6;
            this.btnOrder.Text = "Place Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // QuickOrderDialog
            // 
            this.AcceptButton = this.btnOrder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickOrderDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Place Order";
            this.Load += new System.EventHandler(this.QuickOrderDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void QuickOrderDialog_Load(object sender, EventArgs e)
        {
            UpdateTotal();
            
            if (itemType == ItemType.Part && maxQuantity < 10)
            {
                lblQuantity.Text = $"Quantity (Max: {maxQuantity}):";
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = itemPrice * nudQuantity.Value;
            lblTotalAmount.Text = total.ToString("C");
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            PlaceOrder((int)nudQuantity.Value);
        }

        private void PlaceOrder(int quantity)
        {
            try
            {
                var orderService = new OrderService();
                bool success = false;

                if (itemType == ItemType.Car)
                {
                    success = orderService.CreateCarOrder(
                        AuthenticationService.CurrentUser.UserID, 
                        itemId, 
                        quantity);
                }
                else if (itemType == ItemType.Part)
                {
                    success = orderService.CreatePartOrder(
                        AuthenticationService.CurrentUser.UserID, 
                        itemId, 
                        quantity);
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to place order. Please check item availability and try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error placing order: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}