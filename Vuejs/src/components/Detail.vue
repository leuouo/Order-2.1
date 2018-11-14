<template>
  <popup v-model="showGoods" height="100%">
    <div class="m-goodDetail" ref="goods">
      <div class="m-goodImg">
        <img :src="goods.goodsImg" alt="">

      </div>
      <div class="m-content">
        <div class="m-info">
          <div class="name">{{goods.goodsName}}</div>
          <div class="price">¥{{goods.price|money}}</div>
        </div>
      </div>
      <div class="m-detail">
        <div class="line vux-1px-b">编码：{{goods.goodsCode}}</div>
        <div class="line vux-1px-b">品牌：</div>
        <div class="line vux-1px-b">规格：{{goods.goodsSepc}}</div>
        <div class="line">单位：{{goods.goodsUnit}}</div>
      </div>
      <div class="m-btnGroup">
        <div class="m-btnInner">
          <div class="m-btnEmpty">
            <inline-x-number :min="1" :max="99" width="40px" v-model="number" button-style="round"></inline-x-number>
          </div>
          <button @click="addToCart(goods.id)" class="btn w-button">加入购物车</button>
        </div>
      </div>

      <div class="m-close" @click="showGoods=false">
        <i class="icon icon-close"></i>
      </div>
    </div>
  </popup>
</template>

<script>
  import {Popup, InlineXNumber} from 'vux'

  export default {
    components: {
      Popup, InlineXNumber
    },
    props: {
      goods: {
        type: Object
      }
    },
    data() {
      return {
        showGoods: false,
        number: 1
      }
    },
    created() {

    },
    methods: {
      show() {
        this.number = 1;
        this.showGoods = true;
      },
      async addToCart(goodsId) {
        let sid = this.$cookies.get("cx_sid");
        if (!sid) {
          this.$router.push({path: "/login", query: {redirect: this.$route.fullPath}});
          return;
        }

        let res = await this.$http.post('/api/shoppingCart/AddCart', {goodsId: goodsId, number: this.number});
        if (res.code === 100) {
          this.$vux.toast.show({
            text: res.message
          });
        }
      },
    }
  }
</script>

<style scoped lang="less">
  .m-goodDetail {
    position: relative;
    width: 100%;
    height: 100%;
  }

  .m-goodImg {
    height: 20rem;
  }

  .m-goodImg img {
    width: 100%;
    height: 100%;
  }

  .m-close {
    position: absolute;
    top: .5rem;
    right: .8rem;
    .icon {
      font-size: 32px;
      line-height: 32px;
      color: rgba(224, 221, 221, 0.9);
    }
  }

  .m-content {
    position: relative;
    padding: .83333rem 0 .83333rem .8rem;
    border-bottom: .26667rem solid #f4f4f4;
    display: flex;
    background-color: #fff;
    margin-bottom: .875rem;
  }

  .m-info {
    flex: 1;
  }

  .name {
    font-size: 1.2rem;
  }

  .price {
    font-size: 1.2rem;
    color: #f23030;
  }

  .m-detail {
    padding: .83333rem 0 .83333rem .8rem;
    background-color: #fff;
    font-size: .875rem;
  }

  .line {
    min-height: 2.4rem;
    line-height: 2.4rem;
  }

  .m-btnGroup {
    position: relative;
    z-index: 4;
  }

  .m-btnInner {
    display: flex;
    position: fixed;
    bottom: 0;
    left: 50%;
    width: 100%;
    background-color: #fff;
    flex-flow: row nowrap;
    transform: translateX(-50%);
    align-items: center;
  }

  .m-btnEmpty {
    width: 16rem;
    text-align: right;
  }

  .m-btnGroup .btn {
    flex-grow: 1;
    align-items: center;
    justify-content: center;
    height: 3.38667rem;
    border-radius: 0;
  }
</style>
