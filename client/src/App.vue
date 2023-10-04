<template>
  <v-app>
    <v-main>
      <UserView />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import UserView from "./module/atendimento/view/UserView.vue";

export default Vue.extend({
  name: "App",

  components: {
    UserView,
  },
});
</script>
