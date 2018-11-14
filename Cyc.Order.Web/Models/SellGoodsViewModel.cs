using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyc.Order.Web.Models
{
    public class SellGoodsViewModel
    {
        // 分类名称
        public string Name { get; set; }

       public   List<SellGoods> SellGoods { get; set; }
    }

    public class SellGoods
    {
        public int Id { get; set; }

        public string GoodsImg { get; set; }

        public string GoodsCode { get; set; }

        public string GoodsName { get; set; }

        public string GoodsSepc { get; set; }

        public string GoodsUnit { get; set; }

        public decimal Price { get; set; }
    }
}
