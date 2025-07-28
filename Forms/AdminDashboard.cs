using ABCCarTraders.Business;
using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace ABCCarTraders.Forms
{
    public partial class AdminDashboard : Form
    {
        private CarRepository carRepository;
        private CarPartRepository carPartRepository;
        private UserRepository userRepository;
        private OrderRepository orderRepository;
        private System.Windows.Forms.Timer refreshTimer;

        public AdminDashboard()
        {
            carRepository = new CarRepository();
            carPartRepository = new CarPartRepository();
            userRepository = new UserRepository();
            orderRepository = new OrderRepository();
            
            InitializeComponent();
            SetupRefreshTimer();
            LoadDashboardData();
            
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.carsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carPartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managePartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowStockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCustomersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pendingOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.panelCarsStats = new System.Windows.Forms.Panel();
            this.lblCarsTitle = new System.Windows.Forms.Label();
            this.lblTotalCars = new System.Windows.Forms.Label();
            this.lblAvailableCars = new System.Windows.Forms.Label();
            this.panelPartsStats = new System.Windows.Forms.Panel();
            this.lblPartsTitle = new System.Windows.Forms.Label();
            this.lblTotalParts = new System.Windows.Forms.Label();
            this.lblLowStockParts = new System.Windows.Forms.Label();
            this.panelCustomersStats = new System.Windows.Forms.Panel();
            this.lblCustomersTitle = new System.Windows.Forms.Label();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblActiveCustomers = new System.Windows.Forms.Label();
            this.panelOrdersStats = new System.Windows.Forms.Panel();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblPendingOrders = new System.Windows.Forms.Label();
            this.lblMonthlyRevenue = new System.Windows.Forms.Label();
            this.panelQuickActions = new System.Windows.Forms.Panel();
            this.lblQuickActionsTitle = new System.Windows.Forms.Label();
            this.btnAddCar = new System.Windows.Forms.Button();
            this.btnAddPart = new System.Windows.Forms.Button();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.btnViewCustomers = new System.Windows.Forms.Button();
            this.btnLowStock = new System.Windows.Forms.Button();
            this.btnSalesReport = new System.Windows.Forms.Button();
            this.panelRecentActivity = new System.Windows.Forms.Panel();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            this.panelAlerts = new System.Windows.Forms.Panel();
            this.lblAlertsTitle = new System.Windows.Forms.Label();
            this.lstAlerts = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelCarsStats.SuspendLayout();
            this.panelPartsStats.SuspendLayout();
            this.panelCustomersStats.SuspendLayout();
            this.panelOrdersStats.SuspendLayout();
            this.panelQuickActions.SuspendLayout();
            this.panelRecentActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.panelAlerts.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carsToolStripMenuItem,
            this.carPartsToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.systemToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1400, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            
            // 
            // carsToolStripMenuItem
            // 
            this.carsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageToolStripMenuItem,
            this.addCarToolStripMenuItem});
            this.carsToolStripMenuItem.Name = "carsToolStripMenuItem";
            this.carsToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.carsToolStripMenuItem.Text = "Cars";
            
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.manageToolStripMenuItem.Text = "Manage Cars";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            
            // 
            // addCarToolStripMenuItem
            // 
            this.addCarToolStripMenuItem.Name = "addCarToolStripMenuItem";
            this.addCarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addCarToolStripMenuItem.Text = "Add New Car";
            this.addCarToolStripMenuItem.Click += new System.EventHandler(this.addCarToolStripMenuItem_Click);
            
            // 
            // carPartsToolStripMenuItem
            // 
            this.carPartsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managePartsToolStripMenuItem,
            this.addPartToolStripMenuItem,
            this.lowStockReportToolStripMenuItem});
            this.carPartsToolStripMenuItem.Name = "carPartsToolStripMenuItem";
            this.carPartsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.carPartsToolStripMenuItem.Text = "Car Parts";
            
            // 
            // managePartsToolStripMenuItem
            // 
            this.managePartsToolStripMenuItem.Name = "managePartsToolStripMenuItem";
            this.managePartsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.managePartsToolStripMenuItem.Text = "Manage Parts";
            this.managePartsToolStripMenuItem.Click += new System.EventHandler(this.managePartsToolStripMenuItem_Click);
            
            // 
            // addPartToolStripMenuItem
            // 
            this.addPartToolStripMenuItem.Name = "addPartToolStripMenuItem";
            this.addPartToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addPartToolStripMenuItem.Text = "Add New Part";
            this.addPartToolStripMenuItem.Click += new System.EventHandler(this.addPartToolStripMenuItem_Click);
            
            // 
            // lowStockReportToolStripMenuItem
            // 
            this.lowStockReportToolStripMenuItem.Name = "lowStockReportToolStripMenuItem";
            this.lowStockReportToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.lowStockReportToolStripMenuItem.Text = "Low Stock Report";
            this.lowStockReportToolStripMenuItem.Click += new System.EventHandler(this.lowStockReportToolStripMenuItem_Click);
            
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewCustomersToolStripMenuItem,
            this.customerReportsToolStripMenuItem});
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.customersToolStripMenuItem.Text = "Customers";
            
            // 
            // viewCustomersToolStripMenuItem
            // 
            this.viewCustomersToolStripMenuItem.Name = "viewCustomersToolStripMenuItem";
            this.viewCustomersToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewCustomersToolStripMenuItem.Text = "View Customers";
            this.viewCustomersToolStripMenuItem.Click += new System.EventHandler(this.viewCustomersToolStripMenuItem_Click);
            
            // 
            // customerReportsToolStripMenuItem
            // 
            this.customerReportsToolStripMenuItem.Name = "customerReportsToolStripMenuItem";
            this.customerReportsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.customerReportsToolStripMenuItem.Text = "Customer Reports";
            this.customerReportsToolStripMenuItem.Click += new System.EventHandler(this.customerReportsToolStripMenuItem_Click);
            
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOrdersToolStripMenuItem,
            this.pendingOrdersToolStripMenuItem,
            this.orderReportsToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.ordersToolStripMenuItem.Text = "Orders";
            
            // 
            // viewOrdersToolStripMenuItem
            // 
            this.viewOrdersToolStripMenuItem.Name = "viewOrdersToolStripMenuItem";
            this.viewOrdersToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.viewOrdersToolStripMenuItem.Text = "View All Orders";
            this.viewOrdersToolStripMenuItem.Click += new System.EventHandler(this.viewOrdersToolStripMenuItem_Click);
            
            // 
            // pendingOrdersToolStripMenuItem
            // 
            this.pendingOrdersToolStripMenuItem.Name = "pendingOrdersToolStripMenuItem";
            this.pendingOrdersToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pendingOrdersToolStripMenuItem.Text = "Pending Orders";
            this.pendingOrdersToolStripMenuItem.Click += new System.EventHandler(this.pendingOrdersToolStripMenuItem_Click);
            
            // 
            // orderReportsToolStripMenuItem
            // 
            this.orderReportsToolStripMenuItem.Name = "orderReportsToolStripMenuItem";
            this.orderReportsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.orderReportsToolStripMenuItem.Text = "Order Reports";
            this.orderReportsToolStripMenuItem.Click += new System.EventHandler(this.orderReportsToolStripMenuItem_Click);
            
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salesReportToolStripMenuItem,
            this.inventoryReportToolStripMenuItem,
            this.customerReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            
            // 
            // salesReportToolStripMenuItem
            // 
            this.salesReportToolStripMenuItem.Name = "salesReportToolStripMenuItem";
            this.salesReportToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.salesReportToolStripMenuItem.Text = "Sales Report";
            this.salesReportToolStripMenuItem.Click += new System.EventHandler(this.salesReportToolStripMenuItem_Click);
            
            // 
            // inventoryReportToolStripMenuItem
            // 
            this.inventoryReportToolStripMenuItem.Name = "inventoryReportToolStripMenuItem";
            this.inventoryReportToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.inventoryReportToolStripMenuItem.Text = "Inventory Report";
            this.inventoryReportToolStripMenuItem.Click += new System.EventHandler(this.inventoryReportToolStripMenuItem_Click);
            
            // 
            // customerReportToolStripMenuItem
            // 
            this.customerReportToolStripMenuItem.Name = "customerReportToolStripMenuItem";
            this.customerReportToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.customerReportToolStripMenuItem.Text = "Customer Report";
            this.customerReportToolStripMenuItem.Click += new System.EventHandler(this.customerReportToolStripMenuItem_Click);
            
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblWelcome.Location = new System.Drawing.Point(30, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 26);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = $"Welcome, {AuthenticationService.CurrentUser.FullName}!";
            
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.Gray;
            this.lblDateTime.Location = new System.Drawing.Point(1100, 45);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(200, 17);
            this.lblDateTime.TabIndex = 2;
            this.lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.lblStatsTitle);
            this.panelStats.Controls.Add(this.panelCarsStats);
            this.panelStats.Controls.Add(this.panelPartsStats);
            this.panelStats.Controls.Add(this.panelCustomersStats);
            this.panelStats.Controls.Add(this.panelOrdersStats);
            this.panelStats.Location = new System.Drawing.Point(30, 80);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1330, 200);
            this.panelStats.TabIndex = 3;
            
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.AutoSize = true;
            this.lblStatsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(150, 20);
            this.lblStatsTitle.TabIndex = 0;
            this.lblStatsTitle.Text = "System Overview";
            
            // 
            // panelCarsStats
            // 
            this.panelCarsStats.BackColor = System.Drawing.Color.LightBlue;
            this.panelCarsStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCarsStats.Controls.Add(this.lblCarsTitle);
            this.panelCarsStats.Controls.Add(this.lblTotalCars);
            this.panelCarsStats.Controls.Add(this.lblAvailableCars);
            this.panelCarsStats.Location = new System.Drawing.Point(20, 50);
            this.panelCarsStats.Name = "panelCarsStats";
            this.panelCarsStats.Size = new System.Drawing.Size(300, 120);
            this.panelCarsStats.TabIndex = 1;
            
            // 
            // lblCarsTitle
            // 
            this.lblCarsTitle.AutoSize = true;
            this.lblCarsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCarsTitle.Name = "lblCarsTitle";
            this.lblCarsTitle.Size = new System.Drawing.Size(40, 17);
            this.lblCarsTitle.TabIndex = 0;
            this.lblCarsTitle.Text = "Cars";
            
            // 
            // lblTotalCars
            // 
            this.lblTotalCars.AutoSize = true;
            this.lblTotalCars.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCars.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTotalCars.Location = new System.Drawing.Point(15, 40);
            this.lblTotalCars.Name = "lblTotalCars";
            this.lblTotalCars.Size = new System.Drawing.Size(36, 37);
            this.lblTotalCars.TabIndex = 1;
            this.lblTotalCars.Text = "0";
            
            // 
            // lblAvailableCars
            // 
            this.lblAvailableCars.AutoSize = true;
            this.lblAvailableCars.Location = new System.Drawing.Point(15, 85);
            this.lblAvailableCars.Name = "lblAvailableCars";
            this.lblAvailableCars.Size = new System.Drawing.Size(90, 13);
            this.lblAvailableCars.TabIndex = 2;
            this.lblAvailableCars.Text = "Available: 0";
            
            // 
            // panelPartsStats
            // 
            this.panelPartsStats.BackColor = System.Drawing.Color.LightGreen;
            this.panelPartsStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPartsStats.Controls.Add(this.lblPartsTitle);
            this.panelPartsStats.Controls.Add(this.lblTotalParts);
            this.panelPartsStats.Controls.Add(this.lblLowStockParts);
            this.panelPartsStats.Location = new System.Drawing.Point(340, 50);
            this.panelPartsStats.Name = "panelPartsStats";
            this.panelPartsStats.Size = new System.Drawing.Size(300, 120);
            this.panelPartsStats.TabIndex = 2;
            
            // 
            // lblPartsTitle
            // 
            this.lblPartsTitle.AutoSize = true;
            this.lblPartsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblPartsTitle.Name = "lblPartsTitle";
            this.lblPartsTitle.Size = new System.Drawing.Size(69, 17);
            this.lblPartsTitle.TabIndex = 0;
            this.lblPartsTitle.Text = "Car Parts";
            
            // 
            // lblTotalParts
            // 
            this.lblTotalParts.AutoSize = true;
            this.lblTotalParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalParts.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalParts.Location = new System.Drawing.Point(15, 40);
            this.lblTotalParts.Name = "lblTotalParts";
            this.lblTotalParts.Size = new System.Drawing.Size(36, 37);
            this.lblTotalParts.TabIndex = 1;
            this.lblTotalParts.Text = "0";
            
            // 
            // lblLowStockParts
            // 
            this.lblLowStockParts.AutoSize = true;
            this.lblLowStockParts.ForeColor = System.Drawing.Color.Red;
            this.lblLowStockParts.Location = new System.Drawing.Point(15, 85);
            this.lblLowStockParts.Name = "lblLowStockParts";
            this.lblLowStockParts.Size = new System.Drawing.Size(80, 13);
            this.lblLowStockParts.TabIndex = 2;
            this.lblLowStockParts.Text = "Low Stock: 0";
            
            // 
            // panelCustomersStats
            // 
            this.panelCustomersStats.BackColor = System.Drawing.Color.LightYellow;
            this.panelCustomersStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCustomersStats.Controls.Add(this.lblCustomersTitle);
            this.panelCustomersStats.Controls.Add(this.lblTotalCustomers);
            this.panelCustomersStats.Controls.Add(this.lblActiveCustomers);
            this.panelCustomersStats.Location = new System.Drawing.Point(660, 50);
            this.panelCustomersStats.Name = "panelCustomersStats";
            this.panelCustomersStats.Size = new System.Drawing.Size(300, 120);
            this.panelCustomersStats.TabIndex = 3;
            
            // 
            // lblCustomersTitle
            // 
            this.lblCustomersTitle.AutoSize = true;
            this.lblCustomersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomersTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCustomersTitle.Name = "lblCustomersTitle";
            this.lblCustomersTitle.Size = new System.Drawing.Size(83, 17);
            this.lblCustomersTitle.TabIndex = 0;
            this.lblCustomersTitle.Text = "Customers";
            
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTotalCustomers.Location = new System.Drawing.Point(15, 40);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(36, 37);
            this.lblTotalCustomers.TabIndex = 1;
            this.lblTotalCustomers.Text = "0";
            
            // 
            // lblActiveCustomers
            // 
            this.lblActiveCustomers.AutoSize = true;
            this.lblActiveCustomers.ForeColor = System.Drawing.Color.Green;
            this.lblActiveCustomers.Location = new System.Drawing.Point(15, 85);
            this.lblActiveCustomers.Name = "lblActiveCustomers";
            this.lblActiveCustomers.Size = new System.Drawing.Size(75, 13);
            this.lblActiveCustomers.TabIndex = 2;
            this.lblActiveCustomers.Text = "Active: 0";
            
            // 
            // panelOrdersStats
            // 
            this.panelOrdersStats.BackColor = System.Drawing.Color.LightCoral;
            this.panelOrdersStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrdersStats.Controls.Add(this.lblOrdersTitle);
            this.panelOrdersStats.Controls.Add(this.lblTotalOrders);
            this.panelOrdersStats.Controls.Add(this.lblPendingOrders);
            this.panelOrdersStats.Controls.Add(this.lblMonthlyRevenue);
            this.panelOrdersStats.Location = new System.Drawing.Point(980, 50);
            this.panelOrdersStats.Name = "panelOrdersStats";
            this.panelOrdersStats.Size = new System.Drawing.Size(300, 120);
            this.panelOrdersStats.TabIndex = 4;
            
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(54, 17);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "Orders";
            
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotalOrders.Location = new System.Drawing.Point(15, 40);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(36, 37);
            this.lblTotalOrders.TabIndex = 1;
            this.lblTotalOrders.Text = "0";
            
            // 
            // lblPendingOrders
            // 
            this.lblPendingOrders.AutoSize = true;
            this.lblPendingOrders.ForeColor = System.Drawing.Color.Orange;
            this.lblPendingOrders.Location = new System.Drawing.Point(15, 85);
            this.lblPendingOrders.Name = "lblPendingOrders";
            this.lblPendingOrders.Size = new System.Drawing.Size(75, 13);
            this.lblPendingOrders.TabIndex = 2;
            this.lblPendingOrders.Text = "Pending: 0";
            
            // 
            // lblMonthlyRevenue
            // 
            this.lblMonthlyRevenue.AutoSize = true;
            this.lblMonthlyRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthlyRevenue.ForeColor = System.Drawing.Color.Green;
            this.lblMonthlyRevenue.Location = new System.Drawing.Point(15, 100);
            this.lblMonthlyRevenue.Name = "lblMonthlyRevenue";
            this.lblMonthlyRevenue.Size = new System.Drawing.Size(120, 13);
            this.lblMonthlyRevenue.TabIndex = 3;
            this.lblMonthlyRevenue.Text = "This Month: $0.00";
            
            // 
            // panelQuickActions
            // 
            this.panelQuickActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelQuickActions.Controls.Add(this.lblQuickActionsTitle);
            this.panelQuickActions.Controls.Add(this.btnAddCar);
            this.panelQuickActions.Controls.Add(this.btnAddPart);
            this.panelQuickActions.Controls.Add(this.btnViewOrders);
            this.panelQuickActions.Controls.Add(this.btnViewCustomers);
            this.panelQuickActions.Controls.Add(this.btnLowStock);
            this.panelQuickActions.Controls.Add(this.btnSalesReport);
            this.panelQuickActions.Location = new System.Drawing.Point(30, 300);
            this.panelQuickActions.Name = "panelQuickActions";
            this.panelQuickActions.Size = new System.Drawing.Size(650, 160);
            this.panelQuickActions.TabIndex = 4;
            
            // 
            // lblQuickActionsTitle
            // 
            this.lblQuickActionsTitle.AutoSize = true;
            this.lblQuickActionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickActionsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblQuickActionsTitle.Name = "lblQuickActionsTitle";
            this.lblQuickActionsTitle.Size = new System.Drawing.Size(120, 20);
            this.lblQuickActionsTitle.TabIndex = 0;
            this.lblQuickActionsTitle.Text = "Quick Actions";
            
            // 
            // btnAddCar
            // 
            this.btnAddCar.BackColor = System.Drawing.Color.Green;
            this.btnAddCar.ForeColor = System.Drawing.Color.White;
            this.btnAddCar.Location = new System.Drawing.Point(20, 50);
            this.btnAddCar.Name = "btnAddCar";
            this.btnAddCar.Size = new System.Drawing.Size(120, 40);
            this.btnAddCar.TabIndex = 1;
            this.btnAddCar.Text = "Add New Car";
            this.btnAddCar.UseVisualStyleBackColor = false;
            this.btnAddCar.Click += new System.EventHandler(this.btnAddCar_Click);
            
            // 
            // btnAddPart
            // 
            this.btnAddPart.BackColor = System.Drawing.Color.Blue;
            this.btnAddPart.ForeColor = System.Drawing.Color.White;
            this.btnAddPart.Location = new System.Drawing.Point(160, 50);
            this.btnAddPart.Name = "btnAddPart";
            this.btnAddPart.Size = new System.Drawing.Size(120, 40);
            this.btnAddPart.TabIndex = 2;
            this.btnAddPart.Text = "Add New Part";
            this.btnAddPart.UseVisualStyleBackColor = false;
            this.btnAddPart.Click += new System.EventHandler(this.btnAddPart_Click);
            
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.BackColor = System.Drawing.Color.Orange;
            this.btnViewOrders.ForeColor = System.Drawing.Color.White;
            this.btnViewOrders.Location = new System.Drawing.Point(300, 50);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(120, 40);
            this.btnViewOrders.TabIndex = 3;
            this.btnViewOrders.Text = "View Orders";
            this.btnViewOrders.UseVisualStyleBackColor = false;
            this.btnViewOrders.Click += new System.EventHandler(this.btnViewOrders_Click);
            
            // 
            // btnViewCustomers
            // 
            this.btnViewCustomers.BackColor = System.Drawing.Color.Purple;
            this.btnViewCustomers.ForeColor = System.Drawing.Color.White;
            this.btnViewCustomers.Location = new System.Drawing.Point(20, 100);
            this.btnViewCustomers.Name = "btnViewCustomers";
            this.btnViewCustomers.Size = new System.Drawing.Size(120, 40);
            this.btnViewCustomers.TabIndex = 4;
            this.btnViewCustomers.Text = "View Customers";
            this.btnViewCustomers.UseVisualStyleBackColor = false;
            this.btnViewCustomers.Click += new System.EventHandler(this.btnViewCustomers_Click);
            
            // 
            // btnLowStock
            // 
            this.btnLowStock.BackColor = System.Drawing.Color.Red;
            this.btnLowStock.ForeColor = System.Drawing.Color.White;
            this.btnLowStock.Location = new System.Drawing.Point(160, 100);
            this.btnLowStock.Name = "btnLowStock";
            this.btnLowStock.Size = new System.Drawing.Size(120, 40);
            this.btnLowStock.TabIndex = 5;
            this.btnLowStock.Text = "Low Stock Alert";
            this.btnLowStock.UseVisualStyleBackColor = false;
            this.btnLowStock.Click += new System.EventHandler(this.btnLowStock_Click);
            
            // 
            // btnSalesReport
            // 
            this.btnSalesReport.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSalesReport.ForeColor = System.Drawing.Color.White;
            this.btnSalesReport.Location = new System.Drawing.Point(300, 100);
            this.btnSalesReport.Name = "btnSalesReport";
            this.btnSalesReport.Size = new System.Drawing.Size(120, 40);
            this.btnSalesReport.TabIndex = 6;
            this.btnSalesReport.Text = "Sales Report";
            this.btnSalesReport.UseVisualStyleBackColor = false;
            this.btnSalesReport.Click += new System.EventHandler(this.btnSalesReport_Click);
            
            // 
            // panelRecentActivity
            // 
            this.panelRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRecentActivity.Controls.Add(this.lblRecentActivityTitle);
            this.panelRecentActivity.Controls.Add(this.dgvRecentOrders);
            this.panelRecentActivity.Location = new System.Drawing.Point(700, 300);
            this.panelRecentActivity.Name = "panelRecentActivity";
            this.panelRecentActivity.Size = new System.Drawing.Size(660, 250);
            this.panelRecentActivity.TabIndex = 5;
            
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(130, 20);
            this.lblRecentActivityTitle.TabIndex = 0;
            this.lblRecentActivityTitle.Text = "Recent Orders";
            
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentOrders.Location = new System.Drawing.Point(15, 45);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            this.dgvRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentOrders.Size = new System.Drawing.Size(630, 180);
            this.dgvRecentOrders.TabIndex = 1;
            
            // 
            // panelAlerts
            // 
            this.panelAlerts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAlerts.Controls.Add(this.lblAlertsTitle);
            this.panelAlerts.Controls.Add(this.lstAlerts);
            this.panelAlerts.Location = new System.Drawing.Point(30, 480);
            this.panelAlerts.Name = "panelAlerts";
            this.panelAlerts.Size = new System.Drawing.Size(650, 160);
            this.panelAlerts.TabIndex = 6;
            
            // 
            // lblAlertsTitle
            // 
            this.lblAlertsTitle.AutoSize = true;
            this.lblAlertsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblAlertsTitle.Name = "lblAlertsTitle";
            this.lblAlertsTitle.Size = new System.Drawing.Size(140, 20);
            this.lblAlertsTitle.TabIndex = 0;
            this.lblAlertsTitle.Text = "System Alerts";
            
            // 
            // lstAlerts
            // 
            this.lstAlerts.FormattingEnabled = true;
            this.lstAlerts.Location = new System.Drawing.Point(15, 45);
            this.lstAlerts.Name = "lstAlerts";
            this.lstAlerts.Size = new System.Drawing.Size(620, 95);
            this.lstAlerts.TabIndex = 1;
            
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 679);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1400, 22);
            this.statusStrip1.TabIndex = 7;
            
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel1.Text = "ABC Car Traders Management System";
            
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1185, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = $"Connected as: {AuthenticationService.CurrentUser.Username} | Last Refresh: {DateTime.Now:HH:mm:ss}";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 701);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelAlerts);
            this.Controls.Add(this.panelRecentActivity);
            this.Controls.Add(this.panelQuickActions);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABC Car Traders - Admin Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.panelCarsStats.ResumeLayout(false);
            this.panelCarsStats.PerformLayout();
            this.panelPartsStats.ResumeLayout(false);
            this.panelPartsStats.PerformLayout();
            this.panelCustomersStats.ResumeLayout(false);
            this.panelCustomersStats.PerformLayout();
            this.panelOrdersStats.ResumeLayout(false);
            this.panelOrdersStats.PerformLayout();
            this.panelQuickActions.ResumeLayout(false);
            this.panelQuickActions.PerformLayout();
            this.panelRecentActivity.ResumeLayout(false);
            this.panelRecentActivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.panelAlerts.ResumeLayout(false);
            this.panelAlerts.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Designer Components
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem carsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carPartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managePartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowStockReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCustomersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pendingOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Panel panelCarsStats;
        private System.Windows.Forms.Label lblCarsTitle;
        private System.Windows.Forms.Label lblTotalCars;
        private System.Windows.Forms.Label lblAvailableCars;
        private System.Windows.Forms.Panel panelPartsStats;
        private System.Windows.Forms.Label lblPartsTitle;
        private System.Windows.Forms.Label lblTotalParts;
        private System.Windows.Forms.Label lblLowStockParts;
        private System.Windows.Forms.Panel panelCustomersStats;
        private System.Windows.Forms.Label lblCustomersTitle;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label lblActiveCustomers;
        private System.Windows.Forms.Panel panelOrdersStats;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblPendingOrders;
        private System.Windows.Forms.Label lblMonthlyRevenue;
        private System.Windows.Forms.Panel panelQuickActions;
        private System.Windows.Forms.Label lblQuickActionsTitle;
        private System.Windows.Forms.Button btnAddCar;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Button btnViewCustomers;
        private System.Windows.Forms.Button btnLowStock;
        private System.Windows.Forms.Button btnSalesReport;
        private System.Windows.Forms.Panel panelRecentActivity;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        private System.Windows.Forms.Panel panelAlerts;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.ListBox lstAlerts;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        #endregion

        #region Form Events
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }
        #endregion

        #region Menu Events
        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CarManagementForm().ShowDialog();
            LoadDashboardData(); // Refresh after returning
        }

        private void addCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new CarEditDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Car added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDashboardData();
                }
            }
        }

        private void managePartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CarPartManagementForm().ShowDialog();
            LoadDashboardData();
        }

        private void addPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new CarPartEditDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Car part added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDashboardData();
                }
            }
        }

        private void lowStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLowStockReport();
        }

        private void viewCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CustomerManagementForm().ShowDialog();
            LoadDashboardData();
        }

        private void customerReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCustomerReport();
        }

        private void viewOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OrderManagementForm().ShowDialog();
            LoadDashboardData();
        }

        private void pendingOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPendingOrders();
        }

        private void orderReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOrderReport();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSalesReport();
        }

        private void inventoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInventoryReport();
        }

        private void customerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCustomerReport();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSystemSettings();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
        }
        #endregion

        #region Quick Action Events
        private void btnAddCar_Click(object sender, EventArgs e)
        {
            addCarToolStripMenuItem_Click(sender, e);
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            addPartToolStripMenuItem_Click(sender, e);
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            viewOrdersToolStripMenuItem_Click(sender, e);
        }

        private void btnViewCustomers_Click(object sender, EventArgs e)
        {
            viewCustomersToolStripMenuItem_Click(sender, e);
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            ShowLowStockReport();
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            ShowSalesReport();
        }
        #endregion

        #region Data Loading Methods
        private void SetupRefreshTimer()
        {
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 30000; // 30 seconds
            refreshTimer.Tick += (s, e) => {
                LoadDashboardData();
                toolStripStatusLabel2.Text = $"Connected as: {AuthenticationService.CurrentUser.Username} | Last Refresh: {DateTime.Now:HH:mm:ss}";
            };
            refreshTimer.Start();
        }

        private void LoadDashboardData()
        {
            try
            {
                LoadStatistics();
                LoadRecentOrders();
                LoadSystemAlerts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                // Load car statistics
                var cars = carRepository.GetAllCars();
                lblTotalCars.Text = cars.Count.ToString();
                lblAvailableCars.Text = $"Available: {cars.Count(c => c.IsAvailable)}";

                // Load parts statistics
                var parts = carPartRepository.GetAllCarParts();
                lblTotalParts.Text = parts.Count.ToString();
                int lowStockCount = parts.Count(p => p.StockQuantity <= 5);
                lblLowStockParts.Text = $"Low Stock: {lowStockCount}";

                // Load customer statistics
                var customers = userRepository.GetAllCustomers();
                lblTotalCustomers.Text = customers.Count.ToString();
                lblActiveCustomers.Text = $"Active: {customers.Count(c => c.IsActive)}";

                // Load order statistics
                var orders = orderRepository.GetAllOrders();
                lblTotalOrders.Text = orders.Count.ToString();
                lblPendingOrders.Text = $"Pending: {orders.Count(o => o.Status == OrderStatus.Pending)}";
                
                // Calculate monthly revenue
                var thisMonth = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
                var nextMonth = thisMonth.AddMonths(1);
                var monthlyRevenue = orderRepository.GetRevenueByDateRange(thisMonth, nextMonth);
                lblMonthlyRevenue.Text = $"This Month: {monthlyRevenue:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentOrders()
        {
            try
            {
                var recentOrders = orderRepository.GetRecentOrders(10);
                var orderData = recentOrders.Select(o => new
                {
                    o.OrderID,
                    Customer = o.CustomerName,
                    Date = o.OrderDate.ToString("MM/dd HH:mm"),
                    Amount = o.TotalAmount.ToString("C"),
                    o.Status
                }).ToList();

                dgvRecentOrders.DataSource = orderData;

                // Hide OrderID column
                if (dgvRecentOrders.Columns["OrderID"] != null)
                {
                    dgvRecentOrders.Columns["OrderID"].Visible = false;
                }

                // Apply status color coding
                foreach (DataGridViewRow row in dgvRecentOrders.Rows)
                {
                    if (row.Cells["Status"].Value != null)
                    {
                        string status = row.Cells["Status"].Value.ToString();
                        switch (status)
                        {
                            case "Pending":
                                row.DefaultCellStyle.BackColor = Color.LightYellow;
                                break;
                            case "Processing":
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                break;
                            case "Completed":
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                                break;
                            case "Cancelled":
                                row.DefaultCellStyle.BackColor = Color.LightCoral;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent orders: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSystemAlerts()
        {
            try
            {
                lstAlerts.Items.Clear();

                // Check for low stock items
                var lowStockParts = carPartRepository.GetLowStockParts(5);
                if (lowStockParts.Any())
                {
                    lstAlerts.Items.Add($"⚠️ {lowStockParts.Count} parts are low in stock!");
                }

                // Check for out of stock parts
                var outOfStockParts = carPartRepository.GetOutOfStockParts();
                if (outOfStockParts.Any())
                {
                    lstAlerts.Items.Add($"❌ {outOfStockParts.Count} parts are out of stock!");
                }

                // Check for pending orders
                var pendingOrders = orderRepository.GetPendingOrders();
                if (pendingOrders.Count > 10)
                {
                    lstAlerts.Items.Add($"📋 {pendingOrders.Count} pending orders need attention!");
                }

                // System status
                lstAlerts.Items.Add($"✅ System is running normally - {DateTime.Now:HH:mm:ss}");

                if (lstAlerts.Items.Count == 1) // Only system status
                {
                    lstAlerts.Items.Add("✅ No alerts at this time");
                }
            }
            catch (Exception ex)
            {
                lstAlerts.Items.Clear();
                lstAlerts.Items.Add($"❌ Error loading alerts: {ex.Message}");
            }
        }
        #endregion

        #region Report Methods
        private void ShowLowStockReport()
        {
            try
            {
                var lowStockParts = carPartRepository.GetLowStockParts(5);
                
                if (!lowStockParts.Any())
                {
                    MessageBox.Show("No parts are currently low in stock.", "Low Stock Report", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string report = "LOW STOCK REPORT\n";
                report += "================\n\n";
                
                foreach (var part in lowStockParts.OrderBy(p => p.StockQuantity))
                {
                    report += $"• {part.PartName} ({part.Brand}) - Stock: {part.StockQuantity}\n";
                }

                MessageBox.Show(report, "Low Stock Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating low stock report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPendingOrders()
        {
            try
            {
                var pendingOrders = orderRepository.GetPendingOrders();
                
                if (!pendingOrders.Any())
                {
                    MessageBox.Show("No pending orders found.", "Pending Orders", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string report = "PENDING ORDERS REPORT\n";
                report += "====================\n\n";
                report += $"Total Pending Orders: {pendingOrders.Count}\n";
                report += $"Total Value: {pendingOrders.Sum(o => o.TotalAmount):C}\n\n";
                
                foreach (var order in pendingOrders.Take(10))
                {
                    report += $"• Order #{order.OrderID} - {order.CustomerName} - {order.TotalAmount:C}\n";
                }

                if (pendingOrders.Count > 10)
                {
                    report += $"\n... and {pendingOrders.Count - 10} more orders";
                }

                MessageBox.Show(report, "Pending Orders Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating pending orders report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowSalesReport()
        {
            try
            {
                var thisMonth = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
                var nextMonth = thisMonth.AddMonths(1);
                var lastMonth = thisMonth.AddMonths(-1);
                
                var thisMonthRevenue = orderRepository.GetRevenueByDateRange(thisMonth, nextMonth);
                var lastMonthRevenue = orderRepository.GetRevenueByDateRange(lastMonth, thisMonth);
                var totalRevenue = orderRepository.GetTotalRevenue();
                
                var thisMonthOrders = orderRepository.GetOrdersByDateRange(thisMonth, nextMonth);
                var completedOrders = thisMonthOrders.Count(o => o.Status == OrderStatus.Completed);

                string report = "SALES REPORT\n";
                report += "============\n\n";
                report += $"This Month Revenue: {thisMonthRevenue:C}\n";
                report += $"Last Month Revenue: {lastMonthRevenue:C}\n";
                report += $"Total Revenue: {totalRevenue:C}\n\n";
                report += $"This Month Orders: {thisMonthOrders.Count}\n";
                report += $"Completed Orders: {completedOrders}\n";
                
                if (lastMonthRevenue > 0)
                {
                    var growthPercent = ((thisMonthRevenue - lastMonthRevenue) / lastMonthRevenue) * 100;
                    report += $"Month-over-Month Growth: {growthPercent:F1}%\n";
                }

                MessageBox.Show(report, "Sales Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating sales report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowInventoryReport()
        {
            try
            {
                var cars = carRepository.GetAllCars();
                var parts = carPartRepository.GetAllCarParts();
                var totalInventoryValue = parts.Sum(p => p.Price * p.StockQuantity);

                string report = "INVENTORY REPORT\n";
                report += "================\n\n";
                report += $"Total Cars: {cars.Count}\n";
                report += $"Available Cars: {cars.Count(c => c.IsAvailable)}\n";
                report += $"Total Car Parts: {parts.Count}\n";
                report += $"Total Parts Value: {totalInventoryValue:C}\n";
                report += $"Low Stock Parts: {parts.Count(p => p.StockQuantity <= 5)}\n";
                report += $"Out of Stock Parts: {parts.Count(p => p.StockQuantity == 0)}\n";

                MessageBox.Show(report, "Inventory Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating inventory report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCustomerReport()
        {
            try
            {
                var customers = userRepository.GetAllCustomers();
                var orders = orderRepository.GetAllOrders();
                var avgOrderValue = orderRepository.GetAverageOrderValue();

                string report = "CUSTOMER REPORT\n";
                report += "===============\n\n";
                report += $"Total Customers: {customers.Count}\n";
                report += $"Active Customers: {customers.Count(c => c.IsActive)}\n";
                report += $"Total Orders: {orders.Count}\n";
                report += $"Average Order Value: {avgOrderValue:C}\n";
                
                var thisMonth = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
                var newCustomersThisMonth = customers.Count(c => c.CreatedDate >= thisMonth);
                report += $"New Customers This Month: {newCustomersThisMonth}\n";

                MessageBox.Show(report, "Customer Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating customer report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOrderReport()
        {
            try
            {
                var orders = orderRepository.GetAllOrders();
                var statusCounts = orderRepository.GetOrderStatusCounts();
                var avgOrderValue = orderRepository.GetAverageOrderValue();

                string report = "ORDER REPORT\n";
                report += "============\n\n";
                report += $"Total Orders: {orders.Count}\n";
                report += $"Average Order Value: {avgOrderValue:C}\n\n";

                report += "Orders by Status:\n";
                foreach (var status in statusCounts)
                {
                    report += $"• {status.Key}: {status.Value}\n";
                }

                MessageBox.Show(report, "Order Report", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating order report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowSystemSettings()
        {
            MessageBox.Show("System Settings functionality would be implemented here.\n\nThis could include:\n• Database settings\n• User preferences\n• System configuration\n• Backup settings", 
                "System Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region System Methods
        private void Logout()
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                refreshTimer?.Stop();
                AuthenticationService.Logout();
                this.Hide();
                new LoginForm().Show();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            base.OnFormClosing(e);
        }
        #endregion
    }
}