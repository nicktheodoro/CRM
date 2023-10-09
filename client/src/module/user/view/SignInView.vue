<template>
  <v-container>
    <v-snackbar
      :top="true"
      :right="true"
      :timeout="3000"
      :color="mode"
      v-model="snackbar"
    >
      <v-row justify="space-around" align="center">
        {{ notice }}
        <v-btn text @click.native="closeSnackbar()"
          ><v-icon small> mdi-close </v-icon></v-btn
        >
      </v-row>
    </v-snackbar>
    <v-row justify="center">
      <v-col cols="10" sm="6" md="6" xl="4">
        <sign-in-form
          ref="authForm"
          :controller="controller"
          :loading="loading"
        ></sign-in-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { ComponentCustomProperties } from "vue";
import { usuarioController } from "../../di/di";
import SignInForm from "../components/SignInForm.vue";
import store from "@/store";

export default {
  name: "SignInView",
  components: {
    SignInForm,
  },
  data: (context: ComponentCustomProperties) => ({
    controller: usuarioController(context),
  }),
  computed: {
    loading() {
      return store.state.app.loading;
    },
    mode() {
      return store.state.app.mode;
    },
    snackbar() {
      return store.state.app.snackbar;
    },
    notice() {
      return store.state.app.notice;
    },
  },
  methods: {
    async closeSnackbar() {
      await store.dispatch("closeNotice");
    },
  },
};
</script>
