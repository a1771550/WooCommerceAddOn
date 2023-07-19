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
        private List<MyobCustomerModel> MyobCustomers { get; set; }
        private List<ABSSCustomerModel> ABSSCustomers { get; set; }
        private List<MyobItemModel> MyobProducts { get; set; }
        private List<ABSSItemModel> ABSSProducts { get; set; }
        private List<Customer> Customers { get; set; }
        private List<MyobEmployeeModel> EmployeeList { get; set; }
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

            if (type.ToString().StartsWith("Myob"))
            {
                btnABSS.Enabled = false;
            }
            else
            {
                btnWooCommerce.Enabled = false;
            }

            switch (type)
            {
                case DataType.MyobCustomer:
                    MyobCustomers = new List<MyobCustomerModel>();
                    ABSSCustomers = new List<ABSSCustomerModel>();
                    EmployeeList = new List<MyobEmployeeModel>();
                    this.Text = "ABSS Customer List";
                    break;
                case DataType.MyobProduct:
                    MyobProducts = new List<MyobItemModel>();
                    ABSSProducts = new List<ABSSItemModel>();
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
            btnABSS.Enabled = false;
            switch (type)
            {
                case DataType.MyobCustomer:
                    //HashSet<int> CurrentCardRecordIds = MyobCustomerEditModel.GetCurrentCardRecordIds(comInfo.AccountProfileId);
                    //if (CurrentCardRecordIds != null)
                    //    MyobCustomers = MYOBHelper.GetCustomerList(abss, string.Join(",", CurrentCardRecordIds));
                    //else
                    MyobCustomers = MYOBHelper.GetCustomerList(abss);

                    EmployeeList = MYOBHelper.GetEmployeeList(abss);

                    progressBar1.Visible = false;
                    //if (MyobCustomers.Count == 0)
                    //{
                    //    ABSSCustomers = MyobCustomerEditModel.GetCustomerList(comInfo.AccountProfileId, false);
                    //    if (ABSSCustomers.Count == 0)
                    //    {
                    //        MessageBox.Show("All Customers in ABSS are already uploaded to WooCommerce already. No new customers data are found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        Close();
                    //    }
                    //    else
                    //    {
                    //        type = DataType.AbssCustomer;
                    //        totalpages = ABSSCustomers.Count / PageSize;
                    //        TotalPage = (int)Math.Ceiling(totalpages);
                    //        var mcustomers = ABSSCustomers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    //        BindingList<ABSSCustomerModel> bindingList_abssC = new BindingList<ABSSCustomerModel>(mcustomers);
                    //        source = new BindingSource(bindingList_abssC, null);
                    //        iTotal.Text = ABSSCustomers.Count.ToString();
                    //    }
                    //}
                    //else
                    //{
                    totalpages = MyobCustomers.Count / PageSize;
                    TotalPage = (int)Math.Ceiling(totalpages);
                    var mcustomers = MyobCustomers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    BindingList<MyobCustomerModel> bindingList_abssC = new BindingList<MyobCustomerModel>(mcustomers);
                    source = new BindingSource(bindingList_abssC, null);
                    iTotal.Text = MyobCustomers.Count.ToString();
                    //}
                    break;
                case DataType.MyobProduct:
                    HashSet<int> CurrentItemIds = MyobItemEditModel.GetCurrentItemIds(comInfo.AccountProfileId);
                    MyobProducts = MYOBHelper.GetItemList(abss, string.Join(",", CurrentItemIds));
                    progressBar1.Visible = false;
                    if (MyobProducts.Count == 0)
                    {
                        ABSSProducts = MyobItemEditModel.GetItemList(comInfo.AccountProfileId, false);
                        if (ABSSProducts.Count == 0)
                        {
                            MessageBox.Show("All Items in ABSS are already uploaded to WooCommerce already. No new items data are found.", "No New Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            type = DataType.AbssProduct;
                            totalpages = ABSSProducts.Count / PageSize;
                            TotalPage = (int)Math.Ceiling(totalpages);
                            var mproducts = ABSSProducts.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                            BindingList<ABSSItemModel> bindingList_abssP = new BindingList<ABSSItemModel>(mproducts);
                            source = new BindingSource(bindingList_abssP, null);
                            iTotal.Text = ABSSProducts.Count.ToString();
                        }
                    }
                    else
                    {
                        totalpages = MyobProducts.Count / PageSize;
                        TotalPage = (int)Math.Ceiling(totalpages);
                        var mproducts = MyobProducts.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                        BindingList<MyobItemModel> bindingList_abssP = new BindingList<MyobItemModel>(mproducts);
                        source = new BindingSource(bindingList_abssP, null);
                        iTotal.Text = MyobProducts.Count.ToString();
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
                    btnABSS.Enabled = false;
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
                    btnABSS.Enabled = false;
                    break;
                default:
                case DataType.Order:
                    btnABSS.Enabled = true;
                    frmSalesDate frmSalesDate = new frmSalesDate(comInfo.abssDateFormat);
                    frmSalesDate.ShowDialog();
                    if (frmSalesDate.dialogResult == DialogResult.OK)
                    {
                        //string frmdate = "2023-07-13T00:00:00";
                        //string todate = "2023-07-14T23:59:59";
                        string frmdate = string.Concat(CommonHelper.FormatDate(frmSalesDate.frmDate), "T00:00:00");
                        string todate = string.Concat(CommonHelper.FormatDate(frmSalesDate.toDate), "T23:59:59");
                        for (int i = 1; i <= PageBatchSize; i++)
                        {
                            var _orders = await ModelHelper.GetWooOrder(rest, i, frmdate, todate);
                            if (_orders != null && _orders.Count > 0)
                                Orders.AddRange(_orders);
                        }
                        progressBar1.Visible = false;
                        totalpages = Orders.Count / PageSize;
                        TotalPage = (int)Math.Ceiling(totalpages);
                        Orders = Orders.OrderByDescending(x => x.date_created).ToList();

                        var orders = Orders.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                        List<OrderModel> orderlist = new List<OrderModel>();
                        foreach (var order in orders)
                        {
                            orderlist.Add(new OrderModel
                            {
                                id = order.id,
                                parent_id = order.parent_id,
                                number = order.number,
                                order_key = order.order_key,
                                created_via = order.created_via,
                                version = order.version,
                                status = order.status,
                                currency = order.currency,
                                customer_id = order.customer_id,
                                billing = new WooCommerceNET.WooCommerce.v2.OrderBilling
                                {
                                    first_name = order.billing.first_name,
                                    last_name = order.billing.last_name,
                                    email = order.billing.email,
                                    city = order.billing.city,
                                    country = order.billing.country,
                                    address_1 = order.billing.address_1,
                                    address_2 = order.billing.address_2,
                                    phone = order.billing.phone,
                                },
                                date_created = order.date_created,
                                date_modified = order.date_modified,
                                date_completed = order.date_completed,
                                discount_tax = order.discount_tax,
                                discount_total = order.discount_total,
                                shipping_tax = order.shipping_tax,
                                shipping_total = order.shipping_total,
                                total = order.total,
                                total_tax = order.total_tax,
                                prices_include_tax = order.prices_include_tax,
                            });
                        }
                        BindingList<OrderModel> bindingList_O = new BindingList<OrderModel>(orderlist);
                        source = new BindingSource(bindingList_O, null);
                        iTotal.Text = Orders.Count.ToString();
                    }
                    break;

            }
            dgList.DataSource = source;
            this.dgList.ReadOnly = true;
            if (type == DataType.MyobCustomer && dgList.Rows.Count > 0)
            {
                int idx = 0;
                dgList.Columns["CustomerID"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CardIdentification"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Name"].DisplayIndex = idx;
                idx++;
                dgList.Columns["FirstName"].DisplayIndex = idx;
                idx++;
                dgList.Columns["LastName"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Phone"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Email"].DisplayIndex = idx;
                idx++;
                dgList.Columns["StreetLine1"].DisplayIndex = idx;
                idx++;
                dgList.Columns["StreetLine2"].DisplayIndex = idx;
                idx++;
                dgList.Columns["StreetLine3"].DisplayIndex = idx;
                idx++;
                dgList.Columns["StreetLine4"].DisplayIndex = idx;
                idx++;
                dgList.Columns["City"].DisplayIndex = idx;
                idx++;
                dgList.Columns["State"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Postcode"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Country"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Web"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Notes"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CustomField1"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CustomField2"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CustomField3"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CurrentBalance"].DisplayIndex = idx;
                idx++;
                dgList.Columns["TotalDeposits"].DisplayIndex = idx;
                idx++;
                dgList.Columns["CreditLimit"].DisplayIndex = idx;
            }
            //if (type == DataType.AbssCustomer && dgList.Rows.Count > 0)
            //{
            //    int idx = 0;
            //    dgList.Columns["cusName"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusFirstName"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusSurname"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusContact"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusPhone"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusEmail"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrStreetLine1"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrStreetLine2"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrStreetLine3"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrStreetLine4"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrStreetLine5"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrRegion"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrCity"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrState"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrPostcode"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrCountry"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrPhone1"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrPhone2"].DisplayIndex = idx;
            //    idx++;
            //    dgList.Columns["cusAddrPhone3"].DisplayIndex = idx;
            //}
            if (type == DataType.Order && dgList.Rows.Count > 0)
            {
                int idx = 0;
                dgList.Columns["id"].DisplayIndex = idx;
                idx++;
                dgList.Columns["number"].DisplayIndex = idx;
                idx++;
                dgList.Columns["parent_id"].DisplayIndex = idx;
                idx++;
                dgList.Columns["order_key"].DisplayIndex = idx;
                idx++;
                dgList.Columns["created_via"].DisplayIndex = idx;
                idx++;
                dgList.Columns["status"].DisplayIndex = idx;
                idx++;
                dgList.Columns["currency"].DisplayIndex = idx;
                idx++;
                dgList.Columns["customer_id"].DisplayIndex = idx;
                idx++;
                dgList.Columns["BillingFName"].DisplayIndex = idx;
                dgList.Columns["BillingFName"].HeaderText = "First Name";
                idx++;
                dgList.Columns["BillingLName"].DisplayIndex = idx;
                dgList.Columns["BillingLName"].HeaderText = "Last Name";
                idx++;
                dgList.Columns["Email"].DisplayIndex = idx;
                idx++;
                dgList.Columns["Phone"].DisplayIndex = idx;
                idx++;
                dgList.Columns["BillingAddr1"].DisplayIndex = idx;
                dgList.Columns["BillingAddr1"].HeaderText = "Address 1";
                idx++;
                dgList.Columns["BillingAddr2"].DisplayIndex = idx;
                dgList.Columns["BillingAddr2"].HeaderText = "Address 2";
                idx++;
                dgList.Columns["BillingCity"].DisplayIndex = idx;
                dgList.Columns["BillingCity"].HeaderText = "City";
                idx++;
                dgList.Columns["BillingCountry"].DisplayIndex = idx;
                dgList.Columns["BillingCountry"].HeaderText = "Country";
                //idx++;
                //dgList.Columns["cusAddrPhone1"].DisplayIndex = idx;
                //idx++;
                //dgList.Columns["cusAddrPhone2"].DisplayIndex = idx;
                //idx++;
                //dgList.Columns["cusAddrPhone3"].DisplayIndex = idx;
            }
        }

        private async void btnSaveDB_Click(object sender, EventArgs e)
        {
            bool bok;
            switch (type)
            {
                case DataType.MyobCustomer:
                    #region remove current data first:
                    //var cusIds = AbssCustomers.Select(x => x.CustomerID).Distinct().ToHashSet();
                    //MyobCustomerEditModel.RemoveByCusIds(comInfo.AccountProfileId, cusIds);
                    MyobCustomerEditModel.RemoveAll(comInfo.AccountProfileId);
                    MyobEmployeeEditModel.RemoveAll(comInfo.AccountProfileId);
                    #endregion

                    #region add data:
                    bool cbok = await MyobCustomerEditModel.AddList(MyobCustomers, comInfo.AccountProfileId);
                    bool ebok = await MyobEmployeeEditModel.AddList(EmployeeList, comInfo.AccountProfileId);
                    if (cbok && ebok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show(string.Format("{0} Saved.", DataType.Customer.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                    break;
                case DataType.MyobProduct:
                    #region remove current data first:
                    //var itemIds = AbssProducts.Select(x=>x.ItemID).Distinct().ToHashSet();
                    //MyobItemEditModel.RemoveByItemIds(comInfo.AccountProfileId, itemIds);
                    MyobItemEditModel.RemoveAll(comInfo.AccountProfileId);
                    #endregion

                    #region add data:
                    bok = await MyobItemEditModel.AddList(MyobProducts, comInfo.AccountProfileId);
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
                        //List<CustomerModel> currentwoocustomers = new List<CustomerModel>();
                        #region add data:
                        foreach (var c in Customers)
                        {
                            string cuscode = CommonHelper.GenerateNonce(CustomerCodeLength);
                            string fname = c.first_name.Length > 20 ? c.first_name.Substring(0, 20) : c.first_name;
                            string lname = c.last_name.Length > 50 ? c.last_name.Substring(0, 50) : c.last_name;
                            string uname = c.username.Length > 50 ? c.username.Substring(0, 50) : c.username;
                            //string fname = c.first_name;
                            //string lname = c.last_name;
                            //string uname = c.username;
                            CustomerModel customer = new CustomerModel
                            {
                                cusCustomerID = (int)c.id,
                                cusCode = cuscode,
                                cusCreateTime = c.date_created_gmt,
                                cusModifyTime = c.date_modified_gmt,
                                cusEmail = c.email,
                                cusFirstName = fname,
                                cusLastName = lname,
                                cusName = uname,
                                cusPointsUsed = 0,
                                cusRole = c.role,
                                cusPhone = c.billing.phone,
                                cusContact = uname
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
                                        #region remove current data first:
                                        CustomerEditModel.RemoveAll();
                                        #endregion

                                        CustomerEditModel.AddList(customerlist, context);
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
                            var currentSalesIds = SalesEditModel.GetWooSalesIdList();

                            List<SalesModel> saleslist = new List<SalesModel>();
                            List<SalesLnView> salelnslist = new List<SalesLnView>();
                            List<SalesLnView> currentwoosaless = new List<SalesLnView>();
                            List<Order> neworders = new List<Order>();

                            foreach (Order order in Orders)
                            {
                                if (currentSalesIds.Count > 0 && !currentSalesIds.Any(x => x == (int)order.id))
                                {
                                    neworders.Add(order);
                                }
                                else
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
                    FileInfo fileInfo = new FileInfo(Path.Combine(comInfo.abssFileLocation, comInfo.abssFileName));
                    if (fileInfo.Exists)
                    {
                        islocked = FileHelper.IsFileLocked(fileInfo);
                    }
                    if (islocked)
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
                case DataType.MyobCustomer:
                    bok = await MyobCustomerEditModel.UpdateWoo(rest, comInfo);
                    waitForm.Close();
                    if (bok)
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("Customers Uploaded to WooCommerce", "Upload Customers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                case DataType.AbssProduct:
                case DataType.MyobProduct:
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
            var CurrentWooItemList = ItemEditModel.GetWooItemList(AccountProfileId);

            if (CurrentWooItemList.Count == 0)
            {
                MessageBox.Show("No Woo Product Data Found! Please save WooCommerce product data to DB first.", "No Woo Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                var CurrentItemCodes = MYOBHelper.GetItemCodes(abss);
                List<ItemModel> FilteredWooItemList = CurrentWooItemList.Where(x => !CurrentItemCodes.Contains(x.itmCode)).ToList();
                if (FilteredWooItemList.Count == 0)
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
            var CurrentWooCustomerList = CustomerEditModel.GetWooCustomerList(AccountProfileId);
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
                    var customers = MyobCustomers.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
                    var bindingList_C = new BindingList<MyobCustomerModel>(customers);
                    return new BindingSource(bindingList_C, null);

                case DataType.AbssProduct:
                    var products = MyobProducts.Skip(CurrentPageIndex - 1).Take(PageSize).ToList();
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
