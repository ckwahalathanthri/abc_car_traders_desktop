using ABCCarTraders.Business;
using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ABCCarTraders.Forms
{
    public partial class CustomerDashboard : Form
    {
        #region Private Fields
        private OrderRepository orderRepository;
        private Panel panelStats;
        private Panel panelRecentOrders;
        private DataGridView dgvRecentOrders;
        private Label lblOrderCount, lblTotalSpent, lblLastOrder;
        #endregion

        #region Constructor and Initialization
        public CustomerDashboard()
        {
            try
            {
                InitializeRepositories();
                InitializeComponent();
                this.FormClosed += (s, e) => Application.Exit();

                // Load data after form is fully initialized
                this.Load += CustomerDashboard_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing customer dashboard: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeRepositories()
        {
            try
            {
                orderRepository = new OrderRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Continue without repository - will show error messages instead of data
            }
        }

        private void InitializeComponent()
        {
            // Basic form setup
            this.Size = new Size(1000, 700);
            this.Text = GetFormTitle();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            CreateMenuStrip();
            CreateWelcomeSection();
            CreateStatsPanel();
            CreateRecentOrdersPanel();
        }

        private string GetFormTitle()
        {
            try
            {
                if (AuthenticationService.CurrentUser != null)
                {
                    return $"ABC Car Traders - Customer Dashboard - Welcome {AuthenticationService.CurrentUser.FullName}";
                }
                return "ABC Car Traders - Customer Dashboard";
            }
            catch
            {
                return "ABC Car Traders - Customer Dashboard";
            }
        }

        private void CreateMenuStrip()
        {
            MenuStrip menuStrip = new MenuStrip();

            // Search Menu
            ToolStripMenuItem searchMenu = new ToolStripMenuItem("Search & Browse");
            searchMenu.DropDownItems.Add("Search Cars", null, (s, e) => OpenSearchForm());
            searchMenu.DropDownItems.Add("Search Car Parts", null, (s, e) => OpenSearchForm());
            searchMenu.DropDownItems.Add("Browse All Items", null, (s, e) => OpenSearchForm());

            // Orders Menu
            ToolStripMenuItem ordersMenu = new ToolStripMenuItem("My Orders");
            ordersMenu.DropDownItems.Add("View All Orders", null, (s, e) => OpenOrderManagement());
            ordersMenu.DropDownItems.Add("Order History", null, (s, e) => OpenOrderManagement());

            // Account Menu
            ToolStripMenuItem accountMenu = new ToolStripMenuItem("My Account");
            accountMenu.DropDownItems.Add("View Profile", null, (s, e) => ShowProfile());
            accountMenu.DropDownItems.Add("Change Password", null, (s, e) => ShowChangePassword());

            // System Menu
            ToolStripMenuItem systemMenu = new ToolStripMenuItem("System");
            systemMenu.DropDownItems.Add("Refresh", null, (s, e) => RefreshDashboard());
            systemMenu.DropDownItems.Add("Help", null, (s, e) => ShowHelp());
            systemMenu.DropDownItems.Add("Logout", null, (s, e) => Logout());

            menuStrip.Items.AddRange(new ToolStripItem[] { searchMenu, ordersMenu, accountMenu, systemMenu });

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void CreateWelcomeSection()
        {
            Label lblWelcome = new Label();
            lblWelcome.Text = GetWelcomeText();
            lblWelcome.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.DarkBlue;
            lblWelcome.Location = new Point(30, 50);
            lblWelcome.Size = new Size(600, 30);

            Label lblInfo = new Label();
            lblInfo.Text = "Welcome to ABC Car Traders! Use the menu above to search for cars and parts, manage your orders, and update your account.";
            lblInfo.Location = new Point(30, 90);
            lblInfo.Size = new Size(700, 40);
            lblInfo.Font = new Font("Microsoft Sans Serif", 10F);

            this.Controls.AddRange(new Control[] { lblWelcome, lblInfo });
        }

        private string GetWelcomeText()
        {
            try
            {
                if (AuthenticationService.CurrentUser != null)
                {
                    return $"Welcome back, {AuthenticationService.CurrentUser.FullName}!";
                }
                return "Welcome to ABC Car Traders!";
            }
            catch
            {
                return "Welcome to ABC Car Traders!";
            }
        }

        private void CreateStatsPanel()
        {
            panelStats = new Panel();
            panelStats.Location = new Point(30, 150);
            panelStats.Size = new Size(450, 120);
            panelStats.BackColor = Color.LightBlue;
            panelStats.BorderStyle = BorderStyle.FixedSingle;

            Label lblStatsTitle = new Label();
            lblStatsTitle.Text = "My Order Statistics";
            lblStatsTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblStatsTitle.Location = new Point(10, 10);
            lblStatsTitle.Size = new Size(200, 20);

            lblOrderCount = new Label();
            lblOrderCount.Text = "Loading...";
            lblOrderCount.Location = new Point(10, 40);
            lblOrderCount.Size = new Size(200, 20);

            lblTotalSpent = new Label();
            lblTotalSpent.Text = "Loading...";
            lblTotalSpent.Location = new Point(10, 65);
            lblTotalSpent.Size = new Size(200, 20);

            lblLastOrder = new Label();
            lblLastOrder.Text = "Loading...";
            lblLastOrder.Location = new Point(10, 90);
            lblLastOrder.Size = new Size(400, 20);

            panelStats.Controls.AddRange(new Control[] { lblStatsTitle, lblOrderCount, lblTotalSpent, lblLastOrder });
            this.Controls.Add(panelStats);
        }

        private void CreateRecentOrdersPanel()
        {
            panelRecentOrders = new Panel();
            panelRecentOrders.Location = new Point(500, 150);
            panelRecentOrders.Size = new Size(450, 300);
            panelRecentOrders.BackColor = Color.LightGreen;
            panelRecentOrders.BorderStyle = BorderStyle.FixedSingle;

            Label lblOrdersTitle = new Label();
            lblOrdersTitle.Text = "Recent Orders";
            lblOrdersTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblOrdersTitle.Location = new Point(10, 10);
            lblOrdersTitle.Size = new Size(200, 20);

            dgvRecentOrders = new DataGridView();
            dgvRecentOrders.Location = new Point(10, 40);
            dgvRecentOrders.Size = new Size(430, 220);
            dgvRecentOrders.BackgroundColor = Color.White;
            dgvRecentOrders.AllowUserToAddRows = false;
            dgvRecentOrders.AllowUserToDeleteRows = false;
            dgvRecentOrders.ReadOnly = true;
            dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Button btnViewAllOrders = new Button();
            btnViewAllOrders.Text = "View All Orders";
            btnViewAllOrders.Location = new Point(350, 270);
            btnViewAllOrders.Size = new Size(90, 25);
            btnViewAllOrders.Click += (s, e) => OpenOrderManagement();

            panelRecentOrders.Controls.AddRange(new Control[] { lblOrdersTitle, dgvRecentOrders, btnViewAllOrders });
            this.Controls.Add(panelRecentOrders);
        }
        #endregion

        #region Form Events
        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Loading Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Data Loading Methods
        private void LoadDashboardData()
        {
            LoadStatistics();
            LoadRecentOrders();
        }

        private void LoadStatistics()
        {
            try
            {
                if (orderRepository == null || AuthenticationService.CurrentUser == null)
                {
                    ShowErrorStats();
                    return;
                }

                var orders = orderRepository.GetOrdersByCustomer(AuthenticationService.CurrentUser.UserID);

                if (orders == null)
                {
                    orders = new List<Order>();
                }

                int totalOrders = orders.Count;
                decimal totalSpent = orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalAmount);
                var lastOrder = orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();

                lblOrderCount.Text = $"Total Orders: {totalOrders}";
                lblTotalSpent.Text = $"Total Spent: {totalSpent:C}";

                if (lastOrder != null)
                {
                    lblLastOrder.Text = $"Last Order: {lastOrder.OrderDate:MM/dd/yyyy} - {lastOrder.TotalAmount:C}";
                }
                else
                {
                    lblLastOrder.Text = "Last Order: No orders yet";
                }
            }
            catch (Exception ex)
            {
                ShowErrorStats();
                Console.WriteLine($"Statistics error: {ex.Message}");
            }
        }

        private void ShowErrorStats()
        {
            lblOrderCount.Text = "Unable to load order count";
            lblTotalSpent.Text = "Unable to load spending data";
            lblLastOrder.Text = "Unable to load last order info";
        }

        private void LoadRecentOrders()
        {
            try
            {
                if (orderRepository == null || AuthenticationService.CurrentUser == null)
                {
                    ShowEmptyOrders();
                    return;
                }

                var orders = orderRepository.GetOrdersByCustomer(AuthenticationService.CurrentUser.UserID);

                if (orders == null || orders.Count == 0)
                {
                    ShowEmptyOrders();
                    return;
                }

                var recentOrders = orders.OrderByDescending(o => o.OrderDate).Take(5).ToList();

                var orderData = recentOrders.Select(o => new
                {
                    OrderID = o.OrderID,
                    Date = o.OrderDate.ToString("MM/dd/yyyy"),
                    Amount = o.TotalAmount.ToString("C"),
                    Status = o.Status.ToString()
                }).ToList();

                dgvRecentOrders.DataSource = orderData;

                // Style columns if they exist
                if (dgvRecentOrders.Columns.Count > 0)
                {
                    if (dgvRecentOrders.Columns["OrderID"] != null)
                        dgvRecentOrders.Columns["OrderID"].HeaderText = "Order #";
                    if (dgvRecentOrders.Columns["Date"] != null)
                        dgvRecentOrders.Columns["Date"].HeaderText = "Date";
                    if (dgvRecentOrders.Columns["Amount"] != null)
                        dgvRecentOrders.Columns["Amount"].HeaderText = "Amount";
                    if (dgvRecentOrders.Columns["Status"] != null)
                        dgvRecentOrders.Columns["Status"].HeaderText = "Status";
                }
            }
            catch (Exception ex)
            {
                ShowEmptyOrders();
                Console.WriteLine($"Recent orders error: {ex.Message}");
            }
        }

        private void ShowEmptyOrders()
        {
            var emptyData = new List<object>
            {
                new { OrderID = "No orders", Date = "", Amount = "", Status = "" }
            };
            dgvRecentOrders.DataSource = emptyData;
        }
        #endregion

        #region Menu Event Handlers
        private void OpenSearchForm()
        {
            try
            {
                using (var searchForm = new SearchForm())
                {
                    searchForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening search form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenOrderManagement()
        {
            try
            {
                using (var orderForm = new OrderManagementForm())
                {
                    orderForm.ShowDialog();
                }
                // Refresh data after order management
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening order management: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowProfile()
        {
            try
            {
                if (AuthenticationService.CurrentUser != null)
                {
                    var user = AuthenticationService.CurrentUser;
                    string profileInfo = $"Profile Information\n\n";
                    profileInfo += $"Username: {user.Username}\n";
                    profileInfo += $"Full Name: {user.FullName}\n";
                    profileInfo += $"Email: {user.Email}\n";
                    profileInfo += $"Phone: {user.Phone ?? "Not provided"}\n";
                    profileInfo += $"Address: {user.Address ?? "Not provided"}\n";
                    profileInfo += $"Member Since: {user.CreatedDate:MMMM yyyy}";

                    MessageBox.Show(profileInfo, "My Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowChangePassword()
        {
            MessageBox.Show("Change password feature coming soon!\n\nPlease contact the administrator to change your password.",
                "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshDashboard()
        {
            try
            {
                LoadDashboardData();
                MessageBox.Show("Dashboard refreshed successfully!", "Refresh",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowHelp()
        {
            string helpText = "ABC Car Traders - Customer Help\n\n";
            helpText += "• Search & Browse: Find cars and car parts\n";
            helpText += "• My Orders: View and manage your orders\n";
            helpText += "• My Account: View your profile information\n";
            helpText += "• System: Refresh data, get help, or logout\n\n";
            helpText += "For technical support:\n";
            helpText += "Phone: +94-11-2345678\n";
            helpText += "Email: support@abccartraders.lk";

            MessageBox.Show(helpText, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Logout()
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AuthenticationService.Logout();
                    this.Hide();
                    new LoginForm().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during logout: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}