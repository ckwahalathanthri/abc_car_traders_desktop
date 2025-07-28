using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using ABCCarTraders.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class OrderManagementForm : Form
    {
        private OrderRepository orderRepository;
        private UserRepository userRepository;
        private bool isCustomerView;
        private int customerId;

        public OrderManagementForm(bool isCustomerView = false, int customerId = 0)
        {
            this.isCustomerView = isCustomerView;
            this.customerId = customerId;
            orderRepository = new OrderRepository();
            userRepository = new UserRepository();
            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnViewCustomer = new System.Windows.Forms.Button();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnExportOrders = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblPendingOrders = new System.Windows.Forms.Label();
            this.lblCompletedOrders = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = isCustomerView ? "My Orders" : "Order Management";

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
            this.txtSearch.Size = new System.Drawing.Size(180, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(270, 65);
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
            this.lblStatus.Location = new System.Drawing.Point(360, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";

            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] { "All Orders", "Pending", "Processing", "Completed", "Cancelled" });
            this.cmbStatus.Location = new System.Drawing.Point(410, 67);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbStatus.TabIndex = 5;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(530, 70);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(66, 13);
            this.lblDateRange.TabIndex = 6;
            this.lblDateRange.Text = "Order Date:";

            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(605, 67);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFromDate.TabIndex = 7;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.FilterChanged);

            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(715, 70);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "to";

            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(740, 67);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(100, 20);
            this.dtpToDate.TabIndex = 9;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.FilterChanged);

            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(855, 65);
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
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.BackColor = System.Drawing.Color.Orange;
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(130, 110);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateStatus.TabIndex = 12;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Visible = !isCustomerView;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);

            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.BackColor = System.Drawing.Color.Red;
            this.btnDeleteOrder.ForeColor = System.Drawing.Color.White;
            this.btnDeleteOrder.Location = new System.Drawing.Point(isCustomerView ? 130 : 240, 110);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteOrder.TabIndex = 13;
            this.btnDeleteOrder.Text = isCustomerView ? "Cancel Order" : "Delete Order";
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);

            // 
            // btnViewCustomer
            // 
            this.btnViewCustomer.BackColor = System.Drawing.Color.Purple;
            this.btnViewCustomer.ForeColor = System.Drawing.Color.White;
            this.btnViewCustomer.Location = new System.Drawing.Point(350, 110);
            this.btnViewCustomer.Name = "btnViewCustomer";
            this.btnViewCustomer.Size = new System.Drawing.Size(110, 30);
            this.btnViewCustomer.TabIndex = 14;
            this.btnViewCustomer.Text = "View Customer";
            this.btnViewCustomer.UseVisualStyleBackColor = false;
            this.btnViewCustomer.Visible = !isCustomerView;
            this.btnViewCustomer.Click += new System.EventHandler(this.btnViewCustomer_Click);

            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.BackColor = System.Drawing.Color.DarkGreen;
            this.btnPrintOrder.ForeColor = System.Drawing.Color.White;
            this.btnPrintOrder.Location = new System.Drawing.Point(470, 110);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(100, 30);
            this.btnPrintOrder.TabIndex = 15;
            this.btnPrintOrder.Text = "Print Order";
            this.btnPrintOrder.UseVisualStyleBackColor = false;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);

            // 
            // btnExportOrders
            // 
            this.btnExportOrders.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnExportOrders.ForeColor = System.Drawing.Color.White;
            this.btnExportOrders.Location = new System.Drawing.Point(580, 110);
            this.btnExportOrders.Name = "btnExportOrders";
            this.btnExportOrders.Size = new System.Drawing.Size(100, 30);
            this.btnExportOrders.TabIndex = 16;
            this.btnExportOrders.Text = "Export Orders";
            this.btnExportOrders.UseVisualStyleBackColor = false;
            this.btnExportOrders.Visible = !isCustomerView;
            this.btnExportOrders.Click += new System.EventHandler(this.btnExportOrders_Click);

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
            this.lblStats.Visible = !isCustomerView;

            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Location = new System.Drawing.Point(820, 125);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(60, 13);
            this.lblTotalOrders.TabIndex = 19;
            this.lblTotalOrders.Text = "Total: 0";
            this.lblTotalOrders.Visible = !isCustomerView;

            // 
            // lblPendingOrders
            // 
            this.lblPendingOrders.AutoSize = true;
            this.lblPendingOrders.ForeColor = System.Drawing.Color.Orange;
            this.lblPendingOrders.Location = new System.Drawing.Point(890, 125);
            this.lblPendingOrders.Name = "lblPendingOrders";
            this.lblPendingOrders.Size = new System.Drawing.Size(75, 13);
            this.lblPendingOrders.TabIndex = 20;
            this.lblPendingOrders.Text = "Pending: 0";
            this.lblPendingOrders.Visible = !isCustomerView;

            // 
            // lblCompletedOrders
            // 
            this.lblCompletedOrders.AutoSize = true;
            this.lblCompletedOrders.ForeColor = System.Drawing.Color.Green;
            this.lblCompletedOrders.Location = new System.Drawing.Point(980, 125);
            this.lblCompletedOrders.Name = "lblCompletedOrders";
            this.lblCompletedOrders.Size = new System.Drawing.Size(75, 13);
            this.lblCompletedOrders.TabIndex = 21;
            this.lblCompletedOrders.Text = "Completed: 0";
            this.lblCompletedOrders.Visible = !isCustomerView;

            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.Green;
            this.lblTotalRevenue.Location = new System.Drawing.Point(1070, 125);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(75, 13);
            this.lblTotalRevenue.TabIndex = 22;
            this.lblTotalRevenue.Text = "Revenue: $0";
            this.lblTotalRevenue.Visible = !isCustomerView;

            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusInfo.Location = new System.Drawing.Point(20, 150);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(350, 13);
            this.lblStatusInfo.TabIndex = 23;
            this.lblStatusInfo.Text = "Yellow = Pending, Blue = Processing, Green = Completed, Red = Cancelled";

            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(20, 180);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1200, 480);
            this.dgvOrders.TabIndex = 24;
            this.dgvOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellDoubleClick);

            // 
            // OrderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 700);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.lblStatusInfo);
            this.Controls.Add(this.lblTotalRevenue);
            this.Controls.Add(this.lblCompletedOrders);
            this.Controls.Add(this.lblPendingOrders);
            this.Controls.Add(this.lblTotalOrders);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExportOrders);
            this.Controls.Add(this.btnPrintOrder);
            this.Controls.Add(this.btnViewCustomer);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnUpdateStatus);
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
            this.Name = "OrderManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = isCustomerView ? "My Orders - ABC Car Traders" : "Order Management - ABC Car Traders";
            this.Load += new System.EventHandler(this.OrderManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnViewCustomer;
        private System.Windows.Forms.Label lblStatusInfo;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnExportOrders;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblPendingOrders;
        private System.Windows.Forms.Label lblCompletedOrders;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Button btnPrintOrder;
        #endregion

        #region Form Events
        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            InitializeDateFilters();
            cmbStatus.SelectedIndex = 0; // All Orders
            LoadOrders();
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
            ShowOrderDetails();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            ShowUpdateStatusDialog();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            DeleteSelectedOrder();
        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            ViewOrderCustomer();
        }

        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            PrintSelectedOrder();
        }

        private void btnExportOrders_Click(object sender, EventArgs e)
        {
            ExportOrderList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowOrderDetails();
            }
        }
        #endregion

        #region Data Loading Methods
        private void InitializeDateFilters()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
        }

        private void LoadOrders()
        {
            try
            {
                List<Order> orders;

                if (isCustomerView)
                {
                    orders = orderRepository.GetOrdersByCustomer(AuthenticationService.CurrentUser.UserID);
                }
                else
                {
                    orders = orderRepository.GetAllOrders();
                }

                DisplayOrders(orders);
                if (!isCustomerView)
                {
                    UpdateStatistics(orders);
                }
                UpdateTitle(orders.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayOrders(List<Order> orders)
        {
            var orderData = orders.Select(o => new
            {
                o.OrderID,
                Customer = o.CustomerName,
                OrderDate = o.OrderDate.ToString("MM/dd/yyyy HH:mm"),
                TotalAmount = o.TotalAmount.ToString("C"),
                o.Status,
                ItemCount = GetOrderItemCount(o.OrderID),
                LastUpdated = o.OrderDate.ToString("MM/dd/yyyy")
            }).ToList();

            dgvOrders.DataSource = orderData;

            // Hide OrderID column
            if (dgvOrders.Columns["OrderID"] != null)
            {
                dgvOrders.Columns["OrderID"].Visible = false;
            }

            // Hide Customer column in customer view
            if (isCustomerView && dgvOrders.Columns["Customer"] != null)
            {
                dgvOrders.Columns["Customer"].Visible = false;
            }

            // Set column widths
            SetColumnWidths();

            // Apply status color coding
            ApplyStatusColorCoding();
        }

        private int GetOrderItemCount(int orderId)
        {
            try
            {
                return orderRepository.GetOrderItemCount(orderId);
            }
            catch
            {
                return 0;
            }
        }

        private void SetColumnWidths()
        {
            if (dgvOrders.Columns["Customer"] != null && !isCustomerView)
                dgvOrders.Columns["Customer"].FillWeight = 120;
            if (dgvOrders.Columns["OrderDate"] != null)
                dgvOrders.Columns["OrderDate"].FillWeight = 120;
            if (dgvOrders.Columns["TotalAmount"] != null)
                dgvOrders.Columns["TotalAmount"].FillWeight = 80;
            if (dgvOrders.Columns["Status"] != null)
                dgvOrders.Columns["Status"].FillWeight = 80;
            if (dgvOrders.Columns["ItemCount"] != null)
                dgvOrders.Columns["ItemCount"].FillWeight = 70;
            if (dgvOrders.Columns["LastUpdated"] != null)
                dgvOrders.Columns["LastUpdated"].FillWeight = 90;
        }

        private void ApplyStatusColorCoding()
        {
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();

                    switch (status)
                    {
                        case "Pending":
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                            break;
                        case "Processing":
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
                            break;
                        case "Completed":
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                            break;
                        case "Cancelled":
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        private void UpdateStatistics(List<Order> orders)
        {
            int total = orders.Count;
            int pending = orders.Count(o => o.Status == OrderStatus.Pending);
            int completed = orders.Count(o => o.Status == OrderStatus.Completed);
            decimal revenue = orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalAmount);

            lblTotalOrders.Text = $"Total: {total}";
            lblPendingOrders.Text = $"Pending: {pending}";
            lblCompletedOrders.Text = $"Completed: {completed}";
            lblTotalRevenue.Text = $"Revenue: {revenue:C}";
        }

        private void UpdateTitle(int count)
        {
            string title = isCustomerView ? "My Orders" : "Order Management";
            this.Text = $"{title} - ABC Car Traders ({count} orders)";
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

                List<Order> orders;

                if (isCustomerView)
                {
                    orders = orderRepository.GetOrdersByCustomer(AuthenticationService.CurrentUser.UserID);
                }
                else
                {
                    orders = orderRepository.GetAllOrders();
                }

                // Apply text search filter
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    orders = orders.Where(o =>
                        o.OrderID.ToString().Contains(searchTerm) ||
                        o.CustomerName.ToLower().Contains(searchTerm)
                    ).ToList();
                }

                // Apply status filter
                orders = ApplyStatusFilter(orders, statusFilter);

                // Apply date range filter
                orders = orders.Where(o => o.OrderDate >= fromDate && o.OrderDate < toDate).ToList();

                DisplayOrders(orders);
                if (!isCustomerView)
                {
                    UpdateStatistics(orders);
                }
                UpdateTitle(orders.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Order> ApplyStatusFilter(List<Order> orders, int statusFilter)
        {
            switch (statusFilter)
            {
                case 1: // Pending
                    return orders.Where(o => o.Status == OrderStatus.Pending).ToList();
                case 2: // Processing
                    return orders.Where(o => o.Status == OrderStatus.Processing).ToList();
                case 3: // Completed
                    return orders.Where(o => o.Status == OrderStatus.Completed).ToList();
                case 4: // Cancelled
                    return orders.Where(o => o.Status == OrderStatus.Cancelled).ToList();
                default: // All Orders
                    return orders;
            }
        }

        private void ClearAllFilters()
        {
            txtSearch.Clear();
            cmbStatus.SelectedIndex = 0;
            InitializeDateFilters();
            LoadOrders();
        }
        #endregion

        #region Order Operations
        private void ShowOrderDetails()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to view details.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderID"].Value);

                using (var dialog = new OrderDetailsDialog(orderId))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing order details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowUpdateStatusDialog()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to update status.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderID"].Value);
                string currentStatus = dgvOrders.SelectedRows[0].Cells["Status"].Value.ToString();

                using (var dialog = new OrderStatusUpdateDialog(orderId, currentStatus))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadOrders();
                        MessageBox.Show("Order status updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedOrder()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                string message = isCustomerView ? "Please select an order to cancel." : "Please select an order to delete.";
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderId = dgvOrders.SelectedRows[0].Cells["OrderID"].Value.ToString();
            string action = isCustomerView ? "cancel" : "delete";
            string confirmMessage = isCustomerView ?
                $"Are you sure you want to cancel order #{orderId}?" :
                $"Are you sure you want to delete order #{orderId}?\n\nThis action cannot be undone.";

            if (MessageBox.Show(confirmMessage, $"Confirm {action.ToUpper()}",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int orderIdInt = Convert.ToInt32(orderId);

                    if (orderRepository.DeleteOrder(orderIdInt))
                    {
                        string successMessage = isCustomerView ? "Order cancelled successfully!" : "Order deleted successfully!";
                        MessageBox.Show(successMessage, "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrders();
                    }
                    else
                    {
                        string errorMessage = isCustomerView ? "Failed to cancel order." : "Failed to delete order.";
                        MessageBox.Show(errorMessage, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error {action}ing order: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ViewOrderCustomer()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to view customer information.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string customerName = dgvOrders.SelectedRows[0].Cells["Customer"].Value.ToString();

                // Get customer details from username
                var customer = userRepository.GetCustomerByName(customerName);
                if (customer != null)
                {
                    using (var dialog = new CustomerInfoDialog(customer))
                    {
                        dialog.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Customer information not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing customer information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintSelectedOrder()
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to print.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string orderId = dgvOrders.SelectedRows[0].Cells["OrderID"].Value.ToString();
                MessageBox.Show($"Print functionality for Order #{orderId} would be implemented here.\n\nThis could generate PDF invoices or print receipts.",
                    "Print Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
  private void ExportOrderList()
        {
            try
            {
                MessageBox.Show("Export functionality would be implemented here.\n\nThis could export to CSV, Excel, or PDF formats with detailed order information.", 
                    "Export Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting order list: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }

    // Order Details Dialog
    public partial class OrderDetailsDialog : Form
    {
        private int orderId;
        private OrderRepository orderRepository;
        private Order order;

        public OrderDetailsDialog(int orderId)
        {
            this.orderId = orderId;
            this.orderRepository = new OrderRepository();
            InitializeComponent();
            LoadOrderDetails();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.lblCustomerInfo = new System.Windows.Forms.Label();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.lblItemsTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = $"Order #{orderId}";
            
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.AutoSize = true;
            this.lblOrderInfo.Location = new System.Drawing.Point(20, 60);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(62, 13);
            this.lblOrderInfo.TabIndex = 1;
            this.lblOrderInfo.Text = "Order Info";
            
            // 
            // lblCustomerInfo
            // 
            this.lblCustomerInfo.AutoSize = true;
            this.lblCustomerInfo.Location = new System.Drawing.Point(20, 120);
            this.lblCustomerInfo.Name = "lblCustomerInfo";
            this.lblCustomerInfo.Size = new System.Drawing.Size(79, 13);
            this.lblCustomerInfo.TabIndex = 2;
            this.lblCustomerInfo.Text = "Customer Info";
            
            // 
            // lblItemsTitle
            // 
            this.lblItemsTitle.AutoSize = true;
            this.lblItemsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsTitle.Location = new System.Drawing.Point(20, 180);
            this.lblItemsTitle.Name = "lblItemsTitle";
            this.lblItemsTitle.Size = new System.Drawing.Size(81, 13);
            this.lblItemsTitle.TabIndex = 3;
            this.lblItemsTitle.Text = "Order Items:";
            
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.AllowUserToAddRows = false;
            this.dgvOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderItems.Location = new System.Drawing.Point(20, 200);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.ReadOnly = true;
            this.dgvOrderItems.Size = new System.Drawing.Size(760, 200);
            this.dgvOrderItems.TabIndex = 4;
            
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Green;
            this.lblTotalAmount.Location = new System.Drawing.Point(640, 420);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(52, 17);
            this.lblTotalAmount.TabIndex = 5;
            this.lblTotalAmount.Text = "Total: ";
            
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(680, 460);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            
            // 
            // OrderDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dgvOrderItems);
            this.Controls.Add(this.lblItemsTitle);
            this.Controls.Add(this.lblCustomerInfo);
            this.Controls.Add(this.lblOrderInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderDetailsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order Details";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOrderInfo;
        private System.Windows.Forms.Label lblCustomerInfo;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnClose;
        #endregion

        private void LoadOrderDetails()
        {
            try
            {
                order = orderRepository.GetOrderById(orderId);
                if (order != null)
                {
                    // Display order information
                    lblOrderInfo.Text = $"Order Date: {order.OrderDate:MM/dd/yyyy HH:mm}\nStatus: {order.Status}\nTotal Items: {order.OrderItems?.Count ?? 0}";
                    
                    // Display customer information
                    lblCustomerInfo.Text = $"Customer: {order.CustomerName}";
                    
                    // Display order items
                    if (order.OrderItems != null && order.OrderItems.Any())
                    {
                        var itemData = order.OrderItems.Select(item => new
                        {
                            ItemType = item.ItemType.ToString(),
                            ItemName = item.ItemName,
                            Quantity = item.Quantity,
                            Price = item.Price.ToString("C"),
                            Total = (item.Price * item.Quantity).ToString("C")
                        }).ToList();

                        dgvOrderItems.DataSource = itemData;
                    }
                    
                    // Display total amount
                    lblTotalAmount.Text = $"Total: {order.TotalAmount:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Order Status Update Dialog
    public partial class OrderStatusUpdateDialog : Form
    {
        private int orderId;
        private string currentStatus;
        private OrderRepository orderRepository;

        public OrderStatusUpdateDialog(int orderId, string currentStatus)
        {
            this.orderId = orderId;
            this.currentStatus = currentStatus;
            this.orderRepository = new OrderRepository();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblNewStatus = new System.Windows.Forms.Label();
            this.cmbNewStatus = new System.Windows.Forms.ComboBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.AutoSize = true;
            this.lblOrderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderInfo.Location = new System.Drawing.Point(20, 20);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(150, 17);
            this.lblOrderInfo.TabIndex = 0;
            this.lblOrderInfo.Text = $"Update Order #{orderId}";
            
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(20, 60);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(80, 13);
            this.lblCurrentStatus.TabIndex = 1;
            this.lblCurrentStatus.Text = $"Current Status: {currentStatus}";
            
            // 
            // lblNewStatus
            // 
            this.lblNewStatus.AutoSize = true;
            this.lblNewStatus.Location = new System.Drawing.Point(20, 100);
            this.lblNewStatus.Name = "lblNewStatus";
            this.lblNewStatus.Size = new System.Drawing.Size(68, 13);
            this.lblNewStatus.TabIndex = 2;
            this.lblNewStatus.Text = "New Status:";
            
            // 
            // cmbNewStatus
            // 
            this.cmbNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStatus.FormattingEnabled = true;
            this.cmbNewStatus.Items.AddRange(new object[] { "Pending", "Processing", "Completed", "Cancelled" });
            this.cmbNewStatus.Location = new System.Drawing.Point(120, 97);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new System.Drawing.Size(200, 21);
            this.cmbNewStatus.TabIndex = 3;
            
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(20, 140);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 4;
            this.lblNotes.Text = "Notes:";
            
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(120, 137);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(200, 60);
            this.txtNotes.TabIndex = 5;
            
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Green;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(120, 220);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            
            // 
            // OrderStatusUpdateDialog
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 280);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.cmbNewStatus);
            this.Controls.Add(this.lblNewStatus);
            this.Controls.Add(this.lblCurrentStatus);
            this.Controls.Add(this.lblOrderInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderStatusUpdateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Order Status";
            this.Load += new System.EventHandler(this.OrderStatusUpdateDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblOrderInfo;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblNewStatus;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        #endregion

        private void OrderStatusUpdateDialog_Load(object sender, EventArgs e)
        {
            // Set current status as selected
            cmbNewStatus.Text = currentStatus;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbNewStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a new status.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string newStatus = cmbNewStatus.SelectedItem.ToString();
                OrderStatus status = (OrderStatus)Enum.Parse(typeof(OrderStatus), newStatus);
                
                if (orderRepository.UpdateOrderStatus(orderId, status))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update order status.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order status: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Customer Info Dialog
    public partial class CustomerInfoDialog : Form
    {
        private User customer;

        public CustomerInfoDialog(User customer)
        {
            this.customer = customer;
            InitializeComponent();
            DisplayCustomerInfo();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCustomerDetails = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Customer Information";
            
            // 
            // lblCustomerDetails
            // 
            this.lblCustomerDetails.AutoSize = true;
            this.lblCustomerDetails.Location = new System.Drawing.Point(20, 60);
            this.lblCustomerDetails.Name = "lblCustomerDetails";
            this.lblCustomerDetails.Size = new System.Drawing.Size(100, 13);
            this.lblCustomerDetails.TabIndex = 1;
            this.lblCustomerDetails.Text = "Customer Details";
            
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(280, 220);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            
            // 
            // CustomerInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 280);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblCustomerDetails);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Information";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomerDetails;
        private System.Windows.Forms.Button btnClose;
        #endregion

        private void DisplayCustomerInfo()
        {
            string details = $"Name: {customer.FullName}\n";
            details += $"Username: {customer.Username}\n";
            details += $"Email: {customer.Email}\n";
            details += $"Phone: {customer.Phone ?? "Not provided"}\n";
            details += $"Address: {customer.Address ?? "Not provided"}\n";
            details += $"Status: {(customer.IsActive ? "Active" : "Inactive")}\n";
            details += $"Registered: {customer.CreatedDate:MM/dd/yyyy}";

            lblCustomerDetails.Text = details;
            lblCustomerDetails.Size = new System.Drawing.Size(350, 150);
        }
    }
}