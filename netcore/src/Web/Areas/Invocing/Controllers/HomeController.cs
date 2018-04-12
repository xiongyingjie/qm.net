using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using erp.invoicing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using xyj.core.Extensions;
using xyj.core.Models;
using xyj.core.Models.Http;

namespace Web.Areas.Invocing.Controllers
{
    [Area("Invocing")]
    public class HomeController : BaseController
    {
        private IInvoicingService _invoicingService;

        public HomeController(IInvoicingService invoicingService)
        {
            _invoicingService = invoicingService;
        }

        //  /Invocing/Home/api
        #region api
        //打折销售单
        public IActionResult discount_sell_order(string id,int percent)
        {
            return _invoicingService.discount_sell_order(id, percent)?
                Json(State.Success, "已保存"):
                Json(State.Fail, "打折失败,请重试");
        }
        //确认销售单
        public IActionResult confirm_sell_order(string id)
        {
            return _invoicingService.confirm_sell_order(id) ?
                Json(State.SuccessAutoClose, "已确认订单，请通知管理员审核") :
                Json(State.Fail, "确认失败：请检查订单是否存在相同商品!");
        }
        //审核销售单
        public IActionResult check_ok_sell_order(string id, string note)
        {
            return _invoicingService.check_ok_sell_order(id, note, DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核通过:"+ note) ://请通知仓管员出库
                Json(State.Fail, "审核失败,请重试");
           
        }
        //审核不通过销售单
        public IActionResult check_notok_sell_order(string id,string note)
        {
            return _invoicingService.check_notok_sell_order(id, note,DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核不通过:"+ note) ://请通知销售员修改订单
                Json(State.Fail, "审核失败,请重试");
        }
         //确认出库单
        public IActionResult confirm_inout_order(string id)
        {
            return _invoicingService.confirm_inout_order(id) ?
                Json(State.SuccessAutoClose, "已确认出库单，请通知管理员审核") :
                Json(State.Fail, "确认失败：请检查出库单是否存在相同商品!");
        }
        //审核出库单
        public IActionResult check_ok_inout_order(string id, string note)
        {
            return _invoicingService.check_ok_inout_order(id, note, DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核通过:" + note) ://请通知仓管员出库
                Json(State.Fail, "审核失败,请重试");

        }
        //审核不通过出库单
        public IActionResult check_notok_inout_order(string id, string note)
        {
            return _invoicingService.check_notok_inout_order(id, note, DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核不通过:" + note) ://请通知仓管员修改订单
                Json(State.Fail, "审核失败,请重试");
        }
        //创建销售退货单
        public IActionResult create_return_order(string id)
        {
            var wid = _invoicingService.create_return_order(id, DataContext.UserId);
            return wid .HasValue() ?
                Json(State.SuccessRedirect, "请继续完善退货信息", "*r/Invocing/Home/in_out_order_return_record_item_list?id="+ wid+"&id2=" + id) :
                Json(State.Fail, "操作失败：请重试!");

       
        }
        //确认销售退货单
        public IActionResult confirm_inout_return_order(string id, string note)
        {
            return _invoicingService.confirm_inout_return_order(id, note) ?
                Json(State.SuccessAutoClose, "已确认退货单，请通知管理员审核") :
                Json(State.Fail, "确认失败：请检查退货单是否存在相同商品!");
        }

        //审核出库单
        public IActionResult check_notok_inout_return_order(string id, string note)
        {
            return _invoicingService.check_notok_inout_return_order(id, note, DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核不通过:" + note) ://请通知仓管员修改订单
                Json(State.Fail, "审核失败,请重试");

        }
        //审核不通过出库单
        public IActionResult check_ok_inout_return_order(string id, string note)
        {
            return _invoicingService.check_ok_inout_return_order(id, note, DataContext.UserId) ?
                Json(State.SuccessAutoClose, "审核通过:" + note) ://请通知仓管员出库
                Json(State.Fail, "审核失败,请重试");
          
        }



        //销售单全部出库
        public IActionResult outall_sell_order(string id, string note)
        {
            return Json(State.SuccessAutoClose, "已全部出库:" + note);
        }

        //确认退货单
        public IActionResult confirm_return_sell_order(string id)
        {
            return Json(State.SuccessAutoClose, "已确认");
        }

  
        //销售单退货
        public IActionResult return_sell_order(string id, int num, string note)
        {
            return Json(State.SuccessAutoClose, "已退货:" + num + "/" + note);
        }
        //销售单全部退货
        public IActionResult returnall_sell_order(string id, string note)
        {
            return Json(State.SuccessAutoClose, "已全部退货:" + note);
        }
        #endregion

        #region 基础信息管理
        //Invocing/Home/storage_list
        public IActionResult storage_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("storage_list",
                    new
                    {
                        reportId = "erp.invoicing.RZT.仓库信息设置",
                        Params = "仓库编码;仓库名称;联系人;电话;地址;邮箱",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("仓库编码");
            Search.Add("仓库名称");
            Search.Add("联系人");
            Search.Add("电话");
            Search.Add("地址");
            Search.Add("邮箱");
            InitReport("仓库信息设置", "/Invocing/Home/org_storage_add", "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/product_type_list
        public IActionResult product_type_list(string reportId, string Params, string fid)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("product_type_list",
                    new
                    {
                        reportId = "erp.invoicing.RBM.商品分类设置",
                        Params = "商品分类编号;分类名称",
                        pageIndex = 1,
                        perCount = 10,
                        fid = fid.CheckValue("root")
                    });
            }

            Search.Add("商品分类编号");
            Search.Add("分类名称");
            SetFixedParam(fid, ";;");
            InitReport("商品分类设置", "/Invocing/Home/product_type_add?fid=" + fid, "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/product_list
        public IActionResult product_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("product_list",
                    new
                    {
                        reportId = "erp.invoicing.RKM.商品信息设置",
                        Params = "货号;商品名称;主条码;零售价;批发价;参考进价;产品类别;品牌名称;货商名称",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("所在仓库");
            Search.Add("商品名称");
            Search.Add("主条码");
            Search.Add("零售价");
            Search.Add("批发价");
            Search.Add("参考进价");
            Search.Add("商品状态");
            Search.Add("品牌名称");
            Search.Add("货商名称");
            InitReport("商品信息设置", "/Invocing/Home/product_add", "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/provider_list
        public IActionResult provider_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("provider_list",
                    new
                    {
                        reportId = "erp.invoicing.R76.供应商信息管理",
                        Params = "货商编码;货商名称;联系人;电话;电子邮件;货商分类RPM;发票类型R7T",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("货商编码");
            Search.Add("货商名称");
            Search.Add("联系人");
            Search.Add("电话");
            Search.Add("电子邮件");
            Search.Add("货商分类");
            Search.Add("发票类型");
            InitReport("供应商信息管理", "/Invocing/Home/provider_add", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/customer_list
        public IActionResult customer_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("customer_list",
                    new
                    {
                        reportId = "erp.invoicing.RHR.客户信息设置",
                        Params = "客户编码;客户名称;联系人;电话;电子邮件;客户分类;发票类型;区域;地址",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("客户编码");
            Search.Add("客户名称");
            Search.Add("联系人");
            Search.Add("电话");
            Search.Add("电子邮件");
            Search.Add("客户分类");
            Search.Add("发票类型");
            Search.Add("区域");
            Search.Add("地址");
            InitReport("客户信息设置", "/Invocing/Home/customer_add", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/brand_list
        public IActionResult brand_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("brand_list",
                    new
                    {
                        reportId = "erp.invoicing.R45.品牌管理",
                        Params = "品牌编号;品牌名称",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("品牌编号");
            Search.Add("品牌名称");
            InitReport("品牌管理", "/Invocing/Home/brand_add", "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/product_combine_list
        public IActionResult product_combine_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("product_combine_list",
                    new
                    {
                        reportId = "erp.invoicing.R8D.组合商品设置",
                        Params = "货号;组合名称;组合条码;组装单价;组合时间;备注",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("货号");
            Search.Add("组合名称");
            Search.Add("组合条码");
            Search.Add("组装单价");
            Search.Add("组合时间");
            Search.Add("备注");
            InitReport("组合商品设置", "/Invocing/Home/product_combine_add", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/product_combine_detail_list
        public IActionResult product_combine_detail_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("product_combine_detail_list",
                    new
                    {
                        reportId = "erp.invoicing.R3S.product_combine_detail",
                        Params = "货号;商品名称;组合数量;零售价;批发价;参考进价",
                        pageIndex = 1,
                        perCount = 10,
                        id= id
                    });
            }

            Search.Add("货号");
            Search.Add("商品名称");
            Search.Add("组合数量");
            Search.Add("零售价");
            Search.Add("批发价");
            Search.Add("参考进价");
            SetFixedParam(id, ";;;;;");
            InitReport("组合商品明细", "/Invocing/Home/product_combine_detail_add?id="+ id, "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/product_deposit_list
        public IActionResult product_deposit_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("product_deposit_list",
                    new
                    {
                        reportId = "erp.invoicing.RVJ.押金商品管理",
                        Params = "货号;商品名称;主条码;零售价;批发价;参考进价;产品类别;品牌名称;货商名称",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("货号");
            Search.Add("商品名称");
            Search.Add("主条码");
            Search.Add("零售价");
            Search.Add("批发价");
            Search.Add("参考进价");
            Search.Add("产品类别");
            Search.Add("品牌名称");
            Search.Add("货商名称");
            InitReport("押金商品设置", "/Invocing/Home/product_deposit_add", "", true, "erp.invoicing");
            return ReportJson();
        }
        #endregion

        #region 新增销售单
        //Invocing/Home/invoicing_order_sell_orderlist_list
        public IActionResult invoicing_order_sell_orderlist_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_list",
                    new
                    {
                        reportId = "erp.invoicing.RR4.invoicing_order_sell_orderlist",
                        Params = "单据编号;订单类型;销售单编号;客户名称;业务日期;业务员;销售类型;订单折扣率(%);折扣金额;当前状态;订金",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("单据编号");
            Search.Add("订单类型");
            Search.Add("销售单编号");
            Search.Add("客户名称");
            Search.Add("业务日期");
            Search.Add("业务员");
            Search.Add("销售类型");
            Search.Add("订单折扣率(%)");
            Search.Add("折扣金额");
            Search.Add("当前状态");
            Search.Add("订金");
            InitReport("待确认的销售单列表", "/Invocing/Home/invoicing_order_sell_order_add", "", true, "erp.invoicing");
            return ReportJson();
        }
       
        //Invocing/Home/invoicing_order_item_list
        public IActionResult invoicing_order_item_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_item_list",
                    new
                    {
                        reportId = "erp.invoicing.R55.销售单明细",
                        Params = "商品编号;商品名称;商品单价;订购数量;订单金额;折扣率;折扣金额;应付总额",
                        pageIndex = 1,
                        perCount = 10,
                        id= id
                    });
            }

            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("订购数量");
            Search.Add("订单金额");
            Search.Add("折扣率");
            Search.Add("折扣金额");
            Search.Add("应付总额");
            Search.Add("统一打折",FormControlType.Button,"dazhe()");
            Search.Add("确认销售单", FormControlType.Button, "queren()");
            SetFixedParam(id, ";;;;;;;");
            InitReport("销售单明细", "", "", true, "erp.invoicing");
            return ReportJson();
        }
        #endregion

        #region 审核销售单
        //Invocing/Home/invoicing_order_sell_orderlist_check_list
        public IActionResult invoicing_order_sell_orderlist_check_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RR4.销售订单审核",
                        Params = "单据编号;订单类型;销售单编号;客户名称;业务日期;业务员;销售类型;订单折扣率(%);折扣金额;当前状态;订金",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("单据编号");
            Search.Add("订单类型");
            Search.Add("销售单编号");
            Search.Add("客户名称");
            Search.Add("业务日期");
            Search.Add("业务员");
            Search.Add("销售类型");
            Search.Add("订单折扣率(%)");
            Search.Add("折扣金额");
            Search.Add("当前状态");
            Search.Add("订金");
            InitReport("销售订单审核", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/invoicing_order_sell_orderlist_checkck_list
        public IActionResult invoicing_order_item_check_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_item_check_list",
                    new
                    {
                        reportId = "erp.invoicing.R55.审核销售单明细",
                        Params = ";商品编号;商品名称;商品单价;订购数量;订单金额;折扣率;折扣金额;应付总额",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

          
            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("订购数量");
            Search.Add("订单金额");
            Search.Add("折扣率");
            Search.Add("折扣金额");
            Search.Add("应付总额");
            Search.Add("审核通过", FormControlType.Button, "ok()");
            Search.Add("审核不通过", FormControlType.Button, "notok()");
            SetFixedParam(id, ";;;;;");
            InitReport("审核销售单明细", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        #endregion

        #region 新增销售出库单
        //Invocing/Home/invoicing_order_sell_orderlist_out_list
        public IActionResult invoicing_order_sell_orderlist_out_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_out_list",
                    new
                    {
                        reportId = "erp.invoicing.RR4.销售订单出库",
                        Params = "单据编号;订单类型;销售单编号;客户名称;业务日期;业务员;销售类型;订单折扣率(%);折扣金额;当前状态;订金",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("单据编号");
            Search.Add("订单类型");
            Search.Add("销售单编号");
            Search.Add("客户名称");
            Search.Add("业务日期");
            Search.Add("业务员");
            Search.Add("销售类型");
            Search.Add("订单折扣率(%)");
            Search.Add("折扣金额");
            Search.Add("当前状态");
            Search.Add("订金");
            InitReport("销售订单出库", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/in_out_order_record_list
        public IActionResult in_out_order_record_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_record_list",
                    new
                    {
                        reportId = "erp.invoicing.RL8.销售出库单列表",
                        Params = "单据编号;拣货人;发货单号;出库数量;操作日期;审核日期;当前状态",
                        pageIndex = 1,
                        perCount = 10,
                        id= id
                    });
            }

            Search.Add("单据编号");
            Search.Add("拣货人");
            Search.Add("发货单号");
            Search.Add("出库数量");
            Search.Add("操作日期");
            Search.Add("审核日期");
            Search.Add("当前状态");
            SetFixedParam(id, ";;;;;;");
            InitReport("出库记录", "/Invocing/Home/in_out_order_record_add?id="+id, "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/in_out_order_record_item_list
        public IActionResult in_out_order_record_item_list(string reportId, string Params, string id, string id2)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_record_item_list",
                    new
                    {
                        reportId = "erp.invoicing.RLG.销售出库单明细",
                        Params = "仓库id;商品编号;商品名称;商品单价;折扣率;出库数量",
                        pageIndex = 1,
                        perCount = 10,
                        id = id,
                        id2 = id2//销售订单编号
                    });
            }

            Search.Add("仓库id");
            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("折扣率");
            Search.Add("出库数量");
            Search.Add("确认出库单", FormControlType.Button, "queren()");
            SetFixedParam(id, ";;;;;");
            InitReport("出库单明细", "#", id2, true, "erp.invoicing");
            return ReportJson();
        }
        #endregion

        #region 审核销售出库单
        //Invocing/Home/in_out_order_record_list
        public IActionResult invoicing_order_sell_orderlist_out_check_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_out_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RL8.销售出库单审核",
                        Params = ";;;;;;",
                        pageIndex = 1,
                        perCount = 10,
               
                    });
            }

            Search.Add("单据编号");
            Search.Add("拣货人");
            Search.Add("发货单号");
            Search.Add("出库数量");
            Search.Add("操作日期");
            Search.Add("审核日期");
            Search.Add("当前状态");
            InitReport("销售出库单审核", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/in_out_order_record_check_list
        public IActionResult in_out_order_record_check_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_record_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RL8.销售出库单列表",
                        Params = "单据编号;拣货人;发货单号;出库数量;操作日期;审核日期;当前状态",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("单据编号");
            Search.Add("拣货人");
            Search.Add("发货单号");
            Search.Add("出库数量");
            Search.Add("操作日期");
            Search.Add("审核日期");
            Search.Add("当前状态");
            SetFixedParam(id, ";;;;;;");
            InitReport("出库记录", "/Invocing/Home/in_out_order_record_add?id=" + id, "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/in_out_order_record_item_check_list
        public IActionResult in_out_order_record_item_check_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_record_item_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RLG.审核出库单明细",
                        Params = "仓库id;商品编号;商品名称;商品单价;折扣率;出库数量",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("");
            Search.Add("仓库id");
            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("折扣率");
            Search.Add("出库数量");
            Search.Add("审核通过", FormControlType.Button, "ok()");
            Search.Add("审核不通过", FormControlType.Button, "notok()");
            SetFixedParam(id, ";;;;;");
            InitReport("出库单明细", "#", "", true, "erp.invoicing");
            return ReportJson();
        }

        #endregion

        //Invocing/Home/invoicing_order_item_out_list
        public IActionResult invoicing_order_item_out_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_item_out_list",
                    new
                    {
                        reportId = "erp.invoicing.R55.销售单出库明细",
                        Params = "商品编号;商品名称;商品单价;订购数量;订单金额;折扣率;折扣金额;应付总额",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("订购数量");
            Search.Add("订单金额");
            Search.Add("折扣率");
            Search.Add("折扣金额");
            Search.Add("应付总额");
            Search.Add("全部出库", FormControlType.Button, "out_all()");
         
            SetFixedParam(id, ";;;;;;;");
            InitReport("待出库的商品列表", "", "", true, "erp.invoicing");
            return ReportJson();
        }


        #region 新增退货入库单
        //Invocing/Home/invoicing_order_sell_orderlist_return_list
        public IActionResult invoicing_order_sell_orderlist_return_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_return_list",
                    new
                    {
                        reportId = "erp.invoicing.RR4.销售订单退货",
                        Params = "单据编号;订单类型;销售单编号;客户名称;业务日期;业务员;销售类型;订单折扣率(%);折扣金额;当前状态;订金",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("单据编号");
            Search.Add("订单类型");
            Search.Add("销售单编号");
            Search.Add("客户名称");
            Search.Add("业务日期");
            Search.Add("业务员");
            Search.Add("销售类型");
            Search.Add("订单折扣率(%)");
            Search.Add("折扣金额");
            Search.Add("当前状态");
            Search.Add("订金");
            InitReport("销售订单退货", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/in_out_return_order_record_list
        public IActionResult in_out_return_order_record_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_return_order_record_list",
                    new
                    {
                        reportId = "erp.invoicing.RL8.销售退货单列表",
                        Params = "单据编号;拣货人;发货单号;出库数量;操作日期;审核日期;当前状态",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("单据编号");
            Search.Add("拣货人");
            Search.Add("发货单号");
            Search.Add("出库数量");
            Search.Add("操作日期");
            Search.Add("审核日期");
            Search.Add("当前状态");
            SetFixedParam(id, ";;;;;;");
            InitReport("退货记录", "/Invocing/Home/in_out_order_record_add?id=" + id, "", true, "erp.invoicing");
            return ReportJson();
        }


        //Invocing/Home/in_out_order_record_item_list
        public IActionResult in_out_order_return_record_item_list(string reportId, string Params, string id, string id2)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_return_record_item_list",
                    new
                    {
                        reportId = "erp.invoicing.RLG.退货入库单明细",
                        Params = "仓库id;商品编号;商品名称;商品单价;折扣率;出库数量",
                        pageIndex = 1,
                        perCount = 10,
                        id = id,
                        id2 = id2//销售订单编号
                    });
            }

            Search.Add("仓库id");
            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("折扣率");
            Search.Add("出库数量");
            Search.Add("确认退货单", FormControlType.Button, "queren()");
            SetFixedParam(id, ";;;;;");
            InitReport("退货入库单明细", "#", id2, true, "erp.invoicing");
            return ReportJson();
        }

        #endregion


        #region 审核退货入库单
        //Invocing/Home/in_out_order_record_list
        public IActionResult invoicing_order_sell_orderlist_return_in_check_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_sell_orderlist_return_in_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RL8.审核退货入库单",
                        Params = ";;;;;;",
                        pageIndex = 1,
                        perCount = 10,

                    });
            }

            Search.Add("单据编号");
            Search.Add("拣货人");
            Search.Add("发货单号");
            Search.Add("出库数量");
            Search.Add("操作日期");
            Search.Add("审核日期");
            Search.Add("当前状态");
            InitReport("审核退货入库单", "#", "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/in_out_order_record_item_return_check_list
        public IActionResult in_out_order_record_item_return_check_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("in_out_order_record_item_return_check_list",
                    new
                    {
                        reportId = "erp.invoicing.RLG.审核退货入库单明细",
                        Params = "仓库id;商品编号;商品名称;商品单价;折扣率;出库数量",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("");
            Search.Add("仓库id");
            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("折扣率");
            Search.Add("出库数量");
            Search.Add("审核通过", FormControlType.Button, "ok()");
            Search.Add("审核不通过", FormControlType.Button, "notok()");
            SetFixedParam(id, ";;;;;");
            InitReport("退货入库单明细", "#", "", true, "erp.invoicing");
            return ReportJson();
        }

        #endregion













        //Invocing/Home/invoicing_order_item_in_list
        public IActionResult invoicing_order_item_in_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("invoicing_order_item_in_list",
                    new
                    {
                        reportId = "erp.invoicing.R55.销售订单退货入库明细",
                        Params = "商品编号;商品名称;商品单价;订购数量;订单金额;折扣率;折扣金额;应付总额",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("商品编号");
            Search.Add("商品名称");
            Search.Add("商品单价");
            Search.Add("订购数量");
            Search.Add("订单金额");
            Search.Add("折扣率");
            Search.Add("折扣金额");
            Search.Add("应付总额");
            Search.Add("全部出库", FormControlType.Button, "out_all()");

            SetFixedParam(id, ";;;;;;;");
            InitReport("销售订单退货入库明细", "", "", true, "erp.invoicing");
            return ReportJson();
        }








        //Invocing/Home/org_storage_list
        public IActionResult org_storage_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("org_storage_list",
                    new
                    {
                        reportId = "erp.invoicing.R89.库存查询(仓库)",
                        Params = "仓库编码;仓库名称;联系人;电话;地址;邮箱",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("仓库编码");
            Search.Add("仓库名称");
            Search.Add("联系人");
            Search.Add("电话");
            Search.Add("地址");
            Search.Add("邮箱");
            InitReport("库存查询(仓库)", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
        //Invocing/Home/org_storage_product_list
        public IActionResult org_storage_product_list(string reportId, string Params, string id)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("org_storage_product_list",
                    new
                    {
                        reportId = "erp.invoicing.RK5.仓库产品分布明细",
                        Params = "商品编号;预定数量;借阅数量;在途数量;实际数量;可用余量",
                        pageIndex = 1,
                        perCount = 10,
                        id = id
                    });
            }

            Search.Add("商品编号");
            Search.Add("预定数量");
            Search.Add("借阅数量");
            Search.Add("在途数量");
            Search.Add("实际数量");
            Search.Add("可用余量");
            SetFixedParam(id, ";;;;;;;");
            InitReport("仓库产品分布明细", "#", "", true, "erp.invoicing");
            return ReportJson();
        }

        //Invocing/Home/org_storage_product2_list
        public IActionResult org_storage_product2_list(string reportId, string Params)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("org_storage_product2_list",
                    new
                    {
                        reportId = "erp.invoicing.RQ3.库存查询(产品)",
                        Params = "商品编号;商品名称;",
                        pageIndex = 1,
                        perCount = 10
                    });
            }

            Search.Add("商品编号");
            Search.Add("商品名称");
            InitReport("库存查询(产品)", "#", "", true, "erp.invoicing");
            return ReportJson();
        }
    }


}