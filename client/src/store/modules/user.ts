import { ActionContext } from "vuex";
import {
  authenticateUseCaseImpl,
  createUserUseCaseImpl,
  logoutUseCaseImpl,
} from "@/module/di/di";
import { UserModel } from "@/module/user/domain/model/user";
import { AuthenticateRequest } from "@/module/auth/domain/types";

const state = {
  callingAPI: false,
  searching: "",
  token: {
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    tokenType: "",
  },
};

const mutations = {
  TOGGLE_LOADING_STATUS(state: { callingAPI: boolean }) {
    state.callingAPI = !state.callingAPI;
  },
  SET_TOKEN(state: { token: any }, token: any) {
    state.token = token;
  },
};

const getters = {
  GET_TOKEN(state: { token: any }) {
    return state.token;
  },
};

const actions = {
  isSignedIn(context: ActionContext<any, any>) {
    const token = context.getters.GET_TOKEN;
    console.log(token)
    if (!token.accessToken) return false;

    // const isExpired = !!token.expiresIn && Date.now() > token.expiresIn * 1000;

    // if (isExpired) return false;

    return true;
  },
  async signIn(context: ActionContext<any, any>, request: AuthenticateRequest) {
    try {
      const token = await authenticateUseCaseImpl(request);
      context.commit("SET_TOKEN", token);
      return true;
    } catch (error) {
      console.error("Sign in error:", error);
      throw error;
    }
  },
  async signUp(_: ActionContext<any, any>, request: UserModel) {
    try {
      await createUserUseCaseImpl(request);
      return true;
    } catch (error) {
      console.error("Create user error:", error);
      throw error;
    }
  },
  async signOut(context: ActionContext<any, any>) {
    try {
      await logoutUseCaseImpl(context.getters.GET_TOKEN);
      return true;
    } catch (error) {
      console.error("sign out error:", error);
      throw error;
    }
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};
