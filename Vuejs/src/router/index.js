/*
import Login from '@/components/Login'
import Register from '@/components/Register'
import MyOrder from '@/components/MyOrder'
import OrderDetail from '@/components/OrderDetails'
import Home from '@/components/Home'
import Category from '@/components/Category'
import My from '@/components/My'
import Info from '@/components/Info'
import Address from '@/components/Address'
import List from '@/components/List'
import Detail from '@/components/Detail'
import ShoppingCart from '@/components/ShoppingCart'
import OrderConfirm from '@/components/OrderConfirm'
import SubmitOrderSuccess from '@/components/SubmitOrderSuccess'
*/

export default [
  {
    path: '/login',
    name: 'login',
    component: () => import('@/components/Register'),
    meta: {title: '用户登录'}
  },
  {
    path: '/register',
    name: 'register',
    component: () => import('@/components/Register'),
    meta: {title: '手机号注册'}
  },
  // {
  //   path: '/',
  //   name: 'home',
  //   component: Home,
  //   meta: {title: '首页', keepAlive: true}
  // },
  {
    path: '/',
    name: 'category',
    component: () => import('@/components/Category'),
    meta: {title: '分类', keepAlive: true}
  },
  {
    path: '/my',
    name: 'my',
    component: () => import('@/components/My'),
    meta: {title: '个人中心', requiresAuth: true}
  },
  {
    path: '/info',
    name: 'info',
    component: () => import('@/components/Info'),
    meta: {title: '基本信息', requiresAuth: true}
  },
  {
    path: '/address',
    name: 'address',
    component: () => import('@/components/Address'),
    meta: {title: '配送地址', requiresAuth: true}
  },
  {
    path: '/myOrder',
    name: 'myOrder',
    component: () => import('@/components/MyOrder'),
    meta: {title: '我的订单', requiresAuth: true}
  },
  {
    path: '/list',
    name: 'list',
    component: () => import('@/components/List'),
    meta: {title: '商品列表'}
  },
  {
    path: '/detail',
    name: 'detail',
    component: () => import('@/components/Detail'),
    meta: {title: '商品详情'}
  },
  {
    path: '/shoppingCart',
    name: 'shoppingCart',
    component: () => import('@/components/ShoppingCart'),
    meta: {title: '购物车', requiresAuth: true}
  },
  {
    path: '/orderConfirm',
    name: 'orderConfirm',
    component: () => import('@/components/OrderConfirm'),
    meta: {title: '订单结算', requiresAuth: true}
  },
  {
    path: '/submitOrderSuccess',
    name: 'submitOrderSuccess',
    component: () => import('@/components/SubmitOrderSuccess'),
    meta: {title: '订单提交', requiresAuth: true}
  },
  {
    path: '/orderDetail',
    name: 'orderDetail',
    component: () => import('@/components/OrderDetails'),
    meta: {title: '订单详情', requiresAuth: true}
  },
  {
    path: '/order',
    name: 'order',
    component: resolve => require(['@/components/trade/Index.vue'], resolve),
    children: [
      {
        path: 'list',
        component: resolve => require(['@/components/trade/List.vue'], resolve),
        meta: {title: '我的订单', requiresAuth: true}
      },
      {
        path: 'detail',
        component: resolve => require(['@/components/trade/Detail.vue'], resolve),
        meta: {title: '订单详情', requiresAuth: true}
      }
    ]
  }
]
