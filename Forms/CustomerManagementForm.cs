using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using ABCCarTraders.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class CustomerManagementForm : Form
    {
        private UserRepository userRepository;
        private OrderRepository orderRepository;

        public CustomerManagementForm()
        {
            userRepository = new UserRepository();
            orderRepository = new OrderRepository();
            InitializeComponent();
            LoadCustomers();
        }

        private void InitializeComponent()
        {
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnToggleStatus = new System.Windows.Forms.Button();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnExportCustomers = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblActiveCustomers = new System.Windows.Forms.Label();
            this.lblInactiveCustomers = new System.Windows.Forms.Label();
            this.btnViewOrders = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Customer Management";
            
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
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(380, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] { "All Customers", "Active Only", "Inactive Only" });
            this.cmbStatus.Location = new System.Drawing.Point(430, 67);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 21);
            this.cmbStatus.TabIndex = 5;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(570, 70);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(72, 13);
            this.lblDateRange.TabIndex = 6;
            this.lblDateRange.Text = "Registered:";
            
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(650, 67);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFromDate.TabIndex = 7;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(760, 70);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "to";
            
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(785, 67);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(100, 20);
            this.dtpToDate.TabIndex = 9;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.FilterChanged);
            
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(900, 65);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(75, 25);
            this.btnClearFilters.TabIndex = 10;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.Blue;
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(20, 110);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 30);
            this.btnViewDetails.TabIndex = 11;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.BackColor = System.Drawing.Color.Orange;
            this.btnEditCustomer.ForeColor = System.Drawing.Color.White;
            this.btnEditCustomer.Location = new System.Drawing.Point(130, 110);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(100, 30);
            this.btnEditCustomer.TabIndex = 12;
            this.btnEditCustomer.Text = "Edit Customer";
            this.btnEditCustomer.UseVisualStyleBackColor = false;
            this.btnEditCustomer.Click += new System.EventHandler(this.btnEditCustomer_Click);
            
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.BackColor = System.Drawing.Color.Red;
            this.btnDeleteCustomer.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCustomer.Location = new System.Drawing.Point(240, 110);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteCustomer.TabIndex = 13;
            this.btnDeleteCustomer.Text = "Delete Customer";
            this.btnDeleteCustomer.UseVisualStyleBackColor = false;
            this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
            
            // 
            // btnToggleStatus
            // 
            this.btnToggleStatus.BackColor = System.Drawing.Color.Purple;
            this.btnToggleStatus.ForeColor = System.Drawing.Color.White;
            this.btnToggleStatus.Location = new System.Drawing.Point(360, 110);
            this.btnToggleStatus.Name = "btnToggleStatus";
            this.btnToggleStatus.Size = new System.Drawing.Size(100, 30);
            this.btnToggleStatus.TabIndex = 14;
            this.btnToggleStatus.Text = "Toggle Status";
            this.btnToggleStatus.UseVisualStyleBackColor = false;
            this.btnToggleStatus.Click += new System.EventHandler(this.btnToggleStatus_Click);
            
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.BackColor = System.Drawing.Color.DarkGreen;
            this.btnViewOrders.ForeColor = System.Drawing.Color.White;
            this.btnViewOrders.Location = new System.Drawing.Point(470, 110);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(100, 30);
            this.btnViewOrders.TabIndex = 15;
            this.btnViewOrders.Text = "View Orders";
            this.btnViewOrders.UseVisualStyleBackColor = false;
            this.btnViewOrders.Click += new System.EventHandler(this.btnViewOrders_Click);
            
            // 
            // btnExportCustomers
            // 
            this.btnExportCustomers.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnExportCustomers.ForeColor = System.Drawing.Color.White;
            this.btnExportCustomers.Location = new System.Drawing.Point(580, 110);
            this.btnExportCustomers.Name = "btnExportCustomers";
            this.btnExportCustomers.Size = new System.Drawing.Size(100, 30);
            this.btnExportCustomers.TabIndex = 16;
            this.btnExportCustomers.Text = "Export List";
            this.btnExportCustomers.UseVisualStyleBackColor = false;
            this.btnExportCustomers.Click += new System.EventHandler(this.btnExportCustomers_Click);
            
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(690, 110);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.Location = new System.Drawing.Point(820, 110);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(69, 13);
            this.lblStats.TabIndex = 18;
            this.lblStats.Text = "Statistics:";
            
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Location = new System.Drawing.Point(820, 125);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(75, 13);
            this.lblTotalCustomers.TabIndex = 19;
            this.lblTotalCustomers.Text = "Total: 0";
            
            // 
            // lblActiveCustomers
            // 
            this.lblActiveCustomers.AutoSize = true;
            this.lblActiveCustomers.ForeColor = System.Drawing.Color.Green;
            this.lblActiveCustomers.Location = new System.Drawing.Point(920, 125);
            this.lblActiveCustomers.Name = "lblActiveCustomers";
            this.lblActiveCustomers.Size = new System.Drawing.Size(75, 13);
            this.lblActiveCustomers.TabIndex = 20;
            this.lblActiveCustomers.Text = "Active: 0";
            
            // 
            // lblInactiveCustomers
            // 
            this.lblInactiveCustomers.AutoSize = true;
            this.lblInactiveCustomers.ForeColor = System.Drawing.Color.Red;
            this.lblInactiveCustomers.Location = new System.Drawing.Point(1020, 125);
            this.lblInactiveCustomers.Name = "lblInactiveCustomers";
            this.lblInactiveCustomers.Size = new System.Drawing.Size(75, 13);
            this.lblInactiveCustomers.TabIndex = 21;
            this.lblInactiveCustomers.Text = "Inactive: 0";
            
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusInfo.Location = new System.Drawing.Point(20, 150);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(280, 13);
            this.lblStatusInfo.TabIndex = 22;
            this.lblStatusInfo.Text = "Green = Active Customers, Red = Inactive Customers";
            
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(20, 180);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(1200, 480);
            this.dgvCustomers.TabIndex = 23;
            this.dgvCustomers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_CellDoubleClick);
            
            // 
            // CustomerManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 700);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.lblStatusInfo);
            this.Controls.Add(this.lblInactiveCustomers);
            this.Controls.Add(this.lblActiveCustomers);
            this.Controls.Add(this.lblTotalCustomers);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExportCustomers);
            this.Controls.Add(this.btnViewOrders);
            this.Controls.Add(this.btnToggleStatus);
            this.Controls.Add(this.btnDeleteCustomer);
            this.Controls.Add(this.btnEditCustomer);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.lblDateRange);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "CustomerManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Management - ABC Car Traders";
            this.Load += new System.EventHandler(this.CustomerManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnToggleStatus;
        private System.Windows.Forms.Label lblStatusInfo;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnExportCustomers;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label lblActiveCustomers;
        private System.Windows.Forms.Label lblInactiveCustomers;
        private System.Windows.Forms.Button btnViewOrders;
        #endregion

        #region Form Events
        private void CustomerManagementForm_Load(object sender, EventArgs e)
        {
            InitializeDateFilters();
            cmbStatus.SelectedIndex = 0; // All Customers
            LoadCustomers();
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

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ShowCustomerDetails();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            ShowEditCustomerDialog();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DeleteSelectedCustomer();
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            ToggleCustomerStatus();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            ViewCustomerOrders();
        }

        private void btnExportCustomers_Click(object sender, EventArgs e)
        {
            ExportCustomerList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowCustomerDetails();
            }
        }
        #endregion

        #region Data Loading Methods
        private void InitializeDateFilters()
        {
            dtpFromDate.Value = DateTime.Now.AddYears(-1);
            dtpToDate.Value = DateTime.Now;
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = userRepository.GetAllCustomers();
                DisplayCustomers(customers);
                UpdateStatistics(customers);
                UpdateTitle(customers.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCustomers(List<User> customers)
        {
            var customerData = customers.Select(c => new
            {
                c.UserID,
                c.Username,
                c.FullName,
                c.Email,
                c.Phone,
                c.Address,
                Status = c.IsActive ? "Active" : "Inactive",
                Registered = c.CreatedDate.ToString("MM/dd/yyyy"),
                LastActivity = GetLastOrderDate(c.UserID)
            }).ToList();

            dgvCustomers.DataSource = customerData;

            // Hide UserID column
            if (dgvCustomers.Columns["UserID"] != null)
            {
                dgvCustomers.Columns["UserID"].Visible = false;
            }

            // Set column widths
            SetColumnWidths();

            // Apply status color coding
            ApplyStatusColorCoding();
        }

        private string GetLastOrderDate(int customerId)
        {
            try
            {
                var orders = orderRepository.GetOrdersByCustomer(customerId);
                if (orders.Any())
                {
                    return orders.OrderByDescending(o => o.OrderDate).First().OrderDate.ToString("MM/dd/yyyy");
                }
                return "No orders";
            }
            catch
            {
                return "N/A";
            }
        }

        private void SetColumnWidths()
        {
            if (dgvCustomers.Columns["Username"] != null)
                dgvCustomers.Columns["Username"].FillWeight = 80;
            if (dgvCustomers.Columns["FullName"] != null)
                dgvCustomers.Columns["FullName"].FillWeight = 120;
            if (dgvCustomers.Columns["Email"] != null)
                dgvCustomers.Columns["Email"].FillWeight = 140;
            if (dgvCustomers.Columns["Phone"] != null)
                dgvCustomers.Columns["Phone"].FillWeight = 100;
            if (dgvCustomers.Columns["Address"] != null)
                dgvCustomers.Columns["Address"].FillWeight = 150;
            if (dgvCustomers.Columns["Status"] != null)
                dgvCustomers.Columns["Status"].FillWeight = 60;
            if (dgvCustomers.Columns["Registered"] != null)
                dgvCustomers.Columns["Registered"].FillWeight = 80;
            if (dgvCustomers.Columns["LastActivity"] != null)
                dgvCustomers.Columns["LastActivity"].FillWeight = 90;
        }

        private void ApplyStatusColorCoding()
        {
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();
                    
                    if (status == "Active")
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

        private void UpdateStatistics(List<User> customers)
        {
            int total = customers.Count;
            int active = customers.Count(c => c.IsActive);
            int inactive = total - active;

            lblTotalCustomers.Text = $"Total: {total}";
            lblActiveCustomers.Text = $"Active: {active}";
            lblInactiveCustomers.Text = $"Inactive: {inactive}";
        }

        private void UpdateTitle(int count)
        {
            this.Text = $"Customer Management - ABC Car Traders ({count} customers)";
        }
        #endregion

        #region Search and Filter Methods
        private void PerformSearch()
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim().ToLower();
                int statusFilter = cmbStatus.SelectedIndex;
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1);

                var customers = userRepository.GetAllCustomers();
                
                // Apply text search filter
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    customers = customers.Where(c => 
                        c.FullName.ToLower().Contains(searchTerm) ||
                        c.Username.ToLower().Contains(searchTerm) ||
                        c.Email.ToLower().Contains(searchTerm) ||
                        (c.Phone != null && c.Phone.Contains(searchTerm))
                    ).ToList();
                }

                // Apply status filter
                customers = ApplyStatusFilter(customers, statusFilter);

                // Apply date range filter
                customers = customers.Where(c => c.CreatedDate >= fromDate && c.CreatedDate < toDate).ToList();

                DisplayCustomers(customers);
                UpdateStatistics(customers);
                UpdateTitle(customers.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<User> ApplyStatusFilter(List<User> customers, int statusFilter)
        {
            switch (statusFilter)
            {
                case 1: // Active Only
                    return customers.Where(c => c.IsActive).ToList();
                case 2: // Inactive Only
                    return customers.Where(c => !c.IsActive).ToList();
                default: // All Customers
                    return customers;
            }
        }

        private void ClearAllFilters()
        {
            txtSearch.Clear();
            cmbStatus.SelectedIndex = 0;
            InitializeDateFilters();
            LoadCustomers();
        }
        #endregion

        #region Customer Operations
        private void ShowCustomerDetails()
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to view details.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                var customers = userRepository.GetAllCustomers();
                var selectedCustomer = customers.FirstOrDefault(c => c.UserID == customerId);

                if (selectedCustomer != null)
                {
                    using (var dialog = new CustomerDetailsDialog(selectedCustomer))
                    {
                        dialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing customer details: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEditCustomerDialog()
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                var customers = userRepository.GetAllCustomers();
                var selectedCustomer = customers.FirstOrDefault(c => c.UserID == customerId);

                if (selectedCustomer != null)
                {
                    using (var dialog = new CustomerEditDialog(selectedCustomer))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            LoadCustomers();
                            MessageBox.Show("Customer updated successfully!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing customer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedCustomer()
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string customerName = dgvCustomers.SelectedRows[0].Cells["FullName"].Value.ToString();
            
            if (MessageBox.Show($"Are you sure you want to delete customer '{customerName}'?\n\nThis will also delete all their orders and cannot be undone.", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                    
                    if (userRepository.DeleteUser(customerId))
                    {
                        MessageBox.Show("Customer deleted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete customer.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToggleCustomerStatus()
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to toggle status.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                var customers = userRepository.GetAllCustomers();
                var selectedCustomer = customers.FirstOrDefault(c => c.UserID == customerId);

                if (selectedCustomer != null)
                {
                    selectedCustomer.IsActive = !selectedCustomer.IsActive;
                    string status = selectedCustomer.IsActive ? "activated" : "deactivated";
                    
                    if (userRepository.UpdateUser(selectedCustomer))
                    {
                        MessageBox.Show($"Customer {status} successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer status.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer status: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewCustomerOrders()
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to view their orders.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["UserID"].Value);
                string customerName = dgvCustomers.SelectedRows[0].Cells["FullName"].Value.ToString();
                
                using (var dialog = new CustomerOrdersDialog(customerId, customerName))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing customer orders: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportCustomerList()
        {
            try
            {
                MessageBox.Show("Export functionality would be implemented here.\n\nThis could export to CSV, Excel, or PDF formats.", 
                    "Export Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting customer list: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }

    // Customer Details Dialog
    public partial class CustomerDetailsDialog : Form
    {
        private User customer;
        private OrderRepository orderRepository;

        public CustomerDetailsDialog(User customer)
        {
            this.customer = customer;
            this.orderRepository = new OrderRepository();
            InitializeComponent();
            DisplayCustomerDetails();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRegistered = new System.Windows.Forms.Label();
            this.lblOrderStats = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(136, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Customer Details";
            
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(20, 60);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username:";
            
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(20, 90);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(57, 13);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Full Name:";
            
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 120);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 150);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 13);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone:";
            
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(20, 180);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 5;
            this.lblAddress.Text = "Address:";
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 220);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            
            // 
            // lblRegistered
            // 
            this.lblRegistered.AutoSize = true;
            this.lblRegistered.Location = new System.Drawing.Point(20, 250);
            this.lblRegistered.Name = "lblRegistered";
            this.lblRegistered.Size = new System.Drawing.Size(63, 13);
            this.lblRegistered.TabIndex = 7;
            this.lblRegistered.Text = "Registered:";
            
            // 
            // lblOrderStats
            // 
            this.lblOrderStats.AutoSize = true;
            this.lblOrderStats.Location = new System.Drawing.Point(20, 280);
            this.lblOrderStats.Name = "lblOrderStats";
            this.lblOrderStats.Size = new System.Drawing.Size(41, 13);
            this.lblOrderStats.TabIndex = 8;
            this.lblOrderStats.Text = "Orders:";
            
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(300, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            
            // 
            // CustomerDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblOrderStats);
            this.Controls.Add(this.lblRegistered);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerDetailsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Details";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblRegistered;
        private System.Windows.Forms.Label lblOrderStats;
        private System.Windows.Forms.Button btnClose;
        #endregion

        private void DisplayCustomerDetails()
        {
            lblUsername.Text = $"Username: {customer.Username}";
            lblFullName.Text = $"Full Name: {customer.FullName}";
            lblEmail.Text = $"Email: {customer.Email}";
            lblPhone.Text = $"Phone: {customer.Phone ?? "Not provided"}";
            lblAddress.Text = $"Address: {customer.Address ?? "Not provided"}";
            lblStatus.Text = $"Status: {(customer.IsActive ? "Active" : "Inactive")}";
            lblRegistered.Text = $"Registered: {customer.CreatedDate.ToString("MM/dd/yyyy")}";
            
            // Get order statistics
            try
            {
                var orders = orderRepository.GetOrdersByCustomer(customer.UserID);
                decimal totalSpent = orders.Sum(o => o.TotalAmount);
                lblOrderStats.Text = $"Orders: {orders.Count} orders, Total Spent: {totalSpent:C}";
            }
            catch
            {
                lblOrderStats.Text = "Orders: Unable to load order statistics";
            }
        }
    }

    // Customer Edit Dialog
    public partial class CustomerEditDialog : Form
    {
        private User customer;
        private UserRepository userRepository;

        public CustomerEditDialog(User customer)
        {
            this.customer = customer;
            this.userRepository = new UserRepository();
            InitializeComponent();
            PopulateFields();
        }

        private void InitializeComponent()
        {
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(20, 30);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(57, 13);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name:";
            
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(100, 27);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(250, 20);
            this.txtFullName.TabIndex = 1;
            
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 70);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(100, 67);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);
            this.txtEmail.TabIndex = 3;
            
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 110);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 13);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone:";
            
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(100, 107);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 20);
            this.txtPhone.TabIndex = 5;
            
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(20, 150);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address:";
            
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(100, 147);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(250, 60);
            this.txtAddress.TabIndex = 7;
            
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(100, 220);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(56, 17);
            this.chkIsActive.TabIndex = 8;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // CustomerEditDialog
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblFullName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Customer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void PopulateFields()
        {
            txtFullName.Text = customer.FullName;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone ?? "";
            txtAddress.Text = customer.Address ?? "";
            chkIsActive.Checked = customer.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SaveCustomer();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter a full name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter an email address.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void SaveCustomer()
        {
            try
            {
                customer.FullName = txtFullName.Text.Trim();
                customer.Email = txtEmail.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();
                customer.Address = txtAddress.Text.Trim();
                customer.IsActive = chkIsActive.Checked;

                if (userRepository.UpdateUser(customer))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update customer.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Customer Orders Dialog
    public partial class CustomerOrdersDialog : Form
    {
        private int customerId;
        private string customerName;
        private OrderRepository orderRepository;

        public CustomerOrdersDialog(int customerId, string customerName)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.orderRepository = new OrderRepository();
            InitializeComponent();
            LoadCustomerOrders();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = $"Orders for {customerName}";
            
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(20, 60);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(760, 350);
            this.dgvOrders.TabIndex = 1;
            
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(20, 430);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(51, 13);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary";
            
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(680, 460);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            
            // 
            // CustomerOrdersDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerOrdersDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Orders";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSummary;
        #endregion

        private void LoadCustomerOrders()
        {
            try
            {
                var orders = orderRepository.GetOrdersByCustomer(customerId);
                
                var orderData = orders.Select(o => new
                {
                    o.OrderID,
                    OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                    TotalAmount = o.TotalAmount.ToString("C"),
                    o.Status
                }).ToList();

                dgvOrders.DataSource = orderData;

                // Hide OrderID column
                if (dgvOrders.Columns["OrderID"] != null)
                {
                    dgvOrders.Columns["OrderID"].Visible = false;
                }

                // Update summary
                decimal totalSpent = orders.Sum(o => o.TotalAmount);
                lblSummary.Text = $"Total Orders: {orders.Count} | Total Amount Spent: {totalSpent:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer orders: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}