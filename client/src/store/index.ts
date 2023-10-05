import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    token: {},
    isAuthenticated: false,
  },
  mutations: {
    SET_TOKEN(state, token) {
      state.token = token;
    },
  },
  actions: {
    async authenticate({ commit }, tokenPromise) {
      try {
        const token = await tokenPromise;
        commit("SET_TOKEN", token);
        this.state.isAuthenticated = true;
      } catch (error) {
        console.log(error);
      }
    },
  },
  getters: {
    GET_TOKEN(state) {
      return state.token;
    },
  },
  modules: {},
});
