using System;
using System.Collections.Generic;
using System.Text;

namespace BackupClubJummana.QuickBookModel
{
    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class CurrencyRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }
    public class LinkedTxn
    {
        public string TxnId { get; set; }
        public string TxnType { get; set; }
    }

    public class ItemRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class ItemAccountRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class TaxCodeRef
    {
        public string value { get; set; }
    }

    public class SalesItemLineDetail
    {
        public ItemRef ItemRef { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public ItemAccountRef ItemAccountRef { get; set; }
        public TaxCodeRef TaxCodeRef { get; set; }
    }

    public class SubTotalLineDetail
    {
    }

    public class Line
    {
        public string Id { get; set; }
        public int LineNum { get; set; }
        public double Amount { get; set; }
        public string DetailType { get; set; }
        public SalesItemLineDetail SalesItemLineDetail { get; set; }
        public SubTotalLineDetail SubTotalLineDetail { get; set; }
    }

    public class TaxRateRef
    {
        public string value { get; set; }
    }

    public class TaxLineDetail
    {
        public TaxRateRef TaxRateRef { get; set; }
        public bool PercentBased { get; set; }
        public int TaxPercent { get; set; }
        public double NetAmountTaxable { get; set; }
    }

    public class TaxLine
    {
        public double Amount { get; set; }
        public string DetailType { get; set; }
        public TaxLineDetail TaxLineDetail { get; set; }
    }

    public class TxnTaxDetail
    {
        public double TotalTax { get; set; }
        public IList<TaxLine> TaxLine { get; set; }
    }

    public class CustomerRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class BillAddr
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string Line1 { get; set; }
        public string Country { get; set; }
    }

    public class ShipAddr
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
    }

    public class SalesTermRef
    {
        public string value { get; set; }
    }

    public class Invoice
    {
        public bool AllowIPNPayment { get; set; }
        public bool AllowOnlinePayment { get; set; }
        public bool AllowOnlineCreditCardPayment { get; set; }
        public bool AllowOnlineACHPayment { get; set; }
        public string domain { get; set; }
        public bool sparse { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData MetaData { get; set; }
        public IList<object> CustomField { get; set; }
        public string DocNumber { get; set; }
        public string TxnDate { get; set; }
        public CurrencyRef CurrencyRef { get; set; }
        public IList<LinkedTxn> LinkedTxn { get; set; }
        public IList<Line> Line { get; set; }
        public TxnTaxDetail TxnTaxDetail { get; set; }
        public CustomerRef CustomerRef { get; set; }
        public BillAddr BillAddr { get; set; }
        public ShipAddr ShipAddr { get; set; }
        public bool FreeFormAddress { get; set; }
        public SalesTermRef SalesTermRef { get; set; }
        public string DueDate { get; set; }
        public string GlobalTaxCalculation { get; set; }
        public double TotalAmt { get; set; }
        public string PrintStatus { get; set; }
        public string EmailStatus { get; set; }
        public int Balance { get; set; }
    }

    public class QueryResponseInvoice
    {
        public IList<Invoice> Invoice { get; set; }
        public int startPosition { get; set; }
        public int maxResults { get; set; }
        public int totalCount { get; set; }
    }

    public class QBInvoice
    {
        public QueryResponseInvoice QueryResponse { get; set; }
        public DateTime time { get; set; }
    }

    //------------------------------------------



    public class PrimaryEmailAddr
    {
        public string Address { get; set; }
    }

    public class PrimaryPhone
    {
        public string FreeFormNumber { get; set; }
    }

    public class Customer
    {
        public bool Taxable { get; set; }
        public BillAddr BillAddr { get; set; }
        public ShipAddr ShipAddr { get; set; }
        public bool Job { get; set; }
        public bool BillWithParent { get; set; }
        public double Balance { get; set; }
        public double BalanceWithJobs { get; set; }
        public CurrencyRef CurrencyRef { get; set; }
        public string PreferredDeliveryMethod { get; set; }
        public bool IsProject { get; set; }
        public string ClientEntityId { get; set; }
        public string domain { get; set; }
        public bool sparse { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData MetaData { get; set; }
        public string GivenName { get; set; }
        public string FullyQualifiedName { get; set; }
        public string DisplayName { get; set; }
        public string PrintOnCheckName { get; set; }
        public bool Active { get; set; }
        public string V4IDPseudonym { get; set; }
        public PrimaryEmailAddr PrimaryEmailAddr { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string Suffix { get; set; }
        public string CompanyName { get; set; }
        public PrimaryPhone PrimaryPhone { get; set; }
    }

    public class QueryResponseCusomer
    {
        public IList<Customer> Customer { get; set; }
        public int startPosition { get; set; }
        public int maxResults { get; set; }
    }

    public class QBCustomer
    {
        public QueryResponseCusomer QueryResponse { get; set; }
        public DateTime time { get; set; }
    }

    //--------------------------------- Item-----------------------------------------

    public class IncomeAccountRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class ExpenseAccountRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class AssetAccountRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }


    public class SalesTaxCodeRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class ParentRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class PrefVendorRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class PurchaseTaxCodeRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string FullyQualifiedName { get; set; }
        public bool Taxable { get; set; }
        public bool SalesTaxIncluded { get; set; }
        public double UnitPrice { get; set; }
        public string Type { get; set; }
        public IncomeAccountRef IncomeAccountRef { get; set; }
        public string PurchaseDesc { get; set; }
        public bool PurchaseTaxIncluded { get; set; }
        public double PurchaseCost { get; set; }
        public ExpenseAccountRef ExpenseAccountRef { get; set; }
        public AssetAccountRef AssetAccountRef { get; set; }
        public bool TrackQtyOnHand { get; set; }
        public int QtyOnHand { get; set; }
        public string InvStartDate { get; set; }
        public string domain { get; set; }
        public bool sparse { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData MetaData { get; set; }
        public SalesTaxCodeRef SalesTaxCodeRef { get; set; }
        public bool? SubItem { get; set; }
        public ParentRef ParentRef { get; set; }
        public int? Level { get; set; }
        public int? ReorderPoint { get; set; }
        public PrefVendorRef PrefVendorRef { get; set; }
        public PurchaseTaxCodeRef PurchaseTaxCodeRef { get; set; }
    }

    public class QueryResponseItem
    {
        public IList<Item> Item { get; set; }
        public int startPosition { get; set; }
        public int maxResults { get; set; }
    }

    public class QbItem
    {
        public QueryResponseItem QueryResponse { get; set; }
        public DateTime time { get; set; }
    }




}
