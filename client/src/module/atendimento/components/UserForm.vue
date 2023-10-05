<!-- eslint-disable vue/no-mutating-props -->
<template>
  <v-dialog persistent width="400" v-model="controller.formDialog">
    <v-card>
      <v-card-title class="pa-0 pb-4">
        <v-toolbar flat dense color="primary" class="white--text">
          New product
        </v-toolbar>
      </v-card-title>
      <v-card-text>
        <v-form @submit.prevent ref="form">
          <v-text-field
            label="Name"
            dense
            filled
            v-model="controller.user.name"
            :rules="[(v: string) => !!v || 'Required']"
          />
          <v-text-field
            label="Email"
            dense
            filled
            v-model="controller.user.email"
            :rules="[(v: string) => !!v || 'Required']"
          />
          <v-text-field
            label="Password"
            dense
            filled
            v-model="controller.user.password"
            :rules="[(v: string) => !!v || 'Required']"
          />
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn @click="controller.cancel()" color="red" text>cancel</v-btn>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="controller.save()">
          <v-icon left>mdi-content-save</v-icon>save
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { UserController } from "@/module/atendimento/controller/UserController";

export default {
  props: {
    controller: {
      type: UserController,
      required: true,
    },
  },
};
</script>
