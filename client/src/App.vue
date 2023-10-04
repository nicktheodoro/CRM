<template>
  <v-app>
    <v-main>
      <v-container fill-height>
        <LoginView />
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import LoginView from "./module/login/view/Login.vue";

export default Vue.extend({
  name: "App",

  components: {
    LoginView,
  },
});
</script>
