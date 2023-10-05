<!-- eslint-disable vue/no-mutating-props -->
<template>
  <v-card tile elevation="2">
    <v-card-title>
      <h1>New user</h1>
    </v-card-title>
    <v-card-text>
      <v-form @submit.prevent="controller.signUp()" ref="form">
        <v-text-field
          label="Name"
          v-model="controller.user.name"
          :rules="[(v) => !!v || 'Required']"
        />
        <v-text-field
          label="Email"
          v-model="controller.user.email"
          :rules="[(v) => !!v || 'Required']"
        />
        <v-text-field
          label="Password"
          v-model="controller.user.password"
          :rules="[(v) => !!v || 'Required']"
        />
        <v-btn color="primary" type="submit">Sign in</v-btn>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { UserController } from "@/module/atendimento/controller/UserController";

export default {
  name: "UserForm",
  props: {
    controller: {
      type: UserController,
      required: true,
    },
  },
};
</script>
