using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xyj.core;
using xyj.core.Extensions;
using erp.invoicing.Entity;
using erp.invoicing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace erp.invoicing.Services
{
 

    public class InvoicingService : BaseRepository, IInvoicingService
    {
        #region api

        #region 销售订单
        //打折销售单
        public bool discount_sell_order(string id,int percent)
        {
            var order = Db.sell_order.Find(id);
            double discount_price = 0, total_pay_price=0, ordered_price=0;
            Db.invoicing_order_item.Where(a=>a.invoicing_order_id==id).ToList().ForEach(item =>
            {
                //重新计算总价
                item.ordered_price = item.price * item.ordered_num;
                ordered_price += item.ordered_price;
                //重新设置折扣
                item.discount = percent;
                //重新计算折扣价
                item.discount_price = item.ordered_price * (1-(percent / 100.0));
                discount_price += item.discount_price;
                //重新计算应支付总价
                item.total_pay_price = item.price * item.ordered_num - item.discount_price;
                total_pay_price += item.total_pay_price;
                //标记
                Db.Entry(item).State=EntityState.Modified;
            });
            order.ordered_price = ordered_price;
            order.discount = percent;
            order.discount_price = discount_price;
            order.total_pay_price = total_pay_price;
            
            return Db.Saved();
        }
        //确认销售单
        public bool confirm_sell_order(string id)
        {
            //检查重复商品
            if (Db.invoicing_order_item.Where(a => a.invoicing_order_id == id).GroupBy(a => a.product_id).ToList().Any(aa => aa.AsEnumerable().Count()>1))
            {
                return false;
            }
            var order = Db.sell_order.Find(id);
            var percent = order.discount;
            double discount_price = 0, total_pay_price = 0, ordered_price = 0;
            Db.invoicing_order_item.Where(a => a.invoicing_order_id == id).ToList().ForEach(item =>
            {
                //重新计算总价
                item.ordered_price = item.price * item.ordered_num;
                ordered_price += item.ordered_price;
                //重新设置折扣
                item.discount = percent;
                //重新计算折扣价
                item.discount_price = item.ordered_price * (1 - (percent / 100.0));
                discount_price += item.discount_price;
                //重新计算应支付总价
                item.total_pay_price = item.price * item.ordered_num - item.discount_price;
                total_pay_price += item.total_pay_price;
              
                //标记
                Db.Entry(item).State = EntityState.Modified;
            });
            order.ordered_price = ordered_price;
            order.discount = percent;
            order.discount_price = discount_price;
            order.total_pay_price = total_pay_price;
            order.sell_order_state_id = "2";//2销售单(待审核)
            //标记
            Db.Entry(order).State = EntityState.Modified;
            return Db.Saved();
        }
        //--
        private bool check_sell_order(string id, string note, string uid,string stateId,bool decreseNum)
        {
            var order = Db.sell_order.Find(id);
            order.sell_order_state_id = stateId;//3出库单(待出库)
            order.checkor = uid;
            order.check_note = note;
            order.check_time = DateTime.Now;
            if (decreseNum)
            {
                Db.invoicing_order_item.Where(a => a.invoicing_order_id == id).ToList().ForEach(item =>
                {
                    //--修改库存预定数量
                    var p = Db.product.Find(item.product_id);
                    var storage = Db.org_storage_product.FirstOrDefault(s => s.product_id == item.product_id&&s.org_storage_id== p.org_storage_id);
                    if (storage == null)
                    {
                        var sp = Db.org_storage_product.Where(t => t.product_id == item.product_id)
                            .OrderByDescending(tt => tt.enable_num).First();
                        if (sp == null || sp.enable_num <= 0)
                        {
                            throw new Exception("仓库[" + Db.org_storage.Find(p.org_storage_id).name + "]中不存在商品[" +
                                                item.product_name + "]<br/>请通知采购处采购！");
                        }
                        else
                        {
                            throw new Exception("仓库[" + Db.org_storage.Find(p.org_storage_id).name + "]中不存在商品[" + item.product_name + "]，但在其他仓库找到了该商品.<br/>建议在商品信息中将该商品的仓库修改为：[" + Db.org_storage.Find(sp.org_storage_id).name+"]");
                        }
                        
                    }
                     storage.ordered_num += item.ordered_num;//加预定数量
                    //重新计算可用量
                    storage.enable_num = storage.real_num + storage.onroad_num - storage.borrowed_num - storage.ordered_num;
                    //标记
                    Db.Entry(storage).State = EntityState.Modified;
                });  
            }       
            //标记
            Db.Entry(order).State = EntityState.Modified;
            return Db.Saved();
        }
        //审核通过销售单
        public bool check_ok_sell_order(string id, string note,string uid)
        {
            return check_sell_order(id, note, uid,"3",true);//3出库单(待出库)
        }
        //审核不通过销售单
        public bool check_notok_sell_order(string id, string note, string uid)
        {
            return check_sell_order(id, note, uid, "1",false);//1销售订单(待确认)
        }
        #endregion

        #region 销售出库单
        //确认出库单
        public bool confirm_inout_order(string id)
        {
            //检查重复商品
            if (Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).GroupBy(a => a.invoicing_order_item_id).ToList().Any(aa => aa.AsEnumerable().Count() > 1))
            {
                return false;
            }
            var order = Db.in_out_order_record.Find(id);
            var count =0;
            //取出订单信息
            var itemsInOrder = Db.invoicing_order_item.Where(a => a.invoicing_order_id == order.invoicing_order_id).ToList();
            //取出库存信息[同个产品可能在不同仓库]
            var itemsInStorage = Db.org_storage_product.Where(a => itemsInOrder.Select(aa=>aa.product_id).Contains(a.product_id));
            Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).ToList().ForEach(item =>
            {
                #region 合法参数验证
                //出库数量不能为负
                if (item.num<=0)
                {
                    throw new Exception("出库数量必须大于0");
                }
                //出库数量不能大于订单中待出库的数量(订单数量-已出数量-退货数量)
                if (itemsInOrder.Any(o => o.invoicing_order_item_id == item.invoicing_order_item_id && item.num > o.ordered_num-o.outed_num-o.returned_num))
                {
                    throw new Exception("出库数量不能大于订单中待出库的数量");
                }

                //出库数量不能大于库存数量
                if (itemsInStorage.Where(s => s.product_id == item.invoicing_order_item.product_id).Sum(ss => ss.enable_num) < item.num)
                {
                    throw new Exception(string.Format("以下商品库存不足:<br/>{0}", item.invoicing_order_item.product_name));
                }
                #endregion
                //优先从指定仓库扣减
                var waitToReduceStorage = itemsInStorage.Where(a=>a.org_storage_id==item.org_storage_id&&a.product_id==item.invoicing_order_item.product_id);
                if (waitToReduceStorage.Sum(w=>w.enable_num)<item.num)
                {//指定库库存不足->尝试自动调库
                    var remainNum = item.num;
                   itemsInStorage.Where(a => a.product_id == item.invoicing_order_item.product_id).OrderByDescending(a=>a.enable_num).ToList().ForEach(
                       enableStorage =>
                       {
                           if (enableStorage.enable_num < remainNum)
                           {//出库单自动拆单
                               remainNum -= enableStorage.enable_num;
                               Db.in_out_order_record_item.Add(new in_out_order_record_item()
                               {
                                   in_out_order_record_item_id = item.in_out_order_record_item_id+DateTime.Now.Random(3,"-"),
                                   in_out_order_record_id = item.in_out_order_record_id,
                                   invoicing_order_item_id = item.invoicing_order_item_id,
                                   num = enableStorage.enable_num,
                                   org_storage_id = enableStorage.org_storage_id,
                               });
                           }
                           else
                           {//拆单完成，删除原始单
                               Db.in_out_order_record_item.Remove(item);
                           }
                       });
                }
             
               //出库合计数量  
                count += item.num;
            });
          
            order.num = count;
            order.in_out_order_record_type_id = "31";//31销售出库(待审核)
            //标记
            Db.Entry(order).State = EntityState.Modified;
            return Db.Saved();
        }
        //--
        private bool check_inout_order(string id, string note, string uid, string stateId,bool decreseNum)
        {
            var order = Db.in_out_order_record.Find(id);
            order.in_out_order_record_type_id = stateId;//3出库单(待出库)
            order.checkor = uid;
            order.check_note = note;
            order.check_time = DateTime.Now;
            if (decreseNum)
            { // ToDo:减库存-------存在bug!!!1.库存未扣减正确 2.若出库完成应更改销售单状态
               
                var items = Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).ToList();
                  items.ForEach(item =>
                {
                    var p = Db.invoicing_order_item.Where(a => a.invoicing_order_item_id == item.invoicing_order_item_id).Select(aa => aa.product).FirstOrDefault();
                    //查找产品出库的库存
                    var itemsInStoragesForOut = Db.org_storage_product.Where(a => a.org_storage_id == item.org_storage_id&&a.product_id== p.product_id);
                    var itemInOrder = Db.invoicing_order_item.Find(item.invoicing_order_item_id) ;
                    //查找产品默认存放的库存
                    var itemsInStorageForDeafult = Db.org_storage_product.FirstOrDefault(s => s.product_id == p.product_id && s.org_storage_id == p.org_storage_id);
                    
                    #region 数据合法性检测
                    if (!itemsInStoragesForOut.Any())
                    {
                        throw new Exception($"仓库不存在商品:{item.invoicing_order_item.product_name}");
                    }
                    if (itemsInStoragesForOut.Count() > 1)
                    {
                        throw new Exception("同一仓库[org_storage_id]存在相同类目[product_id]的商品");
                    }
                    if (itemsInStorageForDeafult == null)
                    {
                        throw new Exception($"被预定的仓库{item.org_storage.name}中不存在商品:{item.invoicing_order_item.product_name}");
                    }
                    #endregion
                    var itemsInStorageForOut = itemsInStoragesForOut.First();
                    //-改订单：增加已出库量
                    itemInOrder.outed_num += item.num;
                    //-改库存:减实际数量
                    itemsInStorageForOut.real_num -= item.num;
                    //-改库存2:减预定数量
                    itemsInStorageForDeafult.ordered_num -= item.num;
                    //重新计算可用量
                    itemsInStorageForOut.enable_num = itemsInStorageForOut.real_num + itemsInStorageForOut.onroad_num - itemsInStorageForOut.borrowed_num - itemsInStorageForOut.ordered_num;
                    itemsInStorageForDeafult.enable_num = itemsInStorageForDeafult.real_num + itemsInStorageForDeafult.onroad_num - itemsInStorageForDeafult.borrowed_num - itemsInStorageForDeafult.ordered_num;
                    //标记
                    Db.Entry(itemInOrder).State = EntityState.Modified;
                    //标记
                    Db.Entry(itemsInStorageForOut).State=EntityState.Modified;
                    //标记
                    Db.Entry(itemsInStorageForDeafult).State = EntityState.Modified;
                });
            }
            //标记
            Db.Entry(order).State = EntityState.Modified;
           var result= Db.Saved();

            //判断出库是否全部完成:改订单状态
            if (result&&decreseNum && Db.invoicing_order_item.Where(a => a.invoicing_order_id == order.invoicing_order_id).All(
                item => item.ordered_num == item.outed_num))
            {
                var sellOrder = Db.sell_order.Find(order.invoicing_order_id);
                sellOrder.sell_order_state_id = "4";//4历史单
                //标记
                Db.Entry(sellOrder).State = EntityState.Modified;
                Db.Saved();
            }

            return result;


        }
        //审核通过出库单
        public bool check_ok_inout_order(string id, string note, string uid)
        {
            return check_inout_order(id, note, uid, "32",true);//32销售出库(已出库)
        }
        //审核不通过出库单
        public bool check_notok_inout_order(string id, string note, string uid)
        {
            return check_inout_order(id, note, uid, "30",false);//销售出库(待确认)
        }
        #endregion

        #region 销售退货单
        //创建销售退货单
        public string create_return_order(string id,  string uid)
        {
            var order = Db.invoicing_order.Find(id);
            var wid = DateTime.Now.Random(-1, "xsth");
            order.has_return = 1;//存在退货
            Db.in_out_order_record.Add(new in_out_order_record()
            {
                in_out_order_record_id = wid,
                invoicing_order_id = id,
                in_out_order_record_type_id = "40",//销售退货(待确认)
                send_no = "",
                num = 0,
                send_or = "",
                operat_or = uid,
                operat_time = DateTime.Now,
                operator_note = "该单需退货，订单号：" + id
            });
            //标记
            Db.Entry(order).State = EntityState.Modified;
            return Db.Saved()? wid:"";
        }

        //确认销售退货入库单
        public bool confirm_inout_return_order(string id,string note)
        {
            //检查重复商品
            if (Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).GroupBy(a => a.invoicing_order_item_id).ToList().Any(aa => aa.AsEnumerable().Count() > 1))
            {
                return false;
            }
            var order = Db.in_out_order_record.Find(id);
            var count = 0;
            //取出订单信息
            var itemsInOrder = Db.invoicing_order_item.Where(a => a.invoicing_order_id == order.invoicing_order_id).ToList();
            Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).ToList().ForEach(item =>
            {
                #region 合法参数验证
                //退货数量不能为负
                if (item.num <= 0)
                {
                    throw new Exception("退货数量必须大于0");
                }
                //退货数量不能大于订单中（订购的数量-已退货数量)
                if (itemsInOrder.Any(o => o.invoicing_order_item_id == item.invoicing_order_item_id && item.num > (o.ordered_num-o.return_num)))
                {
                    throw new Exception("退货数量不能大于订单中订购的数量");
                }
                //退货数量不能大于待出库数量((订购-退货)-(已出库-已入库))
                if (itemsInOrder.Any(o => o.invoicing_order_item_id == item.invoicing_order_item_id && item.num > (o.ordered_num - o.return_num)-(o.outed_num-o.returned_num)))
                {
                    throw new Exception("退货数量不能大于待出库数量");
                }
                #endregion
                //退货合计数量  
                count += item.num;
            });

            order.num = count;
            order.in_out_order_record_type_id = "41";//41销售退货(待审核)
            //标记
            Db.Entry(order).State = EntityState.Modified;
            return Db.Saved();
        }
        //--
        private bool check_inout_return_order(string id, string note, string uid, string stateId, bool increaseNum)
        {
            var inoutOrder = Db.in_out_order_record.Find(id);
            var order = Db.sell_order.Find(inoutOrder.invoicing_order_id);
            inoutOrder.in_out_order_record_type_id = stateId;
            inoutOrder.checkor = uid;
            inoutOrder.check_note = note;
            inoutOrder.check_time = DateTime.Now;
            if (increaseNum)
            { // ToDo:加库存-------存在bug!!! 2.若出库完成应更改销售单状态

                var items = Db.in_out_order_record_item.Where(a => a.in_out_order_record_id == id).ToList();
                double ordered_price = 0, discount_price=0, total_pay_price=0;
                items.ForEach(item =>
                {
                    //查找产品
                    var p = Db.invoicing_order_item.Where(a => a.invoicing_order_item_id == item.invoicing_order_item_id).Select(aa => aa.product).FirstOrDefault();
                    //查找产品出库的库存
                    var itemsInStoragesForOut = Db.org_storage_product.Where(a => a.product_id == p.product_id&&a.org_storage_id == item.org_storage_id );
                    var itemInOrder = Db.invoicing_order_item.Find(item.invoicing_order_item_id);
                    //查找产品默认存放的库存
                    var itemsInStorageForDeafult = Db.org_storage_product.FirstOrDefault(s => s.product_id == p.product_id && s.org_storage_id == p.org_storage_id);

                    #region 数据合法性检测
                    if (!itemsInStoragesForOut.Any())
                    {
                        throw new Exception($"仓库不存在商品:{item.invoicing_order_item.product_name}");
                    }
                    if (itemsInStoragesForOut.Count() > 1)
                    {
                        throw new Exception("同一仓库[org_storage_id]存在相同类目[product_id]的商品");
                    }
                    if (itemsInStorageForDeafult == null)
                    {
                        throw new Exception($"被预定的仓库{item.org_storage.name}中不存在商品:{item.invoicing_order_item.product_name}");
                    }
                    #endregion
                    var itemsInStorageForOut = itemsInStoragesForOut.First();
                    /*
                     * 退货数量>待出库数量 :  大于的部分入库
                     * 扣减数量后=>须重新计算订单总价
                     */

                 
                 
                    //-改库存:加实际数量
                    if (item.num > (itemInOrder.ordered_num - itemInOrder.return_num - itemInOrder.outed_num))
                    {
                        var numShouldBeInStore =
                            item.num - (itemInOrder.ordered_num - itemInOrder.return_num - itemInOrder.outed_num);
                        itemsInStorageForOut.real_num += numShouldBeInStore;
                        //-改订单：增加退货入库量
                        itemInOrder.returned_num += numShouldBeInStore;
                        //-改库存2:减预定数量
                        itemsInStorageForDeafult.ordered_num -= (item.num- numShouldBeInStore);
                        //重新计算可用量
                        itemsInStorageForOut.enable_num = itemsInStorageForOut.real_num +
                                                          itemsInStorageForOut.onroad_num -
                                                          itemsInStorageForOut.borrowed_num -
                                                          itemsInStorageForOut.ordered_num;
                    }
                    else
                    {
                        //-改库存2:减预定数量
                        itemsInStorageForDeafult.ordered_num -= item.num;
                    }
                    //-改订单：增加退货数量
                    itemInOrder.return_num += item.num;

                    //重新计算可用量
                    itemsInStorageForDeafult.enable_num = itemsInStorageForDeafult.real_num + itemsInStorageForDeafult.onroad_num - itemsInStorageForDeafult.borrowed_num - itemsInStorageForDeafult.ordered_num;

                    #region 重新计算订单总价
                    //重新计算总价
                    itemInOrder.ordered_price = itemInOrder.price * itemInOrder.ordered_num;
                    ordered_price += itemInOrder.ordered_price;
                    //重新计算折扣价
                    itemInOrder.discount_price = itemInOrder.ordered_price * (1 - (itemInOrder.discount / 100.0));
                    discount_price += itemInOrder.discount_price;
                    //重新计算应支付总价
                    itemInOrder.total_pay_price = itemInOrder.price * itemInOrder.ordered_num - itemInOrder.discount_price;
                    total_pay_price += itemInOrder.total_pay_price;
                    #endregion

                    //标记
                    Db.Entry(itemInOrder).State = EntityState.Modified;
                    //标记
                    Db.Entry(itemsInStorageForOut).State = EntityState.Modified;
                    //标记
                    Db.Entry(itemsInStorageForDeafult).State = EntityState.Modified;
                });
                order.ordered_price = ordered_price;
                order.discount_price = discount_price;
                order.total_pay_price = total_pay_price;
                //标记
                Db.Entry(order).State = EntityState.Modified;
            }

            //标记
            Db.Entry(inoutOrder).State = EntityState.Modified;
            var result = Db.Saved();

            //判断出库是否全部完成:改订单状态
            if (result && increaseNum && Db.invoicing_order_item.Where(a => a.invoicing_order_id == inoutOrder.invoicing_order_id).All(
                    item => item.ordered_num == item.outed_num))
            {
                var sellOrder = Db.sell_order.Find(inoutOrder.invoicing_order_id);
                sellOrder.sell_order_state_id = "4";//4历史单
                //标记
                Db.Entry(sellOrder).State = EntityState.Modified;
                Db.Saved();
            }

            return result;


        }

        //审核通过出库单
        public bool check_ok_inout_return_order(string id, string note, string uid)
        {
            return check_inout_return_order(id, note, uid, "42", true);//32销售出库(已出库)
        }
        //审核不通过出库单
        public bool check_notok_inout_return_order(string id, string note, string uid)
        {
            return check_inout_return_order(id, note, uid, "40", false);//销售出库(待确认)
        }
        #endregion

 

        //销售单全部出库
        public bool outall_sell_order(string id, string note)
        {
            return true;
        }


        //确认退货单
        public bool confirm_return_sell_order(string id)
        {
            return true;
        }


        //销售单退货
        public bool return_sell_order(string id, int num, string note)
        {
            return true;
        }
        //销售单全部退货
        public bool returnall_sell_order(string id, string note)
        {
            return true;
        }

       

        #endregion
    }
}
