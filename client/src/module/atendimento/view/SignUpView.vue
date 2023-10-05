<template>
  <v-container justify="center" align="center">
    <v-row justify="center" align="center">
      <v-col cols="12" lg="12" sm="8" md="6">
        <user-form ref="UserForm" :controller="controller"></user-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { usuarioController } from "../../di/di";
import UserForm from "../components/UserForm.vue";

export default {
  name: "SignUpView",
  components: {
    UserForm,
  },
  data: (context: any) => ({
    controller: usuarioController(context),
  }),
};
</script>
