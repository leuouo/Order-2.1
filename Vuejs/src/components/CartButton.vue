<template>
  <span class="cart-button">
    <a href="javascript:;" v-show="quantity!==0" @click="minus">
      <i class="icon icon-jian"></i>
    </a>
    <span class="cart-button-quantity" v-show="quantity!==0">{{quantity}}</span>
    <a href="javascript:;" @click="plus">
      <i class="icon icon-add"></i>
    </a>
  </span>
</template>

<script>
  export default {
    name: "CartButton",
    data() {
      return {
        quantity: 0
      }
    },
    methods: {
      plus() {
        this.quantity += 1;
      },
      minus() {
        this.quantity -= 1;
      }
    }
  }
</script>

<style scoped>
  .cart-button {
    display: inline-flex;
    align-items: center;
  }

  .cart-button a {
    display: inline-block;
    color: #6fa7e7;
  }

  .cart-button .icon {
    font-size: 1.2rem;
    line-height: 1.2rem;
    height: 1.2rem;
    width: 1.2rem;
  }

  .cart-button-quantity {
    display: inline-block;
    width: 1.5rem;
    text-align: center;
    font-size: .7rem;
    overflow: hidden;
  }

</style>
