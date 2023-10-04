<template>
  <v-row class="fill-height" justify="center" align="center">
    <v-col cols="12" lg="6">
      <user-table ref="UserTable" :controller="controller" />
      <user-form ref="UserForm" :controller="controller" />
    </v-col>
  </v-row>
</template>

<script>
import { usuarioController } from "../../di/di";
import UserForm from "../components/UserForm.vue";
import UserTable from "../components/UserTable.vue";

export default {
  name: "UserView",
  components: {
    UserForm,
    UserTable,
  },
  data: (context) => ({
    controller: usuarioController(context),
  }),
};
</script>
