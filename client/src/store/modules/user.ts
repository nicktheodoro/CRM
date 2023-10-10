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
  isSignedIn: boolean;
}

const state: UserState = {
  token: {
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    tokenType: "",
  },
  isSignedIn: false,
};

const mutations = {
  setToken(
    state: { token: AuthenticateResponse },
    token: AuthenticateResponse
  ) {
    state.token = token;
  },
  setIsSignedIn(state: { isSignedIn: boolean }, signed: boolean) {
    state.isSignedIn = signed;
  },
};

const getters = {
  getToken(state: { token: AuthenticateResponse }) {
    return state.token;
  },
  getIsSignedIn(state: { isSignedIn: boolean }) {
    return state.isSignedIn;
  },
};

const actions = {
  isSignedIn(): boolean {
    return state.isSignedIn;
  },
  async signIn(
    context: ActionContext<any, any>,
    request: AuthenticateRequest
  ): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      const token = await authenticateUseCaseImpl(request);
      mutations.setToken(context.state, token);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async signUp(
    context: ActionContext<any, any>,
    request: UserModel
  ): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      await createUserUseCaseImpl(request);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(
        context,
        error.response.data.errors
          .map((e: any) => `${e.propertyName} : ${e.errorMessage}`)
          .join(", ")
      );
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async signOut(context: ActionContext<any, any>): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      await logoutUseCaseImpl(context.getters.GET_TOKEN);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};
