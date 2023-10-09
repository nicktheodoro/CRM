export interface AppState {
  loading: boolean;
  mode: string;
  snackbar: boolean;
  notice: string;
}

const SUCCESS = "success";
const ERROR = "error";

const state: AppState = {
  loading: true,
  mode: "",
  snackbar: false,
  notice: "",
};

const mutations = {
  setLoading(state: AppState, loading: boolean) {
    state.loading = loading;
  },
  setNotice(state: AppState, notice: string) {
    console.log(" notice .... ", notice);
    state.notice = notice;
  },
  setSnackbar(state: AppState, snackbar: boolean) {
    state.snackbar = snackbar;
  },
  setMode(state: AppState, mode: string) {
    state.mode = mode;
  },
};

const actions = {
  closeNotice(context: any) {
    context.commit("setNotice", "");
    context.commit("setMode", "");
    context.commit("setSnackbar", false);
  },
  closeNoticeWithDelay(context: any, timeout = 2000) {
    setTimeout(() => {
      context.commit("setNotice", "");
      context.commit("setMode", "");
      context.commit("setSnackbar", false);
    }, timeout);
  },
  sendSuccessNotice(context: any, notice: string) {
    context.commit("setNotice", notice);
    context.commit("setMode", SUCCESS);
    context.commit("setSnackbar", true);
    context.dispatch("closeNoticeWithDelay");
  },
  sendErrorNotice(context: any, notice: string) {
    context.commit("setNotice", notice);
    context.commit("setMode", ERROR);
    context.commit("setSnackbar", true);
    context.dispatch("closeNoticeWithDelay");
  },
};

export default {
  state,
  mutations,
  actions,
};
