<template>
  <v-app id="app">
    <v-main id="container">
      <v-layout fluid fill-height align-center justify-center>
        <router-view />
      </v-layout>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "App",
});
</script>
