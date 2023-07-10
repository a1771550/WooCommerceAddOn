using MYOBLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WALib.Helpers;
using WALib.Models;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET;
using WADAL;
using CommonLib.Helpers;
using ModelHelper = WALib.Helpers.ModelHelper;
using MYOBLib.Models;
using WALib.Models.MYOB;
using WaitWnd;
using MyobCustomerModel = WALib.Models.MYOB.MyobCustomerModel;
using System.IO;

namespace WooCommerceAddOn
{
    public partial class frmList : Form
    {
        public Dictionary<string, string> FouledOrderIdList;
        public Dictionary<int, string> FouledCustomerIdList { get; set; }
        private int MaxItemCodeLength { get { return int.Parse(ConfigurationManager.AppSettings["ItemCodeLength"]); } }
        public Dictionary<string, string> FouledProductNameList = new Dictionary<string, string>();
        private ComInfoModel comInfo { get; set; }
        //private string DeviceId { get; set; }
        private List<int> CheckOutIds { get; set; }
        private int CustomerCodeLength { get { return int.Parse(ConfigurationManager.AppSettings["CustomerCodeLength"]); } }
        private int AccountProfileId { get { return int.Parse(ConfigurationManager.AppSettings["AccountProfileId"]); } }

        private DataType type { get; set; }
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageLength"]);
        private int PageBatchSize = int.Parse(ConfigurationManager.AppSettings["PageBatchSize"]);
        private int CurrentPageIndex = 1;
        private int TotalPage = 0;
        public int ExpectedTotal = 0;
        public int ActualTotal = 0;
        //private int Page = 1;
        private List<MyobCustomerModel> AbssCustomers { get; set; }
        private List<MyobItemModel> AbssProducts { get; set; }
        private List<Customer> Customers { get; set; }
        private List<Order> Orders { get; set; }
        private List<Product> Products { get; set; }
        private ABSSModel abss { get; set; }
        private RestAPI rest { get; set; }

        WaitWndFun waitForm = new WaitWndFun();
        public frmList(ComInfoModel comInfo)
        {
            InitializeComponent();
            this.comInfo = comInfo;
            type = comInfo.dataType;
            abss = new ABSSModel
            {
                Driver = comInfo.abssDriver,
                FileLocation = comInfo.abssFileLocation,
                FileName = comInfo.abssFileName,
                UserId = comInfo.abssUserId,
                UserPass = comInfo.abssUserPass,
                AppExeLocation = comInfo.abssExeLocation,
                AppExeName = comInfo.abssExeName,
                KeyLocation = comInfo.abssKeyLocation,
                KeyName = comInfo.abssKeyName
            };

            var url = string.Format("{0}/wp-json/wc/v3/", comInfo.wcURL);
            rest = new RestAPI(url, comInfo.wcConsumerKey, comInfo.wcConsumerSecret);

            if (type.ToString().StartsWith("Abss"))
            {
                btnABSS.Enabled = false;
            }
            else
            {
                btnWooCommerce.Enabled = false;
            }

            switch (type)
            {
                case DataType.AbssCustomer:
                    AbssCustomers = new List<MyobCustomerModel>();                    
                    //url = string.Format("{0}/wp-json/wc/v3/customers/batch", comInfo.wcURL);
                    //rest = new RestAPI(url, comInfo.wcConsumerKey, comInfo.wcConsumerSecret);
                    this.Text = "ABSS Customer List";
                    break;
                case DataType.AbssProduct:
                    //url = string.Format("{0}/wp-json/wc/v3/products/batch", comInfo.wcURL);
                    //rest = new RestAPI(url, comInfo.wcConsumerKey, comInfo.wcConsumerSecret);
                    AbssProducts = new List<MyobItemModel>();
                    this.Text = "ABSS Product List";
                    break;
                case DataType.Product:
                    Products = new List<Product>();
                    this.Text = "WooCommerce Product List";
                    break;
                case DataType.Customer:
                    Customers = new List<Customer>();
                    this.Text = "WooCommerce Customer List";
                    break;
                default:
                case DataType.Order:
                    Orders = new List<Order>();
                    this.Text = "WooCommerce Order List";
                    break;
            }

        }

