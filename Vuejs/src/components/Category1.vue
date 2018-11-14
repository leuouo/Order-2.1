<template>
  <div class="cate-layout">
    <div class="banner"></div>
    <div class="cate-con">
      <div class="cate-nav-wrap" ref="cateNav">
        <ul>
          <li ref="navList" class="item" @click="navItem(idx, $event)" v-for="(item,idx) in list" :key="idx"
              :class="{'active':idx===currentIndex}">
            <a class="txt" href="javascript:;">{{item.name}}</a>
          </li>
        </ul>
      </div>
      <div class="cate-goods" ref="cateGoods">
        <ul>
          <li class="cate-item" v-for="(item,index) in list" :key="index" ref="goodList">
            <div class="cate-item-hd vux-1px-b">
              {{item.name}}
            </div>
            <div class="cate-item-bd">
              <div class="cate-goods-list">
                <div v-for="(goods,idx) in item.sellGoods" :key="idx" class="cate-goods-item" @click="selectGood(goods,$event)">
                  <div class="cate-goods-pic">
                    <img v-lazy="goods.goodsImg" width="72px" height="72px">
                  </div>
                  <div class="cate-goods-info">
                    <div class="g-name">
                      <span class="g-name-text">{{goods.goodsName}}</span>
                    </div>
                    <div class="g-spec">规格: {{goods.goodsUnit}} / {{goods.goodsSepc}}</div>
                    <div class="g-price">¥{{goods.price|money}}</div>
                  </div>
                  <span class="g-add" ><i class="icon icon-add"></i></span>
                </div>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </div>

    <detail ref="goods" :goods="selectedGood"></detail>

    <toolbar :selected="0"></toolbar>
  </div>
</template>

<script>
  import {XDialog} from 'vux';
  import BScroll from 'better-scroll'
  import Toolbar from './Toolbar'
  import Detail from './Detail'
  import CartButton from './CartButton'

  export default {
    components: {Toolbar, XDialog,Detail,CartButton},
    data() {
      return {
        list: [],
        show: false,
        listHeight: [],
        scrollY: 0,
        selectedGood:{}
      }
    },
    computed: {
      currentIndex() {
        for (let i = 0; i < this.listHeight.length; i++) {
          let height1 = this.listHeight[i];
          let height2 = this.listHeight[i + 1];
          if (!height2 || (this.scrollY >= height1 && this.scrollY < height2)) {
            this._followScroll(i);
            return i;
          }
        }
        return 0;
      }
    },
    created() {
      this.getData();
    },
    methods: {
      async getData() {
        let res = await this.$http.post('/api/sell/goods');
        this.list = res.data;

        this.$nextTick(() => {
          this.initScroll();
          this._calculateHeight();
        });
      },
      initScroll() {
        this.navScroll = new BScroll(this.$refs.cateNav, {
          click: true
        });

        this.goodsScroll = new BScroll(this.$refs.cateGoods, {
          click: true,
          probeType: 3
        });

        this.goodsScroll.on('scroll', (pos) => {
          // 判断滑动方向，避免下拉时分类高亮错误（如第一分类商品数量为1时，下拉使得第二分类高亮）
          if (pos.y <= 0) {
            this.scrollY = Math.abs(Math.round(pos.y));
          }
        });
      },
      navItem(index, event) {
        if (!event._constructed) {
          return;
        }
        let goodList = this.$refs.goodList;
        let el = goodList[index];
        this.goodsScroll.scrollToElement(el, 300);
      },
      selectGood(good, event) {
        if (!event._constructed) {
          return;
        }
        this.selectedGood = good;
        this.$refs.goods.show();
      },
      _calculateHeight() {
        let goodList = this.$refs.goodList;
        let height = 0;
        this.listHeight.push(height);
        for (let i = 0; i < goodList.length; i++) {
          let item = goodList[i];
          height += item.clientHeight;
          this.listHeight.push(height);
        }
      },
      _followScroll(index) {
        let navList = this.$refs.navList;
        let el = navList[index];
        this.navScroll.scrollToElement(el, 300, 0, -100);
      }
    }
  }
</script>

<style scoped lang="less">
  .cate-layout {
    position: absolute;
    left: 0;
    top: 0;
    right: 0;
    bottom: 0;
  }

  .banner {
    height: 8rem;
    width: 100%;
    background: url(../assets/b.jpg) center no-repeat #f4f4f4;
    background-size: cover;
  }

  .cate-con {
    display: flex;
    position: fixed;
    top: 8rem;
    left: 0;
    right: 0;
    bottom: 0;
    padding-bottom: 54px;
    overflow: hidden;
  }

  .cate-nav-wrap {
    width: 7rem;
    height: 100%;
    overflow: hidden;
  }

  .cate-nav-wrap .item {
    height: 2.5rem;
    text-align: center;
  }

  .cate-nav-wrap .item .txt {
    display: block;
    line-height: 2.5rem;
    font-size: .875rem;
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
    color: #666;
  }

  .cate-nav-wrap .item.active .txt {
    background-color: #fff;
    color: #333;
  }

  .cate-goods {
    flex: 1;
    background-color: #fff;
    height: 100%;
    overflow: hidden;
  }

  .cate-item-hd {
    height: 1.8rem;
    line-height: 1.8rem;
    font-size: .875rem;
    margin-bottom: .35rem;
    padding-left: .875rem;
    color: #333;
  }

  .cate-goods-item {
    position: relative;
    display: flex;
    margin: .875rem;
    padding-bottom: .875rem;
    .g-add {
      position: absolute;
      right: 0;
      bottom: .7rem;
      .icon-add {
        display: inline-block;
        font-size: 1.2rem;
        line-height: 1.2rem;
        color: #6fa7e7;
        padding: 5px;
      }
    }
  }

  .cate-goods-info {
    flex: 1;
    .g-name {
      display: flex;
      align-items: start;
      &-text{
        overflow: hidden;
        font-size: .875rem;
        white-space: nowrap;
        width: 9rem;
        text-overflow: ellipsis;
      }
    }
    .g-spec {
      margin: 6px 0;
      font-size: 12px;
      color: #93999f;
    }
    .g-price {
      color: #ff0000;
      font-size: .875rem;
    }
  }

  .cate-goods-pic {
    width: 4.5rem;
    height: 4.5rem;
    flex: 0 0 4.5rem;
    margin-right: 15px;
    img {

    }
  }

</style>
