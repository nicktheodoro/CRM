import { ActionContext } from "vuex";

import {
  authenticateUseCaseImpl,
  createUserUseCaseImpl,
  logoutUseCaseImpl,
} from "@/module/di/di";

import { UserModel } from "@/module/user/domain/model/user";
import {
  AuthenticateRequest,
  AuthenticateResponse,
} from "@/module/auth/domain/types";

import app from "./app";

interface UserState {
  token: AuthenticateResponse;
}

const state: UserState = {
  token: {
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    tokenType: "",
  },
};

const mutations = {
  setToken(
    state: { token: AuthenticateResponse },
    token: AuthenticateResponse
  ) {
    state.token = token;
  },
};

const getters = {
  getToken(state: { token: AuthenticateResponse }) {
    return state.token;
  },
};

const actions = {
  isSignedIn(context: ActionContext<any, any>) {
    const token = context.getters.getToken;

    if (!token.accessToken) return false;

    // const isExpired = !!token.expiresIn && Date.now() > token.expiresIn * 1000;

    // if (isExpired) return false;

    return true;
  },
  async signIn(context: ActionContext<any, any>, request: AuthenticateRequest) {
    app.mutations.toggleLoading(app.state);

    try {
      const token = await authenticateUseCaseImpl(request);
      context.commit("setToken", token);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async signUp(context: ActionContext<any, any>, request: UserModel) {
    try {
      await createUserUseCaseImpl(request);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    }
  },
  async signOut(context: ActionContext<any, any>) {
    try {
      await logoutUseCaseImpl(context.getters.GET_TOKEN);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    }
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};
