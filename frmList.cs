using MYOBLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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
using CommonLib.Models;
using MYOBLib.Models;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace WooCommerceAddOn
{
    public partial class frmList : Form
    {
        public Dictionary<string, string> FouledOrderIdList { get; set; }
        public Dictionary<int, string> FouledCustomerIdList { get; set; }
        private int MaxItemCodeLength { get { return int.Parse(ConfigurationManager.AppSettings["ItemCodeLength"]); } }
        private int MaxItemNameLength { get { return int.Parse(ConfigurationManager.AppSettings["ItemNameLength"]); } }
        public Dictionary<string, string> FouledProductNameList { get; set; }
        private ComInfoModel comInfo { get; set; }
        //private string DeviceId { get; set; }
        private List<int> CheckOutIds { get; set; }
        private int CustomerNameLength { get { return int.Parse(ConfigurationManager.AppSettings["CustomerNameLength"]); } }
        private int CustomerCodeLength { get { return int.Parse(ConfigurationManager.AppSettings["CustomerCodeLength"]); } }
        private int AccountProfileId { get { return int.Parse(ConfigurationManager.AppSettings["AccountProfileId"]); } }
        //private bool isDemo { get { return ConfigurationManager.AppSettings["Demo"] == "1"; } }
        private DataType type { get; set; }
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageLength"]);
        private int CurrentPageIndex = 1;
        private int TotalPage = 0;
        public int ExpectedTotal = 0;
        public int ActualTotal = 0;
        //private int Page = 1;

        private List<Customer> Customers { get; set; }
        private List<Order> Orders { get; set; }
        private List<TaxRate> TaxRates { get; set; }
        private List<Product> Products { get; set; }
        private List<Customer> FilteredCustomers { get; set; }
        private List<Order> FilteredOrders { get; set; }
        private List<Product> FilteredProducts { get; set; }

        private List<CustomerModel> CurrentWooCustomerList { get; set; }
        private List<ItemModel> CurrentWooItemList { get; set; }
        private List<SalesLnView> CurrentWooSalesList { get; set; }
        private RestAPI rest { get; set; }
        //RestAPI rest = new RestAPI(string.Format("{0}/wp-json/wc/v3/", endpoint), key, secret);
        public frmList(ComInfoModel comInfo)
        {
            InitializeComponent();
            this.comInfo = comInfo;
            type = comInfo.dataType;

            switch (type)
            {
                case DataType.Product:
                    CurrentWooItemList = new List<ItemModel>();
                    CurrentWooItemList = ItemEditModel.GetWooItemList(AccountProfileId);
                    Products = new List<Product>();

                    this.Text = "Product List";
                    break;
                case DataType.Customer:
                    CurrentWooCustomerList = new List<CustomerModel>();
                    CurrentWooCustomerList = CustomerEditModel.GetWooCustomerList(AccountProfileId);
                    Customers = new List<Customer>();

                    this.Text = "Customer List";
                    break;
                default:
                case DataType.Order:
                    Orders = new List<Order>();
                    TaxRates = new List<TaxRate>();

                    this.Text = "Order List";
                    break;
            }
            var url = string.Format("{0}/wp-json/wc/v3/", comInfo.wcURL);
            rest = new RestAPI(url, comInfo.wcConsumerKey, comInfo.wcConsumerSecret);
        }

        private async void frmList_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;

            switch (type)
            {
                case DataType.Product:
                    Products = await ModelHelper.GetWooProduct(rest);
                    progressBar1.Visible = false;

                    BindingList<Product> bindingList_P = new BindingList<Product>(Products);
                    source = new BindingSource(bindingList_P, null);
                    break;
                case DataType.Customer:
                    Customers = await ModelHelper.GetWooCustomer(rest);
                    progressBar1.Visible = false;

                    BindingList<Customer> bindingList_C = new BindingList<Customer>(Customers);
                    source = new BindingSource(bindingList_C, null);
                    break;
                default:
                case DataType.Order:
                    Orders = await ModelHelper.GetWooOrder(rest);
                    TaxRates = await ModelHelper.GetWooTaxRate(rest);
                    progressBar1.Visible = false;

                    BindingList<Order> bindingList_O = new BindingList<Order>(Orders);
                    source = new BindingSource(bindingList_O, null);
                    break;

            }
            dgList.DataSource = source;
            // WORK IN PAGING FOR DATAGRIDVIEW
            // Get total count of the pages; 
            //this.CalculateTotalPages();
            this.dgList.ReadOnly = true;
            // Load the first page of data; 
            //this.dgList.DataSource = await GetCurrentRecords(CurrentPageIndex);
        }

        private async void btnSaveDB_Click(object sender, EventArgs e)
        {
            switch (type)
            {
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
                        CurrentWooItemList = ItemEditModel.GetWooItemList(AccountProfileId);//don't remove!

                        List<ItemModel> itemlist = new List<ItemModel>();
                        List<ItemModel> currentwooitems = new List<ItemModel>();

                        foreach (var i in Products)
                        {
                            var currentWooItem = CurrentWooItemList.Where(x => x.itmWooItemId == (int)i.id).FirstOrDefault();

                            if (currentWooItem == null)
                            {
                                ItemModel item = PopulateItem(i);
                                if (item != null)
                                {
                                    itemlist.Add(item);
                                }
                            }
                            else
                            {
                                currentwooitems.Add(currentWooItem);
                            }
                        }

                        if (itemlist.Count > 0)
                        {
                            await ItemEditModel.AddList(itemlist);
                            progressBar1.Visible = false;
                        }
                        if (currentwooitems.Count > 0)
                        {
                            await ItemEditModel.EditList(currentwooitems);
                            progressBar1.Visible = false;
                        }

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
                        FouledCustomerIdList = new Dictionary<int, string>();
                        progressBar1.Visible = true;
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        CurrentWooCustomerList = CustomerEditModel.GetWooCustomerList(AccountProfileId);//don't remove!

                        List<CustomerModel> customerlist = new List<CustomerModel>();
                        List<CustomerModel> currentwoocustomers = new List<CustomerModel>();

                        foreach (var c in Customers)
                        {
                            var currentWooCustomer = CurrentWooCustomerList.Where(x => x.cusCustomerID == c.id).FirstOrDefault();

                            if (currentWooCustomer == null)
                            {
                                string cuscode = CommonHelper.GenerateNonce(CustomerCodeLength);

                                //string username = string.IsNullOrEmpty(c.billing.company) ? string.Concat(c.first_name, "", c.last_name) : c.billing.company;

                                //if (username.Length > CustomerNameLength)
                                //{
                                //    FouledCustomerIdList[(int)c.id] = string.Format(@"The length of CompanyName/FirstName+LastName exceeds {0}", CustomerNameLength);
                                //}

                                //if (!string.IsNullOrEmpty(c.billing.phone) && c.billing.phone.Length > CustomerCodeLength)
                                //{
                                //    FouledCustomerIdList[(int)c.id] = string.Format(@"The length of Phone exceeds {0}", CustomerCodeLength);
                                //}

                                //if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(c.billing.phone))
                                //{
                                //    FouledCustomerIdList[(int)c.id] = @"CompanyName/FirstName+LastName and Phone are empty";
                                //}
                                //else
                                //{

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

                                customer.cusPointsSoFar = await GetCustomerPointFrmOrders((long)c.id);
                                customerlist.Add(customer);
                                //}
                            }
                            else
                            {
                                currentWooCustomer.cusPointsSoFar += await GetCustomerPointFrmOrders((long)c.id);
                                currentwoocustomers.Add(currentWooCustomer);
                            }
                        }

                        if (customerlist.Count > 0)
                        {
                            await CustomerEditModel.AddList(customerlist);
                            progressBar1.Visible = false;
                        }
                        if (currentwoocustomers.Count > 0)
                        {
                            await CustomerEditModel.EditList(currentwoocustomers);
                            progressBar1.Visible = false;
                        }

                        if (FouledCustomerIdList.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in FouledCustomerIdList)
                            {
                                sb.AppendFormat("ID:{0} Reasons:{1}{2}", item.Key, item.Value, Environment.NewLine);
                            }
                            var msg = string.Format("AddOn did not save the customers with reasons as follows:{0}{1}", Environment.NewLine, sb.ToString());
                            msg += String.Format("{0} while other customers are saved.", Environment.NewLine);
                            MessageBox.Show(msg, "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0} Saved.", DataType.Customer.ToString()), "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

                            //CurrentWooSalesList = SalesEditModel.GetWooSalesList(abss);//don't remove!

                            List<SalesModel> saleslist = new List<SalesModel>();
                            List<SalesLnView> salelnslist = new List<SalesLnView>();
                            List<SalesLnView> currentwoosaless = new List<SalesLnView>();
                            List<Order> neworders = new List<Order>();

                            //if (CurrentWooSalesList.Count > 0)
                            //{
                            foreach (Order order in Orders)
                            {
                                if (!currentSalesIds.Any(x => x == (int)order.id))
                                {
                                    neworders.Add(order);
                                }
                            }

                            if (neworders.Count > 0)
                            {
                                List<int> abssMissingItemIdList = await AddSales(neworders, comInfo);
                                progressBar1.Visible = false;
                                if (abssMissingItemIdList.Count == 0)
                                {
                                    if (FouledOrderIdList.Count > 0)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        foreach (var item in FouledOrderIdList)
                                        {
                                            sb.AppendFormat("Name:{0} Reasons:{1}{2}", item.Key, item.Value, Environment.NewLine);
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
                                    using (var context = new WADbContext())
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
                        result = await WriteVipToABSS();
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
                    if (MessageBox.Show("Please make sure that your ABSS application is closed before trying to upload data to the ABSS.", "ABSS", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
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
                                result = await WriteSalesToABSS(frmSalesDate, frmSalesDate.IncludeUploaded);
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
                        }
                    }
                    break;
            }
        }


        private async Task<AbssResult> WriteSalesToABSS(frmSalesDate frmSalesDate, bool includeUploaded)
        {
            string sql = MyobHelper.ItemLocListSql;
            ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName);
            abss.accountProfileId = AccountProfileId;
            abss.Location = new LocationModel
            {
                LocationCode = comInfo.abssPrimaryLocation,
            };
            var abssstocklist = ItemEditModel.GetAbssStockList(abss);
            var outofstocklist = abssstocklist.Where(x => x.QuantityOnHand <= 0).ToList();
            //var stocklist = abssstocklist.Except(outofstocklist).ToList();
            //List<WALib.Models.MYOB.MyobItemModel> abssitemlist = ItemEditModel.GetAbssItemList(abss);

            abss.frmDate = frmSalesDate.frmDate;
            abss.toDate = frmSalesDate.toDate;
            var saleslnlist = SalesEditModel.GetWooSalesList(abss, includeUploaded);
            List<PayView> paylist = SalesEditModel.GetWooPayList(saleslnlist);

            if (saleslnlist.Count == 0)
            {
                MessageBox.Show("No Sales Data Found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            List<SalesLnView> pendingorders = new List<SalesLnView>();
            foreach (var salesln in saleslnlist)
            {
                if (!string.IsNullOrEmpty(salesln.rtlItemCode))
                {
                    if (outofstocklist.Any(x => x.ItemCode == salesln.rtlItemCode))
                    {
                        pendingorders.Add(salesln);
                    }
                }
                else
                {
                    ItemModel item = ItemEditModel.GetWooItemById((int)salesln.rtlWooItemId, AccountProfileId);
                    if (item != null)
                    {
                        if (outofstocklist.Any(x => x.itmName.ToLower() == item.itmName.ToLower()))
                        {
                            pendingorders.Add(salesln);
                        }
                    }
                }
            }

            if (pendingorders.Count > 0)
            {
                if (await setOrderPending(pendingorders))
                {
                    saleslnlist = SalesEditModel.GetWooSalesList(abss);
                    return await writeSalesToABSS(saleslnlist, paylist, comInfo.abssDateFormat);
                }
            }
            else
            {
                return await writeSalesToABSS(saleslnlist, paylist, comInfo.abssDateFormat);
            }
            return null;
        }

        private async Task<AbssResult> writeSalesToABSS(List<SalesLnView> saleslnlist, List<PayView> paylist, string dateformatcode)
        {
            AbssResult result = null;
            CheckOutIds = new List<int>();
            const string salesprefix = "SA";
            string salesrefundcode = "";
            //const string deposititempo = "Deposit#SA";
            //const string depositdesc = "G3Customer Deposit";
            //string refsalescode = "";
            const string deliverystatus = "A";
            //string description = "";
            string sql = "";
            int collength;
            List<string> columns;
            string strcolumn = "";

            if (saleslnlist.Count > 0)
            {
                CheckOutIds.AddRange(saleslnlist.Select(x => x.rtsUID).Distinct().ToList());

                var GroupedSalesLnList = saleslnlist.GroupBy(x => x.rtlCode);

                Dictionary<string, PayView> dicPayList = new Dictionary<string, PayView>();

                foreach (var group in GroupedSalesLnList)
                {
                    dicPayList.Add(group.Key, paylist.Where(x => x.rtpCode == group.Key).FirstOrDefault());
                }

                double salespaidamt = 0;
                string paytypeamts = "";
                string paytypes = "";
                string invoicestatus = "";
                string value = "";
                double mthchrpc = 0;

                using (var context = new WADbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var group in GroupedSalesLnList)
                            {
                                if (group.Count() <= 1)
                                {
                                    List<string> values = new List<string>();
                                    var item = group.FirstOrDefault();
                                    item.dateformat = dateformatcode == "E" ? @"dd/MM/yyyy" : @"MM/dd/yyyy";
                                    invoicestatus = !item.IsCheckout && item.InvoiceStatus.ToLower() == "processing" ? "O" : "I";
                                    var payview = dicPayList[item.rtlCode];
                                    paytypes = payview.rtpWooPayMethod;

                                    salesrefundcode = item.rtlCode.Replace("SA", "");
                                    salesrefundcode = string.Concat(salesprefix, salesrefundcode);

                                    salespaidamt = item.InvoiceStatus == "processing" ? 0 : (double)dicPayList[item.rtlCode].rtpPayAmt;

                                    if (item.Item.itmIsNonStock)
                                    {
                                        sql = "INSERT INTO Import_Item_Sales(CoLastName,InvoiceNumber,SaleDate,ItemNumber,Quantity,Price,Discount,Total,SaleStatus,CardID,AmountPaid,PaymentMethod,PaymentIsDue,DiscountDays,BalanceDueDays,PercentDiscount,PercentMonthlyCharge,DeliveryStatus,CustomersNumber,Job,Comment,SalespersonLastName,Memo)  VALUES(";
                                        collength = 23;
                                        columns = new List<string>();
                                        for (int j = 0; j < collength; j++)
                                        {
                                            columns.Add("'{" + j + "}'");
                                        }
                                        strcolumn = string.Join(",", columns);

                                        value = string.Format(strcolumn, item.CustomerName, salesrefundcode, item.SalesDateDisplay, item.Item.itmCode, item.dQty, item.dPrice, item.dLineDiscPc, item.dLineSalesAmt, invoicestatus, item.CustomerCode, salespaidamt, paytypes, 0, 0, 0, item.dLineDiscPc, mthchrpc, deliverystatus, item.rtlRefSales, item.rtlSalesLoc, paytypes, item.SalesPersonName, paytypeamts);
                                    }
                                    else
                                    {
                                        sql = "INSERT INTO Import_Item_Sales(CoLastName,InvoiceNumber,SaleDate,ItemNumber,Quantity,Price,Discount,Total,SaleStatus,Location,CardID,AmountPaid,PaymentMethod,PaymentIsDue,DiscountDays,BalanceDueDays,PercentDiscount,PercentMonthlyCharge,DeliveryStatus,CustomersNumber,Job,Comment,SalespersonLastName,Memo)  VALUES(";

                                        collength = 24;
                                        columns = new List<string>();
                                        for (int j = 0; j < collength; j++)
                                        {
                                            columns.Add("'{" + j + "}'");
                                        }
                                        strcolumn = string.Join(",", columns);


                                        value = string.Format(strcolumn, item.CustomerName, salesrefundcode, item.SalesDateDisplay, item.Item.itmCode, item.dQty, item.dPrice, item.dLineDiscPc, item.dLineSalesAmt, invoicestatus, item.rtlSalesLoc, item.CustomerCode, salespaidamt, paytypes, 0, 0, 0, item.dLineDiscPc, mthchrpc, deliverystatus, item.rtlRefSales, item.rtlSalesLoc, paytypes, item.SalesPersonName, paytypeamts);
                                    }

                                    values.Add(value);

                                    sql += string.Join(",", values) + ")";
                                    ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName, comInfo.abssKeyLocation, comInfo.abssKeyName);
                                    //ModelHelper.GetAbssKeyInfo(ref abss);
                                    result = MyobHelper.WriteMYOB(abss);

                                    ModelHelper.WriteLog(context, string.Format("sql:{0};Success:{1};retmsg:{2}", sql, result.Success, result.Message), "Upload Sales to ABSS");

                                    await context.SaveChangesAsync();

                                }
                                else
                                {
                                    List<string> values = new List<string>();
                                    foreach (var item in group)
                                    {
                                        salespaidamt = item.InvoiceStatus == "processing" ? 0:(double)dicPayList[item.rtlCode].rtpPayAmt;
                                        invoicestatus = !item.IsCheckout && item.InvoiceStatus.ToLower() == "processing" ? "O" : "I";
                                        var payview = dicPayList[item.rtlCode];
                                        paytypes = payview.rtpWooPayMethod;
                                        /*
                                     * "INSERT INTO Import_Item_Sales(CoLastName,InvoiceNumber,SaleDate,ItemNumber,Quantity,Price,Discount,Total,SaleStatus,Location,CardID,AmountPaid,PaymentMethod,PaymentIsDue,DiscountDays,BalanceDueDays,PercentDiscount,PercentMonthlyCharge,DeliveryStatus,CustomersNumber,Job,Comment,SalespersonLastName,Memo)
                                     */
                                        salesrefundcode = item.rtlCode.Replace("SA", "");
                                        salesrefundcode = string.Concat(salesprefix, salesrefundcode);

                                        if (item.Item.itmIsNonStock)
                                        {
                                            sql = "INSERT INTO Import_Item_Sales(CoLastName,InvoiceNumber,SaleDate,ItemNumber,Quantity,Price,Discount,Total,SaleStatus,CardID,AmountPaid,PaymentMethod,PaymentIsDue,DiscountDays,BalanceDueDays,PercentDiscount,PercentMonthlyCharge,DeliveryStatus,CustomersNumber,Job,Comment,SalespersonLastName,Memo)  VALUES(";
                                            collength = 23;
                                            columns = new List<string>();
                                            for (int j = 0; j < collength; j++)
                                            {
                                                columns.Add("'{" + j + "}'");
                                            }
                                            strcolumn = string.Join(",", columns);

                                            value = string.Format("(" + strcolumn + ")", item.CustomerName, salesrefundcode, item.SalesDateDisplay, item.Item.itmCode, item.dQty, item.dPrice, item.dLineDiscPc, item.dLineSalesAmt, invoicestatus, item.CustomerCode, salespaidamt, paytypes, 0, 0, 0, item.dLineDiscPc, mthchrpc, deliverystatus, item.rtlRefSales, item.rtlSalesLoc, paytypes, item.SalesPersonName, paytypeamts);
                                        }
                                        else
                                        {
                                            sql = "INSERT INTO Import_Item_Sales(CoLastName,InvoiceNumber,SaleDate,ItemNumber,Quantity,Price,Discount,Total,SaleStatus,Location,CardID,AmountPaid,PaymentMethod,PaymentIsDue,DiscountDays,BalanceDueDays,PercentDiscount,PercentMonthlyCharge,DeliveryStatus,CustomersNumber,Job,Comment,SalespersonLastName,Memo)  VALUES(";
                                            
                                            collength = 24;
                                            columns = new List<string>();
                                            for (int j = 0; j < collength; j++)
                                            {
                                                columns.Add("'{" + j + "}'");
                                            }
                                            strcolumn = string.Join(",", columns);

                                            value = string.Format("(" + strcolumn + ")", item.CustomerName, salesrefundcode, item.SalesDateDisplay, item.Item.itmCode, item.dQty, item.dPrice, item.dLineDiscPc, item.dLineSalesAmt, invoicestatus, item.rtlSalesLoc, item.CustomerCode, salespaidamt, paytypes, 0, 0, 0, item.dLineDiscPc, mthchrpc, deliverystatus, item.rtlRefSales, item.rtlSalesLoc, paytypes, item.SalesPersonName, paytypeamts);
                                        }

                                        values.Add(value);
                                    }
                                    sql += string.Join(",", values) + ")";
                                    ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName, comInfo.abssKeyLocation, comInfo.abssKeyName);
                                    //ModelHelper.GetAbssKeyInfo(ref abss);
                                    result = MyobHelper.WriteMYOB(abss);

                                    ModelHelper.WriteLog(context, string.Format("sql:{0};Success:{1};retmsg:{2}", sql, result.Success, result.Message), "Upload Sales to ABSS");

                                    await context.SaveChangesAsync();


                                }
                            }

                            await HandleCheckOutIds(CheckOutIds, context, DataType.Order);
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

            return null;
        }

        private async Task<List<int>> AddSales(List<Order> orders, ComInfoModel comInfo)
        {
            FouledOrderIdList = new Dictionary<string, string>();
            DateTime dtnow = CommonHelper.GetCacheDateTimeNow();

            var products = ModelHelper.GetWooProductFrmDb();
            //products = await ModelHelper.GetWooProduct(rest);
            int Roundings = 0;
            List<string> zerostockItemcodes = new List<string>();
            List<string> salesitemcodes = new List<string>();
            //decimal totalpayamt = 0;
            int apId = AccountProfileId;
            int CusID = 0;
            List<RtlPayLn> PayLnList = new List<RtlPayLn>();
            DateTime paiddate = DateTime.MinValue;
            List<int> abssMissingItemIdList = new List<int>();

            using (var context = new WADbContext())
            {
                List<RtlSale> rtlSaleList = new List<RtlSale>();
                List<RtlPay> rtlPayList = new List<RtlPay>();
                List<RtlSalesLn> SalesLines = new List<RtlSalesLn>();
                List<RtlPay> PayList = new List<RtlPay>();
                Dictionary<string, int> dicItemQty = new Dictionary<string, int>();
                List<PaymentType> PaymentTypes = new List<PaymentType>();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //bool isZeroStockInvoice = false;
                        decimal totaltaxamt = 0;
                        decimal totaldiscountamt = 0;
                        decimal totalamount = 0;
                        string wooorderstatus = "";

                        foreach (var order in orders)
                        {
                            CusID = (int)order.customer_id;
                            if (!ModelHelper.CheckCustomerId(CusID))
                            {
                                FouledOrderIdList[order.number] = string.Format("There is no such a customer with ID {0} in our database.", CusID);
                            }
                            else
                            {
                                wooorderstatus = order.status;
                                if (wooorderstatus == "processing" || wooorderstatus == "completed")
                                {
                                    string salescode = ModelHelper.GetSalesCode(order.number.ToString());
                                    //string paycode = order.payment_method;
                                    bool inclusivetax = (bool)order.prices_include_tax;
                                    string Notes = order.customer_note;
                                    DateTime salesdate = order.date_created_gmt == null ? dtnow : CommonHelper.GetLocalDateTime(order.date_created_gmt);
                                    paiddate = order.date_paid_gmt == null ? dtnow : CommonHelper.GetLocalDateTime(order.date_paid_gmt);
                                    totalamount = (decimal)order.total;
                                    decimal paidamt = 0;

                                    decimal taxamt = 0;
                                    //decimal taxamt = 0;
                                    decimal discountamt = 0;
                                    var taxcode = "";
                                    var couponcode = "";
                                    //bool taxable = false;
                                    //decimal taxrate = 0;
                                    //decimal incltaxamt = 0;
                                    //decimal excltaxamt = 0;
                                    ItemModel product = new ItemModel();
                                    int itemId = 0;

                                    foreach (var salesln in order.line_items)
                                    {
                                        product = products.Where(x => x.itmWooItemId == (int)salesln.product_id).FirstOrDefault();
                                        if (product == null)
                                        {
                                            FouledOrderIdList[order.number] = string.Format("There is no such a product with Name {0} in our database.", salesln.name);
                                            //totalamount -= (decimal)order.total;
                                        }
                                        else
                                        {
                                            itemId = product.itmWooItemId;
                                            var wooitem = ModelHelper.GetWooItemById(itemId, context);
                                            if (string.IsNullOrEmpty(wooitem.itmCode))
                                            {
                                                abssMissingItemIdList.Add(itemId);
                                            }

                                            decimal sellingprice = CommonHelper.RoundDecimal((decimal)salesln.price, 2);
                                            //string itemcode = "";
                                            //int itemId = 0;                                   
                                            int qty = decimal.ToInt32((decimal)salesln.quantity);
                                            paidamt += (decimal)salesln.total;

                                            RtlSalesLn rtlSalesLn = new RtlSalesLn(); //MUST not use object initializer in the loop, otherwise will get error.
                                            rtlSalesLn.rtlWooId = (int)salesln.id;
                                            rtlSalesLn.rtlSalesLoc = comInfo.abssPrimaryLocation;
                                            //rtlSalesLn.rtlDvc = device.dvcCode;
                                            rtlSalesLn.rtlCode = salescode;
                                            //rtlSalesLn.rtlSeq = salesln.seq;
                                            rtlSalesLn.rtlItemCode = wooitem.itmCode;
                                            rtlSalesLn.rtlWooItemId = itemId;
                                            //rtlSalesLn.rtlHasSerialNo = salesln.itemsnlist != null; //salesln.itemsnlist.Count>0;=>error!
                                            //rtlSalesLn.rtlTaxRate = taxrate;
                                            rtlSalesLn.rtlLineDiscAmt = discountamt;
                                            //rtlSalesLn.rtlLineDiscPc = salesln.discount;
                                            rtlSalesLn.rtlWooCouponCode = couponcode;
                                            rtlSalesLn.rtlQty = salesln.quantity;
                                            rtlSalesLn.rtlTaxAmt = taxamt;
                                            rtlSalesLn.rtlDate = salesdate;
                                            //rtlSalesLn.rtlBatchCode = salesln.batchcode;
                                            rtlSalesLn.rtlSalesAmt = (decimal)salesln.total;
                                            rtlSalesLn.rtlType = "RS";
                                            rtlSalesLn.rtlSellingPrice = sellingprice;
                                            rtlSalesLn.rtlCheckout = false;
                                            //rtlSalesLn.rtlDesc = salesln.itemdesc;
                                            //rtlSalesLn.rtlRrpTaxIncl = incltaxamt;
                                            //rtlSalesLn.rtlRrpTaxExcl = excltaxamt;
                                            //rtlSalesLn.rtlSellingPriceMinusInclTax = sellingprice;
                                            rtlSalesLn.rtlInclusiveTax = inclusivetax;
                                            rtlSalesLn.rtlTaxCode = taxcode;
                                            SalesLines.Add(rtlSalesLn);
                                        }
                                    }

                                    if (product != null)
                                    {
                                        if ((bool)product.itmIsMyobTaxed)
                                            totaltaxamt += (decimal)order.total_tax;
                                        if (order.tax_lines.Count > 0)
                                        {
                                            taxcode = order.tax_lines[0].rate_code;
                                        }

                                        totaldiscountamt += (decimal)order.discount_total;
                                        if (order.coupon_lines.Count > 0)
                                        {
                                            couponcode = order.coupon_lines[0].code;
                                        }

                                        totaltaxamt -= totaldiscountamt;


                                        //totaltaxamt -= (decimal)order.discount_tax;


                                        //string salesstatus = (isZeroStockInvoice) ? "PENDING" : "CREATED";
                                        string salesstatus = order.status;
                                        decimal linetotal = (inclusivetax) ? totalamount - totaltaxamt : totalamount;
                                        decimal linetotalplustax = (inclusivetax) ? totalamount : totalamount + totaltaxamt;
                                        decimal finaltotal = (inclusivetax) ? totalamount + Roundings : totalamount + totaltaxamt + Roundings;
                                        var rtlSale = new RtlSale
                                        {
                                            rtsWooId = (int)order.id,
                                            rtsSalesLoc = comInfo.abssPrimaryLocation,
                                            rtsDvc = "",
                                            rtsCode = salescode,
                                            rtsType = "RS",
                                            rtsStatus = salesstatus,
                                            rtsCusID = CusID,
                                            rtsLineTotal = linetotal,
                                            rtsLineTotalPlusTax = linetotalplustax,
                                            //rtsFinalDisc = totaldiscpc,
                                            rtsFinalDiscAmt = order.discount_total,
                                            //rtsFinalTotal = totalamount + totaltaxamt + Roundings - totaldiscountamt,
                                            rtsFinalTotal = finaltotal,
                                            rtsRmks = Notes,
                                            rtsUpLdLog = order.created_via,
                                            rtsDate = salesdate.Date,
                                            rtsTime = salesdate,
                                            rtsCreateTime = dtnow,
                                            //rtsInternalRmks = InternalNotes,
                                            rtsMonthBase = false,
                                            rtsLineTaxAmt = totaltaxamt,
                                            rtsEpay = false,
                                            rtsCheckout = false
                                        };
                                        rtlSaleList.Add(rtlSale);
                                        //string paytype = Deposit == 1 ? "DE" : "";
                                        var rtlPay = new RtlPay
                                        {
                                            rtpSalesLoc = comInfo.abssPrimaryLocation.ToString(),
                                            rtpWooPayMethod = order.payment_method,
                                            //rtpDvc = device.dvcCode,
                                            rtpCode = salescode,
                                            //rtpSeq = session.sesDvcSeq,
                                            //rtpPayAmt = totalamount + Roundings,=> fill in later
                                            rtpPayAmt = paidamt,
                                            rtpDate = paiddate.Date,
                                            rtpTime = paiddate,
                                            rtpTxType = "RS",
                                            //rtpRoundings = Roundings,
                                            //rtpChange = Change,
                                            //rtpPayType = paytype,
                                            //rtpEpayType = authcode == "" ? "" : ModelHelper.GetEpayType(authcode).ToString(),
                                            rtpCurrency = order.currency
                                        };
                                        rtlPayList.Add(rtlPay);
                                    }
                                }
                            }
                        }

                        if (rtlSaleList.Count > 0)
                        {
                            context.RtlSales.AddRange(rtlSaleList);
                        }
                        if (rtlPayList.Count > 0)
                        {
                            context.RtlPays.AddRange(rtlPayList);
                        }
                        if (SalesLines.Count > 0)
                        {
                            context.RtlSalesLns.AddRange(SalesLines);
                        }

                        await context.SaveChangesAsync();

                        #region Handling CustomerPoints

                        if (totalamount > 0)
                        {
                            var cuspoint = Convert.ToInt32(totalamount);

                            var customerpointpricelevels = (
                                from cp in context.CustomerPointPriceLevels
                                join pl in context.PriceLevels
                                on cp.PriceLevelID equals pl.PriceLevelID
                                select new CustomerPointPriceLevelModel
                                {
                                    Id = cp.Id,
                                    CustomerPoint = cp.CustomerPoint,
                                    PriceLevelID = cp.PriceLevelID,
                                    PriceLevelDescription = pl.Description
                                }
                                ).ToList();

                            CustomerPointPriceLevelModel lastcp = customerpointpricelevels.OrderByDescending(x => x.Id).FirstOrDefault();

                            var mcustomer = context.MyobCustomers.FirstOrDefault(x => x.cusCustomerID == CusID && x.AccountProfileId == AccountProfileId && x.cusIsActive == true);
                            if (mcustomer != null)
                            {
                                mcustomer.cusPointsSoFar += cuspoint;
                                mcustomer.cusPointsActive = mcustomer.cusPointsSoFar - mcustomer.cusPointsUsed;
                                mcustomer.cusModifyTime = dtnow;
                                //mcustomer.cusModifyBy = user.UserCode;

                                if (mcustomer.cusPointsActive > lastcp.CustomerPoint)
                                {
                                    mcustomer.cusPriceLevelID = lastcp.PriceLevelID;
                                }
                                else
                                {
                                    int idx = 0;
                                    foreach (var cp in customerpointpricelevels)
                                    {
                                        if (cp.CustomerPoint == mcustomer.cusPointsActive)
                                        {
                                            mcustomer.cusPriceLevelID = cp.PriceLevelID;
                                            break;
                                        }
                                        if (cp.CustomerPoint > mcustomer.cusPointsActive)
                                        {
                                            if (customerpointpricelevels[idx - 1] != null)
                                            {
                                                mcustomer.cusPriceLevelID = customerpointpricelevels[idx - 1].PriceLevelID;
                                                break;
                                            }
                                        }
                                        idx++;
                                    }
                                }
                            }
                            await context.SaveChangesAsync();
                        }

                        #endregion
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            return abssMissingItemIdList;
        }


        private ItemModel PopulateItem(Product i)
        {
            List<string> msg = new List<string>();
            FouledProductNameList = new Dictionary<string, string>();
            if ((!string.IsNullOrEmpty(i.sku) && i.sku.Length > MaxItemCodeLength) && (!string.IsNullOrEmpty(i.slug) && i.slug.Length > MaxItemCodeLength))
            {
                if (i.sku.Length > MaxItemCodeLength)
                {
                    msg.Add(string.Format("The length of Product sku exceeds {0}.", MaxItemCodeLength));
                    FouledProductNameList[i.name] = String.Join(",", msg);
                }
                if (i.slug.Length > MaxItemCodeLength)
                {
                    msg.Add(string.Format("The length of Product slug exceeds {0}.", MaxItemCodeLength));
                    FouledProductNameList[i.name] = String.Join(",", msg);
                }
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(i.sku))
                {
                    if (!string.IsNullOrEmpty(i.slug) && i.slug.Length > MaxItemCodeLength)
                    {
                        msg.Add(string.Format("The length of Product slug exceeds {0}.", MaxItemCodeLength));
                        FouledProductNameList[i.name] = String.Join(",", msg);
                        return null;
                    }
                    else
                    {
                        return populateItem(i);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(i.sku) && i.sku.Length > MaxItemCodeLength)
                    {
                        msg.Add(string.Format("The length of Product sku exceeds {0}.", MaxItemCodeLength));
                        FouledProductNameList[i.name] = String.Join(",", msg);
                        return null;
                    }
                    else
                    {
                        return populateItem(i);
                    }
                }

            }

        }

        private ItemModel populateItem(Product i)
        {
            string itemcode = i.sku ?? (i.slug ?? ModelHelper.GenItemCode());
            DateTime dtnow = CommonHelper.GetCacheDateTimeNow();
            string desc = Regex.Replace(i.description, @"\r\n?|\n", "");
            desc = Regex.Replace(i.description, @"<.*?>", String.Empty);
            return new ItemModel
            {
                itmCode = itemcode,
                itmWooItemId = (int)i.id,
                itmCreateTime = i.date_created_gmt ?? dtnow,
                itmModifyTime = i.date_modified_gmt ?? dtnow,
                itmDesc = desc,
                itmName = "",
                itmBaseSellingPrice = i.price == null ? 0 : (double)i.price,
                itmBaseUnitPrice = i.regular_price == null ? 0 : (double)i.regular_price,
                itmIsMyobTaxed = i.tax_status == "taxable",
                itmTaxCode = i.tax_class,
                itmIsNonStock = (i.stock_status.ToLower() == "outofstock" || i.stock_status.ToLower() == "onbackorder"),
                lstQuantityAvailable = (i.manage_stock != null && i.manage_stock == true) ? i.stock_quantity ?? 0 : 0,
                itmIsBought = i.purchasable ?? false,
                itmIsSold = true,
                lstStockLoc = comInfo.abssPrimaryLocation.ToString()
            };
        }

        private async Task<int> GetCustomerPointFrmOrders(long customerId)
        {
            Orders = new List<Order>();
            Orders = await ModelHelper.GetWooOrder(rest);
            var points = 0;
            var oc = Orders.Where(x => x.customer_id == (ulong)customerId).FirstOrDefault();
            if (oc != null)
            {
                if (int.TryParse(oc.total.ToString(), out points))
                {
                    return points;
                }
            }
            return points;
        }

        private async Task<AbssResult> WriteItemToABSS()
        {
            CurrentWooItemList = ItemEditModel.GetWooItemList(AccountProfileId);
            if (CurrentWooItemList.Count == 0)
            {
                MessageBox.Show("No Item Data Found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                CheckOutIds = new List<int>();
                using (var context = new WADbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<string> columns = new List<string>();
                            string sql = MyobHelper.InsertImportItemNoNameSql;
                            /*
                             * "ItemNumber,Buy,Sell,Inventory,AssetAccount,IncomeAccount,ExpenseAccount,ItemPicture,CustomList1,CustomList2,CustomList3,CustomField1,CustomField2,CustomField3,PrimarySupplier,SupplierItemNumber,TaxWhenBought,BuyUnitMeasure,NumberItemsBuyUnit,ReorderQuantity,MinimumLevel,SellingPrice,SellUnitMeasure,TaxWhenSold,NumberItemsSellUnit,QuantityBreak1,PriceLevelAQtyBreak1,PriceLevelBQtyBreak1,PriceLevelCQtyBreak1,PriceLevelDQtyBreak1,PriceLevelEQtyBreak1,PriceLevelFQtyBreak1,InactiveItem,StandardCost,DefaultShipSellLocation,DefaultReceiveAutoBuildLocation"
                             */
                            for (int j = 0; j < MyobHelper.ImportItemNoNameColCount; j++)
                            {
                                columns.Add("'{" + j + "}'");
                            }
                            string strcolumn = string.Join(",", columns);

                            List<string> values = new List<string>();
                            foreach (var item in CurrentWooItemList)
                            {
                                string value = "";                                
                                char buy = item.itmIsBought ? 'Y' : 'N';
                                char sell = 'Y';
                                char inventory = 'Y';                                
                                string taxcode = item.itmTaxCode;
                                string inactivetxt = "N";
                                /*
                             * public static string ImportItemNoNameCol { get { return "ItemNumber,Description,,UseDescriptionOnSale,Buy,Sell,Inventory,AssetAccount,IncomeAccount,ExpenseAccount,ItemPicture,CustomList1,CustomList2,CustomList3,CustomField1,CustomField2,CustomField3,PrimarySupplier,SupplierItemNumber,TaxWhenBought,BuyUnitMeasure,NumberItemsBuyUnit,ReorderQuantity,MinimumLevel,SellingPrice,SellUnitMeasure,TaxWhenSold,NumberItemsSellUnit,QuantityBreak1,PriceLevelAQtyBreak1,PriceLevelBQtyBreak1,PriceLevelCQtyBreak1,PriceLevelDQtyBreak1,PriceLevelEQtyBreak1,PriceLevelFQtyBreak1,InactiveItem,StandardCost,DefaultShipSellLocation,DefaultReceiveAutoBuildLocation"; } }
                             */
                                value = string.Format("(" + strcolumn + ")", item.itmCode, item.itmDesc, 'Y', buy, sell, inventory, 12400, 48000, 54500, "", "", "", "", "", "", "", "", item.itmSupCode, "", "", 1, 0, 0, item.itmBaseSellingPrice, "", taxcode, 1, 0, item.PLA, item.PLB, item.PLC, item.PLD, item.PLE, item.PLF, inactivetxt, 0, "", "");
                                values.Add(value);
                                CheckOutIds.Add(item.itmWooItemId);
                            }
                            sql += string.Join(",", values) + ")";
                            ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName, comInfo.abssKeyLocation, comInfo.abssKeyName);
                            //ModelHelper.GetAbssKeyInfo(ref abss);
                            var result = MyobHelper.WriteMYOB(abss);

                            ModelHelper.WriteLog(context, string.Format("sql:{0};Success:{1};retmsg:{2}", sql, result.Success, result.Message), "Upload Item to ABSS");




                            await context.SaveChangesAsync();
                            await HandleCheckOutIds(CheckOutIds, context, DataType.Product);
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

        private async Task<AbssResult> WriteVipToABSS()
        {
            CurrentWooCustomerList = CustomerEditModel.GetWooCustomerList(AccountProfileId);
            if (CurrentWooCustomerList.Count == 0)
            {
                MessageBox.Show("No Customer Data Found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                CheckOutIds = new List<int>();
                using (var context = new WADbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<string> columns = new List<string>();

                            //"INSERT INTO Import_Customer_Cards (CoLastName,CardID,CardStatus,ItemPriceLevel,InvoiceDelivery,Address1Phone1,CustomField3,Address1Email,Address1ContactName) VALUES("
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
                                string cardstatus = item.cusIsActive ? "N" : "Y";
                                string value = string.Format("(" + strcolumn + ")", item.cusName, item.cusPhone, cardstatus, item.iPriceLevel, deliverystatus, item.cusPhone, item.cusPointsActive, item.cusEmail, item.cusContact);
                                values.Add(value);
                                CheckOutIds.Add(item.cusCustomerID);
                            }

                            sql += string.Join(",", values) + ")";
                            ABSSModel abss = new ABSSModel(sql, comInfo.abssDriver, comInfo.abssUserId, comInfo.abssUserPass, comInfo.abssFileLocation, comInfo.abssFileName, comInfo.abssExeLocation, comInfo.abssExeName, comInfo.abssKeyLocation, comInfo.abssKeyName);
                            //ModelHelper.GetAbssKeyInfo(ref abss);
                            var result = MyobHelper.WriteMYOB(abss);

                            ModelHelper.WriteLog(context, string.Format("sql:{0};Success:{1};retmsg:{2}", sql, result.Success, result.Message), "Upload Customer to ABSS");

                            await context.SaveChangesAsync();
                            await HandleCheckOutIds(CheckOutIds, context, DataType.Customer);
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




        private async Task HandleCheckOutIds(List<int> checkOutIds, WADbContext context, DataType type)
        {
            switch (type)
            {
                case DataType.Product:
                    var items = context.WooItems.Where(x => checkOutIds.Contains(x.itmWooItemId) && x.AccountProfileId == AccountProfileId);
                    foreach (var i in items)
                    {
                        i.itmCheckout = true;
                    }
                    break;
                case DataType.Customer:
                    var customers = context.WooCustomers.Where(x => checkOutIds.Contains(x.cusCustomerID) && x.AccountProfileId == AccountProfileId);
                    foreach (var c in customers)
                    {
                        c.cusCheckout = true;
                    }
                    break;
                default:
                case DataType.Order:
                    var sales = context.RtlSales.Where(x => checkOutIds.Contains(x.rtsUID));
                    foreach (var sale in sales)
                    {
                        sale.rtsCheckout = true;
                    }
                    break;
            }

            await context.SaveChangesAsync();
        }

        private async Task<bool> setOrderPending(List<SalesLnView> pendingorders)
        {
            bool done = false;
            using (var context = new WADbContext())
            {
                foreach (var order in pendingorders)
                {
                    var sales = context.RtlSales.Where(x => x.rtsWooId == order.rtlWooId).FirstOrDefault();
                    if (sales != null)
                    {
                        sales.rtsStatus = "PENDING";
                    }
                }

                if (await context.SaveChangesAsync() > 0)
                {
                    done = true;
                }
            }
            return done;
        }

        private void CalculateTotalPages()
        {
            int rowCount;
            switch (type)
            {
                case DataType.Product:
                    rowCount = Products.Count;
                    break;
                case DataType.Customer:
                    rowCount = Customers.Count;
                    break;
                default:
                case DataType.Order:
                    rowCount = Orders.Count;
                    break;
            }

            TotalPage = rowCount / PageSize;
            if (rowCount % PageSize > 0) // if remainder is more than  zero 
            {
                TotalPage += 1;
            }
        }

        private async Task<BindingSource> GetCurrentRecords()
        {
            //int startIndex = (page - 1) * PageSize;
            switch (type)
            {
                case DataType.Product:
                    FilteredProducts = new List<Product>();
                    FilteredProducts = await ModelHelper.GetWooProduct(rest, CurrentPageIndex);
                    if (FilteredProducts.Count > 0)
                    {
                        var currentIdlist = Products.Select(x => x.id).ToList();
                        foreach (var product in FilteredProducts)
                        {
                            if (!currentIdlist.Contains(product.id))
                            {
                                Products.Add(product);
                            }
                        }
                    }
                    //FilteredProducts = Products.Skip(startIndex).Take(PageSize).ToList();
                    var bindingList_P = new BindingList<Product>(FilteredProducts);

                    return new BindingSource(bindingList_P, null);
                case DataType.Customer:
                    FilteredCustomers = new List<Customer>();
                    FilteredCustomers = await ModelHelper.GetWooCustomer(rest, CustomerRole.all, CurrentPageIndex);
                    if (FilteredCustomers.Count > 0)
                    {
                        var currentIdlist = Customers.Select(x => x.id).ToList();
                        foreach (var customer in FilteredCustomers)
                        {
                            if (!currentIdlist.Contains(customer.id))
                            {
                                Customers.Add(customer);
                            }
                        }
                    }
                    //FilteredCustomers = Customers.Skip(startIndex).Take(PageSize).ToList();
                    var bindingList_C = new BindingList<Customer>(FilteredCustomers);
                    return new BindingSource(bindingList_C, null);
                default:
                case DataType.Order:
                    FilteredOrders = new List<Order>();
                    FilteredOrders = await ModelHelper.GetWooOrder(rest, CurrentPageIndex);
                    if (FilteredOrders.Count > 0)
                    {
                        var currentIdlist = Orders.Select(x => x.id).ToList();
                        foreach (var order in FilteredOrders)
                        {
                            if (!currentIdlist.Contains(order.id))
                            {
                                Orders.Add(order);
                            }
                        }
                    }
                    //FilteredOrders = Orders.Skip(startIndex).Take(PageSize).ToList();
                    var bindingList_O = new BindingList<Order>(FilteredOrders);
                    return new BindingSource(bindingList_O, null);
            }

        }


        //private async void btnFirstPAge_Click(object sender, EventArgs e)
        //{
        //    this.CurrentPageIndex = 1;
        //    this.dgList.DataSource = await GetCurrentRecords(this.CurrentPageIndex);
        //}

        private async void btnNxtPage_Click(object sender, EventArgs e)
        {
            //if (this.CurrentPageIndex < this.TotalPage)
            //{
            this.CurrentPageIndex++;
            this.dgList.DataSource = await GetCurrentRecords();
            //}

        }

        private async void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 1)
            {
                this.CurrentPageIndex--;
                this.dgList.DataSource = await GetCurrentRecords();
            }
        }

        //private void btnLastPage_Click(object sender, EventArgs e)
        //{
        //    this.CurrentPageIndex = TotalPage;
        //    this.dgList.DataSource = GetCurrentRecords(this.CurrentPageIndex);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void source_CurrentChanged(object sender, EventArgs e)
        //{
        //    // The desired page has changed, so fetch the page of records using the "Current" offset 
        //    int offset = (int)source.Current;
        //    switch (type)
        //    {
        //        case DataType.Product:
        //            var records = new List<Product>();
        //            progressBar1.Visible = false;

        //            dataGridView1.DataSource = records;
        //            break;
        //        case DataType.Customer:
        //            Customers = await ModelHelper.GetWooCustomer(rest);
        //            progressBar1.Visible = false;

        //            BindingList<Customer> bindingList_C = new BindingList<Customer>(Customers);
        //            source = new BindingSource(bindingList_C, null);
        //            break;
        //        default:
        //        case DataType.Order:
        //            Orders = await ModelHelper.GetWooOrder(rest);
        //            TaxRates = await ModelHelper.GetWooTaxRate(rest);
        //            progressBar1.Visible = false;

        //            BindingList<Order> bindingList_O = new BindingList<Order>(Orders);
        //            source = new BindingSource(bindingList_O, null);
        //            break;

        //    }
        //}

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
    }
}
