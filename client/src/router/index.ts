import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import store from "@/store";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "home",
    component: () => import("@/module/home/view/HomeView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/users/signin",
    name: "signin",
    component: () => import("@/module/login/view/SignInView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/users/signup",
    name: "signup",
    component: () => import("@/module/atendimento/view/SignUpView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: () => import("@/module/dashboard/views/DashboardView.vue"),
    meta: { requiresAuth: true },
  },
  // {
  //   path: '/about',
  //   name: 'about',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  // }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  const token = store.getters["GET_TOKEN"].value;

  if (to.matched.some((route) => route.meta.requiresAuth)) {
    if (!token && from.name !== "signin" && from.name !== "signup") {
      next("/users/signin");
    }
  }

  next();
});

export default router;
