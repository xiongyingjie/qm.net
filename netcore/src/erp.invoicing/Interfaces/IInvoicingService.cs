namespace erp.invoicing.Interfaces
{ 
    public interface IInvoicingService
    {

        bool confirm_inout_order(string id);
        bool check_ok_inout_order(string id, string note, string uid);
        bool check_notok_inout_order(string id, string note, string uid);
        bool discount_sell_order(string id, int percent);
        bool confirm_sell_order(string id);
        bool check_ok_sell_order(string id, string note, string uid);
        bool check_notok_sell_order(string id, string note, string uid);
        string create_return_order(string id, string uid);
        bool confirm_inout_return_order(string id, string note);
        bool check_notok_inout_return_order(string id, string note, string uid);
        bool check_ok_inout_return_order(string id, string note, string uid);



        bool outall_sell_order(string id, string note);





        bool returnall_sell_order(string id, string note);
        bool confirm_return_sell_order(string id);
        bool return_sell_order(string id, int num, string note);


    }
}
