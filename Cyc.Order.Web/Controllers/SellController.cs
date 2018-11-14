using Cyc.Order.Data;
using Cyc.Order.Data.DataModel;
using Cyc.Order.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakura.AspNetCore;

namespace Cyc.Order.Web.Controllers
{
    [Route("api")]
    public class SellController : BaseController
    {
        private readonly OrderDbContext _context;

        public SellController(OrderDbContext context)
        {
            _context = context;
        }


        [Route("Sell/Goods")]
        public async Task<IActionResult> Index()
        {
            List<SellGoodsViewModel> sellGoodsViewModels = new List<SellGoodsViewModel>();
            // 分类列表
            var brandList = await _context.Brands.Where(b => !b.IsDelete).ToListAsync();
            // 所有商品
            var goodsList = await _context.Goods.Where(g => !g.IsDelete).ToListAsync();
            // 商品价格
            var priceList = await _context.GoodsPrices.Where(g => !g.IsDelete).ToListAsync();

            foreach (var item in brandList)
            {
                // 获取分类下的所有商品
                var sellGoods = goodsList.Where(g => g.BrandId == item.Id).Select(goods =>
                new SellGoods
                {
                    Id = goods.Id,
                    GoodsImg = goods.GoodsImg,
                    GoodsCode = goods.GoodsCode,
                    GoodsName = goods.GoodsName,
                    GoodsSepc = goods.GoodsSepc,
                    GoodsUnit = goods.GoodsUnit
                }).ToList();

                if (sellGoods.Count > 0)
                {
                    var sellGoodsViewModel = new SellGoodsViewModel();
                    sellGoodsViewModel.Name = item.Name;
                    // 遍历商品，获取商品价格
                    foreach (var goods in sellGoods)
                    {
                        var goodsPrice = priceList.FirstOrDefault(p => p.GoodsId == goods.Id && p.ShopId == Sid && !p.IsDelete);
                        if (goodsPrice != null)
                        {
                            goods.Price = goodsPrice.Price;
                        }
                        else
                        {
                            // 取商品默认价格
                            goodsPrice = priceList.FirstOrDefault(p => p.GoodsId == goods.Id && p.ShopId == 0);
                            if (goodsPrice != null)
                            {
                                goods.Price = goodsPrice.Price;
                            }
                        }
                        sellGoodsViewModel.SellGoods = sellGoods;
                    }
                    sellGoodsViewModels.Add(sellGoodsViewModel);
                }
            }

            var res = new ResultModel();
            res.Code = 100;
            res.Data = sellGoodsViewModels;
     
            return Json(res);
        }


        [Route("Sell/List")]
        public async Task<IActionResult> Index(int page = 1, int brandId = 0)
        {
            var query = _context.Goods
                .Where(g => !g.IsDelete);

            if (brandId != 0)
            {
                query = query.Where(g => g.BrandId == brandId);
            }

            var list = await query.Skip((page - 1) * 10).Take(10).ToListAsync();

            // 获取所有商品Id
            var goodsIds = list.Select(g => g.Id).ToArray();
            // 获取所有商品的价格
            var priceList = await _context.GoodsPrices.Where(p => goodsIds.Contains(p.GoodsId)).ToListAsync();

            List<SellGoodViewModel> sellGoodList = new List<SellGoodViewModel>();
            foreach (var item in list)
            {
                SellGoodViewModel sellGood = new SellGoodViewModel();
                sellGood.Goods = item;
                var entity = priceList.FirstOrDefault(p => p.GoodsId == item.Id && p.ShopId == Sid && !p.IsDelete);
                if (entity != null)
                {
                    sellGood.GoodsPrice = entity;
                }
                else
                {
                    // 取商品默认价格
                    entity = priceList.FirstOrDefault(p => p.GoodsId == item.Id && p.ShopId == 0);
                    if (entity != null)
                    {
                        sellGood.GoodsPrice = entity;
                    }
                }
                sellGoodList.Add(sellGood);
            }

            var res = new ResultModel();
            res.Code = 100;
            res.Data = sellGoodList;
            if (sellGoodList.Count == 0)
                res.Code = 0;

            return Json(res);
        }

        [Route("Sell/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = await _context.Goods.Include("Brand")
                .SingleOrDefaultAsync(m => m.Id == id && !m.IsDelete);
            var price = 0.0m;

            var entity = await _context.GoodsPrices.FirstOrDefaultAsync(p => p.GoodsId == goods.Id && p.ShopId == Sid && !p.IsDelete);
            if (entity != null)
            {
                price = entity.Price;
            }
            else
            {
                // 取商品默认价格
                entity = await _context.GoodsPrices.FirstOrDefaultAsync(p => p.GoodsId == goods.Id && p.ShopId == 0);
                if (entity != null)
                {
                    price = entity.Price;
                }
            }

            var model = new { goods = goods, price = price };

            var res = new ResultModel()
            {
                Code = 100,
                Data = model
            };
            return Json(res);
        }

        [HttpPost("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var list = await _context.Brands.ToListAsync();
            var model = new ResultModel();
            model.Code = 100;
            model.Data = list;
            return Json(model);
        }
    }
}