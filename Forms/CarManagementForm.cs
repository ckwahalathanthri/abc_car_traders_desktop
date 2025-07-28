// Forms/CarManagementForm.cs - Complete Upgraded Implementation
using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class CarManagementForm : Form
    {
        private CarRepository carRepository;
        private CategoryRepository categoryRepository;

        public CarManagementForm()
        {
            carRepository = new CarRepository();
            categoryRepository = new CategoryRepository();
            InitializeComponent();
            LoadCars();
        }

        private void InitializeComponent()
        {
            this.dgvCars = new System.Windows.Forms.DataGridView();
            this.btnAddCar = new System.Windows.Forms.Button();
            this.btnEditCar = new System.Windows.Forms.Button();
            this.btnDeleteCar = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnToggleAvailability = new System.Windows.Forms.Button();
            this.lblAvailabilityInfo = new System.Windows.Forms.Label();
            this.lblPriceRange = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.lblYearRange = new System.Windows.Forms.Label();
            this.txtMinYear = new System.Windows.Forms.TextBox();
            this.lblYearTo = new System.Windows.Forms.Label();
            this.txtMaxYear = new System.Windows.Forms.TextBox();
            this.btnClearFilters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Car Management";
            
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(20, 70);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(71, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search Text:";
            
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(100, 67);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Brand, Model, or Color";
            this.txtSearch.Size = new System.Drawing.Size(180, 20);
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
            this.cmbCategory.Size = new System.Drawing.Size(120, 21);
            this.cmbCategory.TabIndex = 5;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            
            // 
            // lblPriceRange
            // 
            this.lblPriceRange.AutoSize = true;
            this.lblPriceRange.Location = new System.Drawing.Point(580, 70);
            this.lblPriceRange.Name = "lblPriceRange";
            this.lblPriceRange.Size = new System.Drawing.Size(34, 13);
            this.lblPriceRange.TabIndex = 6;
            this.lblPriceRange.Text = "Price:";
            
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Location = new System.Drawing.Point(620, 67);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.PlaceholderText = "Min";
            this.txtMinPrice.Size = new System.Drawing.Size(60, 20);
            this.txtMinPrice.TabIndex = 7;
            this.txtMinPrice.TextChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(685, 70);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "to";
            
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.Location = new System.Drawing.Point(705, 67);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.PlaceholderText = "Max";
            this.txtMaxPrice.Size = new System.Drawing.Size(60, 20);
            this.txtMaxPrice.TabIndex = 9;
            this.txtMaxPrice.TextChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // lblYearRange
            // 
            this.lblYearRange.AutoSize = true;
            this.lblYearRange.Location = new System.Drawing.Point(780, 70);
            this.lblYearRange.Name = "lblYearRange";
            this.lblYearRange.Size = new System.Drawing.Size(32, 13);
            this.lblYearRange.TabIndex = 10;
            this.lblYearRange.Text = "Year:";
            
            // 
            // txtMinYear
            // 
            this.txtMinYear.Location = new System.Drawing.Point(820, 67);
            this.txtMinYear.Name = "txtMinYear";
            this.txtMinYear.PlaceholderText = "Min";
            this.txtMinYear.Size = new System.Drawing.Size(50, 20);
            this.txtMinYear.TabIndex = 11;
            this.txtMinYear.TextChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // lblYearTo
            // 
            this.lblYearTo.AutoSize = true;
            this.lblYearTo.Location = new System.Drawing.Point(875, 70);
            this.lblYearTo.Name = "lblYearTo";
            this.lblYearTo.Size = new System.Drawing.Size(16, 13);
            this.lblYearTo.TabIndex = 12;
            this.lblYearTo.Text = "to";
            
            // 
            // txtMaxYear
            // 
            this.txtMaxYear.Location = new System.Drawing.Point(895, 67);
            this.txtMaxYear.Name = "txtMaxYear";
            this.txtMaxYear.PlaceholderText = "Max";
            this.txtMaxYear.Size = new System.Drawing.Size(50, 20);
            this.txtMaxYear.TabIndex = 13;
            this.txtMaxYear.TextChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(960, 65);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(75, 25);
            this.btnClearFilters.TabIndex = 14;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            
            // 
            // btnAddCar
            // 
            this.btnAddCar.BackColor = System.Drawing.Color.Green;
            this.btnAddCar.ForeColor = System.Drawing.Color.White;
            this.btnAddCar.Location = new System.Drawing.Point(20, 110);
            this.btnAddCar.Name = "btnAddCar";
            this.btnAddCar.Size = new System.Drawing.Size(100, 30);
            this.btnAddCar.TabIndex = 15;
            this.btnAddCar.Text = "Add Car";
            this.btnAddCar.UseVisualStyleBackColor = false;
            this.btnAddCar.Click += new System.EventHandler(this.btnAddCar_Click);
            
            // 
            // btnEditCar
            // 
            this.btnEditCar.BackColor = System.Drawing.Color.Orange;
            this.btnEditCar.ForeColor = System.Drawing.Color.White;
            this.btnEditCar.Location = new System.Drawing.Point(130, 110);
            this.btnEditCar.Name = "btnEditCar";
            this.btnEditCar.Size = new System.Drawing.Size(100, 30);
            this.btnEditCar.TabIndex = 16;
            this.btnEditCar.Text = "Edit Car";
            this.btnEditCar.UseVisualStyleBackColor = false;
            this.btnEditCar.Click += new System.EventHandler(this.btnEditCar_Click);
            
            // 
            // btnDeleteCar
            // 
            this.btnDeleteCar.BackColor = System.Drawing.Color.Red;
            this.btnDeleteCar.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCar.Location = new System.Drawing.Point(240, 110);
            this.btnDeleteCar.Name = "btnDeleteCar";
            this.btnDeleteCar.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteCar.TabIndex = 17;
            this.btnDeleteCar.Text = "Delete Car";
            this.btnDeleteCar.UseVisualStyleBackColor = false;
            this.btnDeleteCar.Click += new System.EventHandler(this.btnDeleteCar_Click);
            
            // 
            // btnToggleAvailability
            // 
            this.btnToggleAvailability.BackColor = System.Drawing.Color.Blue;
            this.btnToggleAvailability.ForeColor = System.Drawing.Color.White;
            this.btnToggleAvailability.Location = new System.Drawing.Point(350, 110);
            this.btnToggleAvailability.Name = "btnToggleAvailability";
            this.btnToggleAvailability.Size = new System.Drawing.Size(120, 30);
            this.btnToggleAvailability.TabIndex = 18;
            this.btnToggleAvailability.Text = "Toggle Availability";
            this.btnToggleAvailability.UseVisualStyleBackColor = false;
            this.btnToggleAvailability.Click += new System.EventHandler(this.btnToggleAvailability_Click);
            
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(480, 110);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // 
            // lblAvailabilityInfo
            // 
            this.lblAvailabilityInfo.AutoSize = true;
            this.lblAvailabilityInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblAvailabilityInfo.Location = new System.Drawing.Point(620, 120);
            this.lblAvailabilityInfo.Name = "lblAvailabilityInfo";
            this.lblAvailabilityInfo.Size = new System.Drawing.Size(250, 13);
            this.lblAvailabilityInfo.TabIndex = 20;
            this.lblAvailabilityInfo.Text = "Green = Available, Red = Sold/Unavailable";
            
            // 
            // dgvCars
            // 
            this.dgvCars.AllowUserToAddRows = false;
            this.dgvCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCars.Location = new System.Drawing.Point(20, 160);
            this.dgvCars.MultiSelect = false;
            this.dgvCars.Name = "dgvCars";
            this.dgvCars.ReadOnly = true;
            this.dgvCars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCars.Size = new System.Drawing.Size(1200, 500);
            this.dgvCars.TabIndex = 21;
            this.dgvCars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCars_CellDoubleClick);
            
            // 
            // CarManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 700);
            this.Controls.Add(this.dgvCars);
            this.Controls.Add(this.lblAvailabilityInfo);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnToggleAvailability);
            this.Controls.Add(this.btnDeleteCar);
            this.Controls.Add(this.btnEditCar);
            this.Controls.Add(this.btnAddCar);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.txtMaxYear);
            this.Controls.Add(this.lblYearTo);
            this.Controls.Add(this.txtMinYear);
            this.Controls.Add(this.lblYearRange);
            this.Controls.Add(this.txtMaxPrice);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.txtMinPrice);
            this.Controls.Add(this.lblPriceRange);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "CarManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Management - ABC Car Traders";
            this.Load += new System.EventHandler(this.CarManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.DataGridView dgvCars;
        private System.Windows.Forms.Button btnAddCar;
        private System.Windows.Forms.Button btnEditCar;
        private System.Windows.Forms.Button btnDeleteCar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnToggleAvailability;
        private System.Windows.Forms.Label lblAvailabilityInfo;
        private System.Windows.Forms.Label lblPriceRange;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.Label lblYearRange;
        private System.Windows.Forms.TextBox txtMinYear;
        private System.Windows.Forms.Label lblYearTo;
        private System.Windows.Forms.TextBox txtMaxYear;
        private System.Windows.Forms.Button btnClearFilters;
        #endregion

        #region Form Events
        private void CarManagementForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadCars();
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

        private void FilterChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            ClearAllFilters();
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            ShowAddCarDialog();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            ShowEditCarDialog();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            DeleteSelectedCar();
        }

        private void btnToggleAvailability_Click(object sender, EventArgs e)
        {
            ToggleCarAvailability();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void dgvCars_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowEditCarDialog();
            }
        }
        #endregion

        #region Data Loading Methods
        private void LoadCategories()
        {
            try
            {
                var categories = categoryRepository.GetCarCategories();
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

        private void LoadCars()
        {
            try
            {
                var cars = carRepository.GetAllCars();
                DisplayCars(cars);
                UpdateTitle(cars.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCars(List<Car> cars)
        {
            var carData = cars.Select(c => new
            {
                c.CarID,
                c.Brand,
                c.Model,
                c.Year,
                Price = c.Price.ToString("C"),
                c.Color,
                Mileage = c.Mileage.ToString("N0") + " km",
                c.FuelType,
                c.Transmission,
                Category = c.CategoryName,
                Available = c.IsAvailable ? "Available" : "Sold/Unavailable",
                Added = c.CreatedDate.ToString("MM/dd/yyyy")
            }).ToList();

            dgvCars.DataSource = carData;

            // Hide CarID column
            if (dgvCars.Columns["CarID"] != null)
            {
                dgvCars.Columns["CarID"].Visible = false;
            }

            // Set column widths
            SetColumnWidths();

            // Apply availability color coding
            ApplyAvailabilityColorCoding();
        }

        private void SetColumnWidths()
        {
            if (dgvCars.Columns["Brand"] != null)
                dgvCars.Columns["Brand"].FillWeight = 80;
            if (dgvCars.Columns["Model"] != null)
                dgvCars.Columns["Model"].FillWeight = 100;
            if (dgvCars.Columns["Year"] != null)
                dgvCars.Columns["Year"].FillWeight = 60;
            if (dgvCars.Columns["Price"] != null)
                dgvCars.Columns["Price"].FillWeight = 80;
            if (dgvCars.Columns["Color"] != null)
                dgvCars.Columns["Color"].FillWeight = 70;
            if (dgvCars.Columns["Mileage"] != null)
                dgvCars.Columns["Mileage"].FillWeight = 80;
            if (dgvCars.Columns["FuelType"] != null)
                dgvCars.Columns["FuelType"].FillWeight = 70;
            if (dgvCars.Columns["Transmission"] != null)
                dgvCars.Columns["Transmission"].FillWeight = 80;
            if (dgvCars.Columns["Category"] != null)
                dgvCars.Columns["Category"].FillWeight = 80;
            if (dgvCars.Columns["Available"] != null)
                dgvCars.Columns["Available"].FillWeight = 90;
            if (dgvCars.Columns["Added"] != null)
                dgvCars.Columns["Added"].FillWeight = 70;
        }

        private void ApplyAvailabilityColorCoding()
        {
            foreach (DataGridViewRow row in dgvCars.Rows)
            {
                if (row.Cells["Available"].Value != null)
                {
                    string availability = row.Cells["Available"].Value.ToString();
                    
                    if (availability == "Available")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                }
            }
        }

        private void UpdateTitle(int count)
        {
            this.Text = $"Car Management - ABC Car Traders ({count} cars)";
        }
        #endregion

        #region Search and Filter Methods
        private void PerformSearch()
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                int categoryId = GetSelectedCategoryId();
                decimal? minPrice = GetDecimalValue(txtMinPrice.Text);
                decimal? maxPrice = GetDecimalValue(txtMaxPrice.Text);
                int? minYear = GetIntValue(txtMinYear.Text);
                int? maxYear = GetIntValue(txtMaxYear.Text);

                var cars = carRepository.SearchCars(searchTerm, categoryId);
                
                // Apply additional filters
                cars = ApplyPriceFilter(cars, minPrice, maxPrice);
                cars = ApplyYearFilter(cars, minYear, maxYear);

                DisplayCars(cars);
                UpdateTitle(cars.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching cars: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSelectedCategoryId()
        {
            if (cmbCategory.SelectedItem != null)
            {
                var selectedCategory = (dynamic)cmbCategory.SelectedItem;
                return selectedCategory.CategoryID;
            }
            return 0;
        }

        private decimal? GetDecimalValue(string text)
        {
            if (decimal.TryParse(text, out decimal value))
                return value;
            return null;
        }

        private int? GetIntValue(string text)
        {
            if (int.TryParse(text, out int value))
                return value;
            return null;
        }

        private List<Car> ApplyPriceFilter(List<Car> cars, decimal? minPrice, decimal? maxPrice)
        {
            if (minPrice.HasValue)
                cars = cars.Where(c => c.Price >= minPrice.Value).ToList();
                
            if (maxPrice.HasValue)
                cars = cars.Where(c => c.Price <= maxPrice.Value).ToList();
                
            return cars;
        }

        private List<Car> ApplyYearFilter(List<Car> cars, int? minYear, int? maxYear)
        {
            if (minYear.HasValue)
                cars = cars.Where(c => c.Year >= minYear.Value).ToList();
                
            if (maxYear.HasValue)
                cars = cars.Where(c => c.Year <= maxYear.Value).ToList();
                
            return cars;
        }

        private void ClearAllFilters()
        {
            txtSearch.Clear();
            txtMinPrice.Clear();
            txtMaxPrice.Clear();
            txtMinYear.Clear();
            txtMaxYear.Clear();
            cmbCategory.SelectedIndex = 0;
            LoadCars();
        }
        #endregion

        #region CRUD Operations
        private void ShowAddCarDialog()
        {
            using (var dialog = new CarEditDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadCars();
                    MessageBox.Show("Car added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowEditCarDialog()
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to edit.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells["CarID"].Value);
                var cars = carRepository.GetAllCars();
                var selectedCar = cars.FirstOrDefault(c => c.CarID == carId);

                if (selectedCar != null)
                {
                    using (var dialog = new CarEditDialog(selectedCar))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            LoadCars();
                            MessageBox.Show("Car updated successfully!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing car: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedCar()
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to delete.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string carInfo = $"{dgvCars.SelectedRows[0].Cells["Brand"].Value} {dgvCars.SelectedRows[0].Cells["Model"].Value} ({dgvCars.SelectedRows[0].Cells["Year"].Value})";
            
            if (MessageBox.Show($"Are you sure you want to delete this car?\n\n{carInfo}\n\nThis action cannot be undone.", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells["CarID"].Value);
                    
                    if (carRepository.DeleteCar(carId))
                    {
                        MessageBox.Show("Car deleted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCars();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete car.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting car: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToggleCarAvailability()
        {
            if (dgvCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to toggle availability.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int carId = Convert.ToInt32(dgvCars.SelectedRows[0].Cells["CarID"].Value);
                var cars = carRepository.GetAllCars();
                var selectedCar = cars.FirstOrDefault(c => c.CarID == carId);

                if (selectedCar != null)
                {
                    selectedCar.IsAvailable = !selectedCar.IsAvailable;
                    string status = selectedCar.IsAvailable ? "available" : "unavailable";
                    
                    if (carRepository.UpdateCar(selectedCar))
                    {
                        MessageBox.Show($"Car marked as {status}!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCars();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update car availability.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating car availability: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }

    // Enhanced Car Edit Dialog
    public partial class CarEditDialog : Form
    {
        private Car car;
        private bool isEditMode;
        private CarRepository carRepository;
        private CategoryRepository categoryRepository;

        public CarEditDialog(Car carToEdit = null)
        {
            car = carToEdit ?? new Car();
            isEditMode = carToEdit != null;
            carRepository = new CarRepository();
            categoryRepository = new CategoryRepository();
            InitializeComponent();
            
            if (isEditMode)
            {
                PopulateFields();
            }
        }

        private void InitializeComponent()
        {
            this.lblBrand = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblMileage = new System.Windows.Forms.Label();
            this.nudMileage = new System.Windows.Forms.NumericUpDown();
            this.lblFuelType = new System.Windows.Forms.Label();
            this.cmbFuelType = new System.Windows.Forms.ComboBox();
            this.lblTransmission = new System.Windows.Forms.Label();
            this.cmbTransmission = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkAvailable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMileage)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(20, 30);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(38, 13);
            this.lblBrand.TabIndex = 0;
            this.lblBrand.Text = "Brand:";
            
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(120, 27);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(200, 20);
            this.txtBrand.TabIndex = 1;
            
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(20, 70);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(39, 13);
            this.lblModel.TabIndex = 2;
            this.lblModel.Text = "Model:";
            
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(120, 67);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(200, 20);
            this.txtModel.TabIndex = 3;
            
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(350, 30);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 13);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Year:";
            
            // 
            // nudYear
            // 
            this.nudYear.Location = new System.Drawing.Point(420, 28);
            this.nudYear.Maximum = new decimal(new int[] { 2030, 0, 0, 0 });
            this.nudYear.Minimum = new decimal(new int[] { 1950, 0, 0, 0 });
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(100, 20);
            this.nudYear.TabIndex = 5;
            this.nudYear.Value = new decimal(new int[] { DateTime.Now.Year, 0, 0, 0 });
            
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(350, 70);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 13);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Price:";
            
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            this.nudPrice.Location = new System.Drawing.Point(420, 68);
            this.nudPrice.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(120, 20);
            this.nudPrice.TabIndex = 7;
            
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(20, 110);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "Color:";
            
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(120, 107);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(200, 20);
            this.txtColor.TabIndex = 9;
            
            // 
            // lblMileage
            // 
            this.lblMileage.AutoSize = true;
            this.lblMileage.Location = new System.Drawing.Point(350, 110);
            this.lblMileage.Name = "lblMileage";
            this.lblMileage.Size = new System.Drawing.Size(45, 13);
            this.lblMileage.TabIndex = 10;
            this.lblMileage.Text = "Mileage:";
            
            // 
            // nudMileage
            // 
            this.nudMileage.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudMileage.Location = new System.Drawing.Point(420, 108);
            this.nudMileage.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudMileage.Name = "nudMileage";
            this.nudMileage.Size = new System.Drawing.Size(120, 20);
            this.nudMileage.TabIndex = 11;
            
            // 
            // lblFuelType
            // 
            this.lblFuelType.AutoSize = true;
            this.lblFuelType.Location = new System.Drawing.Point(20, 150);
            this.lblFuelType.Name = "lblFuelType";
            this.lblFuelType.Size = new System.Drawing.Size(57, 13);
            this.lblFuelType.TabIndex = 12;
            this.lblFuelType.Text = "Fuel Type:";
            
            // 
            // cmbFuelType
            // 
            this.cmbFuelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuelType.FormattingEnabled = true;
            this.cmbFuelType.Items.AddRange(new object[] { "Petrol", "Diesel", "Hybrid", "Electric" });
            this.cmbFuelType.Location = new System.Drawing.Point(120, 147);
            this.cmbFuelType.Name = "cmbFuelType";
            this.cmbFuelType.Size = new System.Drawing.Size(200, 21);
            this.cmbFuelType.TabIndex = 13;
            
            // 
            // lblTransmission
            // 
            this.lblTransmission.AutoSize = true;
            this.lblTransmission.Location = new System.Drawing.Point(350, 150);
            this.lblTransmission.Name = "lblTransmission";
            this.lblTransmission.Size = new System.Drawing.Size(71, 13);
            this.lblTransmission.TabIndex = 14;
            this.lblTransmission.Text = "Transmission:";
            
            // 
            // cmbTransmission
            // 
            this.cmbTransmission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransmission.FormattingEnabled = true;
            this.cmbTransmission.Items.AddRange(new object[] { "Manual", "Automatic", "CVT" });
            this.cmbTransmission.Location = new System.Drawing.Point(430, 147);
            this.cmbTransmission.Name = "cmbTransmission";
            this.cmbTransmission.Size = new System.Drawing.Size(120, 21);
            this.cmbTransmission.TabIndex = 15;
            
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 190);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 16;
            this.lblCategory.Text = "Category:";
            
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(120, 187);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbCategory.TabIndex = 17;
            
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 230);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 18;
            this.lblDescription.Text = "Description:";
            
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 227);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(430, 80);
            this.txtDescription.TabIndex = 19;
            
            // 
            // chkAvailable
            // 
            this.chkAvailable.AutoSize = true;
            this.chkAvailable.Checked = true;
            this.chkAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvailable.Location = new System.Drawing.Point(120, 320);
            this.chkAvailable.Name = "chkAvailable";
            this.chkAvailable.Size = new System.Drawing.Size(95, 17);
            this.chkAvailable.TabIndex = 20;
            this.chkAvailable.Text = "Available for Sale";
            this.chkAvailable.UseVisualStyleBackColor = true;
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(320, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(450, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // CarEditDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(580, 420);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkAvailable);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbTransmission);
            this.Controls.Add(this.lblTransmission);
            this.Controls.Add(this.cmbFuelType);
            this.Controls.Add(this.lblFuelType);
            this.Controls.Add(this.nudMileage);
            this.Controls.Add(this.lblMileage);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.nudPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.nudYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.lblBrand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CarEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = isEditMode ? "Edit Car" : "Add Car";
            this.Load += new System.EventHandler(this.CarEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMileage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown nudYear;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblMileage;
        private System.Windows.Forms.NumericUpDown nudMileage;
        private System.Windows.Forms.Label lblFuelType;
        private System.Windows.Forms.ComboBox cmbFuelType;
        private System.Windows.Forms.Label lblTransmission;
        private System.Windows.Forms.ComboBox cmbTransmission;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkAvailable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void CarEditDialog_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = categoryRepository.GetCarCategories();
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
            txtBrand.Text = car.Brand;
            txtModel.Text = car.Model;
            nudYear.Value = car.Year;
            nudPrice.Value = car.Price;
            txtColor.Text = car.Color;
            nudMileage.Value = car.Mileage;
            cmbFuelType.Text = car.FuelType;
            cmbTransmission.Text = car.Transmission;
            txtDescription.Text = car.Description;
            chkAvailable.Checked = car.IsAvailable;
            
            // Set category
            for (int i = 0; i < cmbCategory.Items.Count; i++)
            {
                var item = (dynamic)cmbCategory.Items[i];
                if (item.CategoryID == car.CategoryID)
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
                SaveCar();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                MessageBox.Show("Please enter a brand.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBrand.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter a model.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModel.Focus();
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

        private void SaveCar()
        {
            try
            {
                car.Brand = txtBrand.Text.Trim();
                car.Model = txtModel.Text.Trim();
                car.Year = (int)nudYear.Value;
                car.Price = nudPrice.Value;
                car.Color = txtColor.Text.Trim();
                car.Mileage = (int)nudMileage.Value;
                car.FuelType = cmbFuelType.Text;
                car.Transmission = cmbTransmission.Text;
                car.Description = txtDescription.Text.Trim();
                car.IsAvailable = chkAvailable.Checked;
                
                var selectedCategory = (dynamic)cmbCategory.SelectedItem;
                car.CategoryID = selectedCategory.CategoryID;

                bool success;
                if (isEditMode)
                {
                    success = carRepository.UpdateCar(car);
                }
                else
                {
                    success = carRepository.AddCar(car);
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to {(isEditMode ? "update" : "add")} car.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving car: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}