        private async void frmList_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            decimal totalpages = 0;
            btnABSS.Enabled = ConfigurationManager.AppSettings["EnableAbssUpload"] == "1";
            switch (type)
            {
                case DataType.AbssCustomer:
                    HashSet<int> CurrentCardRecordIds = MyobCustomerEditModel.GetCurrentCardRecordIds(comInfo.AccountProfileId);
                    AbssCustomers = MYOBHelper.GetCustomerList(abss, string.Join(",",CurrentCardRecordIds));
                    progressBar1.Visible = false;
                    if (AbssCustomers.Count == 0)
                    {
                        MessageBox.Show("All Customers in ABSS are already uploaded to WooCommerce already. No new customers data are found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        //AbssCustomers = MyobCustomerEditModel.GetCustomerList(comInfo.AccountProfileId);
                        //btnWooCommerce.Enabled = false;
                        //frmMain frmMain = new frmMain(false);
                        //frmMain.ShowDialog();
                    }
                    else
                    {
                        totalpages = AbssCustomers.Count / PageSize;
                        TotalPage = (int)Math.Ceiling(totalpages);
                        var mcustomers = AbssCustomers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                        BindingList<MyobCustomerModel> bindingList_abssC = new BindingList<MyobCustomerModel>(mcustomers);
                        source = new BindingSource(bindingList_abssC, null);
                        iTotal.Text = AbssCustomers.Count.ToString();
                    }
                    
                    break;
                case DataType.AbssProduct:
                    HashSet<int> CurrentItemIds = MyobItemEditModel.GetCurrentItemIds(comInfo.AccountProfileId);                    
                    AbssProducts = MYOBHelper.GetItemList(abss, string.Join(",",CurrentItemIds));
                    progressBar1.Visible = false;
                    if (AbssProducts.Count == 0)
                    {
                        MessageBox.Show("All Items in ABSS are already uploaded to WooCommerce already. No new items data are found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        //AbssProducts = MyobItemEditModel.GetItemList(comInfo.AccountProfileId);
                        //btnWooCommerce.Enabled = false;
                        //frmMain frmMain = new frmMain(false);
                        //frmMain.ShowDialog();
                    }
                    else
                    {
                        totalpages = AbssProducts.Count / PageSize;
                        TotalPage = (int)Math.Ceiling(totalpages);
                        var mproducts = AbssProducts.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                        BindingList<MyobItemModel> bindingList_abssP = new BindingList<MyobItemModel>(mproducts);
                        source = new BindingSource(bindingList_abssP, null);
                        iTotal.Text = AbssProducts.Count.ToString();
                    }
                    
                    break;

                case DataType.Product:
                    for (int i = 1; i <= PageBatchSize; i++)
                    {
                        var _products = await ModelHelper.GetWooProduct(rest, i);
                        if (_products != null && _products.Count > 0)
                            Products.AddRange(_products);
                    }
                    progressBar1.Visible = false;
                    totalpages = Products.Count / PageSize;
                    TotalPage = (int)Math.Ceiling(totalpages);
                    Products = Products.OrderByDescending(x => x.date_created).ToList();

                    var products = Products.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();

                    BindingList<Product> bindingList_P = new BindingList<Product>(products);
                    source = new BindingSource(bindingList_P, null);
                    iTotal.Text = Products.Count.ToString();
                    break;
                case DataType.Customer:
                    for (int i = 1; i <= PageBatchSize; i++)
                    {
                        var _customers = await ModelHelper.GetWooCustomer(rest, CustomerRole.all, i);
                        if (_customers != null && _customers.Count > 0)
                            Customers.AddRange(_customers);
                    }
                    progressBar1.Visible = false;
                    totalpages = Customers.Count / PageSize;
                    TotalPage = (int)Math.Ceiling(totalpages);
                    Customers = Customers.OrderByDescending(x => x.date_created).ToList();

                    var customers = Customers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    progressBar1.Visible = false;

                    BindingList<Customer> bindingList_C = new BindingList<Customer>(customers);
                    source = new BindingSource(bindingList_C, null);
                    iTotal.Text = Customers.Count.ToString();
                    break;
                default:
                case DataType.Order:
                    for (int i = 1; i <= PageBatchSize; i++)
                    {
                        var _orders = await ModelHelper.GetWooOrder(rest, i);
                        if (_orders != null && _orders.Count > 0)
                            Orders.AddRange(_orders);
                    }
                    progressBar1.Visible = false;
                    totalpages = Orders.Count / PageSize;
                    TotalPage = (int)Math.Ceiling(totalpages);
                    Orders = Orders.OrderByDescending(x => x.date_created).ToList();

                    var orders = Orders.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    BindingList<Order> bindingList_O = new BindingList<Order>(orders);
                    source = new BindingSource(bindingList_O, null);
                    iTotal.Text = Orders.Count.ToString();
                    break;

            }
            dgList.DataSource = source;
            this.dgList.ReadOnly = true;
            if (type == DataType.AbssCustomer)
            {
                //dgList.Columns[0].Visible = false;               
                dgList.Columns["CustomerID"].DisplayIndex = 0;
                dgList.Columns["CardIdentification"].DisplayIndex = 1;
                dgList.Columns["Name"].DisplayIndex = 2;
                dgList.Columns["FirstName"].DisplayIndex = 3;
                dgList.Columns["Notes"].DisplayIndex = 4;
                dgList.Columns["CustomField1"].DisplayIndex = 5;
                dgList.Columns["CustomField2"].DisplayIndex = 6;
                dgList.Columns["CustomField3"].DisplayIndex = 7;
                dgList.Columns["CurrentBalance"].DisplayIndex = 8;
                dgList.Columns["TotalDeposits"].DisplayIndex = 9;
                dgList.Columns["CreditLimit"].DisplayIndex = 10;
            }
        }

        private async void btnSaveDB_Click(object sender, EventArgs e)
        {
            bool bok = false;
            switch (type)
            {
                case DataType.AbssCustomer:
                    #region remove current data first:
                    //var cusIds = AbssCustomers.Select(x => x.CustomerID).Distinct().ToHashSet();
                    //MyobCustomerEditModel.RemoveByCusIds(comInfo.AccountProfileId, cusIds);
                    MyobCustomerEditModel.RemoveAll(comInfo.AccountProfileId);
                    #endregion

                    #region add data:
                    bok = await MyobCustomerEditModel.AddList(AbssCustomers, comInfo.AccountProfileId);
                    if (bok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show(string.Format("{0} Saved.", DataType.Customer.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                    break;
                case DataType.AbssProduct:
                    #region remove current data first:
                    //var itemIds = AbssProducts.Select(x=>x.ItemID).Distinct().ToHashSet();
                    //MyobItemEditModel.RemoveByItemIds(comInfo.AccountProfileId, itemIds);
                    MyobItemEditModel.RemoveAll(comInfo.AccountProfileId);
                    #endregion

                    #region add data:
                    bok = await MyobItemEditModel.AddList(AbssProducts, comInfo.AccountProfileId);
                    if (bok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show(string.Format("{0} Saved.", DataType.Product.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                    break;
                case DataType.Product:

                    if (Products.Count == 0)
                    {
                        MessageBox.Show("No Data Found!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        FouledProductNameList = new Dictionary<string, string>();
                        progressBar1.Visible = true;
                        progressBar1.Style = ProgressBarStyle.Marquee;

                        List<ItemModel> itemlist = new List<ItemModel>();
                        List<ItemModel> currentwooitems = new List<ItemModel>();

                        #region remove current data first:
                        ItemEditModel.RemoveAll();
                        #endregion

                        #region add data:
                        foreach (var i in Products)
                        {
                            ItemModel item = ItemEditModel.PopulateItem(i, ref FouledProductNameList, MaxItemCodeLength, comInfo);
                            if (item != null)
                            {
                                itemlist.Add(item);
                            }
                        }
                        if (itemlist.Count > 0)
                        {
                            using var context = new WADbContext();
                            ItemEditModel.AddList(itemlist, comInfo.AccountProfileId, context);
                            progressBar1.Visible = false;
                        }
                        #endregion

                        if (FouledProductNameList.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in FouledProductNameList)
                            {
                                sb.AppendFormat("Name:{0} Reasons:{1}{2}", item.Key, item.Value, Environment.NewLine);
                            }
                            var msg = string.Format("AddOn did not save the products with reasons as follows:{0}{1}", Environment.NewLine, sb.ToString());
                            msg += String.Format("{0} while other products are saved.", Environment.NewLine);
                            MessageBox.Show(msg, "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0} Saved.", DataType.Product.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    break;
                case DataType.Customer:

                    if (Customers.Count == 0)
                    {
                        MessageBox.Show("No Data Found!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        progressBar1.Visible = true;
                        progressBar1.Style = ProgressBarStyle.Marquee;

                        List<CustomerModel> customerlist = new List<CustomerModel>();
                        List<CustomerModel> currentwoocustomers = new List<CustomerModel>();

                        #region remove current data first:
                        CustomerEditModel.RemoveAll();
                        #endregion

                        #region add data:
                        foreach (var c in Customers)
                        {
                            string cuscode = CommonHelper.GenerateNonce(CustomerCodeLength);
                            CustomerModel customer = new CustomerModel
                            {
                                cusCustomerID = (int)c.id,
                                cusCode = cuscode,
                                cusCreateTime = c.date_created_gmt,
                                cusModifyTime = c.date_modified_gmt,
                                cusEmail = c.email,
                                cusFirstName = c.first_name,
                                cusLastName = c.last_name,
                                cusName = c.username,
                                cusPointsUsed = 0,
                                cusRole = c.role,
                                cusPhone = c.billing.phone,
                                cusContact = c.username
                            };
                            //customer.cusPointsSoFar = await GetCustomerPointFrmOrders((long)c.id);
                            customerlist.Add(customer);
                        }
                        if (customerlist.Count > 0)
                        {
                            using (var context = new WADbContext())
                            {
                                using (var transaction = context.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        await CustomerEditModel.AddList(customerlist, context);
                                        transaction.Commit();
                                        progressBar1.Visible = false;
                                        MessageBox.Show(string.Format("{0} Saved.", DataType.Customer.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        throw new Exception(ex.Message);
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    break;
                default:
                case DataType.Order:

                    if (ModelHelper.CheckIfHasWooCustomersItems())
                    {
                        if (Orders.Count == 0)
                        {
                            MessageBox.Show("No Data Found!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            ABSSModel abss = new ABSSModel();
                            var location = comInfo.abssPrimaryLocation;
                            abss.Location = new LocationModel
                            {
                                LocationCode = location,
                            };
                            abss.accountProfileId = AccountProfileId;

                            progressBar1.Visible = true;
                            progressBar1.Style = ProgressBarStyle.Marquee;
                            var currentSalesIds = SalesEditModel.GetWooSalesIdList(abss);
                            List<SalesModel> saleslist = new List<SalesModel>();
                            List<SalesLnView> salelnslist = new List<SalesLnView>();
                            List<SalesLnView> currentwoosaless = new List<SalesLnView>();
                            List<Order> neworders = new List<Order>();

                            foreach (Order order in Orders)
                            {
                                if (!currentSalesIds.Any(x => x == (int)order.id))
                                {
                                    neworders.Add(order);
                                }
                            }

                            if (neworders.Count > 0)
                            {
                                using (var context = new WADbContext())
                                {
                                    using (var transaction = context.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            List<int> abssMissingItemIdList = ModelHelper.AddSales(neworders, comInfo, context, ref FouledOrderIdList);
                                            progressBar1.Visible = false;
                                            if (abssMissingItemIdList.Count == 0)
                                            {
                                                if (FouledOrderIdList.Count > 0)
                                                {
                                                    StringBuilder sb = new StringBuilder();
                                                    foreach (var item in FouledOrderIdList)
                                                    {
                                                        sb.AppendFormat("Order Number:{0} Reasons:{1}{2}", item.Key, item.Value, Environment.NewLine);
                                                    }
                                                    var msg = string.Format("AddOn did not save the orders with reasons as follows:{0}{1}", Environment.NewLine, sb.ToString());
                                                    msg += String.Format("{0} while other orders are saved.", Environment.NewLine);
                                                    MessageBox.Show(msg, "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                {
                                                    MessageBox.Show(string.Format("{0} Saved.", DataType.Order.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                            else
                                            {
                                                var list = ModelHelper.GetWooItemByIdList(abssMissingItemIdList, context);
                                                List<string> namelist = new List<string>();
                                                foreach (var item in list)
                                                {
                                                    namelist.Add(item.itmName);
                                                }
                                                string msg = string.Format("{0} Saved. Howerver, there is some information of WooCommerce products that ABSS is missing. These products are: {1}", DataType.Order.ToString(), string.Join(",", namelist));
                                                MessageBox.Show(msg, "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            throw new Exception(ex.Message);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                progressBar1.Visible = false;
                                MessageBox.Show("Current sales data in DB is up-to-date.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please save Customer and Product data to the database first", "Step Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
            }
        }

        private async void btnABSS_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            AbssResult result;
            switch (type)
            {
                case DataType.Product:  
                    if (ItemEditModel.GetWooItemCount(AccountProfileId) == 0)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("No Product Data Found! Please save WooCommerce product data to DB first.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        result = await WriteItemToABSS();
                        progressBar1.Visible = false;

                        if (result != null)
                        {
                            const string caption = "ABSS Upload";
                            if (result.Success)
                            {
                                MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    break;
                case DataType.Customer:
                    if (CustomerEditModel.GetWooCustomerCount(AccountProfileId) == 0)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("No Customer Data Found! Please save WooCommerce customer data to DB first.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        result = await WriteCustomerToABSS();
                        progressBar1.Visible = false;

                        if (result != null)
                        {
                            const string caption = "ABSS Upload";
                            if (result.Success)
                            {
                                MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    break;
                default:
                case DataType.Order:
                    bool islocked = false;
                    FileInfo fileInfo = new FileInfo(Path.Combine(comInfo.abssFileLocation,comInfo.abssFileName));
                    if (fileInfo.Exists)
                    {
                        islocked = FileHelper.IsFileLocked(fileInfo);
                    }
                    if(islocked)
                    {
                        MessageBox.Show("The ABSS file is being locked. Please make sure the file is unlocked before trying to do data transference.", "ABSS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (SalesEditModel.GetWooSalesCount() == 0)
                        {
                            progressBar1.Visible = false;
                            MessageBox.Show("No Sales Data Found! Please save WooCommerce order data to DB first.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            progressBar1.Visible = false;
                            frmSalesDate frmSalesDate = new frmSalesDate(comInfo.abssDateFormat);
                            frmSalesDate.ShowDialog();
                            if (frmSalesDate.dialogResult == DialogResult.OK)
                            {
                                progressBar1.Visible = true;
                                progressBar1.Style = ProgressBarStyle.Marquee;
                                using (var context = new WADbContext())
                                {
                                    using (var transaction = context.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            result = await ModelHelper.WriteSalesToABSS(context, comInfo, frmSalesDate.IncludeUploaded, frmSalesDate.frmDate, frmSalesDate.toDate);
                                            progressBar1.Visible = false;

                                            if (result != null)
                                            {
                                                const string caption = "ABSS Upload";
                                                if (result.Success)
                                                {
                                                    MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                {
                                                    MessageBox.Show(result.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            transaction.Commit();
                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            throw new Exception(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    break;
            }
        }
        private async void btnWooCommerce_Click(object sender, EventArgs e)
        {
            waitForm.Show(this);
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            bool bok;
            switch (type)
            {
                case DataType.AbssCustomer:
                    bok = await MyobCustomerEditModel.UpdateWoo(rest);
                    waitForm.Close();
                    if (bok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("Customers Uploaded to WooCommerce", "Upload Customers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                case DataType.AbssProduct:
                    bok = await MyobItemEditModel.UpdateWoo(rest, comInfo);
                    waitForm.Close();
                    if (bok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("Products Uploaded to WooCommerce", "Upload Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }             
        }


        private async Task<AbssResult> WriteItemToABSS()
        {
            var CurrentWooItemList = ItemEditModel.GetWooItemList(AccountProfileId, false);

            if (CurrentWooItemList.Count == 0)
            {
                MessageBox.Show("No Woo Product Data Found! Please save WooCommerce product data to DB first.", "No Woo Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                var CurrentItemCodes = MYOBHelper.GetItemCodes(abss);
                List<ItemModel> FilteredWooItemList = CurrentWooItemList.Where(x => !CurrentItemCodes.Contains(x.itmCode)).ToList();
                if(FilteredWooItemList.Count == 0)
                {
                    MessageBox.Show("No New Item Data Found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }                

                using (var context = new WADbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //CheckOutIds = new List<int>();
                            var result = await ItemEditModel.WriteWooItemsToABSS(FilteredWooItemList, comInfo, context);
                            transaction.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception(ex.Message);
                        }
                    }
                }

            }
        }



        private async Task<AbssResult> WriteCustomerToABSS()
        {
            var CurrentWooCustomerList = CustomerEditModel.GetWooCustomerList(AccountProfileId, false);
            if (CurrentWooCustomerList.Count == 0)
            {
                MessageBox.Show("No Customer Data Found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                var CurrentCustomerCodes = MYOBHelper.GetCustomerCodes(abss);
                List<CustomerModel> FilteredWooCustomerList = CurrentWooCustomerList.Where(x => !CurrentCustomerCodes.Contains(x.cusCode)).ToList();
                if (FilteredWooCustomerList.Count == 0)
                {
                    MessageBox.Show("No New Customer Data Found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }

                CheckOutIds = new List<int>();
                using (var context = new WADbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<string> columns = new List<string>();

                            //CoLastName,CardID,InvoiceDelivery,Address1Phone1,Address1Email,Address1ContactName
                            string sql = MyobHelper.InsertImportCustomerWContactSql;

                            for (int j = 0; j < MyobHelper.ImportCustomerWContactColCount; j++)
                            {
                                columns.Add("'{" + j + "}'");
                            }
                            string strcolumn = string.Join(",", columns);

                            List<string> values = new List<string>();
                            foreach (var item in CurrentWooCustomerList)
                            {
                                string deliverystatus = "A";
                                //string cardstatus = item.cusIsActive ? "N" : "Y";
                                string value = string.Format("(" + strcolumn + ")", item.cusName, item.cusCode, deliverystatus, item.cusPhone, item.cusEmail, item.cusContact);
                                values.Add(value);
                                CheckOutIds.Add(item.cusCustomerID);
                            }

                            sql += string.Join(",", values) + ")";
                            ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName, comInfo.abssKeyLocation, comInfo.abssKeyName);
                            //ModelHelper.GetAbssKeyInfo(ref abss);
                            var result = MyobHelper.WriteMYOB(abss);

                            ModelHelper.WriteLog(context, string.Format("sql:{0};Success:{1};retmsg:{2}", sql, result.Success, result.Message), "Upload Customer to ABSS");

                            await context.SaveChangesAsync();
                            await ModelHelper.HandleCheckOutIds(CheckOutIds, context, DataType.Customer);
                            transaction.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        private BindingSource GetCurrentRecordsABSS()
        {
            switch (type)
            {
                case DataType.AbssCustomer:
                    var customers = AbssCustomers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_C = new BindingList<MyobCustomerModel>(customers);
                    return new BindingSource(bindingList_C, null);

                case DataType.AbssProduct:
                    var products = AbssProducts.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_P = new BindingList<MyobItemModel>(products);
                    return new BindingSource(bindingList_P, null);
                default:
                case DataType.AbssOrder:
                    var bindingList_O = new BindingList<SalesModel>();
                    return new BindingSource(bindingList_O, null);
            }
        }
        private BindingSource GetCurrentRecords()
        {
            switch (type)
            {
                case DataType.Product:
                    var products = Products.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_P = new BindingList<Product>(products);
                    return new BindingSource(bindingList_P, null);
                case DataType.Customer:
                    var customers = Customers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_C = new BindingList<Customer>(customers);
                    return new BindingSource(bindingList_C, null);
                default:
                case DataType.Order:
                    var orders = Orders.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_O = new BindingList<Order>(orders);
                    return new BindingSource(bindingList_O, null);
            }
        }

        private void btnNxtPage_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex < TotalPage)
            {
                this.CurrentPageIndex++;
                if (type.ToString().StartsWith("Abss"))
                {
                    this.dgList.DataSource = GetCurrentRecordsABSS();
                }
                else
                {
                    this.dgList.DataSource = GetCurrentRecords();
                }

            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 1)
            {
                this.CurrentPageIndex--;
                if (type.ToString().StartsWith("Abss"))
                {
                    this.dgList.DataSource = GetCurrentRecordsABSS();
                }
                else
                {
                    this.dgList.DataSource = GetCurrentRecords();
                }

            }
        }

        class PageOffsetList : System.ComponentModel.IListSource
        {
            public bool ContainsListCollection { get; protected set; }
            private int ExpectedTotal { get; set; }
            private int PageSize { get; set; }
            public PageOffsetList(int expectedTotal, int pageSize)
            {
                ExpectedTotal = expectedTotal;
                PageSize = pageSize;
            }

            public System.Collections.IList GetList()
            {
                // Return a list of page offsets based on "totalRecords" and "pageSize"
                var pageOffsets = new List<int>();
                for (int offset = 0; offset < ExpectedTotal; offset += PageSize)
                    pageOffsets.Add(offset);
                return pageOffsets;
            }
        }

        private void numExpectedTotal_ValueChanged(object sender, EventArgs e)
        {
            //ExpectedTotal = (int)numActualTotal.Value;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
